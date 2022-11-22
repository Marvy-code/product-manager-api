namespace ProductManager.Infrastructure
{
    public static class DbConfig
    {
        public static string PostGresServer { get; } = Environment.GetEnvironmentVariable("POSTGRES_SERVER");
        public static string PostGresDb { get; } = Environment.GetEnvironmentVariable("POSTGRES_DB");
        public static string PostGresUser { get; } = Environment.GetEnvironmentVariable("POSTGRES_USER");
        public static string PostGresPassword { get; } = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
    }
}