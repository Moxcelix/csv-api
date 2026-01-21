# CSV Processor API

A REST API for processing CSV files containing operation execution metrics. Built with .NET 8, PostgreSQL, and Docker.

## Features

- **CSV Upload & Parsing** - Process CSV files with custom timestamp formats
- **Metrics Analysis** - Calculate statistical metrics from operation data
- **Advanced Filtering** - Filter results by multiple criteria
- **Pagination & Sorting** - Efficient data retrieval with pagination
- **Docker Support** - Containerized deployment with Docker Compose
- **OpenAPI Documentation** - Interactive API documentation with Swagger UI

## Quick Start

### Using Docker (Recommended)

```bash
# Clone repository
git clone https://github.com/yourusername/csv-api.git
cd csv-api

# Start services
docker-compose up -d

# API available at: http://localhost:5000
# Swagger UI: http://localhost:5000/swagger