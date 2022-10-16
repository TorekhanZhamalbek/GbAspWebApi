using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.Dto;
using MetricsAgent.Services.Impl;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram/available")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMetricsController> logger,
            IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete ram metric.");
            _ramMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetricDto>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics by time call.");
            return Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("all")]
        public ActionResult<IList<RamMetricDto>> GetRamMetricsAll()
        {
            _logger.LogInformation("Get all ram metrics call.");
            return Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetAll()));
        }

        [HttpGet("byId")]
        public ActionResult<RamMetricDto> GetRamMetricById([FromRoute] int id)
        {
            _logger.LogInformation("Get ram metrics by id call.");

            return Ok(_mapper.Map<RamMetricDto>(_ramMetricsRepository.GetById(id)));
        }

        [HttpPut("update")]
        public IActionResult UpdateRamMetric([FromBody] RamMetric request)
        {
            _logger.LogInformation("Update ram metrics by id call.");
            _ramMetricsRepository.Update(request);
            return Ok();
        }
    }
}
