using System.Linq.Expressions;
using Hangfire;

namespace Background_Jobs.Services
{
    public class HanfireBackgroundJobService : IBackroundService
    {
        // IBackgroundJobClient  -> Enqueue / Schedule (one-time jobs) ke liye
        // IRecurringJobManager  -> Recurring (cron) jobs ke liye
        // Dono Hangfire khud DI me register karta hai (AddHangfire se).
        private readonly IBackgroundJobClient _client;
        private readonly IRecurringJobManager _recurring;
        private readonly ILogger<HanfireBackgroundJobService> _logger;

        public HanfireBackgroundJobService(
            IBackgroundJobClient client,
            IRecurringJobManager recurring,
            ILogger<HanfireBackgroundJobService> logger)
        {
            _client = client;
            _recurring = recurring;
            _logger = logger;
        }

        public string Enqueue<T>(Expression<Func<T, Task>> job)
        {
            var jobId = _client.Enqueue(job);
            _logger.LogInformation("[QUEUED] {JobType} -> JobId: {JobId}", typeof(T).Name, jobId);
            return jobId;
        }

        public string Schedule<T>(Expression<Func<T, Task>> job, TimeSpan delay)
        {
            var jobId = _client.Schedule(job, delay);
            _logger.LogInformation("[SCHEDULED] {JobType} -> JobId: {JobId}, runs after {Delay}",
                typeof(T).Name, jobId, delay);
            return jobId;
        }

        public void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> job, string cron)
        {
            _recurring.AddOrUpdate(jobId, job, cron, new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
            _logger.LogInformation("[RECURRING] {JobType} -> RecurringJobId: {JobId}, Cron: {Cron}",
                typeof(T).Name, jobId, cron);
        }
    }
}
