using System.Data;
using System.Data.SqlClient;

public static class ProductController
{
    // Trả về tất cả sản phẩm
    public static DataTable GetAll()  // sử dụng cho quản lý sản phẩm
    {
        string sql = "SELECT ProductID, Barcode, ProductName, ImportPrice, SalePrice, PromoPrice, PromoStart, PromoEnd, MinStock, CategoryID, SupplierID, Unit,Stock,CurrentPrice FROM Products WHERE IsActive = 1";
        return BaseModel.GetDataTable(sql) ?? new DataTable();
    }

    // Tìm kiếm nâng cao theo keyword, category, supplier
    public static DataTable Search(string keyword = "", int categoryId = 0, int supplierId = 0)
    {
        DataTable dt = GetAll();

        if (!string.IsNullOrWhiteSpace(keyword) && dt.Rows.Count > 0)
            dt = dt.Select($"ProductName LIKE '%{keyword.Replace("'", "''")}%'").CopyToDataTable();

        if (categoryId > 0 && dt.Columns.Contains("CategoryID"))
            dt = dt.Select($"CategoryID = {categoryId}").CopyToDataTable();

        if (supplierId > 0 && dt.Columns.Contains("SupplierID"))
            dt = dt.Select($"SupplierID = {supplierId}").CopyToDataTable();

        return dt;
    }

    // Lấy sản phẩm theo barcode (giữ nguyên form cũ)
    public static Product GetByBarcode(string barcode)
    {
        return Product.GetByBarcode(barcode);
    }

    // Cập nhật tồn kho
    public static bool UpdateStock(int productId, int newStock)
    {
        string sql = "UPDATE Products SET Stock = @stock WHERE ProductID = @id";
        return BaseModel.Execute(sql, new[]
        {
            new SqlParameter("@stock", newStock),
            new SqlParameter("@id", productId)
        }) > 0;
    }
}
