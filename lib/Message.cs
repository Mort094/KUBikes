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
        private int _status;

        public int messages_Id
        {
            get { return _message_id; }
            set { _message_id = value; }
        }

        public int messages_user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public int messages_cycle_id
        {
            get { return _cycle_id; }
            set { _cycle_id = value; }
        }

        public string messages_emne
        {
            get { return _topic; }
            set { _topic = value; }
        }

        public string messages_besked
        {
            get { return _body; }
            set { _body = value; }
        }

        public int messages_status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Message ()
        {

        }
        public Message (int message_id, int user_id, int cycle_id, string topic, string body, int statusVariable)
        {
            messages_Id = message_id;
            messages_user_id = user_id;
            messages_cycle_id = cycle_id;
            messages_emne = topic;
            messages_besked = body;
            messages_status = statusVariable;
        }
    }
}
