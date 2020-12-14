using lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KUBike_REST_Core_5.DBUTil
{
    public class ManageMessages
    {
        private const string connString =
            @"Server=tcp:mort-db-server.database.windows.net;Initial Catalog=mort-db;User ID=mort-admin;Password=Secret1!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const string SQL_GET_ALL_MESSAGE = "select * from messages";

        private const string SQL_GET_ALL_MESSAGE2 = "select * from messages where messages_cycle_id = @Cid";

        private const string SQL_GET_ONE_MESSAGE = "select * from messages where messages_Id = @Mid";

        private const string SQL_OPRET_MESSAGE = "insert into messages (messages_user_id, messages_cycle_id, messages_emne, messages_besked, messages_status) values (@Uid, @Cid, @messages_emne, @Body, @Status)";

        private const string SQL_STATUS_CODE = "update messages set messages_status = @status where messages_Id = @id";

        //En Hent-alle metode som skal create en list af alle Trips
        public IList<Message> HentAlle()
        {
            IList<Message> messages = new List<Message>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(SQL_GET_ALL_MESSAGE, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) messages.Add(ReadNextMessage(reader));
                }
            }

            return messages;
        }

        // Hent-alle-Med-Cykel metode som skal create en list af alle message baseret cycle_id
        public IList<Message> HentAlleMedCykel(int id)
        {
            IList<Message> messages = new List<Message>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(SQL_GET_ALL_MESSAGE2, conn))
                {
                    cmd.Parameters.AddWithValue("@Cid", id);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) messages.Add(ReadNextMessage(reader));
                }
            }

            return messages;
        }

        //Hent-En metode som henter en Message baseret på en id som er en int
        public Message HentEn(int id)
        {
            Message message = new Message();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_GET_ONE_MESSAGE, conn))
                {
                    cmd.Parameters.AddWithValue("@Mid", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        message = ReadNextMessage(reader);
                    }

                }

            }
            return message;
        }

        //Opret tager alle vores varibler og indsætter dem som parameter som vi kan POST.
        public bool OpretMessage(Message message)
        {
            var OK = true;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(SQL_OPRET_MESSAGE, conn))
                {
                    cmd.Parameters.AddWithValue("@Uid", message.messages_user_id);
                    cmd.Parameters.AddWithValue("@Cid", message.messages_cycle_id);
                    cmd.Parameters.AddWithValue("@messages_emne", message.messages_emne);
                    cmd.Parameters.AddWithValue("@Body", message.messages_besked);
                    cmd.Parameters.AddWithValue("@Status", message.messages_status);
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

        //SetStatusOne bliver sat i messages_Id til 1
        public bool SetStatusOne(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_STATUS_CODE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@status", 1);
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


        //SetStatusTwp bliver sat i messages_Id til 2
        public bool SetStatustwo(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_STATUS_CODE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@status", 2);
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

        //SetStatusthree bliver sat i messages_Id til 3
        public bool SetStatusthree(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_STATUS_CODE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@status", 3);
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
        //SetStatusStolen bliver sat i messages_Id til 4
        public bool SetStatusStolen(int id)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_STATUS_CODE, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@status", 4);
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

        //Den læser parameters igennem i Message
        private Message ReadNextMessage(SqlDataReader reader)
        {
            var message = new Message();

            message.messages_Id = reader.GetInt32(0);
            message.messages_user_id = reader.GetInt32(1);
            message.messages_cycle_id = reader.GetInt32(2);
            message.messages_emne = reader.GetString(3);
            message.messages_besked = reader.GetString(4);
            message.messages_status = reader.GetInt32(5);

            return message;
        }

    }
}
