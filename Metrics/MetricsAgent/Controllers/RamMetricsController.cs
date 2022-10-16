using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram/available")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        public RamMetricsController(IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMetricsController> logger)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsRepository.Create(new Models.RamMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
