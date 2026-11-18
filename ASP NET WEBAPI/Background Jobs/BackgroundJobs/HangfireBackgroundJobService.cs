using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Hangfire;




namespace Background_Jobs
{
    public class HanfireBackgroundJobService : IBackroundService
    {

        private readonly IBackgroundJobClient _client;
        private readonly IRecurringJobManager _recurring;
        public HanfireBackgroundJobService(IBackgroundJobClient client, IRecurringJobManager recurring)
        {
            //what the purpose of these two parameter ---

            //first parameter are used to create the client object of the Hanfire 

            //and second parameter are used to create the job of of the recurring manager...

            this._client = client;
            this._recurring = recurring;

        }

        public string Enqueue<T>(Expression<Func<T, Task>> job)
        {
            return _client.Enqueue(job);
        }

        public string Schedule<T>(Expression<Func<T, Task>> job, TimeSpan delay)
        {
            return _client.Schedule(job, delay);
        }

        public void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> job, string cron)
        {

            _recurring.AddOrUpdate(jobId, job, cron, new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Utc
            });

        }


    }
}