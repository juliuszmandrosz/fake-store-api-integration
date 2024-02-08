# FakeStoreApiIntegration

## Technologies

- C# 12.0
- .NET 8.0

## Libraries

- Microsoft.AspNetCore.OpenApi 8.0.0
- Microsoft.Extensions.Caching.StackExchangeRedis 8.0.1
- Swashbuckle.AspNetCore 6.4.0

## Prerequisites

- Docker installed
- .NET 8.0 installed

## Getting started

1. Run Redis container:

```bash
docker-compose up
```

2. Run api:

```bash
dotnet run
```