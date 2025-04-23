# 📚 Digital Library - Thư viện số
<div align="justify" style="text-align: justify; text-justify: inter-word;">
Digital Library là một hệ thống thư viện số hiện đại được phát triển theo mô hình tách biệt Client-Server, sử dụng Blazor WebAssembly cho giao diện người dùng và ASP.NET Core Web API cho dịch vụ phía máy chủ. Dự án cho phép người dùng dễ dàng tìm kiếm, đọc trực tuyến hoặc tải xuống các tài liệu số, đồng thời hỗ trợ thanh toán trực tuyến để mua các gói truy cập tài liệu cao cấp. Ngoài ra, hệ thống tích hợp chức năng gửi email thông báo cho người dùng. Digital Library cũng cung cấp các công cụ thống kê mạnh mẽ, giúp quản trị viên theo dõi lượt xem, lượt tải và lượng truy cập của người dùng theo thời gian thực, từ đó quản lý nội dung hiệu quả và tối ưu hóa trải nghiệm người dùng. Với nền tảng công nghệ hiện đại và khả năng mở rộng cao, Digital Library hướng tới việc trở thành một giải pháp thư viện số toàn diện, phục vụ nhu cầu tra cứu và lưu trữ tài liệu số trong thời đại số hóa.
</div>

## 🛠 Công nghệ sử dụng

### **Client Side**
<p align="left">
  <img src="https://img.shields.io/badge/Blazor-WebAssembly-blue?logo=blazor" alt="Blazor WASM">
  <img src="https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet" alt=".NET 8">
</p>

### **Server Side**
<p align="left">
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-blueviolet?logo=.net" alt="ASP.NET Core">
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?logo=microsoft-sql-server" alt="SQL Server">
  <img src="https://img.shields.io/badge/EF_Core-ORM-blue?logo=.net" alt="EF Core">
</p>

### **Tích hợp**
<p align="left">
  <img src="https://img.shields.io/badge/JWT-Auth-black?logo=json-web-tokens" alt="JWT Auth">
  <img src="https://img.shields.io/badge/Brevo-Email-orange?logo=mailgun" alt="Brevo Email">
  <img src="https://img.shields.io/badge/PayOS-Payment-green" alt="PayOS">
  <img src="https://img.shields.io/badge/ChartJS-Visualization-FF6384?logo=chart.js" alt="ChartJS">
</p>

## 🚀 Tính năng chính

<div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(300px, 1fr)); gap: 1rem;">

### 🔐 Xác thực
- Đăng ký/đăng nhập người dùng
- Phân quyền (User/Admin)
- Bảo mật JWT Token
- Gửi email xác nhận

### 📚 Tài liệu
- Đọc online (ReadOnly PDF)
- Tải xuống tài liệu
- Tìm kiếm đa tiêu chí
- Quản lý tài liệu (CRUD)

### 📊 Thống kê
- Lượt xem/tải tài liệu
- Doanh thu hệ thống
- Lưu lượng truy cập

### 💳 Thanh toán
- Mua gói đọc trực tuyến
- Tích hợp PayOS

</div>

## 📂 Cấu trúc dự án

```bash
DigitalLibrary/
│
├── DigitalLibrary.Client     # Frontend Blazor WebAssembly
├── DigitalLibrary.Server     # Backend ASP.NET Core Web API
└── DigitalLibrary.Shared     # Shared Models/DTOs
```
🛠 Cài đặt
Clone dự án

```bash
git clone https://github.com/DUNGHA43/PJ_DigitalLibrary.git
Thiết lập database:
Cấu hình chuỗi kết nối database trong appsettings.json của Server project.
Run:
Cấu hình chạy đồng thời lần lượt Server side -> Client side.
```

📬 Liên hệ
<div align="left">
Phương thức	Liên kết
<br>
GitHub:	github.com/dungha43
<br>
Email:	dunghasonlla@outlook.com
</div>

Lưu ý: Đây là dự án cá nhân phục vụ mục đích học tập và nghiên cứu, có thể áp dụng cho hệ thống thư viện số tại Việt Nam.

<div align="center">
License
</div>

