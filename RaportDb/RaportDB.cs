using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace SIPRK2013SDFIX.RaportDb
{
    public class RaportDB
    {
        string datasource = "Data Source = newDBRap.db";
        public int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int rowNumbers;

            using (var con = new SQLiteConnection(datasource))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query,con))
                {
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    rowNumbers = cmd.ExecuteNonQuery();
                }
                return rowNumbers;
            }            
        }

        public DataTable GetDataRaport(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            using (var con = new SQLiteConnection(datasource))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }
    }
}
