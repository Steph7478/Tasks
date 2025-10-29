# ✅ Tasks API

## 📋 Project Overview  
**Tasks API** is a backend application built with **.NET 9**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.  
It provides a **REST API** for task management — allowing users to create, list, update, delete, and change the status of tasks.  

The project is designed with a strong focus on **security**, **testability**, and **layered separation of concerns**, including *Domain*, *Application*, *Infrastructure*, *Presentation*, and *Security* layers.

---

## 🚧 Project Status  
This project is **complete and fully functional**, with unit tests covering all major use cases and controllers.  
It can be further extended with authentication (JWT), advanced logging, or frontend integration.

---

## ✨ Features  
- 🧩 **Full CRUD:** Create, list, update, and delete tasks.  
- ⚙️ **Status Management:** Dynamically change task status (`Pending`, `InProgress`, `Completed`, `Cancelled`).  
- 🧠 **Domain-Driven Logic:** Core business logic isolated in the Domain layer through `TaskDomainService`.  
- 🧱 **Clean Architecture:** Clear separation between Application, Domain, Infrastructure, and Presentation layers.  
- 🧪 **Unit Testing:** Application and controller logic tested with **xUnit** and **Moq**.  
- 🧰 **In-Memory Database for Tests:** Simplifies isolated testing using `TestDbContextFactory`.  
- 🔒 **Security Configurations:**  
  - CORS rules via `CorsConfig.cs`  
  - Secure headers via `HeadersConfig.cs`  
  - Role-based permissions system (`RolesAuthorize`, `PermissionConfig`)  

---

## 🛠️ Technologies  
- .NET 9  
- Entity Framework Core  
- SQLite / In-Memory Database  
- xUnit & Moq for testing  
- ASP.NET Core Web API  
- Clean Architecture + DDD pattern  

---

## ⚙️ Installation & Running  

### Prerequisites  
- .NET 9 SDK  
- SQLite (optional for production)  

### Steps  
Clone the repository:  
```bash
git clone https://github.com/your-username/Tasks.git
cd Tasks
```

Run the API in development mode:  
```bash
dotnet run --project Tasks
```

Access the API endpoints:  
- **HTTP:** [http://localhost:5269](http://localhost:5269)  
- **HTTPS:** [https://localhost:7002](https://localhost:7002)

---

## 🔗 API Endpoints  

| Method     | Endpoint                 | Description        |
| ---------- | ------------------------ | ------------------ |
| **POST**   | `/api/tasks/add`         | Create a new task  |
| **GET**    | `/api/tasks/all`         | Get all tasks      |
| **GET**    | `/api/tasks/{id}`        | Get a task by ID   |
| **PUT**    | `/api/tasks/{id}/update` | Update a task      |
| **PUT**    | `/api/tasks/{id}/status` | Update task status |
| **DELETE** | `/api/tasks/{id}`        | Delete a task      |

Example usage with `curl`:
```bash
curl -k -X GET "https://localhost:7002/api/tasks/all"      -H "Accept: application/json"
```

---

## 🧪 Testing  

The `Tasks.Tests` project includes tests for:  
- **Application Use Cases:** `AddTask`, `DeleteTask`, `UpdateStatus`, etc.  
- **Domain Services:** `TaskDomainService` logic.  
- **Presentation Layer:** Controller endpoints and mappings.  

Run all tests using:
```bash
dotnet test
```

---

## 🧱 Architecture Overview  

```
├── Tasks.sln
├── Tasks
│   ├── application       # Use cases, DTOs, and mapping logic
│   ├── domain            # Entities, enums, repositories, and domain services
│   ├── infrastructure    # Database context, repository implementations
│   ├── presentation      # Controllers, API DTOs, presentation mappers
│   ├── security          # CORS, headers, roles, and permissions
│   └── scripts           # Shell scripts for API testing
└── Tasks.Tests           # Unit and integration tests
```

---

## 🔒 Security  

The project includes built-in security configurations:
- **CORS Policies:** Controlled in `CorsConfig.cs`.  
- **Security Headers:** Applied globally via `HeadersConfig.cs`.  
- **Role-based Authorization:** Implemented with `RolesAuthorizeAttribute` and `PermissionConfig`.  

---
