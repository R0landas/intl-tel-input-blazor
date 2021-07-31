using IntlTelInputBlazor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterIntlTelInput(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped <IntlTelInputJsInterop>();
            return serviceCollection;
        }
    }
}