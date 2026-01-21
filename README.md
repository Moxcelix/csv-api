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
```
# API Endpoints Specification
---

## 1. CSV File Upload

### **POST** `/csv/upload`

Upload a CSV file for processing. The API will parse the CSV, validate the data, store it in the database, and calculate aggregated metrics.

#### Request
**Content-Type:** `multipart/form-data`

| Parameter | Type | Required | Description | Validation Rules |
|-----------|------|----------|-------------|------------------|
| `file` | `file` | Yes | CSV file to upload | • File size: ≤ 10MB<br>• Extension: .csv<br>• Content-Type: csv |

#### Request Body Format
The CSV file must contain exactly 3 columns separated by semicolons (`;`):
```
<Start Time>;<Execution Time>;<Value>
```
**Column Specifications:**
1. **Start Time**: Operation start timestamp in ISO-like format
   - Format: `YYYY-MM-DDThh-mm-ss.ffffZ`
   - Example: `2024-01-15T08-30-45.1234Z`
   - Must be in UTC (Z suffix)
   - Milliseconds precision (1-4 digits)

2. **Execution Time**: Duration in seconds
   - Type: Floating-point number
   - Range: ≥ 0
   - Precision: Up to 6 decimal places
   - Example: `1.234`

3. **Value**: Metric value
   - Type: Floating-point number
   - Range: Any valid double
   - Precision: Up to 6 decimal places
   - Example: `42.5`

#### Example CSV Content
```
2024-01-15T08-30-45.1234Z;1.234;42.5
2024-01-15T09-15-20.5678Z;2.567;89.3
2024-01-15T10-05-10.9012Z;0.789;15.7
```
#### Response

**Success Response (200 OK)**
```json
{
  "message": "Success",
  "count": 10,
}
```

**Example Request cURL:**
```bash
curl -X POST "http://localhost:5000/csv/upload" \
  -H "accept: application/json" \
  -F "file=@data.csv"
```

## 2. Search Results with Filters

### **GET** `/results/find`

Search for processed results with comprehensive filtering capabilities. Supports pagination for large result sets.

#### Request
**Query Parameters**

| Parameter | Type | Required | Description | Format/Constraints |
|-----------|------|----------|-------------|-------------------|
| `name` | `string` | No | Process name filter | Partial match |
| `startTime` | `DateTime` | No | Start of time range | ISO 8601 (e.g., `2024-01-01T00:00:00Z`) |
| `endTime` | `DateTime` | No | End of time range | ISO 8601 (e.g., `2024-01-31T23:59:59Z`) |
| `minValue` | `double` | No | Minimum value mean | Decimal number |
| `maxValue` | `double` | No | Maximum value mean | Decimal number |
| `minExecutionTime` | `double` | No | Minimum avg execution time | ≥ 0 |
| `maxExecutionTime` | `double` | No | Maximum avg execution time | ≥ 0 |
| `page` | `integer` | No | Page number | ≥ 1 |
| `pageSize` | `integer` | No | Items per page | 1-100 |

#### Filter Logic

- Time Filters: Applied to FirstOperationTime field

- Value Filters: Applied to ValueMean field

- Execution Time Filters: Applied to AverageExecutionTime field

- All filters are combined with AND logic

- Date ranges are inclusive

- Value ranges are inclusive

#### Response

**Success Response (200 OK)**
```json
{
  "results": [
    {
      "processName": "data_20240115",
      "deltaTime": "00:45:30",
      "firstOperationTime": "2024-01-15T08:30:45.123Z",
      "averageExecutionTime": 1.845,
      "valueMean": 67.82,
      "valueMedian": 65.4,
      "valueMin": 12.3,
      "valueMax": 150.6
    }
  ]
}
```


**Example Request cURL:**

#### Basic search

```bash
curl "http://localhost:5000/results/find?name=data&page=1&pageSize=10"
```

#### Complex filter

```bash
curl "http://localhost:5000/results/find?name=process&minValue=10&maxValue=100&minExecutionTime=0.5&maxExecutionTime=5.0&page=2&pageSize=25"
```

## 3. Get Last Values for Process

### **GET** `/values/last`

Retrieve the last 10 operation values for a specific process, sorted by operation start time in descending order (most recent first).

#### Query Parameters

| Parameter | Type | Required | Description | Format/Constraints |
|-----------|------|----------|-------------|-------------------|
| `processName` | `string` | Yes | Process name to filter by | Exact match |

#### Request Details
- Requires exact process name match
- Returns maximum 10 most recent operations
- Sorted by `StartDate` descending (newest first)
- Only returns operations that belong to the specified process

#### Response

**Success Response (200 OK)**
```json
{
  "values": [
    {
      "startDate": "2024-01-15T17:25:35.890Z",
      "executionTimeSeconds": 0.234,
      "value": 12.3,
    },
    {
      "startDate": "2024-01-15T16:40:50.456Z",
      "executionTimeSeconds": 3.123,
      "value": 150.6,
    }
  ]
}
```


**Example Request cURL:**
```bash
curl "http://localhost:5000/values/last?processName=file1"
```
