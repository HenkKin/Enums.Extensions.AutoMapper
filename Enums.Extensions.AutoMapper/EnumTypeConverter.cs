using System;
using System.Collections.Concurrent;
using System.Text;
using AutoMapper;

namespace Enums.Extensions.AutoMapper
{
    internal class EnumTypeConverter<TSourceEnum, TDestinationEnum> : ITypeConverter<TSourceEnum, TDestinationEnum>
        where TSourceEnum : struct, Enum
        where TDestinationEnum : struct, Enum
    {
        private readonly EnumMappingType _enumMappingType;
        private readonly ConcurrentDictionary<TSourceEnum, TDestinationEnum> _enumValueMappings = new ConcurrentDictionary<TSourceEnum, TDestinationEnum>();

        public EnumTypeConverter(EnumMappingType enumMappingType = EnumMappingType.Value)
        {
            _enumMappingType = enumMappingType;

            var destinationEnumValues = Enum.GetValues(typeof(TDestinationEnum));

            foreach (var destinationEnumValue in destinationEnumValues)
            {
                if (enumMappingType == EnumMappingType.Name)
                {
                    var destinationEnumName = Enum.GetName(typeof(TDestinationEnum), destinationEnumValue);
                    if (destinationEnumName == null)
                    {
                        throw new ArgumentException(
                            $"Can not find name of {destinationEnumValue} in type {typeof(TDestinationEnum).FullName}");
                    }

                    const bool ignoreCase = false;
                    if (Enum.TryParse(destinationEnumName, ignoreCase, out TSourceEnum sourceEnumValue))
                    {
                        _enumValueMappings.TryAdd(sourceEnumValue, (TDestinationEnum)destinationEnumValue);
                    }
                }
                else
                {
                    var sourceEnumValues = Enum.GetValues(typeof(TSourceEnum));
                    foreach (var sourceEnumValue in sourceEnumValues)
                    {
                        // TODO: are values matching?
                        if ((int)sourceEnumValue == (int)destinationEnumValue)
                        {
                            _enumValueMappings.TryAdd((TSourceEnum)sourceEnumValue, (TDestinationEnum)destinationEnumValue);
                        }
                    }
                }
            }
        }


        public TDestinationEnum Convert(TSourceEnum source, TDestinationEnum destination, ResolutionContext context)
        {
            if (context.GetType() == typeof(TestResolutionContext))
            {
                AssertEnumConfigurationIsValid();
                return default;
            }

            if (!_enumValueMappings.TryGetValue(source, out TDestinationEnum result))
            {
                AssertEnumConfigurationIsValid();
                throw new AutoMapperMappingException($"Value {source} of type {typeof(TSourceEnum).FullName} not supported");
            }

            return result;
        }

        public void UpdateEnumMapping(TSourceEnum source, TDestinationEnum destination)
        {
            _enumValueMappings.AddOrUpdate(source, destination, (key, existingValue)=> destination);
        }

        public void AssertEnumConfigurationIsValid()
        {
            var hasMappingError = false;
            var sourceEnumMappings = Enum.GetValues(typeof(TSourceEnum));
            var messageBuilder = new StringBuilder($"Missing enum mapping from {typeof(TSourceEnum).FullName} to {typeof(TDestinationEnum).FullName} based on {_enumMappingType}");
            messageBuilder.AppendLine();
            messageBuilder.AppendLine("The following source values are not mapped:");

            foreach (TSourceEnum sourceEnumMapping in sourceEnumMappings)
            {
                if (!_enumValueMappings.ContainsKey(sourceEnumMapping))
                {
                    hasMappingError = true;
                    messageBuilder.AppendLine($" - {sourceEnumMapping}");
                }
            }

            if (hasMappingError)
            {
                throw new AutoMapperConfigurationException(messageBuilder.ToString());
            }
        }
    }
}
