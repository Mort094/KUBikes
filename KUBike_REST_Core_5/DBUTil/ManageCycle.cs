using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using lib;

namespace KUBike_REST_Core_5.DBUTil
{
    public class ManageCycle
    {
        // connection to database
        private const string connString =
            @"Server=tcp:mort-db-server.database.windows.net;Initial Catalog=mort-db;User ID=mort-admin;Password=Secret1!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //sql command to GET all bikes
        private const string GET_ALL_SQL_ADMIN = "select * from cycles";
        //sql command to GET all bikes that are not currently in route
        private const string GET_ALL_SQL = "select * from cycles where FK_cycle_status_id = 2";
        //sql command to GET one bike from id 
        private const string GET_ONE_SQL = "select * from cycles where cycle_id = @id";
        //sql command to GET one bike from id if available
        private const string GET_AVAILABLE_ONE_SQL = "select * from cycles where cycle_id = @id and FK_cycle_status_id = 2";
        //sql command to PUT change of bike status between available and in use.
        private const string UPDATESTATUS_SQL = "update cycles set FK_cycle_status_id = @start where cycle_id = @id";
        //sql command to POST new cycle
        private const string ADD_CYCLE_SQL = "insert into cycles (cycle_name, cycle_coordinates, FK_cycle_status_id) values (@Cname, @Ccoor, 2)";
        //sql command to DELETE one cycle from id
        private const string DELETE_CYCLE_SQL = "DELETE FROM cycles WHERE cycle_id = @Cid and FK_cycle_status_id = @Fid";




        //metode til at hente en liste af alle cykler i database
        //returnerer en liste af alle cykler i databasen
        public IList<Cycle> HentAlleAdmin()
        {
            IList<Cycle> cycles = new List<Cycle>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_SQL_ADMIN, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) cycles.Add(ReadNextCycle(reader));
                }
            }

            return cycles;
        }

        //metode til at hente en liste af alle cykler der lige nu er ledige (har status kode 2)
        //returnerer en liste af ledige cykler.
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

        //metode til at hente en cykel i databasen baseret på cyklens id, ved fejl overvej da om der gives en gylding int id
        //returnerer en cykel med det valgte id
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

        //metode til at hente en cykel baseret på id, hvis denne er ledig (har status kode 2)
        //returnerer en cykel med det indtastede id hvis denne er ledig.
        public Cycle HentEnLedig(int id)
        {
            Cycle cycle = new Cycle();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_AVAILABLE_ONE_SQL, conn))
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


        //metode til at ændre status på en cykel fra available (2) til unavailable (1)    
        //returnerer true hvis succesfuld og false hvis mislykkeds.
        public bool StartRute(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(UPDATESTATUS_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@start", 1);
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception)
                    {
                        OK = false;
                    }
                }
                return OK;
            }
        }


        //metode til at ændre en cykels status (hentet fra id) fra i brug (1) til tilgængelig (2)
        //returnerer true hvis succesfuld og false hvis mislykkeds.
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
                    catch (Exception)
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }


        //metode til at tilføje et cykel objekt til database med navn på cykel og coordinater på cykel. 
        //returnerer true hvis succesfuld og false hvis mislykkeds.
        public bool AddCycle(Cycle cycle)
        {
            var OK = true;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(ADD_CYCLE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Cname", cycle.Cycle_name);
                    cmd.Parameters.AddWithValue("@Ccoor", cycle.Cycle_coordinates);
                    cmd.Parameters.AddWithValue("@Fid", 2);

                    try
                    {
                        var rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception)
                    {
                        OK = false;
                    }
                }
            }

            return OK;
        }

        //metode til at slette en cykel fra id, returnerer det slettede objekt. 
        public Cycle DeleteCycle(int id)
        {
            Cycle cycle = HentEn(id);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(DELETE_CYCLE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Cid", id);
                    cmd.Parameters.AddWithValue("@Fid", 2);
                    int rows = cmd.ExecuteNonQuery();
                }
            }
            return cycle;
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