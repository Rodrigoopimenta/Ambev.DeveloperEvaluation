# Ambev Developer Evaluation (.NET Backend)

This is a .NET Core Web API project developed as part of the Ambev developer evaluation. It includes features such as creating and retrieving sales, domain-driven design (DDD) principles, MediatR for CQRS, validation, logging, and event publishing through RabbitMQ.

## Features

- Create a new sale
- Retrieve sale details
- Validate commands using FluentValidation
- AutoMapper for object mapping
- Publish domain events (e.g., SaleCreated)

## Prerequisites

Ensure you have the following installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [PostgreSQL](https://www.postgresql.org/download/) (with connection string configured in `appsettings.json`)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [Docker](https://www.docker.com/) (optional for PostgreSQL and RabbitMQ)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/ambev-evaluation-api.git
cd ambev-evaluation-api
```

### 2. Setup the database (PostgreSQL)

Create a database manually or using Docker:

```bash
docker run --name postgres -e POSTGRES_PASSWORD=admin -e POSTGRES_DB=AmbevEvaluation -p 5432:5432 -d postgres
```

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=AmbevEvaluation;Username=postgres;Password=admin"
}
```

Run EF Core migrations:

```bash
dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM
```

### 3. Setup RabbitMQ

Start RabbitMQ using Docker:

```bash
docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

Access management UI at: http://localhost:15672 (username: `guest`, password: `guest`)

### 4. Run the API

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Swagger UI: https://localhost:5001/swagger

## API Overview

### POST /api/sales
Create a new sale

### GET /api/sales/{id}
Get a sale by ID

## Event System

Events are published to RabbitMQ and logged via Serilog:

- `SaleCreated`
- `SaleModified`
- `SaleCancelled`
- `ItemCancelled`

## Notes

- Events are published in JSON format to the `sales_events` queue.
- Consumer logs incoming events using `Serilog`.

---

For questions or contributions, feel free to fork the repo or open an issue.

