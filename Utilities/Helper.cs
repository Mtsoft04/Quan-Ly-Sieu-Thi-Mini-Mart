using System;
using System.Globalization;
using System.Windows.Forms;

public static class Helper
{
    private static readonly CultureInfo vnCulture = new CultureInfo("vi-VN");

    // Format tiền Việt Nam đẹp: 12.500 đ
    public static string FormatMoney(decimal money)
    {
        return money.ToString("#,##0", vnCulture) + " đ";
    }

    // Thông báo thành công
    public static void ShowSuccess(string message)
    {
        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Thông báo lỗi
    public static void ShowError(string message)
    {
        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // Thông báo cảnh báo
    public static void ShowWarning(string message)
    {
        MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    // Hỏi có/không
    public static bool AskYesNo(string question)
    {
        return MessageBox.Show(question, "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    // Chỉ cho nhập số
    public static void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 = Backspace
            e.Handled = true;
    }
}