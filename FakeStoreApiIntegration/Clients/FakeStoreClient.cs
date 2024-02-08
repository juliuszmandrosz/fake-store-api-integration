using FakeStoreApiIntegration.Products;

namespace FakeStoreApiIntegration.Clients;

public interface IFakeStoreClient
{
    Task<List<Product>?> GetProductsByCategoryAsync(string category, int page, int limit);
    Task<List<Product>?> SearchProductsAsync(string query, int page, int limit);
}

public class FakeStoreClient : IFakeStoreClient
{
    private readonly HttpClient _httpClient;

    public FakeStoreClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>?> GetProductsByCategoryAsync(string category, int page, int limit)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Product>>($"products/category/{category}");
        if (response is null) return null;
        var skip = (page - 1) * limit;
        return response
            .Skip(skip)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Product>?> SearchProductsAsync(string query, int page, int limit)
    {
        var response =
            await _httpClient.GetFromJsonAsync<List<Product>>("products");
        if (response is null) return null;
        var skip = (page - 1) * limit;
        return response
            .Where(p => p.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Skip(skip)
            .Take(limit)
            .ToList();
    }
}