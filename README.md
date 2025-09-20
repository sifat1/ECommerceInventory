
## E-Commerce Inventory API

RESTful API for managing products, categories, and authentication, built with **ASP.NET Core 8**, **PostgreSQL**, and **Elasticsearch**.

---

### 🛠 Prerequisites

* **.NET 8 SDK** or newer
* **PostgreSQL** (running locally or accessible via connection string)
* **Elasticsearch** (for product search)

---

### ⚙️ Local Setup

1. **Clone & Restore**

   ```bash
   git clone <repo-url>
   cd ECommerceInventory
   dotnet restore
   ```

2. **Configure Connection Strings**
   Edit `appsettings.Development.json` (or `appsettings.json`) and set:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=ecommercedb;Username=<pg-user>;Password=<pg-password>"
   },
   "Jwt": {
     "Key": "your-secret-key",
     "Issuer": "your-app"
   }
   ```

3. **Apply Migrations**
   Create and update the database schema:

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**

   ```bash
   dotnet run
   ```

   The app will start on `http://localhost:5293` (or a random port if specified).

---

### 📜 API Documentation

*Swagger UI* is enabled in **Development** environment:

```
http://localhost:5293/swagger/index.html
```

It includes:

* Tagged endpoints: **Auth**, **Products**, **Categories**
* Example requests & responses
* JWT Bearer authentication support

---

### 🔑 Authentication

1. Register a user via `/api/auth/register`.
2. Obtain a JWT token from `/api/auth/login`.
3. Use the token in the `Authorization: Bearer <token>` header for protected endpoints.

---

###  Technology Stack

* ASP.NET Core 8
* Entity Framework Core (PostgreSQL)
* Swashbuckle.AspNetCore (Swagger/OpenAPI)
* Elasticsearch .NET client
