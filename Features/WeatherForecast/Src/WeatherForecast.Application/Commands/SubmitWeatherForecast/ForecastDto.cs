using System;
using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using WeatherForecast.Application.Entities;

namespace WeatherForecast.Application.Commands.SubmitWeatherForecast
{
    public class ForecastDto : Forecast, IMapFrom<SubmitWeatherForecastCommand>
    {
        /// <inheritdoc />
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubmitWeatherForecastCommand, ForecastDto>()
                   .ForMember(dest => dest.DateTimeOffset, x => x.MapFrom<DateTimeOffsetResolver>());
        }
    }

    public class DateTimeOffsetResolver : IValueResolver<SubmitWeatherForecastCommand, ForecastDto, DateTimeOffset>
    {
        /// <inheritdoc />
        public DateTimeOffset Resolve(
            SubmitWeatherForecastCommand source,
            ForecastDto destination,
            DateTimeOffset destMember,
            ResolutionContext context
        )
        {
            DateTime result = source.LocalDateTime;
            DateTime.SpecifyKind(result, DateTimeKind.Local);
            return result;
        }
    }
}
