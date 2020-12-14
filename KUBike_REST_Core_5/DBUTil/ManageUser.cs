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

        private const string GET_ONE_SQL = "select user_id from Users where user_email = @email";



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
        private const string LOGIN_SQL = "select user_id from Users where user_email = @email and user_password = @password and FK_account_status_id = 1 ";

        //Login er lavet til at få en true/false statement som tjekker og en email stemmer overens med en email og password.
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

        private const string INSERT_SQL = "insert into Users (user_firstname, user_lastname, user_email, user_password, user_mobile, FK_account_status_id) values (@fname, @lname, @email, @password, @mobile, @asid)";

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
        private const string Update_SQL = "UPDATE users SET user_firstname = @uName, user_lastname = @uLastname, user_email = @uEmail, user_mobile = @uMobile WHERE user_id = @uId";
        //UpdaterUser updater alle parameter som er indsat og den tjekker hvilke id der er blevet id.
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

        private const string DEACTIVATE_SQL = "update users set FK_account_status_id = @delete where user_id = @uid";
        //DeactiveUser ændre AccountStatus til 2 hvilket betyder at du ikke kan logge ind, men der skal først indsat et id som skal har accountstatus til 2.
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

        //Hent-EnMedID metode som henter en user baseret på en id som er en int
        private const string GETONETEST_SQL = "SELECT * FROM Users WHERE user_id = @uId";
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

        //Delete metode henter en user baseret på en id som er en int, derefter sletter metoden useren med det pågældende id.
        private const string DELETE_SQL = "DELETE FROM Users WHERE user_id = @uId";
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