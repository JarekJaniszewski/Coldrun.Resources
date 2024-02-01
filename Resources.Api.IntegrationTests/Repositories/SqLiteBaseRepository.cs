using System.Data.SQLite;

namespace Resources.Api.IntegrationTests.Repositories
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\Coldrun.db"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}
