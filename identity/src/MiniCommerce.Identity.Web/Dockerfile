FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["identity/src/MiniCommerce.Identity.Web/MiniCommerce.Identity.Web.csproj", "identity/src/MiniCommerce.Identity.Web/"]
COPY ["identity/src/MiniCommerce.Identity.Infrastructure/MiniCommerce.Identity.Infrastructure.csproj", "identity/src/MiniCommerce.Identity.Infrastructure/"]
COPY ["identity/src/MiniCommerce.Identity.Application/MiniCommerce.Identity.Application.csproj", "identity/src/MiniCommerce.Identity.Application/"]
COPY ["identity/src/MiniCommerce.Identity.Domain/MiniCommerce.Identity.Domain.csproj", "identity/src/MiniCommerce.Identity.Domain/"]

RUN dotnet restore "./identity/src/MiniCommerce.Identity.Web/MiniCommerce.Identity.Web.csproj"
COPY . .

WORKDIR "/src/identity/src/MiniCommerce.Identity.Web"
RUN dotnet build "./MiniCommerce.Identity.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniCommerce.Identity.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniCommerce.Identity.Web.dll"]
