using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Xunit;

namespace Enums.Extensions.AutoMapper.Tests.EnumByNameTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EnumByName_MissingSourceTests
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
        public void EnumByName_Assert_WhenSourceIsMissingByName_ItShouldNotThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });
            
            // Act
            config.AssertEnumConfigurationIsValid();
        }

        [Fact]
        public void EnumByName_Assert_WhenMissingDestinationHasCustomMapping_ItShouldNotThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name)
                .MapFromEnumValue(Source.Default, Destination.First);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            // Act
            config.AssertEnumConfigurationIsValid();
        }

        [Fact]
        public void EnumByName_Map_WhenMissingDestinationHasCustomMapping_ItShouldMap()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name)
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