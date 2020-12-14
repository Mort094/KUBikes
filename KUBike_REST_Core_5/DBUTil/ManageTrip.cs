using lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace KUBike_REST_Core_5.DBUTil
{
    public class ManageTrip
    {
        private const string connString = @"Server=tcp:mort-db-server.database.windows.net;
                                            Initial Catalog=mort-db;User ID=mort-admin;Password=Secret1!;
                                            Connect Timeout=30;
                                            Encrypt=True;
                                            TrustServerCertificate=False;
                                            ApplicationIntent=ReadWrite;
                                            MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Trip";

        private const string GET_ALL_BIKES_BY_USER_SQL = "select cycle_id from Trip where user_id = @id and trip_end = 'Awaiting end'";

        private const string GET_ONE_SQL = "select * from Trip where trip_id = @Id";

        private const string GET_ONE_SQL_WITH_USER = "select trip_id from Trip where cycle_id = @cycle_id and user_id = @user_id and trip_end = @Tend";

        private const string GET_ALL_BIKES_FROM_ACTIVE_ROUTES = "select cycle_id from trip where trip_end = 'Awaiting End'";

        //En Hent-alle metode som skal create en list af alle Trips
        public IList<Trip> HentAlle()
        {
            IList<Trip> trips = new List<Trip>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) trips.Add(ReadNextTrip(reader));
                }
            }

            return trips;
        }

        // Hent-alle-User-Trips metode som skal create en list af alle usertrips baseret user_id
        public IList<int> HentAlleUserTrips(int id)
        {
            IList<int> trips = new List<int>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_BIKES_BY_USER_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) trips.Add(ReadBike(reader));
                }
            }
            return trips;
        }


        //Hent-Alle-Aktive-Cykler-Fra-Ruter laver en liste af aktive cykler og henter alle cykel id der ikke er i brug
        public IList<int> HentAlleAktiveCyklerFraRuter()
        {
            IList<int> cycleTrips = new List<int>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_BIKES_FROM_ACTIVE_ROUTES, conn)) 
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) cycleTrips.Add(ReadBike(reader));
                }
            }
            return cycleTrips;
        }

        //Hent-En metode som henter en trip baseret på en id som er en int
        public Trip HentEn(int id)
        {
            var trip = new Trip();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) trip = ReadNextTrip(reader);
                }
            }

            return trip;
        }

        //Hent-En-Med-Bruger henter en trip id der bruger parameter userid og cycleID
        public int HentEnMedBruger(int userid, int CycleID)
        {
            int id = 0;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ONE_SQL_WITH_USER, conn))
                {
                    cmd.Parameters.AddWithValue("@user_id", userid);
                    cmd.Parameters.AddWithValue("@cycle_id", CycleID);
                    cmd.Parameters.AddWithValue("@Tend", "Awaiting end");
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) id = ReadBike(reader);
                }
            }
            return id;
        }

        private const string OPRET_TUR_SQL = "insert into Trip (trip_start, trip_end, trip_map_json, cycle_id , user_id) values (@tstart, @tslut, @map, @cycleID, @userID)";

        //Opret tager alle vores varibler og indsætter dem som parameter som vi kan POST.
        public bool OpretTrip(Trip trip)
        {
            var OK = true;
            var co = new Coordinate();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(OPRET_TUR_SQL, conn))
                { 
                    cmd.Parameters.AddWithValue("@tstart", trip.Trip_start);
                    cmd.Parameters.AddWithValue("@tslut", trip.Trip_end);
                    cmd.Parameters.AddWithValue("@map", co.ToString());
                    cmd.Parameters.AddWithValue("@cycleID", trip.Cycle_id);
                    cmd.Parameters.AddWithValue("@userID", trip.User_id);
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

        private const string AFLUT_TUR_SQL = "update Trip set trip_end = @TEnd, trip_map_json = @map  where trip_id = @id";
        //Afslut tager tripid og time til at return en true/false statement. 
        public bool AfslutTrip (int id, string time)
        {
            var OK = true;
            var co = new Coordinate();

            using (var conn =new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(AFLUT_TUR_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@TEnd", time);
                    cmd.Parameters.AddWithValue("map", co.ToString());
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

        //Den læser parameters igennem i Trip
        private Trip ReadNextTrip(SqlDataReader reader)
        {
            var trip = new Trip();

            trip.Trip_id = reader.GetInt32(0);
            trip.Trip_start = reader.GetString(1);
            trip.Trip_end = reader.GetString(2);
            trip.Trip_map_json = reader.GetString(3);
            trip.User_id = reader.GetInt32(4);
            trip.Cycle_id = reader.GetInt32(5);

            return trip;
        }
        private int ReadBike(SqlDataReader reader)
        {
            int id;
            id = reader.GetInt32(0);
            return id;
        }
    }
}
