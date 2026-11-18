
namespace Background_Jobs.Jobs
{
    public class CleanupPendingTaskJob
    {
        private readonly ILogger<CleanupPendingTaskJob> _logger;
       public CleanupPendingTaskJob(ILogger<CleanupPendingTaskJob> logger)
        {
            this._logger=logger;
        } 

        public async Task CleanupPendingTodos(int userId)
        {
                _logger.LogInformation($"Executing cleanup pending Todo Jobs for User ID {DateTime.UtcNow}");
                await Task.Delay(5000);
        }
    }
}