public static class CurrentUser
{
    public static int UserID { get; set; }
    public static string Username { get; set; }
    public static string FullName { get; set; }
    public static string RoleName { get; set; }
    public static bool IsAdmin => RoleName == "Admin";

    // Xóa khi đăng xuất
    public static void Clear()
    {
        UserID = 0;
        Username = FullName = RoleName = null;
    }
}