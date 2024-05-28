using Manero.Services;

namespace Manero.Registrations
{
    public static class ServiceRegistrations
    {
        public static void AddServiceRegistrations(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<ProductService>(client =>
            {
                client.BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/");
            });

            services.AddHttpClient<OrderService>(client =>
            {
                client.BaseAddress = new Uri("https://order-provider.azurewebsites.net/");
            });

            services.AddHttpClient("CartProductClient", client =>
            {
                client.BaseAddress = new Uri("https://cart-product-provider.azurewebsites.net/");
            });

            services.AddScoped<ProductService>();
            services.AddScoped<IOrderService, OrderService>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var cartHttpClient = httpClientFactory.CreateClient("CartProductClient");
                var orderHttpClient = httpClientFactory.CreateClient();

                var logger = sp.GetRequiredService<ILogger<OrderService>>();
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new OrderService(cartHttpClient, orderHttpClient, configuration, logger);
            });
        }
    }
}

