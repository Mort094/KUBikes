using System;

namespace lib
{
    public class User
    {
        private string _user_firstname;
        private string _user_lastname;
        private string _user_email;
        private string _user_password;
        private int _user_mobile;
        private int _account_status_id;
        public User()
        {
        }

        public User(int user_id, string user_firstname, string user_lastname, string user_email, string user_password, int user_mobile, int account_status_id)
        {
            User_id = user_id;
            _user_firstname = user_firstname;
            _user_lastname = user_lastname;
            _user_email = user_email;
            _user_password = user_password;
            _user_mobile = user_mobile;
            _account_status_id = account_status_id;
        }

        public int User_id
        {
            get; set;
        }

        public string User_firstname { get => _user_firstname; set 
            {
                if (value == null) throw new ArgumentNullException("Why are you making me  mad");
                if (value.Length < 2) throw new ArgumentException("STUPID");

                _user_firstname = value;
            } 
        }
        public string User_lastname { get => _user_lastname; set
            {
                if (value == null) throw new ArgumentNullException("Why are you making me  mad");
                if (value.Length < 2) throw new ArgumentException("STUPID");

                _user_lastname = value;
            }
        }
        public string User_email { get => _user_email; set
            {
                if (value == null) throw new ArgumentNullException("Why are you making me  mad");
                if (value.Length < 2) throw new ArgumentException("STUPID");

                _user_email = value;
            }
        }
        public string User_password { get => _user_password; set
            {
                if (value == null) throw new ArgumentNullException("Why are you making me  mad");
                if (value.Length < 1) throw new ArgumentException("STUPID");

                _user_password = value;
            }
        }
        public int User_mobile { get => _user_mobile; set
            {
                if (value == null) throw new ArgumentNullException("Why are you making me  mad");
                if (value <= 10) throw new ArgumentException("STUPID");

                _user_mobile = value;
            }
        }
        public int Account_status_id { get; set; }

        
    }
}
