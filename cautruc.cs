//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//MiniMartPOS(Solution)
//│
//└── MiniMartPOS(Project – Windows Forms App(.NET Framework 4.8))
//    │
//    ├── App.config                  ← Chỉ có mỗi chuỗi kết nối
//    ├── Program.cs                  ← Khởi động frmLogin
//    │
//    ├── Models                      ← Tất cả Model (ADO.NET thuần)
//    │   ├── BaseModel.cs            ← 3 hàm chung kết nối DB
//    │   ├── User.cs                 ← Đăng nhập + đổi pass + thêm user (pass text thường)
//    │   ├── Category.cs
//    │   ├── Supplier.cs
//    │   ├── Product.cs
//    │   ├── ImportReceipt.cs
//    │   ├── ImportDetail.cs
//    │   ├── Order.cs
//    │   ├── OrderDetail.cs
//    │   └── StockLog.cs
//    │
//    ├── Controllers                 ← Nghiệp vụ (tạm để trống hoặc dùng luôn trong Form)
//    │   ├── ProductController.cs
//    │   ├── CategoryController.cs
//    │   ├── SupplierController.cs
//    │   ├── ImportController.cs
//    │   ├── SaleController.cs
//    │   └── UserController.cs
//    │
//    ├── Views                       ← Tất cả Form (chỉ có .cs và .Designer.cs)
//    │   ├── frmLogin.cs + frmLogin.Designer.cs
//    │   ├── frmMain.cs + frmMain.Designer.cs
//    │   ├── frmPOS.cs + frmPOS.Designer.cs
//    │   ├── frmProductManager.cs + Designer.cs
//    │   ├── frmProductEdit.cs + Designer.cs
//    │   ├── frmCategoryManager.cs + Designer.cs
//    │   ├── frmSupplierManager.cs + Designer.cs
//    │   ├── frmImport.cs + Designer.cs
//    │   ├── frmImportAdd.cs + Designer.cs
//    │   ├── frmOrderHistory.cs + Designer.cs
//    │   ├── frmUserManager.cs + Designer.cs
//    │   ├── frmChangePassword.cs + Designer.cs
//    │   └── frmReportDaily.cs + Designer.cs
//    │
//    ├── Utilities                   ← Chỉ 2 file siêu nhẹ
//    │   ├── CurrentUser.cs          ← Lưu user đang login
//    │   └── Helper.cs               ← Format tiền VNĐ + MessageBox
//    |   └─- PdfGenerator.cs          ← Tạo file PDF hóa đơn bán hàng
//    │
//    └── Properties                  ← Visual Studio tự sinh (không cần đụng tới)
//        ├── AssemblyInfo.cs
//        └── Resources.resx (trống)