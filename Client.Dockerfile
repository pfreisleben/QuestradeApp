FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "src/Client/Client/Client.csproj" "src/Client/Client/"
COPY "src/Client/Client.Infrastructure/Client.Infrastructure.csproj" "src/Client/Client.Infrastructure/"
COPY "src/Shared/Shared.csproj" "src/Shared/"

COPY "src/" .
WORKDIR "/src/Client/Client"
RUN dotnet build "Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
EXPOSE 80
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY "src/Client/Client/nginx.conf" /etc/nginx/nginx.conf
