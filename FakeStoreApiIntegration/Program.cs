using System.Runtime.CompilerServices;
using FakeStoreApiIntegration.Clients;
using FakeStoreApiIntegration.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddOutputCache(opt => opt.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(5))
    .AddStackExchangeRedisCache(x =>
    {
        x.InstanceName = builder.Configuration["Redis:InstanceName"];
        x.Configuration = builder.Configuration["Redis:Configuration"];
    });

builder.Services.AddHttpClient<IFakeStoreClient, FakeStoreClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["FakeStoreApi:BaseUrl"]!);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.MapProductEndpoints();

app.Run();

public abstract partial class Program
{
}