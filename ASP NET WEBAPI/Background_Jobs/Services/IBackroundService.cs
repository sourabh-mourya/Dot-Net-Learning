using System.Linq.Expressions;

namespace Background_Jobs.Services
{
    public interface IBackroundService
    {
        // Enqueue: job ko turant background queue me daalta hai.
        // Return: Hangfire ka unique Job ID.
        string Enqueue<T>(Expression<Func<T, Task>> job);

        // Schedule: job ko delay ke baad run karta hai.
        string Schedule<T>(Expression<Func<T, Task>> job, TimeSpan delay);

        // AddOrUpdateRecurring: recurring job create/update karta hai (cron ke hisaab se).
        void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> job, string cron);
    }
}
