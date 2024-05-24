using Manero.Services;

namespace Manero.Registrations
{
    public static class ServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration config) {


            services.AddHttpClient<ProductService>(client =>
            {
                client.BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/");
            });

            services.AddHttpClient<OrderService>(client =>
            {
                client.BaseAddress = new Uri("https://order-provider.azurewebsites.net/");
            });

            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
        }
    }
}
