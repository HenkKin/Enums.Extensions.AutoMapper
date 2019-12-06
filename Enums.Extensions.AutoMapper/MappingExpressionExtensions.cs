using System;
using System.Reflection;
using AutoMapper;

namespace Enums.Extensions.AutoMapper
{
    public static class MappingExpressionExtensions
    {
        public static IEnumMappingExpression<TSourceEnum, TDestinationEnum> AsEnumMap<TSourceEnum,
            TDestinationEnum>(this IMappingExpression<TSourceEnum, TDestinationEnum> mappingExpression, EnumMappingType enumMappingType = EnumMappingType.Value)
            where TSourceEnum : struct, Enum
            where TDestinationEnum : struct, Enum
        {
            if (!typeof(TSourceEnum).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"{typeof(TSourceEnum).FullName} must be an enum type");
            }

            if (!typeof(TDestinationEnum).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"{typeof(TDestinationEnum).FullName} must be an enum type");
            }

            var enumMapping = new EnumMappingExpression<TSourceEnum, TDestinationEnum>(mappingExpression, enumMappingType);
            return enumMapping;
        }
    }
}
