using System.Linq;
using System.Reflection;

namespace AutoMapper.Extensions.Enums
{
    public static class ConfigurationProviderExtensions
    {
        public static void AssertEnumConfigurationIsValid(this IConfigurationProvider configurationProvider)
        {
            var typeMaps = configurationProvider.GetAllTypeMaps()
                .Where(tm=> tm.SourceType.IsEnum &&
                            tm.DestinationType.IsEnum &&
                            tm.CustomMapFunction != null)
                .ToList();

            foreach (var typeMap in typeMaps)
            {
                var validationContext = new ValidationContext(typeMap.Types, null, typeMap);

                if (validationContext.Types.SourceType.IsEnum && validationContext.Types.DestinationType.IsEnum && validationContext.TypeMap.CustomMapFunction != null)
                {
                    try
                    {
                        validationContext.TypeMap.CustomMapFunction.Compile()
                            .DynamicInvoke(default,default, new TestResolutionContext());
                    }
                    catch (TargetInvocationException ex)
                    {
                        throw ex.InnerException;
                    }
                }
            }
        }
    }
}
