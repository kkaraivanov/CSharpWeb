namespace ExamHelperLibrary.Common
{
    public static class DbContextConnectionString
    {
        //public static string GetConnectionString =>
        //    "Server=.;Database=ExamDb;Integrated Security=True;";

        // TODO: Add database name on connection string
        public static string GetConnectionString =>
            "Server=.\\SQLKARAIVANOV;Database=ATestDb;Integrated Security=true;";
    }
}