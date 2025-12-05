using System.Data;
using System.Data.SqlClient;

namespace MiniMartPOS.Models
{
    public class User : BaseModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public static bool Login(string username, string password, out User user)
        {
            user = null;
            string sql = @"
                SELECT u.UserID, u.Username, u.PasswordHash AS Password, u.FullName, u.Phone,
                       u.RoleID, r.RoleName, u.IsActive
                FROM Users u
                INNER JOIN Roles r ON u.RoleID = r.RoleID
                WHERE u.Username = @user AND u.IsActive = 1";

            var dt = GetDataTable(sql, new[] { new SqlParameter("@user", username) });
            if (dt.Rows.Count == 0) return false;

            var row = dt.Rows[0];
            if (row["Password"].ToString() != password) return false;

            user = new User
            {
                UserID = (int)row["UserID"],
                Username = row["Username"].ToString(),
                FullName = row["FullName"].ToString(),
                Phone = row["Phone"]?.ToString(),
                RoleID = (int)row["RoleID"],
                RoleName = row["RoleName"].ToString(),
                IsActive = true
            };

            CurrentUser.UserID = user.UserID;
            CurrentUser.Username = user.Username;
            CurrentUser.FullName = user.FullName;
            CurrentUser.RoleName = user.RoleName;

            return true;
        }

        public static bool ChangePassword(int userId, string newPass)
        {
            string sql = "UPDATE Users SET PasswordHash = @pass WHERE UserID = @id";
            return Execute(sql, new[]
            {
                new SqlParameter("@pass", newPass),
                new SqlParameter("@id", userId)
            }) > 0;
        }
    }
}