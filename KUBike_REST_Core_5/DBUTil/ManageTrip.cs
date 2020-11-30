﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using lib;


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

        private const string GET_ONE_SQL = "select * from Trip " +
                                           "where trip_id = @Id";

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

        private const string OPRET_TUR_SQL = "insert into Trip (trip_start, trip_end, trip_map_json, FK_cycle_id, FK_user_id) values (@tstart, @tslut, @map, @cycleID, @userID)";

        public bool OpretTrip(Trip trip)
        {
            var OK = true;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(OPRET_TUR_SQL, conn))
                {
                    { 
                            Cycle cycle = new Cycle();
                            User user = new User();

                            cmd.Parameters.AddWithValue("@tstart", DateTime.Now);
                            cmd.Parameters.AddWithValue("@tslut", null);
                            cmd.Parameters.AddWithValue("@map", "xx");
                            cmd.Parameters.AddWithValue("@cycleID", cycle.Cycle_id);
                            cmd.Parameters.AddWithValue("@userID", user.User_id);
                        try
                        {
                            var rows = cmd.ExecuteNonQuery();
                            OK = rows == 1;
                        }
                        catch (Exception ex)
                        {
                            OK = false;
                        }
                    }
                }
            }

            return OK;
        }


        private Trip ReadNextTrip(SqlDataReader reader)
        {
            var trip = new Trip();

            trip.Trip_id = reader.GetInt32(0);
            trip.Trip_start = reader.GetDateTime(1);
            trip.Trip_end = reader.GetDateTime(2);
            trip.Trip_map_json = reader.GetString(3);
            trip.User_id = reader.GetInt32(4);
            trip.Cycle_id = reader.GetInt32(5);

            return trip;
        }
    }
}