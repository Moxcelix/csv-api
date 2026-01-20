# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the entire src directory
COPY src/ .

# Build and publish the specific project
RUN dotnet build ./CsvApi.csproj -c Release
RUN dotnet publish ./CsvApi.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "CsvApi.dll"]