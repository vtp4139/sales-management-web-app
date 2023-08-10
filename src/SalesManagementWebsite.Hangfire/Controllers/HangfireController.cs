using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace SalesManagementWebsite.Hangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        [HttpGet]
        [Route("executed-only-once")]
        public string ExecutedOnlyOnce()
        {
            //Fire - and - Forget Job - this job is executed only once
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("This job is executed only once"));

            return $"Job ID: {jobId}. Welcome mail sent to the user!";
        }

        [HttpGet]
        [Route("executed-after-some-time")]
        public string ExecutedAfterSomeTime()
        {
            //Delayed Job - this job executed only once but not immedietly after some time.
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("This job executed only once but not immedietly after some time"), TimeSpan.FromSeconds(20));

            return $"Job ID: {jobId}. You added one product into your checklist successfully!";
        }

        [HttpGet]
        [Route("productpayment")]
        public string ProductPayment()
        {
            //Fire and Forget Job - this job is executed only once
            var parentjobId = BackgroundJob.Enqueue(() => Console.WriteLine("You have done your payment suceessfully!"));

            //Continuations Job - this job executed when its parent job is executed.
            BackgroundJob.ContinueJobWith(parentjobId, () => Console.WriteLine("Product receipt sent!"));

            return "You have done payment and receipt sent on your mail id!";
        }

        [HttpGet]
        [Route("cron-schedule")]
        public string CronSchedule(string cron)
        {
            //Recurring Job - this job is executed many times on the specified cron schedule
            RecurringJob.AddOrUpdate("My Job", () => Console.WriteLine("Sent similar product offer and suuggestions"), cron);

            return "Set Cron successfully!";
        }
    }
}
