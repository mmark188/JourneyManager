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

        public DataTable GetCarData(string carname)
        {
            string sql = "SELECT nev, futottkm, kmallas FROM data INNER JOIN cars ON data.autoid = cars.id WHERE autoid=" + GetAutoID(carname);
            DataTable dt = new DataTable();
            OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 'db.mdb'");
            OleDbDataAdapter da = new OleDbDataAdapter(sql, cn);
            cn.Open();
            da.Fill(dt);
            cn.Close();
            return dt;
        }
        public int GetAutoID(string carname)
        {
            string sql = "SELECT id FROM cars WHERE nev = " + carname;
            int i;
            string connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source ='db.mdb'";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connection))
                {
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    conn.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                  i= reader.GetInt32(0);
                    conn.Close();
                  
                }
                return i;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }
            return 0;
        }
        public List<string> GetCarList()
        {
            string sql = "SELECT DISTINCT nev FROM cars";
            List<string> l = new List<string>();
            string connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source ='db.mdb'";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, connection);
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connection))
                {
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    conn.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                   l.Add(reader.GetString(0));
                    conn.Close();
                }
                return l;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }
            return null;
        }
    }
}
