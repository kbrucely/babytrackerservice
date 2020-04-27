using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using babytrackerservice.Models;
using babytrackerservice.Repositories;
using Dapper;

namespace babytrackerservice.Repositories
{
    public class ChangingRepository
    {
        private string connectionString;
        public ChangingRepository()
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

        public void Add(Changing poop)
        {
            // peel what we need off of the incoming request object
            var parameters = new { poop_at = poop.poop_at, baby_id = poop.baby.baby_id };
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                //feed in the parameters into the query directly instead of trying to reference the object. 
                string sQuery = "INSERT INTO baby_changings (baby_id, poop_at)"
                                + " VALUES(@baby_id, @poop_at)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, parameters);
            }
        }

        public IEnumerable<Changing> GetAll()
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT c.poop_id, c.poop_at, n.baby_id, n.first_name, n.last_name, n.baby_birthday"
                                + " FROM baby_changings c INNER JOIN baby_name n ON c.baby_id = n.baby_id";
                dbConnection.Open();
                return dbConnection.Query<Changing,BabyName,Changing>(
                    sQuery,
                    map: (c, b) => {
                        c.baby = b;
                        return c;
                    },
                    splitOn: "baby_id"
                    );
            }
        }

        public Changing GetByID(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT c.poop_id, c.poop_at, n.baby_id, n.first_name, n.last_name, n.baby_birthday" 
                                +" FROM baby_changings c INNER JOIN baby_name n ON c.baby_id = n.baby_id"
                                +" WHERE c.poop_id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Changing,BabyName,Changing>(
                    sQuery, 
                    map: (c, b) => {
                        c.baby = b;
                        return c;
                    },
                    splitOn: "baby_id",
                    param: new { @Id = id }
                    ).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM baby_changings"
                             + " WHERE feed_id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Changing poop)
        {
            var parameters = new { poop_id = poop.poop_id, poop_at = poop.poop_at, baby_id = poop.baby.baby_id };
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE baby_changings SET"
                               + " poop_at=@poop_at, baby_id=@baby_id"
                               + " WHERE poop_id = @poop_id";
                dbConnection.Open();
                dbConnection.Query(sQuery, parameters);
            }
        }
    }
}

