using MiniMartPOS.Models;
using System;
using System.Data.SqlClient;

namespace MiniMartPOS.Controllers
{
    public static class UserController
    {
        public static bool ChangePassword(string newPass)
        {
            return User.ChangePassword(CurrentUser.UserID, newPass);
        }

        public static bool AddUser(string username, string password, string fullName, string phone, int roleId)
        {
            string sql = @"INSERT INTO Users (Username, PasswordHash, FullName, Phone, RoleID, IsActive)
                           VALUES (@u, @p, @f, @ph, @r, 1)";
            var p = new[]
            {
                new SqlParameter("@u", username),
                new SqlParameter("@p", password),
                new SqlParameter("@f", fullName),
                new SqlParameter("@ph", phone ?? (object)DBNull.Value),
                new SqlParameter("@r", roleId)
            };
            return BaseModel.Execute(sql, p) > 0;
        }
    }
}