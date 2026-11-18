
using System.Linq.Expressions;

namespace Background_Jobs
{
    public interface IBackroundService
    {
        // Enqueue:
        // Job ko immediately background queue mein add karta hai.
        // Hangfire worker available hone par job execute karta hai.
        // Example: Employee create hone ke baad welcome email bhejna.
        // Expression<Func<T, Task>>:
        // Batata hai ki kis class ka kaunsa asynchronous method execute karna hai.
        // Return string: Hangfire ka unique Job ID return karta hai.
        string Enqueue<T>(Expression<Func<T, Task>> job);


        // Schedule:
        // Job ko future mein execute karne ke liye schedule karta hai.
        // TimeSpan delay: Kitni der baad job execute karna hai.
        // Example: 10 minutes baad reminder email bhejna.
        // Return string: Scheduled job ka unique Job ID.
        string Schedule<T>(
            Expression<Func<T, Task>> job,
            TimeSpan delay
        );


        // AddOrUpdateRecurring:
        // Recurring job ko create karta hai ya existing job ko update karta hai.
        // jobId: Recurring job ki unique identity.
        // job: Execute hone wala asynchronous method.
        // cron: Job kab execute hoga, uska schedule define karta hai.
        // Example: Har day midnight par employee report generate karna.
        // Is method ka return type void hai kyunki yahan Job ID return nahi kar rahe.
        void AddOrUpdateRecurring<T>(
            string jobId,
            Expression<Func<T, Task>> job,
            string cron
        );
    }
}
