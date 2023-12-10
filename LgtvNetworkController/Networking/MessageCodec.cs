using System.Security.Cryptography;
using System.Text;

namespace LgtvNetworkController.Networking;

public interface IMessageCodec
{
    string Decrypt(byte[] message);
    byte[] Encrypt(string message);
}

public class MessageCodec : IMessageCodec
{
    private readonly MessageCodecOptions options;
    
    private readonly byte[] derivedKey;
    private readonly Aes aes;

    private int KeyLengthBytes => options.Key.Length * sizeof(char);

    public MessageCodec(MessageCodecOptions options)
    {
        this.options = options;

        derivedKey = DeriveKey(
            options.Key,
            options.Salt,
            options.Iterations,
            options.HashAlgorithmName
        );
        aes = Aes.Create();
        aes.KeySize = KeyLengthBytes * 8;
        aes.Padding = PaddingMode.None;
    }

    public static MessageCodec Create(Action<MessageCodecOptions> configure)
    {
        var options = MessageCodecOptions.GetDefault();
        configure(options);
        return new MessageCodec(options);
    }

    private string PrepareMessage(string message)
    {
        if (message.Contains(options.MessageTerminator))
            throw new ArgumentException("Message cannot contain message terminator character");

        var newMessage = message + options.MessageTerminator;
        var remainder = newMessage.Length % options.MessageBlockSize;
        if (remainder > 0)
        {
            var padding = options.MessageBlockSize - remainder;
            newMessage += new string((char)padding, padding);
        }

        return newMessage;
    }

    private static byte[] GenerateRandomByteArray(int length)
    {
        var array = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(array);

        return array;
    }

    private byte[] DeriveKey(string key, byte[] salt, int iterations, string hashAlgorithmName)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(key, salt, iterations, new HashAlgorithmName(hashAlgorithmName));
        return rfc2898.GetBytes(KeyLengthBytes);
    }

    public byte[] Encrypt(string message)
    {
        var preparedMessage = PrepareMessage(message);
        var iv = GenerateRandomByteArray(options.IVLength);
        var ivEnc = EncryptECB(iv);
        var dataEnc = EncryptCBC(preparedMessage, iv);
        return ivEnc.Concat(dataEnc).ToArray();
    }

    private byte[] EncryptECB(byte[] data)
    {
        aes.Mode = CipherMode.ECB;
        using var encryptor = aes.CreateEncryptor(derivedKey, new byte[16]);  // todo: hardcoded bytes?
        return encryptor.TransformFinalBlock(data, 0, data.Length);
    }

    private byte[] EncryptCBC(string message, byte[] iv)
    {
        aes.Mode = CipherMode.CBC;
        using var encryptor = aes.CreateEncryptor(derivedKey, iv);
        var data = Encoding.UTF8.GetBytes(message);
        return encryptor.TransformFinalBlock(data, 0, data.Length);
    }

    public string Decrypt(byte[] message)
    {
        var iv = DecryptECB(message.Take(KeyLengthBytes).ToArray());
        var data = message.Skip(KeyLengthBytes).ToArray();
        var decrypted = DecryptCBC(data, iv);
        return decrypted[..decrypted.IndexOf(options.ResponseTerminator)];
    }

    private byte[] DecryptECB(byte[] data)
    {
        aes.Mode = CipherMode.ECB;
        using var decryptor = aes.CreateDecryptor(derivedKey, new byte[16]);  // todo: hardcoded bytes?
        return decryptor.TransformFinalBlock(data, 0, data.Length);
    }

    private string DecryptCBC(byte[] data, byte[] iv)
    {
        aes.Mode = CipherMode.CBC;
        using var decryptor = aes.CreateDecryptor(derivedKey, iv);
        var decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
        return Encoding.UTF8.GetString(decrypted);
    }

    public class MessageCodecOptions
    {
        public string HashAlgorithmName { get; set; } = "";
        public string Key { get; set; } = "";
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public int Iterations { get; set; }
        public int IVLength { get; set; }
        public char MessageTerminator { get; set; }
        public char ResponseTerminator { get; set; }
        public int MessageBlockSize { get; set; }

        public static MessageCodecOptions GetDefault() =>
            new()
            {
                HashAlgorithmName = "SHA256",
                Key = "",
                Salt = new byte[16] { 99, 97, 184, 14, 155, 220, 166, 99, 141, 7, 32, 242, 204, 86, 143, 185 },
                Iterations = 1 << 14,
                IVLength = 16,
                MessageTerminator = '\r',
                ResponseTerminator = '\n',
                MessageBlockSize = 16,
            };
    }
}
