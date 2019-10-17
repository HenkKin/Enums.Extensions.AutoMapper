using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace AutoMapper.Extensions.Enums.Tests.EnumByValueTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EnumByValue_MissingSourceTests
    {
        public enum Source
        {
            Default = 0,
            Second = 2
        }

        public enum Destination
        {
            Default = 0,
            First = 1,
            Second = 2
        }
        
        [Fact]
        public void EnumByValue_Assert_WhenSourceIsMissingByValue_ItShouldNotThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Value);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            // Act
            config.AssertEnumConfigurationIsValid();
        }

        [Fact]
        public void EnumByValue_Assert_WhenMissingDestinationHasCustomMapping_ItShouldNotThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Value)
                .MapFromEnumValue(Source.Default, Destination.First);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            // Act
            config.AssertEnumConfigurationIsValid();
        }

        [Fact]
        public void EnumByValue_Map_WhenMissingDestinationHasCustomMapping_ItShouldMap()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Value)
                .MapFromEnumValue(Source.Default, Destination.First);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            var mapper = config.CreateMapper();

            // Act
            var result = mapper.Map<Destination>(Source.Default);

            // Assert
            Assert.Equal(Destination.First, result);
        }
    }
}