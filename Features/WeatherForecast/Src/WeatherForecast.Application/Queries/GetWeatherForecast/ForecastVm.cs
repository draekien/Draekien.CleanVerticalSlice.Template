using System;
using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using WeatherForecast.Application.Entities;

namespace WeatherForecast.Application.Queries.GetWeatherForecast
{
    public class ForecastVm : IMapFrom<Forecast>
    {
        /// <summary>
        /// The local date time
        /// </summary>
        public DateTime LocalDateTime { get; set; }
        /// <summary>
        /// The summary of the forecast
        /// </summary>
        /// <example>Cool</example>
        public string Summary { get; set; }
        /// <summary>
        /// The temperature in Celsius
        /// </summary>
        /// <example>24</example>
        public int TemperatureC { get; set; }
        /// <summary>
        /// The temperature in Fahrenheit
        /// </summary>
        /// <example>75</example>
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        /// <inheritdoc />
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Forecast, ForecastVm>()
                   .ForMember(dest => dest.LocalDateTime, options => options.MapFrom(src => src.DateTimeOffset.LocalDateTime));
        }
    }
}
