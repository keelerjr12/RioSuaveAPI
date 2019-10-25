using Microsoft.Extensions.Configuration;

namespace RioSuaveAPI.ProtectedJsonConfiguration
{
    public static class ProtectedJsonConfigurationProviderExtension
    {
        public static IConfigurationBuilder AddProtectedJsonConfiguration(this IConfigurationBuilder builder)
        {
            return builder.Add(new ProtectedJsonConfigurationSource());
        }
    }
}
