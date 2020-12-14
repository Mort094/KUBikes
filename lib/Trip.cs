using System;
using System.Collections.Generic;
using System.Text;

namespace lib
{
    public class Trip
    {
        //Constructor til at man kan indsætte de ønsket værdier
        public Trip(int trip_id, string trip_start, string trip_end, string trip_map_json, int cycle_id, int user_id)
        {
            Trip_id = trip_id;
            Trip_start = trip_start;
            Trip_end = trip_end;
            Trip_map_json = trip_map_json;
            Cycle_id = cycle_id;
            User_id = user_id;
        }

        // En Constructor der lavet til at man kan kalde på klassen uden at indsæt variabler
        public Trip()
        {
           
        }


        //Properties defineret
        public int Trip_id { get; set; }
        public string Trip_start { get; set; }
        public string Trip_end { get; set; }
        public string Trip_map_json { get; set; }
        public int Cycle_id { get; set; }
        public int User_id { get; set; }


    }
}
