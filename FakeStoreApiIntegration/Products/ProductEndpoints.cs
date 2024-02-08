using FakeStoreApiIntegration.Clients;

namespace FakeStoreApiIntegration.Products;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products/categories/{category}",
                async (IFakeStoreClient client, string category, int page = 1, int limit = 10) =>
                {
                    var products = await client.GetProductsByCategoryAsync(category, page, limit);
                    return products is null ? Results.NotFound() : Results.Ok(products);
                })
            .CacheOutput()
            .WithName("GetProductsByCategory")
            .WithOpenApi();

        app.MapGet("/products/search",
                async (IFakeStoreClient client, string query = "", int page = 1, int limit = 10) =>
                {
                    var products = await client.SearchProductsAsync(query, page, limit);
                    return products is null ? Results.NotFound() : Results.Ok(products);
                })
            .CacheOutput()
            .WithName("SearchProducts")
            .WithOpenApi();
    }
}