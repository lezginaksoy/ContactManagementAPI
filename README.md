# Contact Management API

A simple .NET 8 Web API for managing contacts and assigning them to investment funds. Uses **PostgreSQL** as the database and follows a **Code-First approach** with EF Core.

## 📌 Features
- ✅ Create, Read, Update, Delete (CRUD) operations for **Contacts**.
- ✅ Assign and remove contacts from **Funds**.
- ✅ **Swagger UI** for easy API testing or using ContactManagementAPI.http
- ✅ **Unit Testing** with xUnit and Moq.
- ✅ **PostgreSQL Database with EF Core Migrations**.

---

## 🚀 **Getting Started**
### **1️⃣ Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
---
## 🚀  Clone Repo and Getting Started with EF Core Migrations

### **1️⃣ Add the Initial Migration**

Creates the first migration, which defines the initial database schema.
```sh

📌 migration and restore db
  dotnet ef migrations add InitialCreate  
  dotnet ef database update initialcreate

📌 github
 git clone https://github.com/YOUR_USERNAME/contact-management-api.git


📌 Run Tests (Run all tests)
  dotnet test

  
📌 Unit Tests and Code Coverage Report
  
  .../TestResults/Index.html

  or generate a new report.

  dotnet test --collect:"Xplat code coverage"
  reportgenerator -reports:coverage.cobertura.xml -reporttypes:html -targetDir:.

