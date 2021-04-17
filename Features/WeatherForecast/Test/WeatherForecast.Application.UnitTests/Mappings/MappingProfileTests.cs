using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Mappings
{
    public class MappingProfileTests
    {
        [Fact]
        public void GivenAllMappingConfigurationsFromApplicationAssembly_ThenMappingConfigurationsShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(config => { config.AddProfile(new MappingProfile(typeof(DependencyInjection).Assembly)); });

            // Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
