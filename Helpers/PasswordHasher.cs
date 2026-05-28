namespace HTNL.Edu.Helpers
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Hash mật khẩu bằng BCrypt với work factor 12.
        /// </summary>
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Xác minh mật khẩu so với hash đã lưu trong database.
        /// </summary>
        public static bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        /// <summary>
        /// Kiểm tra xem chuỗi đã được BCrypt hash chưa.
        /// Dùng để nhận biết mật khẩu cũ (text thô) khi migration.
        /// </summary>
        public static bool IsHashed(string password)
        {
            return password != null
                   && password.Length >= 60
                   && (password.StartsWith("$2a$") || password.StartsWith("$2b$") || password.StartsWith("$2y$"));
        }
    }
}
