using System.Data;
using System.Data.SqlClient;

public class OrderDetail : BaseModel
{
    public int ID { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Thêm sản phẩm vào hóa đơn (tự trừ tồn kho nhờ trigger)
    public static bool Add(int orderId, int productId, int qty, decimal price)
    {
        string sql = @"INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
                       VALUES (@OrderID, @ProductID, @Qty, @Price)";

        var p = new[]
        {
            new SqlParameter("@OrderID", orderId),
            new SqlParameter("@ProductID", productId),
            new SqlParameter("@Qty", qty),
            new SqlParameter("@Price", price)
        };

        return Execute(sql, p) > 0;
    }
}