using System;
using System.Collections.Generic;
using System.Text;

namespace lib
{
    public class Trip
    {
        public Trip(int trip_id, DateTime trip_start, DateTime trip_end, string trip_map_json, int cycle_id, int user_id)
        {
            Trip_id = trip_id;
            Trip_start = trip_start;
            Trip_end = trip_end;
            Trip_map_json = trip_map_json;
            Cycle_id = cycle_id;
            User_id = user_id;
        }

        public Trip()
        {

        }

        public int Trip_id { get; set; }
        public DateTime Trip_start { get; set; }
        public DateTime Trip_end { get; set; }
        public string Trip_map_json { get; set; }
        public int Cycle_id { get; set; }
        public int User_id { get; set; }


    }
}
