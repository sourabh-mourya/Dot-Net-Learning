using System.Diagnostics;
using Hangfire.Common;
using Hangfire.Server;

namespace Background_Jobs.Filters
{
    // Har job ke start / success / fail ka log console me dikhata hai.
    public class JobLoggingFilter : JobFilterAttribute, IServerFilter
    {
        private const string StopwatchKey = "JobLoggingFilter.Stopwatch";
        private readonly ILogger<JobLoggingFilter> _logger;

        public JobLoggingFilter(ILogger<JobLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnPerforming(PerformingContext context)
        {
            context.Items[StopwatchKey] = Stopwatch.StartNew();

            _logger.LogInformation("[START] Job #{JobId} {Type}.{Method}",
                context.BackgroundJob.Id,
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name);
        }

        public void OnPerformed(PerformedContext context)
        {
            var elapsedMs = context.Items.TryGetValue(StopwatchKey, out var sw) && sw is Stopwatch stopwatch
                ? stopwatch.ElapsedMilliseconds
                : -1;

            var type = context.BackgroundJob.Job.Type.Name;
            var method = context.BackgroundJob.Job.Method.Name;

            if (context.Exception != null)
            {
                _logger.LogError(context.Exception,
                    "[FAILED] Job #{JobId} {Type}.{Method} after {Elapsed} ms",
                    context.BackgroundJob.Id, type, method, elapsedMs);
            }
            else
            {
                _logger.LogInformation("[SUCCESS] Job #{JobId} {Type}.{Method} in {Elapsed} ms",
                    context.BackgroundJob.Id, type, method, elapsedMs);
            }
        }
    }
}
