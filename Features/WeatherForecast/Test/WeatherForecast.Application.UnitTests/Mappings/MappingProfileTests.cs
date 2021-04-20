using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using Draekien.CleanVerticalSlice.Common.TestUtils.Mappings;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Mappings
{
    public class MappingProfileTests
    {
        [Fact]
        public void GivenAllMappingConfigurationsFromApplicationAssembly_ThenMappingConfigurationsShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperTestingProfile(typeof(DependencyInjection).Assembly)));

            // Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
