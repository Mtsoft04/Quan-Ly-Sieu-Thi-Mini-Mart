using System;
using System.Data;
using System.Data.SqlClient;

public class ImportReceipt : BaseModel
{
    public int ReceiptID { get; set; }
    public string ReceiptNo { get; set; }
    public int? SupplierID { get; set; }
    public int UserID { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime ImportDate { get; set; }
    public string Note { get; set; }

    // Lấy danh sách phiếu nhập
    public static DataTable GetAll()
    {
        string sql = @"SELECT ir.*, s.SupplierName, u.FullName 
                       FROM ImportReceipts ir
                       LEFT JOIN Suppliers s ON ir.SupplierID = s.SupplierID
                       LEFT JOIN Users u ON ir.UserID = u.UserID
                       ORDER BY ir.ImportDate DESC";
        return GetDataTable(sql);
    }

    // Tạo phiếu nhập mới (tự sinh mã PN + seq)
    public static int Create(int? supplierId, int userId, string note = "")
    {
        string sql = @"DECLARE @NewNo VARCHAR(20) = 'PN' + FORMAT(GETDATE(),'yyMMdd') + '-' + 
                       RIGHT('0000' + CAST(NEXT VALUE FOR seq_Receipt AS VARCHAR(5)),5);
                       
                       INSERT INTO ImportReceipts (ReceiptNo, SupplierID, UserID, TotalAmount, Note)
                       VALUES (@NewNo, @SupplierID, @UserID, 0, @Note);
                       SELECT SCOPE_IDENTITY();";

        var p = new[]
        {
            new SqlParameter("@SupplierID", supplierId ?? (object)DBNull.Value),
            new SqlParameter("@UserID", userId),
            new SqlParameter("@Note", note ?? (object)DBNull.Value)
        };

        return Convert.ToInt32(ExecuteScalar(sql, p));
    }
}