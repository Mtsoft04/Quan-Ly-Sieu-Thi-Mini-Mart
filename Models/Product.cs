using System;
using System.Data;
using System.Data.SqlClient;

public class Product : BaseModel
{
    public int ProductID { get; set; }
    public string Barcode { get; set; }
    public string ProductName { get; set; }
    public int CategoryID { get; set; }
    public int? SupplierID { get; set; }
    public decimal ImportPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal? PromoPrice { get; set; }
    public DateTime? PromoStart { get; set; }
    public DateTime? PromoEnd { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public bool IsActive { get; set; }

    public decimal CurrentPrice
    {
        get
        {
            if (PromoPrice.HasValue && PromoStart <= DateTime.Today && PromoEnd >= DateTime.Today)
                return PromoPrice.Value;
            return SalePrice;
        }
    }

    public static DataTable Search(string keyword = "")
    {
        string sql = @"SELECT p.*, c.CategoryName 
                       FROM Products p
                       LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                       WHERE (p.Barcode LIKE @key OR p.ProductName LIKE @key) AND p.IsActive = 1
                       ORDER BY p.ProductName";
        string key = $"%{keyword}%";
        return GetDataTable(sql, new[] { new SqlParameter("@key", key) });
    }

    public static Product GetByBarcode(string barcode)
    {
        string sql = @"SELECT p.*, c.CategoryName FROM Products p
                       LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                       WHERE p.Barcode = @bc AND p.IsActive = 1";
        var dt = GetDataTable(sql, new[] { new SqlParameter("@bc", barcode) });
        if (dt.Rows.Count == 0) return null;

        var row = dt.Rows[0];
        return new Product
        {
            ProductID = (int)row["ProductID"],
            Barcode = row["Barcode"]?.ToString(),
            ProductName = row["ProductName"]?.ToString(),
            SalePrice = row["SalePrice"] is DBNull ? 0 : (decimal)row["SalePrice"],
            PromoPrice = row["PromoPrice"] as decimal?,
            PromoStart = row["PromoStart"] as DateTime?,
            PromoEnd = row["PromoEnd"] as DateTime?,
            Stock = row["Stock"] is DBNull ? 0 : (int)row["Stock"]
        };
    }
}
