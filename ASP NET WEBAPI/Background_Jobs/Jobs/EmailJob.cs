using Hangfire;

namespace Background_Jobs.Jobs
{
    // Retry: fail hone par max 3 baar try karega
    [AutomaticRetry(Attempts = 3)]
    public class EmailJob
    {
        private readonly ILogger<EmailJob> _logger;

        // Hangfire job class ko DI se banata hai, isliye yahan ILogger / DbContext / services inject kar sakte ho.
        public EmailJob(ILogger<EmailJob> logger)
        {
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string email, string name)
        {
            _logger.LogInformation("Welcome email bhej rahe hai -> {Email} ({Name})", email, name);

            await Task.Delay(TimeSpan.FromSeconds(3)); // yahan real email logic aayega (SMTP / SendGrid)

            _logger.LogInformation("Welcome email SENT -> {Email}", email);
        }

        public async Task SendReminderEmailAsync(string email, string message)
        {
            _logger.LogInformation("Reminder email bhej rahe hai -> {Email}: {Message}", email, message);

            await Task.Delay(TimeSpan.FromSeconds(1));

            _logger.LogInformation("Reminder email SENT -> {Email}", email);
        }
    }
}
