using System;

namespace lib
{
    public class User
    {
        //Instansfelter
        private string _user_firstname;
        private string _user_lastname;
        private string _user_email;
        private string _user_password;
        private int _user_mobile;
        private int _account_status_id;
        private string _user_question_one;
        private string _user_answer_one;
        private string _user_question_two;
        private string _user_answer_two;
        private string _user_question_three;
        private string _user_answer_three;
        public User()
        {
        }

        public User(int user_id, string user_firstname, string user_lastname, string user_email, string user_password, int user_mobile, int account_status_id, string questionOne, string answerOne, string questionTwo, string answerTwo, string questionThree, string answerThree)
        {
            User_id = user_id;
            User_firstname = user_firstname;
            User_lastname = user_lastname;
            User_email = user_email;
            User_password = user_password;
            User_mobile = user_mobile;
            Account_status_id = account_status_id;
            UserQuestionOne = questionOne;
            UserAnswerOne = answerOne;
            UserQuestionTwo = questionTwo;
            UserAnswerTwo = answerTwo;
            UserQuestionThree = questionThree;
            UserAnswerThree = answerThree;
        }

        //Properties defineret og der er tilføjet property test der tester om den opfylder de krav der er tilføjet
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
                if (value == 0) throw new ArgumentNullException("Why are you making me  mad");
                if (value.ToString().Length != 8) throw new ArgumentException("STUPID");

                _user_mobile = value;
            }
        }
        public int Account_status_id 
        {
            get { return _account_status_id; }
            set { _account_status_id = value; } 
        }

        public string UserQuestionOne
        {
            get { return _user_question_one; }
            set { _user_question_one = value; }
        }

        public string UserAnswerOne
        {
            get { return _user_answer_one; }
            set { _user_answer_one = value; }
        }

        public string UserQuestionTwo
        {
            get { return _user_question_two; }
            set { _user_question_two = value; }
        }

        public string UserAnswerTwo
        {
            get { return _user_answer_two; }
            set { _user_answer_two = value; }
        }

        public string UserQuestionThree
        {
            get { return _user_question_three; }
            set { _user_question_three = value; }
        }

        public string UserAnswerThree
        {
            get { return _user_answer_three; }
            set { _user_answer_three = value; }
        }

    }
}
