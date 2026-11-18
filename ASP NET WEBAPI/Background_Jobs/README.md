# Background_Jobs (Hangfire + MySQL + ASP.NET Core)

## Setup
1. MySQL me DB banao:  CREATE DATABASE hangfire_db;
2. appsettings.json me password daalo.
3. Run:
   dotnet restore
   dotnet run

Hangfire tables (Hangfire_*) pehli run par khud ban jayengi.

## Test (curl)
curl -X POST "http://localhost:5100/api/jobs/enqueue?email=test@gmail.com&name=Sourabh"
curl -X POST "http://localhost:5100/api/jobs/schedule?email=test@gmail.com&delaySeconds=20"
curl -X POST "http://localhost:5100/api/jobs/recurring?jobId=my-report&cron=*/2%20*%20*%20*%20*"
curl -X POST "http://localhost:5100/api/jobs/fail"

Dashboard: http://localhost:5100/hangfire
