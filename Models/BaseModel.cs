using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public abstract class BaseModel
{
    protected static readonly string ConnStr =
        ConfigurationManager.ConnectionStrings["MiniMartConn"].ConnectionString;

    // ĐỔI TỪ protected → public ĐỂ CONTROLLER DÙNG ĐƯỢC!!!
    public static DataTable GetDataTable(string query, SqlParameter[] parameters = null)
    {
        var dt = new DataTable();
        using (var conn = new SqlConnection(ConnStr))
        using (var cmd = new SqlCommand(query, conn))
        {
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        return dt;
    }

    public static int Execute(string query, SqlParameter[] parameters = null)
    {
        using (var conn = new SqlConnection(ConnStr))
        {
            conn.Open();
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
    }

    public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
    {
        using (var conn = new SqlConnection(ConnStr))
        {
            conn.Open();
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
    }
}