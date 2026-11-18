using Hangfire;

namespace Background_Jobs.Jobs
{
    // Sirf demo: retry aur failure logs dekhne ke liye
    public class FailingJob
    {
        private readonly ILogger<FailingJob> _logger;

        public FailingJob(ILogger<FailingJob> logger)
        {
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 2)]
        public Task AlwaysFailAsync()
        {
            _logger.LogWarning("FailingJob start hua, ab exception aayega...");
            throw new InvalidOperationException("Demo failure - retry test");
        }
    }
}
