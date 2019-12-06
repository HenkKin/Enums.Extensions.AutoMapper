using AutoMapper;
using Xunit;

namespace Enums.Extensions.AutoMapper.Tests.EnumByValueTests
{
    public class EnumByValue_SameNamesAndDifferentValuesTests
    {
        public enum Source
        {
            Default = 0,
            First = 1,
            Second = 2
        }

        public enum Destination
        {
            First = 0,
            Second = 1,
            Default = 2
        }


        [Fact]
        public void EnumByValue_Assert_WhenAllSourceEnumValuesAreMappableByValue_ItShouldNotThrowAMappingException()
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

        [Theory]
        [InlineData(Source.Default, Destination.First)]
        [InlineData(Source.First, Destination.Second)]
        [InlineData(Source.Second, Destination.Default)]
        public void EnumByValue_Map_WhenAllSourceEnumValuesAreMappableByValue_ItShouldMap(Source source, Destination expected)
        {
            // Arrange
            var profile = new TestProfile();
            profile.CreateMap<Source, Destination>()
                .AsEnumMap(EnumMappingType.Value);

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