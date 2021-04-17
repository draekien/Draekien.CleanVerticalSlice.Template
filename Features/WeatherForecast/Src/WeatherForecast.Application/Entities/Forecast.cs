using System;

namespace WeatherForecast.Application.Entities
{
    public class Forecast
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public string Summary { get; set; }
        public int TemperatureC { get; set; }
    }
}
