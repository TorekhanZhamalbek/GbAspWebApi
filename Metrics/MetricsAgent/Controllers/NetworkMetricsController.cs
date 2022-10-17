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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository networkMetricsRepository,
            ILogger<NetworkMetricsController> logger,
            IMapper mapper)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create network metric.");
            _networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete network metric.");
            _networkMetricsRepository.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics by time call.");
            return Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }

        [HttpGet("all")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetricsAll()
        {
            _logger.LogInformation("Get all network metrics call.");
            return Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetAll()));
        }

        [HttpGet("byId")]
        public ActionResult<NetworkMetricDto> GetNetworkMetricById([FromRoute] int id)
        {
            _logger.LogInformation("Get network metrics by id call.");

            return Ok(_mapper.Map<NetworkMetricDto>(_networkMetricsRepository.GetById(id)));
        }

        [HttpPut("update")]
        public IActionResult UpdateNetworkMetric([FromBody] NetworkMetric request)
        {
            _logger.LogInformation("Update network metrics by id call.");
            _networkMetricsRepository.Update(request);
            return Ok();
        }
    }
}
