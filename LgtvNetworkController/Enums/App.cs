namespace LgtvNetworkController.Enums;

public class App
{
    public readonly string Value;

    public static readonly App Amazon = new("amazon");
    public static readonly App Browser = new("com.webos.app.browser");
    public static readonly App DisneyPlus = new("com.disney.disneyplus-prod");
    public static readonly App Gallery = new("com.webos.app.igallery");
    public static readonly App GooglePlay = new("googleplaymovieswebos");
    public static readonly App Guide = new("com.webos.service.iepg");
    public static readonly App HboMax = new("com.hbo.hbomax");
    public static readonly App Hulu = new("hulu");
    public static readonly App Music = new("com.webos.app.music");
    public static readonly App Netflix = new("netflix");
    public static readonly App PhotoVideo = new("com.webos.app.photovideo");
    public static readonly App Settings = new("com.palm.app.settings");
    public static readonly App SlingTV = new("com.movenetworks.app.sling-tv-sling-production");
    public static readonly App Vudu = new("vudu");
    public static readonly App YouTube = new("youtube.leanback.v4");

    private App(string value) => Value = value;

    public override string ToString() => Value;
}
