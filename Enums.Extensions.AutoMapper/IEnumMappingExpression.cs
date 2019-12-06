using System;

namespace Enums.Extensions.AutoMapper
{
    public interface IEnumMappingExpression<in TSourceEnum, in TDestinationEnum>
            where TSourceEnum : Enum
            where TDestinationEnum : Enum
    {
        IEnumMappingExpression<TSourceEnum, TDestinationEnum> MapFromEnumValue(TSourceEnum source, TDestinationEnum destination);
    }
}