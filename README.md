# AptCare - Apartment Care Management System

## 📋 Giới thiệu dự án (Project Overview)

**AptCare** là hệ thống backend quản lý chung cư thông minh, được phát triển bằng ASP.NET Core 8.0. Hệ thống cung cấp các tính năng toàn diện để quản lý các hoạt động trong tòa nhà chung cư, bao gồm quản lý cư dân, căn hộ, bảo trì, sửa chữa và các dịch vụ tiện ích khác.

**AptCare** is a smart apartment management backend system, developed using ASP.NET Core 8.0. The system provides comprehensive features for managing apartment building operations, including resident management, apartment management, maintenance, repairs, and other utility services.

---

## 🎯 Mục tiêu dự án (Project Objectives)

### Mục tiêu chính (Main Objectives)
1. **Số hóa quy trình quản lý chung cư** - Chuyển đổi các quy trình thủ công sang hệ thống điện tử
2. **Tối ưu hóa công việc bảo trì** - Quản lý hiệu quả các yêu cầu sửa chữa và bảo trì
3. **Nâng cao trải nghiệm cư dân** - Cung cấp giao diện đơn giản để cư dân gửi yêu cầu và theo dõi tiến độ
4. **Quản lý tài chính minh bạch** - Theo dõi hợp đồng, hóa đơn và giao dịch

### Mục tiêu phụ (Secondary Objectives)
- Tích hợp thông báo đẩy (Push notifications) qua Firebase Cloud Messaging
- Hỗ trợ trao đổi thời gian thực qua SignalR
- Xây dựng API RESTful theo chuẩn OpenAPI/Swagger
- Đảm bảo bảo mật với JWT Authentication

---

## ✨ Tính năng chính (Main Features)

### 👥 Quản lý người dùng (User Management)
- Quản lý nhiều vai trò: Cư dân, Lễ tân, Kỹ thuật viên, Trưởng nhóm kỹ thuật, Quản lý, Admin
- Xác thực qua email với OTP
- Quản lý token và phiên đăng nhập

### 🏢 Quản lý căn hộ (Apartment Management)
- Quản lý tầng và căn hộ
- Theo dõi trạng thái căn hộ (Available, Occupied, Maintenance)
- Liên kết cư dân với căn hộ

### 📋 Quản lý hợp đồng (Contract Management)
- Tạo và quản lý hợp đồng thuê
- Theo dõi thời hạn và điều khoản hợp đồng

### 🔧 Quản lý yêu cầu sửa chữa (Repair Request Management)
- Cư dân gửi yêu cầu sửa chữa
- Phân công kỹ thuật viên
- Theo dõi tiến độ xử lý
- Báo cáo sửa chữa chi tiết

### 📅 Quản lý lịch hẹn (Appointment Management)
- Đặt lịch hẹn với kỹ thuật viên
- Quản lý ca làm việc (Work Slots)
- Theo dõi trạng thái cuộc hẹn

### 📊 Báo cáo và kiểm tra (Reports & Inspections)
- Báo cáo kiểm tra định kỳ
- Báo cáo sự cố
- Phê duyệt báo cáo nhiều cấp

### 💰 Quản lý tài chính (Financial Management)
- Quản lý hóa đơn
- Tích hợp thanh toán PayOS
- Theo dõi giao dịch

### 💬 Trao đổi và thông báo (Communication & Notifications)
- Chat thời gian thực qua SignalR
- Thông báo đẩy qua Firebase Cloud Messaging
- Lịch sử tin nhắn và cuộc hội thoại

### 🏛️ Quản lý khu vực chung (Common Area Management)
- Quản lý các khu vực công cộng
- Theo dõi tài sản khu vực chung

---

## 🛠️ Công nghệ sử dụng (Technology Stack)

### Backend
- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: PostgreSQL
- **Authentication**: JWT Bearer Token
- **Real-time Communication**: SignalR

### Tích hợp bên thứ ba (Third-party Integrations)
- **Cloud Storage**: Cloudinary
- **Push Notifications**: Firebase Cloud Messaging (FCM)
- **Payment Gateway**: PayOS
- **Email Service**: SMTP
- **Message Queue**: RabbitMQ

### Công cụ phát triển (Development Tools)
- **API Documentation**: Swagger/OpenAPI
- **Object Mapping**: AutoMapper
- **Environment Configuration**: DotNetEnv

---

## 📁 Cấu trúc dự án (Project Structure)

```
AptCare_BE/
├── AptCare.Api/              # Web API Layer
│   ├── Controllers/          # API Controllers
│   ├── Extensions/           # Service Extensions
│   ├── Filters/              # Action Filters
│   ├── MailTemplate/         # Email Templates
│   ├── MapperProfile/        # AutoMapper Profiles
│   ├── Middleware/           # Custom Middleware
│   └── Program.cs            # Application Entry Point
├── AptCare.Service/          # Business Logic Layer
│   ├── Services/             # Service Implementations
│   │   ├── Interfaces/       # Service Interfaces
│   │   ├── Implements/       # Service Implementations
│   │   └── Background/       # Background Services
│   ├── Dtos/                 # Data Transfer Objects
│   ├── Exceptions/           # Custom Exceptions
│   └── Hub/                  # SignalR Hubs
├── AptCare.Repository/       # Data Access Layer
│   ├── Entities/             # Database Entities
│   ├── Enum/                 # Enumerations
│   ├── Repositories/         # Repository Implementations
│   ├── Migrations/           # EF Core Migrations
│   ├── Seeds/                # Database Seeding
│   └── UnitOfWork/           # Unit of Work Pattern
└── AptCare.UT/               # Unit Tests
    └── Services/             # Service Tests
```

---

## 🚀 Hướng dẫn cài đặt (Installation Guide)

### Yêu cầu hệ thống (System Requirements)
- .NET 8.0 SDK
- PostgreSQL 14+
- Redis (optional, for caching)
- RabbitMQ (for message queue)

### Các bước cài đặt (Installation Steps)

1. **Clone repository**
   ```bash
   git clone https://github.com/NguyenDucHuan/AptCare_BE.git
   cd AptCare_BE
   ```

2. **Cấu hình biến môi trường**
   
   Tạo file `.env` trong thư mục gốc với các biến sau:
   ```env
   # Database
   DB_CONNECTION_STRING=Host=localhost;Database=aptcare;Username=postgres;Password=yourpassword
   
   # JWT
   JWT_SECRET=your_jwt_secret_key
   JWT_ISSUER=AptCare
   JWT_AUDIENCE=AptCareUsers
   
   # Cloudinary
   CLOUDINARY_CLOUD_NAME=your_cloud_name
   CLOUDINARY_API_KEY=your_api_key
   CLOUDINARY_API_SECRET=your_api_secret
   
   # Firebase
   FIREBASE_PROJECT_ID=your_project_id
   
   # PayOS
   PAYOS_CLIENT_ID=your_client_id
   PAYOS_API_KEY=your_api_key
   PAYOS_CHECKSUM_KEY=your_checksum_key
   
   # SMTP
   SMTP_HOST=smtp.gmail.com
   SMTP_PORT=587
   SMTP_USERNAME=your_email
   SMTP_PASSWORD=your_app_password
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update --project AptCare.Repository --startup-project AptCare.Api
   ```

5. **Run the application**
   ```bash
   cd AptCare.Api
   dotnet run
   ```

6. **Truy cập API Documentation**
   - Swagger UI: `https://localhost:5001/swagger`
   - Health Check: `https://localhost:5001/health`

---

## 🔐 Xác thực và phân quyền (Authentication & Authorization)

### Các vai trò trong hệ thống (System Roles)

| Role | Description (Vietnamese) | Description (English) |
|------|-------------------------|----------------------|
| `Resident` | Cư dân | Apartment resident |
| `Receptionist` | Lễ tân | Building receptionist |
| `Technician` | Kỹ thuật viên | Maintenance technician |
| `TechnicianLead` | Trưởng nhóm kỹ thuật | Technical team leader |
| `Manager` | Quản lý | Building manager |
| `Admin` | Quản trị viên | System administrator |

### API Authentication
Tất cả các API yêu cầu xác thực sử dụng JWT Bearer Token:
```http
Authorization: Bearer <your_jwt_token>
```

---

## 📝 API Endpoints

### Authentication
- `POST /api/auth/login` - Đăng nhập
- `POST /api/auth/register` - Đăng ký
- `POST /api/auth/refresh-token` - Làm mới token
- `POST /api/auth/forgot-password` - Quên mật khẩu

### Users & Accounts
- `GET /api/account-manage` - Lấy danh sách người dùng
- `POST /api/account-manage` - Tạo người dùng mới
- `PUT /api/account-manage/{id}` - Cập nhật người dùng

### Apartments
- `GET /api/apartment` - Lấy danh sách căn hộ
- `POST /api/apartment` - Tạo căn hộ mới
- `PUT /api/apartment/{id}` - Cập nhật căn hộ

### Repair Requests
- `GET /api/repair-request` - Lấy danh sách yêu cầu sửa chữa
- `POST /api/repair-request` - Tạo yêu cầu mới
- `PUT /api/repair-request/{id}` - Cập nhật yêu cầu

### Appointments
- `GET /api/appointment` - Lấy danh sách lịch hẹn
- `POST /api/appointment` - Tạo lịch hẹn mới

*Xem đầy đủ API documentation tại `/swagger`*

---

## 🧪 Testing

Chạy unit tests:
```bash
dotnet test
```

---

## 📄 License

This project is developed for educational purposes.

---

## 👥 Đội ngũ phát triển (Development Team)

- **Nguyễn Đức Huân** - Backend Developer

---

## 📞 Liên hệ (Contact)

- **GitHub**: [NguyenDucHuan](https://github.com/NguyenDucHuan)
- **Website**: [aptcare.click](https://aptcare.click)
