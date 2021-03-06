using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using lib;

namespace KUBike_REST_Core_5.DBUTil
{
    public class ManageUser
    {
        private const string connString = @"Server=tcp:mort-db-server.database.windows.net;
                                            Initial Catalog=mort-db;User ID=mort-admin;Password=Secret1!;
                                            Connect Timeout=30;
                                            Encrypt=True;
                                            TrustServerCertificate=False;
                                            ApplicationIntent=ReadWrite;
                                            MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Users";

        private const string GET_ONE_SQL = "select User_id from Users where User_email = @email";



        //En Hent-alle metode som skal create en list af alle users
        public IList<User> HentAlle()
        {
            IList<User> users = new List<User>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) users.Add(ReadNextUser(reader));
                }
            }

            return users;
        }

        //Hent-En metode som henter en user baseret på en email som er en string
        public int HentEn(string email)
        {
            int id = 0;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) id = ReadID(reader);
                }
            }
            return id;
        }

        //private const string LOGIN_SQL = "select user_id from Users where user_email = @email and user_password = @password";
        private const string LOGIN_SQL = "select User_id from Users where User_email = @email and User_password = @password and Account_status_id = 1 ";
        public bool Login(string email, string password)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(LOGIN_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader c = cmd.ExecuteReader();

                     return c.HasRows;
                }
            }

        }

        private const string INSERT_SQL = "insert into Users (User_firstname, User_lastname, User_email, User_password, User_mobile, Account_status_id, UserQuestionOne, UserAnswerOne, UserQuestionTwo, UserAnswerTwo, UserQuestionThree, UserAnswerThree) values (@fname, @lname, @email, @password, @mobile, @asid, @Qone, @Aone, @Qtwo, @Atwo, @Qthree, @Athree)";

        //Opret tager alle vores varibler og indsætter dem som parameter som vi kan POST.
        public bool OpretUser(User user)
        {
            var OK = true;

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@fname", user.User_firstname);
                    cmd.Parameters.AddWithValue("@lname", user.User_lastname);
                    cmd.Parameters.AddWithValue("@email", user.User_email);
                    cmd.Parameters.AddWithValue("@password", user.User_password);
                    cmd.Parameters.AddWithValue("@mobile", user.User_mobile);
                    cmd.Parameters.AddWithValue("@asid", 1);
                    cmd.Parameters.AddWithValue("@Qone", user.UserQuestionOne);
                    cmd.Parameters.AddWithValue("@Aone", user.UserAnswerOne);
                    cmd.Parameters.AddWithValue("@Qtwo", user.UserQuestionTwo);
                    cmd.Parameters.AddWithValue("@Atwo", user.UserAnswerTwo);
                    cmd.Parameters.AddWithValue("@Qthree", user.UserQuestionThree);
                    cmd.Parameters.AddWithValue("@Athree", user.UserAnswerThree);
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
        private const string Update_SQL = "UPDATE users SET User_firstname = @uName, User_lastname = @uLastname, User_email = @uEmail, User_mobile = @uMobile WHERE User_id = @uId";
        public bool UpdateUser(int id, User user)
        {
            bool OK = true;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(Update_SQL, conn))
                 {
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@uName", user.User_firstname);
                    cmd.Parameters.AddWithValue("@uLastname", user.User_lastname);
                    cmd.Parameters.AddWithValue("@uEmail", user.User_email);
                    cmd.Parameters.AddWithValue("@uMobile", user.User_mobile);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch
                    {
                        OK = false;
                    }
                }

            }
            return OK;
        }

        private const string DEACTIVATE_SQL = "update users set Account_status_id = @delete where User_id = @uid";

        public bool DeactivateUser(int id)
        {
            bool OK = true;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(DEACTIVATE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", id);
                    cmd.Parameters.AddWithValue("@delete", 2);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }


        private const string GETONETEST_SQL = "SELECT * FROM Users WHERE User_id = @uId";
        public User HentEnMedId(int id)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(GETONETEST_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@uId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = ReadNextUser(reader);
                    }
                }
            }
            return user;
        }

        private const string DELETE_SQL = "DELETE FROM Users WHERE User_id = @uId";
        public User DeleteUser(int id)
        {
            User user = HentEnMedId(id);
            if (user.User_id != -1)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(DELETE_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@uId", id);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            return user;
        }

        //Den læser parameters igennem i User
        private User ReadNextUser(SqlDataReader reader)
        {
            var user = new User();

            user.User_id = reader.GetInt32(0);
            user.User_firstname = reader.GetString(1);
            user.User_lastname = reader.GetString(2);
            user.User_email = reader.GetString(3);
            user.User_password = reader.GetString(4);
            user.User_mobile = reader.GetInt32(5);
            user.Account_status_id = reader.GetInt32(6);
            user.UserQuestionOne = reader.GetString(7);
            user.UserAnswerOne = reader.GetString(8);
            user.UserQuestionTwo = reader.GetString(9);
            user.UserAnswerTwo = reader.GetString(10);
            user.UserQuestionThree = reader.GetString(11);
            user.UserAnswerThree = reader.GetString(12);

            return user;
        }

        private int ReadID(SqlDataReader reader)
        {
            int id;
            id = reader.GetInt32(0);
            return id;
        }
    }
}