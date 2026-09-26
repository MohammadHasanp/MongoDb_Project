# AspProMongoDb 🍃

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![MongoDB](https://img.shields.io/badge/MongoDB-Driver%203.10.0-47A248?logo=mongodb&logoColor=white)](https://www.mongodb.com/docs/drivers/csharp/)
[![Razor Pages](https://img.shields.io/badge/ASP.NET%20Core-Razor%20Pages-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/razor-pages/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

A clean, production-style **ASP.NET Core 8 Razor Pages** web application demonstrating how to integrate **MongoDB** as the primary data store — featuring a **generic repository/service pattern**, **MongoDB transactions with sessions**, and a complete **CRUD** user management module.

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [MongoDB Replica Set Setup](#mongodb-replica-set-setup)
  - [Configuration](#configuration)
  - [Run the Application](#run-the-application)
- [Application Pages](#-application-pages)
- [How It Works](#-how-it-works)
- [Extending the Project](#-extending-the-project)
- [Author](#-author)

---

## 🔍 Overview

**AspProMongoDb** shows a practical, layered approach for building web apps on top of MongoDB with .NET. Instead of scattering database calls across page models, all data access flows through a **generic base service** (`BaseServices<TEntity>`) that wraps the official MongoDB C# driver, wires every write operation into a **client session + transaction**, and resolves collections automatically by entity name.

The included sample module is a **User Management** system with full Create / Read / Update / Delete pages built with Razor Pages and Bootstrap 5.

## ✨ Features

- ✅ **Full CRUD** for users (list, create, view details, edit, delete)
- 🧱 **Generic Repository/Service pattern** — add a new entity with just a few lines of code
- 🔐 **MongoDB Transactions** — inserts, updates, and deletes run inside a session-backed transaction
- 🆔 **GUID primary keys** with standard BSON GUID representation (`BaseEntity`)
- 💉 **Built-in Dependency Injection** — `IMongoClient` (singleton), `MongoDbContext` (scoped), services (transient)
- ⚙️ **Configuration-driven** — connection string & database name live in `appsettings.json` (Options pattern)
- 🎨 **Responsive UI** — Bootstrap 5, jQuery validation, RTL-ready assets included

## 🛠 Tech Stack

| Layer      | Technology |
|------------|------------|
| Framework  | .NET 8 / ASP.NET Core Razor Pages |
| Language   | C# 12 (primary constructors, implicit usings, nullable enabled) |
| Database   | MongoDB (official `MongoDB.Driver` v3.10.0) |
| Frontend   | Razor (CSHTML), Bootstrap 5, jQuery + jQuery Validation |
| IDE        | Visual Studio 2022 (solution: `AspProMongoDb.sln`) |

## 🏗 Architecture

```
┌─────────────────────────────────────────────────────┐
│                  Razor Pages (UI)                   │
│   Pages/Users → Index · Create · Edit · Details ·  │
│                 Delete                              │
└───────────────────────┬─────────────────────────────┘
                        │ IUserServices (DI)
┌───────────────────────▼─────────────────────────────┐
│                  Service Layer                      │
│   UserServices : BaseServices<User>                 │
│   Generic CRUD: Insert / Update / Delete /          │
│                 GetById / GetAll                    │
└───────────────────────┬─────────────────────────────┘
                        │ MongoDbContext
┌───────────────────────▼─────────────────────────────┐
│                 Data Access Layer                   │
│   IMongoClient → Session → Transaction → Commit     │
└───────────────────────┬─────────────────────────────┘
                        │
                ┌───────▼────────┐
                │    MongoDB     │
                │ (Replica Set)  │
                └────────────────┘
```

## 📂 Project Structure

```
MongoDb_Project/
├── AspProMongoDb.sln                  # Visual Studio solution
└── AspProMongoDb.web/                 # Main web application
    ├── Comm/                          # Common/shared abstractions
    │   ├── BaseEntity.cs              # Base entity with GUID BsonId
    │   ├── IBaseServices.cs           # Generic CRUD contract
    │   └── BaseServices.cs            # Generic CRUD implementation (transactional)
    ├── DataBase/
    │   └── MongoDbContext.cs          # Session, database & transaction management
    ├── Entities/
    │   └── User.cs                    # User entity (FullName, UserName, Email)
    ├── Model/
    │   └── MongoSettings.cs           # Strongly-typed Mongo configuration
    ├── Services/
    │   ├── IUserServices.cs           # User service contract
    │   └── UserServices.cs            # User service implementation
    ├── Pages/
    │   ├── Users/                     # CRUD pages for users
    │   │   ├── Index.cshtml           # List all users
    │   │   ├── Create.cshtml          # Add a new user
    │   │   ├── Details.cshtml         # View a user
    │   │   ├── Edit.cshtml            # Update a user
    │   │   └── Delete.cshtml          # Remove a user
    │   ├── Shared/                    # Layout & partials
    │   ├── Index.cshtml               # Home page
    │   └── Privacy.cshtml
    ├── wwwroot/                       # Static assets (Bootstrap, jQuery, CSS, JS)
    ├── appsettings.json               # App configuration (Mongo connection)
    └── Program.cs                     # App bootstrap & DI registration
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) **running as a replica set** (required for transactions)

### MongoDB Replica Set Setup

MongoDB transactions only work on a replica set. For local development, a single-node replica set is enough:

1. Start `mongod` with a replica set name:

   ```bash
   mongod --replSet rs0 --dbpath <your-data-path>
   ```

   Or add this to your `mongod.cfg` and restart the MongoDB service:

   ```yaml
   replication:
     replSetName: rs0
   ```

2. Initialize the replica set (one time only), using `mongosh`:

   ```javascript
   rs.initiate()
   ```

### Configuration

Connection settings are in `AspProMongoDb.web/appsettings.json`:

```json
{
  "MongoSettings": {
    "ConnectionString": "mongodb://localhost:27017/?replicaSet=rs0",
    "DataBaseName": "AspPro_Mongo"
  }
}
```

Adjust the connection string / database name to match your environment. The database and the `User` collection are created automatically on first write.

### Run the Application

```bash
git clone https://github.com/MohammadHasanp/MongoDb_Project.git
cd MongoDb_Project
dotnet restore
dotnet run --project AspProMongoDb.web
```

Then open your browser at:

- HTTP → `http://localhost:5108`
- HTTPS → `https://localhost:7030`

Navigate to **`/Users`** to try the CRUD module.

## 📄 Application Pages

| Route            | Page             | Description                       |
|------------------|------------------|-----------------------------------|
| `/`              | Home             | Landing page                      |
| `/Users`         | Users → Index    | List of all users                 |
| `/Users/Create`  | Users → Create   | Add a new user                    |
| `/Users/Details` | Users → Details  | View a single user by `id`        |
| `/Users/Edit`    | Users → Edit     | Update an existing user           |
| `/Users/Delete`  | Users → Delete   | Confirm & delete a user           |

## ⚙️ How It Works

1. **DI Registration (`Program.cs`)** — `MongoSettings` is bound from configuration, `IMongoClient` is registered as a singleton, `MongoDbContext` as scoped, and `IUserServices` as transient.
2. **`MongoDbContext`** — starts a client session on construction and exposes `StartTransaction()` / `Commit()` plus access to the configured database.
3. **`BaseServices<TEntity>`** — resolves the collection by entity type name (e.g. `User` → `User` collection) and performs CRUD; every write (`Insert`, `Update`, `Delete`) is wrapped in a transaction.
4. **Razor Page Models** — receive `IUserServices` via constructor injection (C# primary constructors) and stay completely free of database details.

## 🧩 Extending the Project

Adding a new entity takes three small steps:

```csharp
// 1. Create the entity
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// 2. Create the service contract + implementation
public interface IProductServices : IBaseServices<Product> { }
public class ProductServices(MongoDbContext context)
    : BaseServices<Product>(context), IProductServices;

// 3. Register it in Program.cs
services.AddTransient<IProductServices, ProductServices>();
```

That's it — you instantly get transactional `Insert`, `Update`, `Delete`, `GetById`, and `GetAll` for the new entity.

## 👤 Author

**Mohammad Hasan Pirayandeh** — [@MohammadHasanp](https://github.com/MohammadHasanp)

---

⭐ If you find this project useful, please give it a star!
