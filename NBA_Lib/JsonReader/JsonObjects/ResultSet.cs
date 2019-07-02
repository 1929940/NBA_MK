using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_Lib.JsonReader.JsonObjects
{
    public class ResultSet
    {
        public List<string> Headers { get; set; }
        public List<List<object>> RowSet { get; set; }
    }
}
