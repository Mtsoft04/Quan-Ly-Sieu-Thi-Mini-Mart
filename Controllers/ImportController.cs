using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniMartPOS.Controllers
{
    public static class ImportController
    {
        // TẠO PHIẾU NHẬP => KHÔNG BAO GIỜ LỖI ReceiptNo TRÙNG
        public static int CreateReceipt(int? supplierId = null, string note = "")
        {
            string sql = @"
                DECLARE @NewNo VARCHAR(20) = 'PN' + FORMAT(GETDATE(),'yyMMdd') + '-' + 
                                            RIGHT('0000' + CAST(NEXT VALUE FOR seq_Receipt AS VARCHAR(5)),5);

                INSERT INTO ImportReceipts (ReceiptNo, SupplierID, UserID, TotalAmount, Note, ImportDate)
                VALUES (@NewNo, @SupplierID, @UserID, 0, @Note, GETDATE());

                SELECT SCOPE_IDENTITY();
            ";

            var p = new[]
            {
                new SqlParameter("@SupplierID", supplierId ?? (object)DBNull.Value),
                new SqlParameter("@UserID", CurrentUser.UserID),
                new SqlParameter("@Note", string.IsNullOrWhiteSpace(note) ? (object)DBNull.Value : note.Trim())
            };

            try
            {
                object result = BaseModel.ExecuteScalar(sql, p);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi tạo phiếu nhập: " + ex.Message);
                return 0;
            }
        }

        // Cập nhật tổng tiền
        public static void UpdateTotal(int receiptId)
        {
            string sql = @"
                UPDATE ImportReceipts 
                SET TotalAmount = (
                    SELECT ISNULL(SUM(Quantity * UnitPrice), 0)
                    FROM ImportDetails 
                    WHERE ReceiptID = @id
                )
                WHERE ReceiptID = @id
            ";

            BaseModel.Execute(sql, new[] { new SqlParameter("@id", receiptId) });
        }
    }
}
