using System;
using System.Reflection;

namespace AutoMapper.Extensions.Enums
{
    internal class EnumMappingExpression<TSourceEnum, TDestinationEnum> : IEnumMappingExpression<TSourceEnum, TDestinationEnum>
            where TSourceEnum : struct, IConvertible
            where TDestinationEnum : struct, IConvertible
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