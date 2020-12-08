using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib
{
    public class Coordinate
    {
   

        public Coordinate()
        {
            Latitude = Coordinate.RandomNumberBetween(55, 57);
            Longitude = Coordinate.RandomNumberBetween(12, 13);
        }



       public double Latitude { get; set; }
       public double Longitude { get;  set ; }


        private static readonly Random random = new Random();

        private static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }




        public override string ToString()
        {
            return $"{Latitude}°, {Longitude}°";
        }
    }
}
