using MiniMartPOS.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniMartPOS.Controllers
{
    public static class SupplierController
    {
        public static DataTable GetAll()
        {
            string sql = "SELECT * FROM Suppliers ORDER BY SupplierName";
            return BaseModel.GetDataTable(sql);
        }

        public static bool Add(string name, string phone = null, string address = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string sql = "INSERT INTO Suppliers (SupplierName, Phone, Address) VALUES (@n, @p, @a)";
            var p = new[]
            {
                new SqlParameter("@n", name.Trim()),
                new SqlParameter("@p", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone.Trim()),
                new SqlParameter("@a", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address.Trim())
            };
            return BaseModel.Execute(sql, p) > 0;
        }

        public static bool Update(int id, string name, string phone = null, string address = null)
        {
            if (string.IsNullOrWhiteSpace(name) || id <= 0)
                return false;

            string sql = "UPDATE Suppliers SET SupplierName = @n, Phone = @p, Address = @a WHERE SupplierID = @id";
            var p = new[]
            {
                new SqlParameter("@n", name.Trim()),
                new SqlParameter("@p", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone.Trim()),
                new SqlParameter("@a", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address.Trim()),
                new SqlParameter("@id", id)
            };
            return BaseModel.Execute(sql, p) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = "DELETE FROM Suppliers WHERE SupplierID = @id";
            return BaseModel.Execute(sql, new[] { new SqlParameter("@id", id) }) > 0;
        }
    }
}