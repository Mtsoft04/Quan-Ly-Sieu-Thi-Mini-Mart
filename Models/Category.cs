using System.Data;

namespace MiniMartPOS.Models
{
    public class Category : BaseModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; } = true;
        public static DataTable GetAll()
        {
            string sql = @"
                SELECT CategoryID, CategoryName 
                FROM Categories 
                WHERE IsActive = 1 
                ORDER BY CategoryName";
            return GetDataTable(sql);
        }
    }
}