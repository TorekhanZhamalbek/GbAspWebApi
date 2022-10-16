using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.Dto;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {

        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository cpuMetricsRepository,
            ILogger<CpuMetricsController> logger,
            IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete cpu metric.");
            _cpuMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics by time call.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("all")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetricsAll()
        {
            _logger.LogInformation("Get all cpu metrics call.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetAll()));
        }

        [HttpGet("byId")]
        public ActionResult<CpuMetricDto> GetCpuMetricById([FromRoute] int id)
        {
            _logger.LogInformation("Get cpu metrics by id call.");

            return Ok(_mapper.Map<CpuMetricDto>(_cpuMetricsRepository.GetById(id)));
        }

        [HttpPut("update")]
        public IActionResult UpdateCpuMetric([FromBody] CpuMetric request)
        {
            _logger.LogInformation("Update cpu metrics by id call.");
            _cpuMetricsRepository.Update(request);
            return Ok();
        }
    }
}
