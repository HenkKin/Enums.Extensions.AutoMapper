using System;
using System.Reflection;

namespace AutoMapper.Extensions.Enums
{
    public static class MappingExpressionExtensions
    {
        public static IEnumMappingExpression<TSourceEnum, TDestinationEnum> AsEnumMap<TSourceEnum,
            TDestinationEnum>(this IMappingExpression<TSourceEnum, TDestinationEnum> mappingExpression, EnumMappingType enumMappingType = EnumMappingType.Value)
            where TSourceEnum : struct, IConvertible
            where TDestinationEnum : struct, IConvertible
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
