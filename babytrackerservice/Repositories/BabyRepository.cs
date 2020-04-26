using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using babytrackerservice.Models;
using Dapper;

namespace babytrackerservice.Repositories
{
    public class BabyRepository
    {
        private string connectionString;
        public BabyRepository()
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

        public void Add(BabyName baby)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO baby_name (first_name, last_name, baby_birthday)"
                                + " VALUES(@first_name, @last_name, @baby_birthday)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, baby);
            }
        }

        public IEnumerable<BabyName> GetAll()
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<BabyName>("SELECT * FROM baby_name");
            }
        }

        public BabyName GetByID(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM baby_name"
                               + " WHERE baby_id = @Id";
                dbConnection.Open();
                return dbConnection.Query<BabyName>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM baby_name"
                             + " WHERE baby_id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(BabyName baby)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE baby_name SET"
                               + " first_name=@first_name, last_name=@last_name, baby_birthday=@baby_birthday"
                               + " WHERE baby_id = @baby_id";
                dbConnection.Open();
                dbConnection.Query(sQuery, baby);
            }
        }
    }
}
