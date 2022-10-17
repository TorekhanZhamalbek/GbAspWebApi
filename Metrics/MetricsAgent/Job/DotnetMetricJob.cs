using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class DotnetMetricJob : IJob
    {
        private PerformanceCounter _dotnetCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public DotnetMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# bytes in all heaps", "_Global_");
        }


        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var dotnetMetricsRepository = serviceScope.ServiceProvider.GetService<IDotnetMetricsRepository>();
                try
                {
                    var dotnetUsageInPercents = _dotnetCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"dotnet {time} > {dotnetUsageInPercents}");
                    dotnetMetricsRepository.Create(new Models.DotnetMetric
                    {
                        Value = (int)dotnetUsageInPercents,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {

                }
            }


            return Task.CompletedTask;
        }
    }
}
