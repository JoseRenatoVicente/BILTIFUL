namespace BILTIFUL.Application
{
    public static class Configuration
    {
        static string datasource = "JOSE";
        static string database = "BILTIFUL";
        static string username = "sa";
        static string password = "iuq4nge1";
        public static string ConnectionString
        {
            get
            {
                return @$"Data Source={datasource};Initial Catalog={database};Persist Security Info=True;User ID={username};Password={password};";
            }
        }
    }
}
