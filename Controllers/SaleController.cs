using System;
using System.Data;
using System.Data.SqlClient;

public static class SaleController
{
    public static bool Checkout(DataTable cart, decimal discount = 0, string paymentMethod = "cash")
    {
        if (cart == null || cart.Rows.Count == 0) return false;

        decimal total = 0;
        foreach (DataRow row in cart.Rows)
            total += Convert.ToDecimal(row["LineTotal"]);

        decimal final = total - discount;

        // 1. Tạo hóa đơn
        int orderId = Order.Create(total, discount, final, paymentMethod);
        if (orderId <= 0) return false;

        // 2. Thêm chi tiết hóa đơn
        foreach (DataRow row in cart.Rows)
        {
            int productId = (int)row["ProductID"];
            int qty = (int)row["Quantity"];
            decimal price = (decimal)row["UnitPrice"];
            OrderDetail.Add(orderId, productId, qty, price);
        }

        // 3. In PDF
        var order = new Order
        {
            OrderID = orderId,
            OrderNo = GetOrderNo(orderId),
            OrderDate = DateTime.Now
        };
        PdfGenerator.CreateInvoice(order, cart);

        Helper.ShowSuccess($"Bán hàng thành công! Hóa đơn: {order.OrderNo}");
        return true;
    }

    private static string GetOrderNo(int orderId)
    {
        string sql = "SELECT OrderNo FROM Orders WHERE OrderID = @id";
        var result = BaseModel.ExecuteScalar(sql, new[] { new SqlParameter("@id", orderId) });
        return result?.ToString() ?? "HD???";
    }
}
