using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_Lib.JsonReader.JsonRoots;

namespace NBA_Test
{
    public class MockData
    {
        public FranchiseHistoryRootObject FranchiseData { get; set; }
        public LeagueStandingsRootObject LeagueStandings { get; set; }
        public TeamRoosterRootObject TeamRooster { get; set; }
        public PlayerProfileRootObject PlayerProfile { get; set; }

        public MockData()
        {
            FranchiseData = MockFranchise();
            LeagueStandings = MockLeague();
            TeamRooster = MockRooster();
            PlayerProfile = MockPlayerProfile();
        }
        private FranchiseHistoryRootObject MockFranchise()
        {
            FranchiseHistoryRootObject output = new FranchiseHistoryRootObject()
            {
                ResultSets = new List<ResultSet>()
                {
                    new ResultSet()
                    {
                        RowSet = new List<List<object>>()
                    },
                    new ResultSet()
                    {
                        RowSet = new List<List<object>>()
                    }
                }
            };

            object[] Chicago = new object[6];
            object[] Utah = new object[6];
            object[] GoldenStates = new object[36];


            Chicago[1] = 111;
            Chicago[2] = "Chicago";
            Chicago[3] = "Bulls";
            Chicago[4] = 1970;
            Chicago[5] = 2018;

            Utah[1] = 222;
            Utah[2] = "Utah";
            Utah[3] = "Jazz";
            Utah[4] = 1949;
            Utah[5] = 1949;

            GoldenStates[1] = 333;
            GoldenStates[2] = "Golden States";
            GoldenStates[3] = "Warriors";
            GoldenStates[4] = 1960;
            GoldenStates[5] = 1966;


            output.ResultSets[0].RowSet.Add(Chicago.ToList());
            output.ResultSets[1].RowSet.Add(Utah.ToList());
            output.ResultSets[0].RowSet.Add(GoldenStates.ToList());

            return output;
        }
        private LeagueStandingsRootObject MockLeague()
        {
            LeagueStandingsRootObject output = new LeagueStandingsRootObject()
            {
                ResultSets = new List<ResultSet>()
                {
                    new ResultSet()
                    {
                        RowSet = new List<List<object>>()
                    }
                }
            };

            object[] Chicago = new object[36];
            object[] Utah = new object[36];
            object[] GoldenStates = new object[36];
            object[] LaClippers = new object[36];

            Chicago[2] = 111;
            Chicago[3] = "Chicago";
            Chicago[4] = "Bulls";
            Chicago[5] = "East";
            Chicago[7] = 2;
            Chicago[9] = "West";
            Chicago[12] = 9;
            Chicago[13] = 1;
            Chicago[14] = 0.9;
            Chicago[35] = 5;

            Utah[2] = 222;
            Utah[3] = "Utah";
            Utah[4] = "Jazz";
            Utah[5] = "East";
            Utah[9] = "West";
            Utah[12] = 8;
            Utah[13] = 2;
            Utah[14] = 0.8;

            GoldenStates[2] = 333;
            GoldenStates[3] = "Golden States";
            GoldenStates[4] = "Warriors";
            GoldenStates[5] = "East";
            GoldenStates[9] = "West";
            GoldenStates[12] = 7;
            GoldenStates[13] = 3;
            GoldenStates[14] = 0.7;

            LaClippers[2] = 444;
            LaClippers[3] = "LA";
            LaClippers[4] = "Clippers";
            LaClippers[5] = "East";
            LaClippers[7] = 3;
            LaClippers[9] = "West";
            LaClippers[12] = 6;
            LaClippers[13] = 4;
            LaClippers[14] = 0.6;

            output.ResultSets[0].RowSet.Add(Chicago.ToList());
            output.ResultSets[0].RowSet.Add(Utah.ToList());
            output.ResultSets[0].RowSet.Add(GoldenStates.ToList());
            output.ResultSets[0].RowSet.Add(LaClippers.ToList());

            return output;
        }
        private TeamRoosterRootObject MockRooster()
        {
            return null;
        }
        private PlayerProfileRootObject MockPlayerProfile()
        {
            return null;
        }

    }
}
