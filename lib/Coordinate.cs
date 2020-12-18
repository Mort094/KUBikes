using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib
{
    public class Coordinate
    {
   
        //Constructor er lavet til at klassen skal har two metoder implementeret i vores properties.
        //Vi har benyttet også af en RandomNumberBetween metode.
        public Coordinate()
        {
            Latitude = Coordinate.RandomNumberBetween(55.6289, 55.7635);
            Longitude = Coordinate.RandomNumberBetween(12.3548, 12.6172);
        }


        //Properties defineret
       public double Latitude { get; set; }
       public double Longitude { get;  set ; }

        //Random er erklaret
        private static readonly Random random = new Random();
        //Metoden RandomNumberBetween er lavet igennem en max og min værdi.
        private static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }


        //ToString metode bruges til at sende result som en string og i den ønsket format

        public override string ToString()
        {
            return $"{Latitude}°, {Longitude}°";
        }
    }
}
