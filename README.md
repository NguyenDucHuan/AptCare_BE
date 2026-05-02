# AptCare Backend (AptCare_BE)

A comprehensive apartment management system backend built with ASP.NET Core.  This project provides RESTful APIs for managing apartments, residents, services, and maintenance requests.

## 🏗️ Project Structure

The project follows a layered architecture with clear separation of concerns:

```
AptCare_BE/
├── AptCare.Api/          # API Layer - Controllers, Middleware, Filters
├── AptCare. Service/      # Business Logic Layer
├── AptCare. Repository/   # Data Access Layer
└── AptCare. UT/          # Unit Tests
```

## 🚀 Technologies Used

### Core Framework
- **. NET 8.0** - Latest . NET framework for building modern applications
- **ASP.NET Core Web API** - For building RESTful APIs

### Database & ORM
- **PostgreSQL** - Primary database
- **Entity Framework Core 8.0** - Object-relational mapping
- **Microsoft.AspNetCore.Identity** - User authentication and authorization

### Authentication & Security
- **JWT Bearer Authentication** - Token-based authentication
- **BCrypt. Net** - Password hashing
- **Google.Apis.Auth** - Google OAuth integration

### Mapping & Validation
- **AutoMapper** - Object-to-object mapping

### Cloud & Storage
- **AWS S3** - Cloud storage service
- **Cloudinary** - Image and media management

### Email & Messaging
- **MailKit** - Email sending functionality
- **RabbitMQ** - Message queue for async operations

### Real-time Communication
- **SignalR** - Real-time web functionality

### Caching
- **Redis (StackExchange.Redis)** - Distributed caching

### Payment Integration
- **PayOS** - Payment processing

### Excel Operations
- **EPPlus** - Excel file generation and manipulation

### API Documentation
- **Swashbuckle (Swagger)** - API documentation and testing

### Testing
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework for tests
- **FluentAssertions** - Assertion library
- **Coverlet** - Code coverage tool

### Additional Libraries
- **System. Linq.Dynamic.Core** - Dynamic LINQ queries
- **DotNetEnv** - Environment variable management

## 📋 Prerequisites

Before running this project, ensure you have: 

- .NET 8.0 SDK installed
- PostgreSQL database
- Redis server
- RabbitMQ server (for message queuing)
- AWS S3 account (for cloud storage)
- Cloudinary account (for media management)

## ⚙️ Configuration

1. Create a `.env` file **inside the `AptCare.Api` directory** (`AptCare.Api/.env`)
2. Configure your database connection string
3. Set up authentication keys and secrets
4. Configure cloud storage credentials (AWS S3, Cloudinary)
5. Set up email service credentials
6. Configure Redis and RabbitMQ connection strings

### Example `.env` (place this file at `AptCare.Api/.env`)

> **Important**: Do **not** commit real secrets to GitHub. The values below are **examples** only.

```env
# ASP.NET Core
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080

# Connection String
ConnectionStrings__DefaultConnection=Host=<HOST>;Port=<PORT>;Database=<DB>;Username=<USER>;Password=<PASSWORD>;SSL Mode=Require;Trust Server Certificate=true
#ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=aptcare;Username=postgres;Password=12345;SSL Mode=Disable;Trust Server Certificate=true

# JWT
Jwt__Key=<YOUR_JWT_SECRET>
Jwt__Issuer=AptCare
Jwt__Audience=AptCareUsers
Jwt__ExpirationMinutes=60

# Mail (Gmail STARTTLS/587)
MailSettings__SystemName=AptCare
MailSettings__Host=smtp.gmail.com
MailSettings__Port=587
MailSettings__Sender=<YOUR_GMAIL>
MailSettings__Password=<YOUR_GMAIL_APP_PASSWORD>

# Cloudinary
CloudinarySettings__CloudName=<CLOUD_NAME>
CloudinarySettings__ApiKey=<API_KEY>
CloudinarySettings__ApiSecret=<API_SECRET>
CloudinarySettings__UploadPreset=<UPLOAD_PRESET>

# FCM
FCMSettings__ServerKey=<FCM_SERVER_KEY>
FCMSettings__SenderId=<FCM_SENDER_ID>
FCMSettings__ProjectId=<FCM_PROJECT_ID>

# PayOS
PAYOS_BASE_URL=https://api.payos.vn
PAYOS_CLIENT_ID=<PAYOS_CLIENT_ID>
PAYOS_API_KEY=<PAYOS_API_KEY>
PAYOS_CHECKSUM_KEY=<PAYOS_CHECKSUM_KEY>
PAYOS_RETURN_URL=https://aptcare.vn/payment/return

# AWS (Development)
AWS__AccessKey=<AWS_ACCESS_KEY>
AWS__SecretKey=<AWS_SECRET_KEY>
AWS__Region=ap-southeast-1
AWS__BucketName=aptcare-storage

# Redis
Redis__Host=<REDIS_HOST>
Redis__Port=<REDIS_PORT>
Redis__User=default
Redis__Password=<REDIS_PASSWORD>

# RabbitMQ
RabbitMQ__Host=amqps://<USER>:<PASSWORD>@<HOST>/<VHOST>
RabbitMQ__Port=5672
RabbitMQ__UserName=<RABBITMQ_USERNAME>
RabbitMQ__Password=<RABBITMQ_PASSWORD>
RabbitMQ__VirtualHost=<RABBITMQ_VIRTUALHOST>
```

## 🏃 Getting Started

1. Clone the repository:
```bash
git clone https://github.com/NguyenDucHuan/AptCare_BE.git
cd AptCare_BE
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Update database: 
```bash
dotnet ef database update --project AptCare.Repository --startup-project AptCare.Api
```

4. Run the application:
```bash
dotnet run --project AptCare.Api
```

5. Access the Swagger documentation: 
```
https://localhost:5001/swagger
```

## 🧪 Running Tests

Execute unit tests using:
```bash
dotnet test
```

## 📧 Features

- User authentication and authorization
- Apartment and resident management
- Service request handling
- Maintenance tracking
- Payment processing
- Real-time notifications
- Email notifications
- File and image uploads
- Excel import/export functionality

## 📄 License

This project is currently unlicensed. 

## 👤 Author

**NguyenDucHuan**
- GitHub: [@NguyenDucHuan](https://github.com/NguyenDucHuan)

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! 
