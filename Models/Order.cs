using System;
using System.Data;
using System.Data.SqlClient;

public class Order : BaseModel
{
    public int OrderID { get; set; }
    public string OrderNo { get; set; }
    public int UserID { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OrderDate { get; set; }

    // Tạo hóa đơn mới + trả về OrderID
    public static int Create(decimal total, decimal discount, decimal final, string payment = "cash")
    {
        string sql = @"DECLARE @NewNo VARCHAR(20) = 'HD' + FORMAT(GETDATE(),'yyMMdd') + '-' + 
                       RIGHT('0000' + CAST(NEXT VALUE FOR seq_Order AS VARCHAR(5)),5);
                       
                       INSERT INTO Orders (OrderNo, UserID, TotalAmount, Discount, FinalAmount, PaymentMethod)
                       VALUES (@NewNo, @UserID, @Total, @Discount, @Final, @Payment);
                       SELECT SCOPE_IDENTITY();";

        var p = new[]
        {
            new SqlParameter("@UserID", CurrentUser.UserID),
            new SqlParameter("@Total", total),
            new SqlParameter("@Discount", discount),
            new SqlParameter("@Final", final),
            new SqlParameter("@Payment", payment)
        };

        return Convert.ToInt32(ExecuteScalar(sql, p));
    }

    public static DataTable GetAll()
    {
        string sql = @"SELECT o.*, u.FullName FROM Orders o
                       LEFT JOIN Users u ON o.UserID = u.UserID
                       ORDER BY o.OrderDate DESC";
        return GetDataTable(sql);
    }
}