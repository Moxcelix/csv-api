FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY src/CsvApi.sln .
COPY src/CsvApi.API/*.csproj src/CsvApi.API/
COPY src/CsvApi.Application/*.csproj src/CsvApi.Application/
COPY src/CsvApi.Domain/*.csproj src/CsvApi.Domain/
COPY src/CsvApi.Infrastructure/*.csproj src/CsvApi.Infrastructure/

# Restore packages
RUN dotnet restore CsvApi.sln

# Copy everything else
COPY . .

# Build and publish only the API project
WORKDIR /app/src/CsvApi.API
RUN dotnet publish -c Release -o /out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 80
ENTRYPOINT ["dotnet", "CsvApi.API.dll"]