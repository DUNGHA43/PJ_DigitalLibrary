📚 Digital Library - Thư viện số
Digital Library là một hệ thống thư viện số hiện đại, được phát triển với Blazor WebAssembly (Client) và ASP.NET Core Web API (Server).
Dự án hỗ trợ quản lý, tra cứu tài liệu, thống kê lượt xem, lượt tải và lượng truy cập người dùng.

🛠 Công nghệ sử dụng

Client: Blazor WebAssembly (.NET 8)

Server: ASP.NET Core Web API (.NET 8)

Entity Framework Core - Quản lý cơ sở dữ liệu

SQL Server - Lưu trữ dữ liệu

Authentication: JWT Bearer Token

Brevo API (SMTP) - Gửi email đến người dùng

PayOS - Thanh toán trực tuyến

Chart Library (Recharts, ChartJS) - Thống kê dữ liệu trực quan

ReadPDF.js - ReadOnly PDF

🚀 Các tính năng nổi bật

🔒 Đăng ký, đăng nhập, phân quyền người dùng (User/Admin)

📚 Đọc online, tải về tài liệu

📚 Quản lý và tìm kiếm tài liệu theo nhiều tiêu chí

📈 Thống kê lượt xem và lượt tải tài liệu, doanh thu và lượng truy cập

📤 Gửi email đến người dùng (sử dụng Brevo)

🛒 Thanh toán gói đọc trực tuyến

🔐 Xác thực bảo mật bằng Token

🎨 Giao diện tối ưu cho trải nghiệm người dùng

📂 Cấu trúc dự án
arduino
Copy
Edit
DigitalLibrary/
│
├── DigitalLibrary.Client     // Frontend Blazor WebAssembly
├── DigitalLibrary.Server     // Backend ASP.NET Core Web API
├── DigitalLibrary.Shared     // Các models DTOs chung giữa Client và Server
🧑‍💻 Hướng dẫn chạy dự án
Clone dự án về máy

bash
Copy
Edit
git clone https://github.com/DUNGHA43/PJ_DigitalLibrary.git
Mở bằng Visual Studio 2022 hoặc chạy dòng lệnh:

bash
Copy
Edit
cd DigitalLibrary
dotnet build
dotnet run --project DigitalLibrary.Server
Thiết lập database:

Cấu hình chuỗi kết nối database trong appsettings.json của Server project.

📬 Liên hệ
GitHub: github.com/dungha43

Email: dunghasonlla@outlook.com

Note: Đây là dự án cá nhân, được xây dựng nhằm mục đích học tập, nghiên cứu và triển khai hệ thống thư viện số thực tế tại Việt Nam.

🚀 Cảm ơn bạn đã ghé thăm!