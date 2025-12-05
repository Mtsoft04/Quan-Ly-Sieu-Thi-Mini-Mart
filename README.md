# 🛒 Đồ Án Công Nghệ Phần Mềm: Hệ Thống Quản Lý Mini Mart (Mini Mart Management System)
 # 1. Giới Thiệu Dự Án
Hệ Thống Quản Lý Mini Mart là một phần mềm được thiết kế để tự động hóa và tối ưu hóa các quy trình quản lý hoạt động hàng ngày tại một siêu thị mini hoặc cửa hàng tiện lợi.
Dự án này là Đồ án Công nghệ phần mềm, được phát triển nhằm mục đích áp dụng các kiến thức về quy trình phát triển phần mềm, thiết kế kiến trúc và kỹ thuật lập trình hiện đại.
 # 🎯 Mục Tiêu Chính
•	Cung cấp một giải pháp quản lý toàn diện, từ quản lý kho hàng đến hệ thống bán hàng (POS).
•	Áp dụng mô hình kiến trúc Model-View-Controller (MVC) để tách biệt các lớp logic, đảm bảo tính dễ bảo trì và mở rộng.
________________________________________
# 2. Công Nghệ và Môi Trường Phát Triển
Lĩnh vực	Công nghệ/Ngôn ngữ	Mô tả
Ngôn ngữ Lập trình	C#	Ngôn ngữ chính được sử dụng để xây dựng ứng dụng.
Môi trường Phát triển	Visual Studio	Môi trường phát triển tích hợp (IDE) chính thức của Microsoft.
Kiến trúc Thiết kế	Model-View-Controller (MVC)	Mô hình thiết kế được sử dụng để tổ chức cấu trúc project.
Cơ sở Dữ liệu	SQL Server	Hệ quản trị cơ sở dữ liệu quan hệ (RDBMS) để lưu trữ dữ liệu.
Kết nối Dữ liệu	ADO.NET / Entity Framework (Tùy chọn)	Thư viện kết nối ứng dụng C# với SQL Server.
________________________________________
 # 3. Lý Thuyết: Mô Hình Thiết Kế MVC
MVC (Model-View-Controller) là một mô hình kiến trúc phần mềm phổ biến, được thiết kế để phân tách một ứng dụng thành ba phần có mối liên hệ với nhau. Mục đích là để phân tách các mối quan tâm (separation of concerns), giúp phát triển, kiểm thử và bảo trì code dễ dàng hơn.
 
Shutterstock
# 3.1. M (Model) - Tầng Dữ liệu và Logic Nghiệp vụ
•	Chức năng: Chịu trách nhiệm quản lý dữ liệu của ứng dụng, xử lý logic nghiệp vụ (business logic) và tương tác với cơ sở dữ liệu (SQL Server).
•	Ví dụ trong dự án: Các lớp (Class) đại diện cho Sản Phẩm, Hóa Đơn, Khách Hàng, và các hàm xử lý thêm/sửa/xóa (CRUD) dữ liệu.
#  3.2. V (View) - Tầng Giao diện Người dùng
•	Chức năng: Hiển thị dữ liệu cho người dùng và thu thập các yêu cầu từ người dùng. View không chứa bất kỳ logic nghiệp vụ nào.
•	Ví dụ trong dự án: Các form (hoặc giao diện Web) như màn hình bán hàng Máy POS, màn hình Quản lý Sản phẩm.
# 3.3. C (Controller) - Tầng Điều khiển
•	Chức năng: Đóng vai trò là trung gian, nhận yêu cầu từ View, xử lý yêu cầu đó bằng cách gọi các phương thức trong Model, và cuối cùng chọn View phù hợp để hiển thị kết quả.
•	Ví dụ trong dự án: Lớp xử lý sự kiện khi người dùng nhấn nút "Thêm Sản Phẩm" hay "Thanh Toán".
________________________________________
# 4. Các Chức Năng Chính
Dự án bao gồm các module và chức năng sau:
# ⚙️ Module Quản Lý Sản Phẩm (Product Management)
•	Thêm mới sản phẩm (Tên, mã vạch, giá nhập, giá bán, số lượng tồn kho).
•	Chỉnh sửa thông tin sản phẩm.
•	Xóa sản phẩm khỏi hệ thống.
•	Tìm kiếm và lọc sản phẩm theo tên, mã vạch, hoặc danh mục.
# 💸 Module Bán Hàng (Máy POS - Point of Sale)
•	Giao diện bán hàng trực quan, cho phép quét mã vạch hoặc tìm kiếm sản phẩm.
•	Tự động tính toán tổng tiền, chiết khấu và tiền thừa.
•	Tạo và lưu trữ hóa đơn bán hàng.
# 📊 Module Báo Cáo
•	Báo cáo doanh thu theo ngày/tháng.
•	Báo cáo tình trạng tồn kho.
________________________________________
# 5. Hướng Dẫn Cài Đặt và Chạy Dự Án
 # 5.1. Yêu Cầu Hệ Thống
#  1.Visual Studio: Phiên bản 2019 hoặc mới hơn (đã cài đặt workload .NET Desktop Development).
# 2. SQL Server: Phiên bản 2012 trở lên (hoặc SQL Server Express).
# 5.2. Thiết Lập Database
# 1.Khởi tạo Database: Tạo một database mới trong SQL Server (ví dụ: MiniMartDB).
# 2.Restore/Chạy Script: Thực hiện restore file backup database hoặc chạy file script SQL (Script_DB.sql - Nếu có) để tạo các bảng (tables) cần thiết và dữ liệu mẫu.
# 3.Cấu hình Kết nối: Mở file cấu hình (thường là App.config hoặc Web.config trong các ứng dụng C#) và cập nhật chuỗi kết nối (Connection String) để trỏ đến SQL Server của bạn.
XML
<connectionStrings>
    <add name="MiniMartDBContext" 
         connectionString="Data Source=TEN_SERVER_CUA_BAN;Initial Catalog=MiniMartDB;Integrated Security=True;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
# 5.3. Chạy Ứng Dụng
# 1. Mở dự án trong Visual Studio.
# 2.Build solution để đảm bảo không có lỗi biên dịch.
# 3.Nhấn F5 hoặc nút Start để chạy ứng dụng.

