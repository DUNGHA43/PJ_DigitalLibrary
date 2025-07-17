link online: https://dunghalibrary.id.vn (đã ngưng hoạt động)
# 📚 Digital Library - Thư viện số
<div align="justify" style="text-align: justify; text-justify: inter-word;">
Digital Library là một hệ thống thư viện số hiện đại được phát triển theo mô hình tách biệt Client-Server, sử dụng Blazor WebAssembly cho giao diện người dùng và ASP.NET Core Web API cho dịch vụ phía máy chủ. Dự án cho phép người dùng dễ dàng tìm kiếm, đọc trực tuyến hoặc tải xuống các tài liệu số, đồng thời hỗ trợ thanh toán trực tuyến để mua các gói truy cập tài liệu cao cấp. Ngoài ra, hệ thống tích hợp chức năng gửi email thông báo cho người dùng. Digital Library cũng cung cấp các công cụ thống kê mạnh mẽ, giúp quản trị viên theo dõi lượt xem, lượt tải và lượng truy cập của người dùng theo thời gian thực, từ đó quản lý nội dung hiệu quả và tối ưu hóa trải nghiệm người dùng. Với nền tảng công nghệ hiện đại và khả năng mở rộng cao, Digital Library hướng tới việc trở thành một giải pháp thư viện số toàn diện, phục vụ nhu cầu tra cứu và lưu trữ tài liệu số trong thời đại số hóa.
</div>

## 🖼️ Giao diện người dùng (Client)

### 📌 Trang chủ người dùng
![Giao diện trang chủ](ImageDemo/home_user.jpeg)

### 📌 Thông tin tài liệu chi tiết
![Giao diện thông tin tài liệu](ImageDemo/detail_document.jpeg)

### 📌 Trình đọc tài liệu trực tuyến
![Giao diện đọc tài liệu](ImageDemo/read_document.jpeg)

### 📌 Hồ sơ người dùng
![Giao diện thông tin người dùng](ImageDemo/detail_user.jpeg)

### 📌 Gói đọc và trạng thái đăng ký
![Giao diện thông tin gói đọc đăng ký](ImageDemo/detail_subscriptions.jpeg)

### 📌 Bộ lọc điều hướng (navbar filter)
![Giao diện navbar filter](ImageDemo/menu_filter.png)

### 📌 Tìm kiếm nâng cao
![Giao diện tìm kiếm nâng cao](ImageDemo/filter_advanced.jpg)

### 📌 Thanh toán gói đọc
![Giao diện thanh toán gói](ImageDemo/payment.png)

### 📌 Thanh toán thành công
![Giao diện thanh toán thành công](ImageDemo/success_pyament.png)

### 📌 Thanh toán thất bại
![Giao diện thanh toán thất bại](ImageDemo/error_payment.png)

---

## 🖥️ Giao diện quản trị (Admin)

### 📊 Dashboard tổng quan
![Giao diện dashboard](ImageDemo/dashboard_admin.png)

### 💰 Thống kê doanh thu theo gói
![Giao diện thống kê doanh thu](ImageDemo/statistic_revenue.jpeg)

### 📈 Thống kê lượt đọc và tải tài liệu
![Giao diện thống kê lượt đọc và tải online](ImageDemo/statistic_read_dowload.jpeg)

### 🌐 Thống kê lượt truy cập (Traffic Log)
![Giao diện thống kê lượt truy cập](ImageDemo/statistic_trafficlog.jpeg)

### 👤 Quản lý người dùng
![Giao diện quản lý người dùng](ImageDemo/admin_users.jpeg)

### 📚 Quản lý tài liệu
![Giao diện quản lý tài liệu](ImageDemo/admin_document.jpeg)

### 🧩 Phân loại tài liệu (theo chuyên ngành, học phần)
![Giao diện phân loại tài liệu (chức năng con trong quản lý tài liệu)](ImageDemo/admin_map_document.jpeg)

### 🗂️ Quản lý thể loại tài liệu
![Giao diện quản lý thể loại](ImageDemo/admin_cate.jpeg)

### 📑 Quản lý chủ đề
![Giao diện quản lý chủ đề](ImageDemo/admin_subject.jpeg)

### ✍️ Quản lý tác giả
![Giao diện quản lý tác giả](ImageDemo/admin_author.jpeg)

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

