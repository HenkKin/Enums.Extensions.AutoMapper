using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace AutoMapper.Extensions.Enums.Tests.EnumByValueTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EnumByValue_SameNamesAndSameValuesTests
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
        [InlineData(Source.Default, Destination.Default)]
        [InlineData(Source.First, Destination.First)]
        [InlineData(Source.Second, Destination.Second)]
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