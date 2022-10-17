using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class NetworkMetricJob : IJob
    {
        private PerformanceCounter _networkCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public NetworkMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            String[] instancename = category.GetInstanceNames();
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instancename[0].ToString());
        }


        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var networkMetricsRepository = serviceScope.ServiceProvider.GetService<INetworkMetricsRepository>();
                try
                {
                    var networkUsageInPercents = _networkCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"Network {time} > {networkUsageInPercents}");
                    networkMetricsRepository.Create(new Models.NetworkMetric
                    {
                        Value = (int)networkUsageInPercents,
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
