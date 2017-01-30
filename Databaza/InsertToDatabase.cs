using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Glad.Databaza
{
    public class InsertToDatabase
    {
        private string _dbConnection = "Data Source=dba.sqlite";

        public bool VlozVysledokSuboja(string typAreny, string hrac, string protivnik, string server, string zlato, string premia, string zasahy, string vitaz)
        {
            var sql = "INSERT INTO suboje (datumVlozenia, typAreny, hrac, protivnik ,vitaz, server, zlato, premia, zasahy) values (datetime('NOW'), '" + typAreny + "', '" + hrac + "', '" + protivnik + "', '" + vitaz + "', '" + server + "', '" + zlato + "', '" + premia + "', '" + zasahy + "');";
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        mycommand.ExecuteReader();
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("no such table: suboje"))
                {
                    using (TransactionScope tran = new TransactionScope())
                    {
                        using (SQLiteConnection DbConnection = new SQLiteConnection(_dbConnection))
                        {
                            DbConnection.Open();

                            sql =
                                "CREATE TABLE suboje ([ID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,[typAreny] VARCHAR(1)  NULL, [hrac] VARCHAR(50)  NULL," +
                                "[protivnik] VARCHAR(50)  NULL,[vitaz] VARCHAR(50)  NULL,[server] VARCHAR(2)  NULL,[zlato] VARCHAR(8)  NULL, [premia] VARCHAR(8)  NULL, [zasahy] VARCHAR(15)  NULL, [datumVlozenia] DATETIME  NULL);";

                            using (SQLiteCommand command = new SQLiteCommand(sql, DbConnection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        tran.Complete();
                    }
                }

                return false;
            }

            return true;
        }
    }
}
