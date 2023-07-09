# syntax=docker/dockerfile:

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/Services/IdentityApi/IdentityApi/IdentityApi.csproj" "Services/IdentityApi/IdentityApi/"
COPY "src/Services/IdentityApi/IdentityApi.Application/IdentityApi.Application.csproj" "Services/IdentityApi/IdentityApi.Application/"
COPY "src/Services/IdentityApi/IdentityApi.Domain/IdentityApi.Domain.csproj" "Services/IdentityApi/IdentityApi.Domain/"
COPY "src/Services/IdentityApi/IdentityApi.Infrastructure/IdentityApi.Infrastructure.csproj" "Services/IdentityApi/IdentityApi.Infrastructure/"
COPY "src/Shared/Shared.csproj" "/Shared/"
RUN dotnet restore "Services/IdentityApi/IdentityApi/IdentityApi.csproj"
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p 9873
COPY "src/" .
WORKDIR "/src/Services/IdentityApi/IdentityApi"
RUN dotnet build "IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /https/ /https/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityApi.dll"]
