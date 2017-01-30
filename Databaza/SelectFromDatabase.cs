using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;

namespace Glad.Databaza
{
    public class SelectFromDatabase
    {
        private string _dbConnection = "Data Source=dba.sqlite";

        public List<Zaznam> GetZaznamy(List<HracArena> zoznamHracov, string typAreny)
        {
            List<Zaznam> result = new List<Zaznam>();

            var sql = "SELECT * FROM suboje WHERE typAreny = '" + typAreny + "' AND " +
                      "protivnik IN ('" + zoznamHracov[0].MenoHraca + "', '" +
                      zoznamHracov[1].MenoHraca + "', '" + zoznamHracov[2].MenoHraca + "', '" +
                      zoznamHracov[3].MenoHraca + "', '" + zoznamHracov[4].MenoHraca + "');";

            using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
            {
                cnn.Open();
                using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                {
                    using (var reader = mycommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var zaznam = new Zaznam();
                                zaznam.Protivnik = reader["protivnik"].ToString();
                                zaznam.Premia = reader["premia"].ToString();
                                zaznam.Vitaz = reader["vitaz"].ToString();
                                zaznam.Zlato = reader["zlato"].ToString();
                                zaznam.Datum = DateTime.Parse(reader["datumVlozenia"].ToString());

                                result.Add(zaznam);
                            }
                        }
                    }
                }
            }
            
            return result;
        }
    }
}
