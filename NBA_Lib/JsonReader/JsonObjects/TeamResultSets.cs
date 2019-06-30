using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class TeamResultSet
    {
        public string name { get; set; }
        public List<string> headers { get; set; }
        public List<List<object>> rowSet { get; set; }
    }
}
