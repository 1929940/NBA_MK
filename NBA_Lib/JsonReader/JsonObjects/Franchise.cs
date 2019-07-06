using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class Franchise
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int Min_Year { get; set; }
        public int Max_Year { get; set; }

        public static List<string> GetActiveSeasonsList(List<Franchise> franchises)
        {
            int min = franchises.Min(s => s.Min_Year);
            int max = franchises.Max(s => s.Max_Year);

            List<string> output = new List<string>();

            do
            {
                string prefix = min.ToString();
                min++;
                string postfix = min.ToString().Substring(2);

                string merged = prefix + "-" + postfix;

                output.Add(merged);
            } while (min <= max);

            output.Reverse();
            return output;
        }

        public override string ToString()
        {
            string output = String.Format(
                $"TeamID: {TeamID}\n" +
                $"TeamName: {TeamName}\n" +
                $"Min_Year: {Min_Year}\n" +
                $"Max_Year: {Max_Year}\n");

            return output;
        }
    }

}

