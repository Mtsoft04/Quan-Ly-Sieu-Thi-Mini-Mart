using System;
using System.Windows.Forms;
using MiniMartPOS.Views;  // <-- Đảm bảo namespace đúng với project của bạn

namespace MiniMartPOS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cấu hình giao diện Windows Forms chuẩn
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tự động kiểm tra và tạo database nếu chưa tồn tại (tùy chọn)
            // DatabaseInitializer.EnsureDatabaseExists(); // ← Bạn có thể thêm sau nếu muốn

            // Luôn bắt đầu bằng form Đăng nhập
            Application.Run(new frmLogin());
        }
    }
}