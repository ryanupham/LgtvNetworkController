using System.Collections.Concurrent;

namespace LgtvNetworkController.Utilities;

internal class ConcurrentWaitingQueue
{
    private readonly ConcurrentQueue<TaskCompletionSource> queue = new();

    public Task Enqueue()
    {
        var waitCompletionSource = new TaskCompletionSource();
        queue.Enqueue(waitCompletionSource);
        if (queue.Count == 1)
        {
            NextItemReady();
        }

        return waitCompletionSource.Task;
    }

    public void Dequeue()
    {
        if (!queue.TryDequeue(out var _))
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        if (!queue.IsEmpty)
        {
            NextItemReady();
        }
    }

    private void NextItemReady()
    {
        if (queue.TryPeek(out var nextCompletionSource))
        {
            nextCompletionSource.SetResult();
        }
    }
}
