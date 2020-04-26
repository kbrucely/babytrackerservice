using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using babytrackerservice.Models;
using Dapper;

namespace babytrackerservice.Repositories
{
    public class FeedingRepository
    {

        private string connectionString;
        public FeedingRepository()
        {
            connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }
        public System.Data.IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Feeding feed)
        {
            var parameters = new { fed_at = feed.fed_at, baby_id = feed.baby.baby_id };
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                //feed in the parameters into the query directly instead of trying to reference the object. 
                string sQuery = "INSERT INTO baby_feedings (baby_id, fed_at)"
                                + " VALUES(@baby_id, @fed_at)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, parameters);
            }
        }

        public IEnumerable<Feeding> GetAll()
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Feeding>("SELECT * FROM baby_feedings");
            }
        }

        public Feeding GetByID(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM baby_feedings"
                               + " WHERE feed_id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Feeding>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM baby_feedings"
                             + " WHERE feed_id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Feeding feed)
        {
            var parameters = new { feed_id = feed.feed_id, fed_at = feed.fed_at, baby_id = feed.baby.baby_id };
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE baby_feedings SET"
                               + " fed_at=@fed_at, baby_id=@baby_id"
                               + " WHERE feed_id = @feed_id";
                dbConnection.Open();
                dbConnection.Query(sQuery, parameters);
            }
        }
    }
}
