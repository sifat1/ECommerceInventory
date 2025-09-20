# E-Commerce Inventory API

ASP.NET Core 8 Web API with PostgreSQL, Identity + JWT, and Elasticsearch.

## Prerequisites
- .NET 8 SDK
- PostgreSQL running locally
- Elasticsearch (for product search) use port 9200

## Local Setup
```bash
git clone https://github.com/your/repo.git
cd ECommerceInventory

# Update appsettings.Development.json:
# "ConnectionStrings": {
#   "DefaultConnection": "Host=localhost;Database=ECommerceDb;Username=postgres;Password=yourpw"
# }

dotnet ef database update    # run migrations
dotnet run                   # starts the API
