namespace ExamHelperLibrary.Common
{
    public static class DbContextConnectionString
    {
        //public static string GetConnectionString =>
        //    "Server=.;Database=BattleCards;Integrated Security=True;";
        public static string GetConnectionString =>
            "Server=.\\SQLKARAIVANOV;Database=BattleCards;Integrated Security=true;";
    }
}