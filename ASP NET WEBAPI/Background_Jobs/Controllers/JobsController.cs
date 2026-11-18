using Background_Jobs.Jobs;
using Background_Jobs.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Background_Jobs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IBackroundService _jobs;

        public JobsController(IBackroundService jobs)
        {
            _jobs = jobs;
        }

        // POST /api/jobs/enqueue?email=test@gmail.com&name=Sourabh
        [HttpPost("enqueue")]
        public IActionResult Enqueue([FromQuery] string email, [FromQuery] string name)
        {
            var jobId = _jobs.Enqueue<EmailJob>(j => j.SendWelcomeEmailAsync(email, name));
            return Ok(new { message = "Job queued", jobId });
        }

        // POST /api/jobs/schedule?email=test@gmail.com&delaySeconds=30
        [HttpPost("schedule")]
        public IActionResult Schedule([FromQuery] string email, [FromQuery] int delaySeconds = 30)
        {
            var jobId = _jobs.Schedule<EmailJob>(
                j => j.SendReminderEmailAsync(email, "Yeh ek reminder hai"),
                TimeSpan.FromSeconds(delaySeconds));

            return Ok(new { message = $"Job {delaySeconds}s baad chalega", jobId });
        }

        // POST /api/jobs/recurring?jobId=my-report&cron=*/2 * * * *
        [HttpPost("recurring")]
        public IActionResult Recurring([FromQuery] string jobId = "daily-employee-report",
                                       [FromQuery] string? cron = null)
        {
            _jobs.AddOrUpdateRecurring<ReportJob>(jobId, j => j.GenerateDailyReportAsync(), cron ?? Cron.Daily());
            return Ok(new { message = "Recurring job registered", jobId });
        }

        // POST /api/jobs/fail  -> retry + failure logs dekhne ke liye
        [HttpPost("fail")]
        public IActionResult Fail()
        {
            var jobId = _jobs.Enqueue<FailingJob>(j => j.AlwaysFailAsync());
            return Ok(new { message = "Failing job queued", jobId });
        }
    }
}
