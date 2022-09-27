namespace WeatherForecastService.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary => TemperatureC switch
        {
            <= -30 => "Freezing",
            <= -20 => "Bracing",
            <= -10 => "Chilly",
            <= 5 => "Cool",
            <= 10 => "Mild",
            <= 20 => "Warm",
            <= 30 => "Balmy",
            <= 35 => "Hot",
            _ => "No case availabe"
        };
    }
}
