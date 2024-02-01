using Dapper;
using Resources.Api.IntegrationTests.Repositories.Models;

namespace Resources.Api.IntegrationTests.Repositories
{
    public class TruckRepository : SqLiteBaseRepository
    {
        public static int CreateTruck(CreateTruckDto truckDto)
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            var id = cnn.Query<int>(
                @"INSERT INTO Trucks (Name, Description, Code, Status)
                    VALUES (@Name, @Description, @Code, @Status);
                    select last_insert_rowid()", truckDto).First();
            return id;
        }

        public static void ClearTruckTable()
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            cnn.Execute(
                @"
                    DELETE FROM Trucks
                ");
        }
        public static IEnumerable<string> ExistTruckTable()
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            var results = cnn.Query<string>(
                @"
                    SELECT name FROM sqlite_master WHERE type='table' AND name='Trucks' ORDER BY name;;
                ");
            return results;
        }
        public static IEnumerable<CreateTruckDto> GetTrucks()
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            var  results = cnn.Query<CreateTruckDto>(
                @"SELECT Id,Name, Description, Code, Status
                  FROM Trucks");
            return results;
        }
    }
}
