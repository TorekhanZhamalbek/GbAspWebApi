using Microsoft.AspNetCore.Mvc;
using WeatherForecastService.Models;

namespace WeatherForecastService.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherForecastControllers : Controller
    {
        private readonly WeatherForecastHolder _holder;

        public WeatherForecastControllers(WeatherForecastHolder holder)
        {
            _holder = holder;
        }


        [HttpPost("add")]
        public IActionResult Add12345([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            _holder.Add(date, temperatureC);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            _holder.Update(date, temperatureC);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _holder.Delete(date);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult Get([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            return Ok(_holder.Get(dateFrom, dateTo));
        }
    }
}
