using System;
using System.Collections.Generic;
using System.Text;

namespace lib
{
    public class Cycle
    {
        // En Constructor der lavet til at man kan kalde på klassen uden at indsæt variabler
        public Cycle()
        {
        }

        //Constructor til at man kan indsætte de ønsket værdier
        public Cycle(int cycle_id, string cycle_name, string cycle_coordinates, int cycle_status_id)
        {
            Cycle_id = cycle_id;
            Cycle_name = cycle_name;
            Cycle_coordinates = cycle_coordinates;
            Cycle_status_id = cycle_status_id;
        }

        //Properties defineret
        public int Cycle_id { get; set; }
        public string Cycle_name { get; set; }
        public string Cycle_coordinates { get; set; }
        public int Cycle_status_id { get; set; }
    }
}
