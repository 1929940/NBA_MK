using System.Collections.Generic;

namespace NBA_Lib.JsonReader.JsonRoots
{
    public class ResultSet
    {
        public List<string> Headers { get; set; }
        public List<List<object>> RowSet { get; set; }
    }
}
