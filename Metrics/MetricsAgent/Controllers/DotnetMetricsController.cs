using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        private readonly ILogger<DotnetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;
        private readonly IMapper _mapper;

        public DotnetMetricsController(IDotnetMetricsRepository dotnetMetricsRepository,
            ILogger<DotnetMetricsController> logger,
            IMapper mapper)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsRepository.Create(_mapper.Map<DotnetMetric>(request));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete dotnet metric.");
            _dotnetMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics by time call.");
            return Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("all")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetricsAll()
        {
            _logger.LogInformation("Get all dotnet metrics call.");
            return Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetAll()));
        }

        [HttpGet("byId")]
        public ActionResult<DotnetMetricDto> GetDotnetMetricById([FromRoute] int id)
        {
            _logger.LogInformation("Get dotnet metrics by id call.");

            return Ok(_mapper.Map<DotnetMetricDto>(_dotnetMetricsRepository.GetById(id)));
        }

        [HttpPut("update")]
        public IActionResult UpdateDotnetMetric([FromBody] DotnetMetric request)
        {
            _logger.LogInformation("Update dotnet metrics by id call.");
            _dotnetMetricsRepository.Update(request);
            return Ok();
        }
    }
}
