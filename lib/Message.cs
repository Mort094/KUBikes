using System;
using System.Collections.Generic;
using System.Text;

namespace lib
{
    public class Message
    {
        private int _message_id;
        private int _user_id;
        private int _cycle_id;
        private string _topic;
        private string _body;
        private string _status;

        public int Messages_id
        {
            get { return _message_id; }
            set { _message_id = value; }
        }

        public int User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public int Cycle_id
        {
            get { return _cycle_id; }
            set { _cycle_id = value; }
        }

        public string Emne
        {
            get { return _topic; }
            set { _topic = value; }
        }

        public string Besked
        {
            get { return _body; }
            set { _body = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Message ()
        {

        }
        public Message (int message_id, int user_id, int cycle_id, string topic, string body, string statuS)
        {
            Messages_id = message_id;
            User_id = user_id;
            Cycle_id = cycle_id;
            Emne = topic;
            Besked = body;
            status = statuS;
        }
    }
}
