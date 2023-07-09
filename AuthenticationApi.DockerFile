# syntax=docker/dockerfile:

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/Services/AuthenticationApi/AuthenticationApi/AuthenticationApi.csproj" "Services/AuthenticationApi/AuthenticationApi/"
COPY "src/Services/AuthenticationApi/AuthenticationApi.Application/AuthenticationApi.Application.csproj" "Services/AuthenticationApi/AuthenticationApi.Application/"
COPY "src/Services/AuthenticationApi/AuthenticationApi.Domain/AuthenticationApi.Domain.csproj" "Services/AuthenticationApi/AuthenticationApi.Domain/"
COPY "src/Services/AuthenticationApi/AuthenticationApi.Infrastructure/AuthenticationApi.Infrastructure.csproj" "Services/AuthenticationApi/AuthenticationApi.Infrastructure/"
COPY "src/Shared/Shared.csproj" "/Shared/"
RUN dotnet restore "Services/AuthenticationApi/AuthenticationApi/AuthenticationApi.csproj"
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p 9873
COPY "src/" .
WORKDIR "/src/Services/AuthenticationApi/AuthenticationApi"
RUN dotnet build "AuthenticationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /https/ /https/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationApi.dll"]
