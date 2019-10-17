using System;

namespace AutoMapper.Extensions.Enums
{
    public interface IEnumMappingExpression<in TSourceEnum, in TDestinationEnum>
            where TSourceEnum : struct, IConvertible
            where TDestinationEnum : struct, IConvertible
    {
        IEnumMappingExpression<TSourceEnum, TDestinationEnum> MapFromEnumValue(TSourceEnum source, TDestinationEnum destination);
    }
}