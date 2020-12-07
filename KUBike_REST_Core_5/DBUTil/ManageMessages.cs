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

        private const string SQL_GET_ALL_MESSAGE2 = "select * from messages where cycle_id = @Cid";

        private const string SQL_GET_ONE_MESSAGE = "select * from messages where messages_Id = @Mid";

        private const string SQL_OPRET_MESSAGE = "insert into messages (user_id, cycle_id, Emne, Besked, status) values (@Uid, @Cid, @Emne, @Body, @Status)";

        private const string SQL_STATUS_CODE = "update messages set status = @status where messages_Id = @id";

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

        public bool OpretMessage(Message message)
        {
            var OK = true;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(SQL_OPRET_MESSAGE, conn))
                {
                    cmd.Parameters.AddWithValue("@Uid", message.User_id);
                    cmd.Parameters.AddWithValue("@Cid", message.Cycle_id);
                    cmd.Parameters.AddWithValue("@Emne", message.Emne);
                    cmd.Parameters.AddWithValue("@Body", message.Besked);
                    cmd.Parameters.AddWithValue("@Status", message.status);
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


        private Message ReadNextMessage(SqlDataReader reader)
        {
            var message = new Message();

            message.Messages_id = reader.GetInt32(0);
            message.User_id = reader.GetInt32(1);
            message.Cycle_id = reader.GetInt32(2);
            message.Emne = reader.GetString(3);
            message.Besked = reader.GetString(4);
            message.status = reader.GetInt32(5);

            return message;
        }

    }
}
