using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using lib;

namespace KUBike_REST_Core_5.DBUTil
{
    public class ManageCycle
    {
        private const string connString =
            @"Server=tcp:mort-db-server.database.windows.net;Initial Catalog=mort-db;User ID=mort-admin;Password=Secret1!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Cycles";

        public IList<Cycle> HentAlle()
        {
            IList<Cycle> cycles = new List<Cycle>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) cycles.Add(ReadNextCycle(reader));
                }
            }

            return cycles;
        }

        private const string GET_ONE_SQL = "select * from cycles where cycle_id = @id";
        public Cycle HentEn(int id)
        {
            Cycle cycle = new Cycle();

            using (SqlConnection conn = new SqlConnection (connString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cycle = ReadNextCycle(reader);
                    }
                   
                }

            }
            return cycle;
        }

        private const string UPDATESTATUS_SQL = "update cycles set FK_cycle_status_id = @start where cycle_id = @id";
        public bool StartRute(int id)
        {
            bool OK = true;

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(UPDATESTATUS_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@start", 1);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }

        public bool SlutRute(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(UPDATESTATUS_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@start", 2);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }


        private Cycle ReadNextCycle(SqlDataReader reader)
        {
            var c = new Cycle();

            c.Cycle_id = reader.GetInt32(0);
            c.Cycle_name = reader.GetString(1);
            c.Cycle_coordinates = reader.GetString(2);
            c.Cycle_status_id = reader.GetInt32(3);


            return c;
        }


    }
}