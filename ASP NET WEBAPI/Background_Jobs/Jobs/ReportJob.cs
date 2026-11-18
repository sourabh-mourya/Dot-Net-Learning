namespace Background_Jobs.Jobs
{
    public class ReportJob
    {
        private readonly ILogger<ReportJob> _logger;

        public ReportJob(ILogger<ReportJob> logger)
        {
            _logger = logger;
        }

        // Daily report (recurring job se call hoga)
        public async Task GenerateDailyReportAsync()
        {
            _logger.LogInformation("Daily employee report generate ho rahi hai...");

            await Task.Delay(TimeSpan.FromSeconds(2)); // real report logic yahan

            _logger.LogInformation("Daily employee report READY at {Time}", DateTime.UtcNow);
        }

        // Har minute chalega - sirf demo ke liye, taaki console me logs dikhte rahe
        public Task HeartbeatAsync()
        {
            _logger.LogInformation("Heartbeat job chal raha hai at {Time}", DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}
