# syntax=docker/dockerfile:

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/Services/ScoreApi/ScoreApi/ScoreApi.csproj" "Services/ScoreApi/ScoreApi/"
COPY "src/Services/ScoreApi/ScoreApi.Application/ScoreApi.Application.csproj" "Services/ScoreApi/ScoreApi.Application/"
COPY "src/Services/ScoreApi/ScoreApi.Domain/ScoreApi.Domain.csproj" "Services/ScoreApi/ScoreApi.Domain/"
COPY "src/Services/ScoreApi/ScoreApi.Infrastructure/ScoreApi.Infrastructure.csproj" "Services/ScoreApi/ScoreApi.Infrastructure/"
COPY "src/Shared/Shared.csproj" "/Shared/"
RUN dotnet restore "Services/ScoreApi/ScoreApi/ScoreApi.csproj"
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p 9873
COPY "src/" .
WORKDIR "/src/Services/ScoreApi/ScoreApi"
RUN dotnet build "ScoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScoreApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /https/ /https/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScoreApi.dll"]
