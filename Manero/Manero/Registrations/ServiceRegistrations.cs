using Manero.Services;

namespace Manero.Registrations
{
    public static class ServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration config) {

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/") });

            //services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/api/GetCartItems?code=zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==") });

            services.AddHttpClient<ProductService>(client =>
            {
                client.BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/");
            });

        }
    }
}
