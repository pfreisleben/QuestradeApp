# syntax=docker/dockerfile:

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/Services/FinanceApi/FinanceApi/FinanceApi.csproj" "Services/FinanceApi/FinanceApi/"
COPY "src/Services/FinanceApi/FinanceApi.Application/FinanceApi.Application.csproj" "Services/FinanceApi/FinanceApi.Application/"
COPY "src/Services/FinanceApi/FinanceApi.Domain/FinanceApi.Domain.csproj" "Services/FinanceApi/FinanceApi.Domain/"
COPY "src/Services/FinanceApi/FinanceApi.Infrastructure/FinanceApi.Infrastructure.csproj" "Services/FinanceApi/FinanceApi.Infrastructure/"
COPY "src/Shared/Shared.csproj" "/Shared/"
RUN dotnet restore "Services/FinanceApi/FinanceApi/FinanceApi.csproj"
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p 9873
COPY "src/" .
WORKDIR "/src/Services/FinanceApi/FinanceApi"
RUN dotnet build "FinanceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /https/ /https/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceApi.dll"]
