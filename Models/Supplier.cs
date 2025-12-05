using System.Data;

namespace MiniMartPOS.Models
{
    public class Supplier : BaseModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public static DataTable GetAll()
        {
            string sql = @"
                SELECT SupplierID, SupplierName 
                FROM Suppliers 
                ORDER BY SupplierName";
            return GetDataTable(sql);
        }
    }
}