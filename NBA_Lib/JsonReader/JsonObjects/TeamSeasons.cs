//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NBA_Lib.JsonReader.JsonObjects
//{
//    public class TeamSeasons
//    {
//        public int TeamID { get; set; }
//        public int Min_Year { get; set; }
//        public int Max_Year { get; set; }

//        public static List<string> GetSeasonsList(List<TeamSeasons> seasons)
//        {
//            int min = seasons.Min(s => s.Min_Year);
//            int max = seasons.Max(s => s.Max_Year);

//            List<string> output = new List<string>();

//            do
//            {
//                string prefix = min.ToString();
//                min++;
//                string postfix = min.ToString().Substring(2);

//                string merged = prefix + "-" + postfix;

//                output.Add(merged);
//            } while (min < max);



//            output.Reverse();
//            return output;
//        }
//    }

//}
