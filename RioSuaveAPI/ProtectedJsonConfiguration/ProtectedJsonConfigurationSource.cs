using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace RioSuaveAPI.ProtectedJsonConfiguration
{
    public class ProtectedJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            // TODO: REFACTOR THIS!
            Path = "appsettings.enc";
            EnsureDefaults(builder);
            ResolveFileProvider();
            return new ProtectedJsonConfigurationProvider(this);
        }
    }
}
