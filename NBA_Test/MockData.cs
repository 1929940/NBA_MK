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
            object[] GoldenStates = new object[6];
            object[] La = new object[6];



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

            La[1] = 444;
            La[2] = "LA";
            La[3] = "Clippers";
            La[4] = 1949;
            La[5] = 2018;

            output.ResultSets[0].RowSet.Add(Chicago.ToList());
            output.ResultSets[1].RowSet.Add(Utah.ToList());
            output.ResultSets[0].RowSet.Add(GoldenStates.ToList());
            output.ResultSets[0].RowSet.Add(La.ToList());

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
            PlayerProfileRootObject output = new PlayerProfileRootObject()
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

            object[] Chicago2000 = new object[27];
            object[] Chicago2001 = new object[27];
            object[] La2001 = new object[27];
            object[] Total2001 = new object[27];
            object[] La2002 = new object[27];
            object[] CareerTotal = new object[27];

            Chicago2000[1] = "2000-01";
            Chicago2000[3] = 111;

            Chicago2001[1] = "2001-02";
            Chicago2001[3] = 111;

            #region La 2001
            La2001[1] = "2001-02";
            La2001[3] = 444;
                 
            La2001[6] = 1;
            La2001[7] = 1;
            La2001[8] = 1;
                     
            La2001[9] = 1;
            La2001[10] = 1;
            La2001[11] = 1.0;
                     
            La2001[12] = 1;
            La2001[13] = 1;
            La2001[14] = 1.0;
                     
            La2001[15] = 1;
            La2001[16] = 1;
            La2001[17] = 1.0;
                 
            La2001[18] = 1;
            La2001[19] = 1;
            La2001[20] = 1;
                 
            La2001[26] = 1;
            La2001[21] = 1;
            La2001[22] = 1;
            La2001[23] = 1;
            La2001[24] = 1;
            La2001[25] = 1;
            #endregion

            #region Total 2001
            Total2001[1] = "2001-02";
            Total2001[3] = 0;

            Total2001[6] = 1;
            Total2001[7] = 1;
            Total2001[8] = 1;

            Total2001[9] = 1;
            Total2001[10] = 1;
            Total2001[11] = 1.0;
                          
            Total2001[12] = 1;
            Total2001[13] = 1;
            Total2001[14] = 1.0;
                          
            Total2001[15] = 1;
            Total2001[16] = 1;
            Total2001[17] = 1.0;
                          
            Total2001[18] = 1;
            Total2001[19] = 1;
            Total2001[20] = 1;
                          
            Total2001[26] = 1;
            Total2001[21] = 1;
            Total2001[22] = 1;
            Total2001[23] = 1;
            Total2001[24] = 1;
            Total2001[25] = 1;
            #endregion

            #region La 2002
            La2002[1] = "2002-03";
            La2002[3] = 444;

            La2002[6] = 2;
            La2002[7] = 2;
            La2002[8] = 2;

            La2002[9] = 2;
            La2002[10] = 2;
            La2002[11] = 1.0;

            La2002[12] = 2;
            La2002[13] = 2;
            La2002[14] = 1.0;

            La2002[15] = 2;
            La2002[16] = 2;
            La2002[17] = 1.0;
                         
            La2002[18] = 2;
            La2002[19] = 2;
            La2002[20] = 2;
                         
            La2002[26] = 2;
            La2002[21] = 2;
            La2002[22] = 2;
            La2002[23] = 2;
            La2002[24] = 2;
            La2002[25] = 2;
            #endregion

            #region Career Total

            CareerTotal[3] = 3;
            CareerTotal[4] = 3;
            CareerTotal[5] = 3;

            CareerTotal[6] = 3;
            CareerTotal[7] = 3;
            CareerTotal[8] = 1.0;

            CareerTotal[9] = 3;
            CareerTotal[10] = 3;
            CareerTotal[11] = 1.0;

            CareerTotal[12] = 3;
            CareerTotal[13] = 3;
            CareerTotal[14] = 1.0;

            CareerTotal[15] = 3;
            CareerTotal[16] = 3;
            CareerTotal[17] = 3;

            CareerTotal[23] = 3;
            CareerTotal[18] = 3;
            CareerTotal[19] = 3;
            CareerTotal[20] = 3;
            CareerTotal[21] = 3;
            CareerTotal[22] = 3;
            #endregion

            output.ResultSets[0].RowSet.Add(Chicago2000.ToList());
            output.ResultSets[0].RowSet.Add(Chicago2001.ToList());
            output.ResultSets[0].RowSet.Add(La2001.ToList());
            output.ResultSets[0].RowSet.Add(Total2001.ToList());
            output.ResultSets[0].RowSet.Add(La2002.ToList());

            output.ResultSets[1].RowSet.Add(CareerTotal.ToList());

            return output;
        }

    }
}
