using System.Data;
using System.Data.SqlClient;

public class ImportDetail : BaseModel
{
    public int ID { get; set; }
    public int ReceiptID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Thêm chi tiết phiếu nhập (tự cập nhật tồn kho nhờ trigger)

    public static bool Add(int receiptId, int productId, int qty, decimal price)
    {
        string sql = @"
            INSERT INTO ImportDetails (ReceiptID, ProductID, Quantity, UnitPrice)
            VALUES (@ReceiptID, @ProductID, @Qty, @Price)
        ";

        var p = new[]
        {
            new SqlParameter("@ReceiptID", receiptId),
            new SqlParameter("@ProductID", productId),
            new SqlParameter("@Qty", qty),
            new SqlParameter("@Price", price)
        };

        return Execute(sql, p) > 0;
    }
}