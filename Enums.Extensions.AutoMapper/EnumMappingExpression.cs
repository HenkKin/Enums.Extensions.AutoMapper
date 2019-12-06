using System;
using System.Reflection;
using AutoMapper;

namespace Enums.Extensions.AutoMapper
{
    internal class EnumMappingExpression<TSourceEnum, TDestinationEnum> : IEnumMappingExpression<TSourceEnum, TDestinationEnum>
            where TSourceEnum : struct, Enum
            where TDestinationEnum : struct, Enum
    {
        private readonly EnumTypeConverter<TSourceEnum, TDestinationEnum> _enumTypeConverter;
        protected IMappingExpression<TSourceEnum, TDestinationEnum>  MappingExpression { get; }

        public EnumMappingExpression(IMappingExpression<TSourceEnum, TDestinationEnum> mappingExpression, EnumMappingType enumMappingType)
        {
            if (!typeof(TSourceEnum).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"{typeof(TSourceEnum).FullName} must be an enum type");
            }

            if (!typeof(TDestinationEnum).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"{typeof(TDestinationEnum).FullName} must be an enum type");
            }

            _enumTypeConverter = new EnumTypeConverter<TSourceEnum, TDestinationEnum>(enumMappingType);
            mappingExpression.ConvertUsing(_enumTypeConverter);
            MappingExpression = mappingExpression;
        }

        public IEnumMappingExpression<TSourceEnum, TDestinationEnum> MapFromEnumValue(TSourceEnum source, TDestinationEnum destination)
        {
           _enumTypeConverter.UpdateEnumMapping(source, destination);
            return this;
        }
    }
}