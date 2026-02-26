using Hangfire;
using System.Linq.Expressions;
using Microsoft.AspNetCore.SignalR;
using IDAProject.Web.Api.Controllers;
using IDAProject.Web.Api.Models.Interfaces.Managers;

namespace IDAProject.Web.Api.Infrastructure
{
    public static class HangfireRecurringJobs
    {
        private static IConfiguration? _configuration;

        public static void CreateJobs(IConfiguration configuration)
        {
            _configuration = configuration;

            AddOrUpdateJob<INotificationsManager>("email-queue", repo => repo.SendQueuedEmailsAsync());
        }

        private static void AddOrUpdateJob<T>(string jobId, Expression<Func<T, Task>> methodCall)
        {
            // Cron fields legend:
            // # field #   meaning        allowed values
            // # -------   ------------   --------------
            // #    1      minute         0-59
            // #    2      hour           0-23
            // #    3      day of month   1-31
            // #    4      month          1-12 (or names, see below)
            // #    5      day of week    0-7 (0 or 7 is Sun, or use names)

            var cronExpression = _configuration!.GetSection($"HangfireJobs:{jobId}").Value;
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }
    }
}