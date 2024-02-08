using System.Net.Http.Json;
using FakeStoreApiIntegration.Products;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FakeStoreApiIntegrationTests;

public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _appFactory;
    private readonly HttpClient _client;

    public ProductIntegrationTests(WebApplicationFactory<Program> appFactory)
    {
        _appFactory = appFactory;
        _client = appFactory.CreateClient();
    }

    [Fact]
    public async Task GetProductsByCategory_ResultShouldNotBeNull()
    {
        const string category = "jewelery";
        var result = await _client.GetFromJsonAsync<List<Product>>($"/products/categories/{category}?page=1&limit=10");
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task SearchProducts_ResultShouldNotBeNull()
    {
        const string query = "test123";
        var result = await _client.GetFromJsonAsync<List<Product>>($"/products/search/?query={query}&page=1&limit=10");
        result.Should().NotBeNull();
    }
}