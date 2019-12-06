using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Xunit;

namespace Enums.Extensions.AutoMapper.Tests.EnumByNameTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EnumByName_SameNamesAndSameValuesTests
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
            First = 1,
            Second = 2
        }


        [Fact]
        public void EnumByName_Assert_WhenAllSourceEnumValuesAreMappableByName_ItShouldNotThrowAMappingException()
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

        [Theory]
        [InlineData(Source.Default, Destination.Default)]
        [InlineData(Source.First, Destination.First)]
        [InlineData(Source.Second, Destination.Second)]
        public void EnumByName_Map_WhenAllSourceEnumValuesAreMappableByName_ItShouldMap(Source source, Destination expected)
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

            // Act
            var result = mapper.Map<Destination>(source);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}