using System;
using System.Data;
using System.Data.SqlClient;

public class StockLog : BaseModel
{
    public int LogID { get; set; }
    public int ProductID { get; set; }
    public int ChangeQty { get; set; }
    public string Reason { get; set; }  // Nhập kho / Bán hàng
    public int? ReferenceID { get; set; }
    public int? UserID { get; set; }
    public DateTime LogDate { get; set; }

    public static DataTable GetByProduct(int productId)
    {
        string sql = "SELECT * FROM StockLogs WHERE ProductID = @id ORDER BY LogDate DESC";
        return GetDataTable(sql, new[] { new SqlParameter("@id", productId) });
    }
}