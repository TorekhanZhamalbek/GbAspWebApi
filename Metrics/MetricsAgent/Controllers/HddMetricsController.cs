using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.Dto;
using MetricsAgent.Services.Impl;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository hddMetricsRepository,
            ILogger<HddMetricsController> logger,
            IMapper mapper)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation("Create hdd metric.");
            _hddMetricsRepository.Create(_mapper.Map<HddMetric>(request));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete hdd metric.");
            _hddMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetricDto>> GetHddMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get hdd metrics by time call.");
            return Ok(_mapper.Map<List<HddMetricDto>>(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("all")]
        public ActionResult<IList<HddMetricDto>> GetHddMetricsAll()
        {
            _logger.LogInformation("Get all hdd metrics call.");
            return Ok(_mapper.Map<List<HddMetricDto>>(_hddMetricsRepository.GetAll()));
        }

        [HttpGet("byId")]
        public ActionResult<HddMetricDto> GetHddMetricById([FromRoute] int id)
        {
            _logger.LogInformation("Get hdd metrics by id call.");

            return Ok(_mapper.Map<HddMetricDto>(_hddMetricsRepository.GetById(id)));
        }

        [HttpPut("update")]
        public IActionResult UpdateHddMetric([FromBody] HddMetric request)
        {
            _logger.LogInformation("Update hdd metrics by id call.");
            _hddMetricsRepository.Update(request);
            return Ok();
        }
    }
}
