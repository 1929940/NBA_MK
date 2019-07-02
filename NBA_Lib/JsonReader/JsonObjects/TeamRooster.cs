using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class TeamRooster
    {
        public string PlayerName { get; set; }
        public int? Number { get; set; }

        private string position;
        public string Position
        {
            get { return position; }
            set { position = ConvertPosition(value); }
        }

        public string Height { get; set; }
        public int? Weight { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public int? PlayerID { get; set; }
        public string Role { get; set; }

        //change to private
        private string ConvertPosition(string position)
        {
            string output = "";
            var dict = new Dictionary<char, string>()
            {
                {'F' , "Forward"},
                {'G' , "Guard"},
                {'C' , "Center"},
            };

            // Im sure this can be improved
            // Sure needs to be commented
            if ((position?.Length == 1) || ((position?.Length > 1) && (position?.Length == 3) && (position?[1] == '-') ))
            {
                foreach (var c in position)
                {
                    if(c == '-')
                    {
                        output += '-';
                    }
                    else
                    {
                        foreach (var key in dict.Keys)
                        {
                            if (c == key)
                            {
                                output += dict[key];
                                break;
                            }
                        }
                    }
                }
                return output;
            }
            else return output = position;
            

        }

        public override string ToString()
        {
            string output;

            output = String.Format("Player Name: {0}\n" +
            "Number: {1}\n" +
            "Position: {2}\n" +
            "Height: {3}\n" +
            "Weight: {4}\n" +
            "BirthDate: {5}\n" +
            "Age: {6}\n" +
            "PlayerID: {7}\n" +
            "Role: {8}\n", 
            PlayerName, Number, Position, Height, Weight, 
            BirthDate, Age, PlayerID, Role);

            return output;
        }
    }
}
