-Clone the repository
git clone https://github.com/vivekgahlyan/dentsply-sirona-audit-service.git

-PostgreSQL
I have used personal postgreSQL instance for e2e working solution.

-Run the application
dotnet run

-Tech Stack
.NET 8 (ASP.NET Core Web API)
Entity Framework Core
PostgreSQL
AutoMapper
Swagger / Swashbuckle

-Design Pattern
Repository Design Pattern

-Testing
You can use Postman or curl to test endpoints.

-Usage Exmaple
1) To create new audit entry
POST /api/Audit/CreateAuditDetail

Request JSON Example:
{
  "entityName": "Customer",
  "userId": "user123",
  "action": "Updated",
  "before": {
    "Id": 1,
    "Name": "John Doe",
    "Email": "old@example.com"
  },
  "after": {
    "Id": 1,
    "Name": "John D.",
    "Email": "new@example.com"
  }
}

Response JSON Example:
{
  "id": 1,
  "entityName": "Customer",
  "action": "Updated",
  "timestampUtc": "2025-09-20T12:30:45Z",
  "userId": "user123",
  "changes": [
    {
      "propertyName": "Name",
      "oldValue": "John Doe",
      "newValue": "John D."
    },
    {
      "propertyName": "Email",
      "oldValue": "old@example.com",
      "newValue": "new@example.com"
    }
  ]
}

2) Get audit by auditId
GET /api/Audit/GetAuditDetailsById/1

Response JSON
{
    "id": 1,
    "entityName": "Product",
    "action": "Created",
    "timestampUtc": "2025-09-20T16:06:22.495016Z",
    "userId": "admin@company.com",
    "changes": [
        {
            "propertyName": "Id",
            "oldValue": null,
            "newValue": "501"
        },
        {
            "propertyName": "Name",
            "oldValue": null,
            "newValue": "\"Laptop\""
        },
        {
            "propertyName": "Price",
            "oldValue": null,
            "newValue": "65000"
        },
        {
            "propertyName": "Stock",
            "oldValue": null,
            "newValue": "10"
        }
    ]
}