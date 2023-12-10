namespace LgtvNetworkController.Utilities;

internal class AtomicCounter
{
    private long value;
    public long Value => value;

    public long Increment() => Interlocked.Increment(ref value);
}
