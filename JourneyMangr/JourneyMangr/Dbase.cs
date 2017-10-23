using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace JourneyMangr
{
    public class DBase
    {

        private static DBase uniqueInstance = null;
        private DBase() { }
        public static DBase GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new DBase();
            }

            return uniqueInstance;
        }

        private static Random rnd = new Random();
        OleDbConnection con = new OleDbConnection
            (@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =
            'db.mdb'");
        
        private int GetMaxIndexFromDB()
        {
            con.Close();
            con.Open();
            string select = "SELECT MAX(id) FROM data";
            OleDbCommand cmd = new OleDbCommand(select, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("SELECT MAX(id) FROM data");
            int max = (int)cmd.ExecuteScalar();
            con.Close();
            return max;
        }
        /// <summary>
        /// Véletlenszerű szó lekérése az adatbázisból
        /// </summary>
        /// <returns></returns>
        public void AddCar(string name, int ccm, string fuel)
        {
        

            string sql = "INSERT INTO cars ([nev], [motorccm], [uzemanyag]) VALUES ([@Nev], [@Ccm], [@Fuel])";
            using (OleDbConnection cn = new OleDbConnection
                (@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 'db.mdb'"))
            using (OleDbCommand cmd = new OleDbCommand(sql, cn))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@Nev", name);
                cmd.Parameters.AddWithValue("@Ccm", ccm);
                cmd.Parameters.AddWithValue("@Fuel", fuel);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
           
        }
        public string GetRandomWord()
        {
            con.Open();

            int i = rnd.Next(GetMaxIndexFromDB());
            string select = "SELECT * FROM Words WHERE id=" + i;
            string connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =
            'words.mdb'";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connection))
                {
                    OleDbCommand command = new OleDbCommand(select, conn);
                    conn.Open();
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string word = reader.GetString(1);
                        return word;
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }
            return null;
        }
        /// <summary>
        /// Eredmény hozzáadása az adatbázishoz
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void AddScore(string name, int score)
        {

            string sql = "INSERT INTO Scores ([nev], [score]) VALUES ([@Name], [@Score])";

            using (OleDbConnection cn = new OleDbConnection
                (@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 'words.mdb'"))
            using (OleDbCommand cmd = new OleDbCommand(sql, cn))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Score", score);
                cmd.ExecuteNonQuery();
                cn.Close();
            }

        }
        /// <summary>
        /// Eredménytábla lekérése
        /// </summary>
        /// <returns></returns>
        public DataTable GetScoreboard()
        {
            string sql = "SELECT * FROM Scores";
            DataTable dt = new DataTable();
            OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 'words.mdb'");
            OleDbDataAdapter da = new OleDbDataAdapter(sql, cn);
            cn.Open();
            da.Fill(dt);
            cn.Close();
            return dt;
        }
    }
}

