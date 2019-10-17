using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace AutoMapper.Extensions.Enums.Tests.EnumByNameTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EnumByName_MissingDestinationTests
    {
        public enum Source
        {
            Default = 0,
            First = 1,
            Second = 2
        }

        public enum Destination
        {
            Default = 0,
            Second = 2
        }


        [Fact]
        public void EnumByName_Assert_WhenDestinationIsMissingByName_ItShouldThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            var act = new Action(() => config.AssertEnumConfigurationIsValid());

            // Act
            Assert.Throws<AutoMapperConfigurationException>(act);
        }

        [Fact]
        public void EnumByName_Map_WhenDestinationIsMissingByName_ItShouldNotMap()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            var mapper = config.CreateMapper();

            var act = new Action(() => mapper.Map<Destination>(Source.First));

            // Act
            Assert.Throws<AutoMapperConfigurationException>(act);
        }

        [Fact]
        public void EnumByName_Assert_WhenMissingDestinationHasCustomMapping_ItShouldNotThrowAMappingException()
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Name)
                .MapFromEnumValue(Source.First, Destination.Default);

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
                .MapFromEnumValue(Source.First, Destination.Default);

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(profile);
            });

            var mapper = config.CreateMapper();

            // Act
            var result = mapper.Map<Destination>(Source.First);

            // Assert
            Assert.Equal(Destination.Default, result);
        }
    }
}