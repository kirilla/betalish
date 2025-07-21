namespace Betalish.Application.Locks;

public static class InvoiceNumberAsyncLock
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public static async Task ExecuteAsync(Func<Task> criticalSection)
    {
        await _semaphore.WaitAsync();
        try
        {
            await criticalSection();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
