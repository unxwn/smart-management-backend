# 🏥 Clinic Management API
A RESTful ASP.NET Core Web API for managing appointments, patients, doctors, payments, prescriptions and more in a private clinic system.  
Built with .NET 8, Entity Framework Core, MySQL, AutoMapper, and JWT-based authentication.

---

## 🚀 Features
- 🔐 Secure JWT authentication
- 🧾 Full CRUD for medical records, visits, prescriptions, etc.
- 🌍 CORS-enabled for frontend interaction
- 📄 Swagger / OpenAPI documentation
- ☁️ Cloud-ready with Docker & Render deployment

---

## 🧰 Technologies
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- AutoMapper
- MySQL / Aiven
- BCrypt for password hashing
- JWT authentication
- Swagger / OpenAPI
- Docker (for deployment)
- Render.com (cloud hosting)

---

## ⚙️ Getting Started (Local Development)
### 1. Clone the repository
```bash
git clone https://github.com/your-username/clinic-management-backend.git
cd clinic-management-backend
```

### 2. Configure secrets (locally)
Use User Secrets to store sensitive configuration:
```bash
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "your_super_secret_key_here"
dotnet user-secrets set "JwtSettings:Issuer" "Clinic.Api"
dotnet user-secrets set "JwtSettings:Audience" "Clinic.Client"
dotnet user-secrets set "ConnectionStrings:AivenDbClinic" "your_mysql_connection_string_here"
```
Alternatively, you can define these as environment variables.

### 3. Run the project
Make sure you have the .NET 8 SDK installed. Then:
```bash
dotnet ef database update --project Clinic.Infrastructure --startup-project Clinic.Api
dotnet run --project Clinic.Api
```
API will be available locally.

---

🐳 Docker Support (Production Ready)
To build and run the Docker image:

```bash
docker build -t clinic-api .
docker run -p 5000:80 \
  -e "JwtSettings__SecretKey=your_secret" \
  -e "JwtSettings__Issuer=Clinic.Api" \
  -e "JwtSettings__Audience=Clinic.Client" \
  -e "ConnectionStrings__AivenDbClinic=your_mysql_connection_string" \
  clinic-api
```

---

📘 API Documentation
Once running, visit Swagger UI at:
```url
https://localhost:{PORT}/swagger
```
or
```url
https://your-deployed-api.com/swagger
```

---

👤 Authors
- Fullstack: [unxwn](https://github.com/unxwn)
- Frontend: [rokytskyii](https://github.com/rokytskyii)
