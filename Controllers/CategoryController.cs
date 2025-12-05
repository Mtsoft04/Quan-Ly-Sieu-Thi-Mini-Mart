using MiniMartPOS.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiniMartPOS.Controllers
{
    public static class CategoryController
    {
        public static DataTable GetAll()
        {
            string sql = "SELECT CategoryID, CategoryName, IsActive FROM Categories WHERE IsActive=1";
            var dt = BaseModel.GetDataTable(sql);
            return dt ?? new DataTable(); // Tránh null
        }

        public static bool Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string sql = "INSERT INTO Categories (CategoryName, IsActive) VALUES (@name, 1)";
            var p = new[] { new SqlParameter("@name", name.Trim()) };
            return BaseModel.Execute(sql, p) > 0;
        }

        public static bool Update(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || id <= 0)
                return false;

            string sql = "UPDATE Categories SET CategoryName = @name WHERE CategoryID = @id";
            var p = new[]
            {
                new SqlParameter("@name", name.Trim()),
                new SqlParameter("@id", id)
            };
            return BaseModel.Execute(sql, p) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = "UPDATE Categories SET IsActive = 0 WHERE CategoryID = @id";
            return BaseModel.Execute(sql, new[] { new SqlParameter("@id", id) }) > 0;
        }
    }
}