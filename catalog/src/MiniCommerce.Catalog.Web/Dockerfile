FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*

USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["catalog/src/MiniCommerce.Catalog.Web/MiniCommerce.Catalog.Web.csproj", "catalog/src/MiniCommerce.Catalog.Web/"]
COPY ["catalog/src/MiniCommerce.Catalog.Infrastructure/MiniCommerce.Catalog.Infrastructure.csproj", "catalog/src/MiniCommerce.Catalog.Infrastructure/"]
COPY ["catalog/src/MiniCommerce.Catalog.Application/MiniCommerce.Catalog.Application.csproj", "catalog/src/MiniCommerce.Catalog.Application/"]
COPY ["catalog/src/MiniCommerce.Catalog.Domain/MiniCommerce.Catalog.Domain.csproj", "catalog/src/MiniCommerce.Catalog.Domain/"]

RUN dotnet restore "./catalog/src/MiniCommerce.Catalog.Web/MiniCommerce.Catalog.Web.csproj"
COPY . .

WORKDIR "/src/catalog/src/MiniCommerce.Catalog.Web"
RUN dotnet build "./MiniCommerce.Catalog.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniCommerce.Catalog.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniCommerce.Catalog.Web.dll"]
