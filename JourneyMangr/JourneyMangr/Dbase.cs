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
            (@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source='db.mdb'");

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

        public void AddCarData(string carname,CarData car)
        {
            string sql = "INSERT INTO data ([autoid], [futottkm], [kmallas], [fogyasztas], [szerviz], [ar]) VALUES ([@Autoid], [@Futottkm], [@Kmallas], [@Fogyasztas], [@Szerviz], [@Ar])";
            using (OleDbConnection cn = new OleDbConnection
                (@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 'db.mdb'"))
            using (OleDbCommand cmd = new OleDbCommand(sql, cn))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@Autoid", GetAutoID(carname));
                cmd.Parameters.AddWithValue("@Futottkm", car.futottkm);
                cmd.Parameters.AddWithValue("@Kmallas", car.kmallas);
                cmd.Parameters.AddWithValue("@Fogyasztas", car.fogyasztas);
                cmd.Parameters.AddWithValue("@Szerviz", car.szerviz);
                cmd.Parameters.AddWithValue("@Ar", car.ar);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
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
            string sql = "SELECT nev, futottkm, kmallas, fogyasztas, szerviz, ar FROM data INNER JOIN cars ON data.autoid = cars.id WHERE autoid=" + GetAutoID(carname);
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
            string sql = "SELECT id FROM cars WHERE nev = ?";
            int i = 0;
                OleDbCommand command = con.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
            try
            {
                con.Open();
                command.Parameters.AddWithValue("carname", carname);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = Convert.ToInt32(reader["id"]);
                }
                return i;
            }
            catch (Exception e)
            {
                throw new Exception("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }

            finally
            {
                if (con!=null)
                    con.Close();
            }
            return 0;
        }
        public List<string> GetCarList()
        {
            string sql = "SELECT nev FROM cars";
            List<string> l = new List<string>();
            OleDbCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            try
            {


                con.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(reader["nev"].ToString());
                }
                return l;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return null;
        }
        public List<CarData> GetCarDataList(string carname)
        {
            List<CarData> l = new List<CarData>();
            CarData d;
            string sql = "SELECT nev, futottkm, kmallas, fogyasztas, szerviz, ar FROM data INNER JOIN cars ON data.autoid = cars.id WHERE autoid=" + GetAutoID(carname);
            OleDbCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            try
            {


                con.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    d = new CarData(reader["nev"].ToString(),Convert.ToInt32(reader["futottkm"]),Convert.ToInt32(reader["kmallas"]),Convert.ToInt32(reader["fogyasztas"]),reader["szerviz"].ToString(),Convert.ToInt32(reader["ar"]));
                    l.Add(d);
                }
                return l;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Ellenőrizd az adatbázis kapcsolat meglétét!");
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return null;
        }
        public double CalcFTperKM(string carname)
        {
            List<CarData> d = GetCarDataList(carname);
            double sum = 0;
            long km = 0;
            foreach (var i in d)
            {
                sum += i.ar;
                km += i.futottkm;
            }
            sum = sum / km;
            return sum;
        }
        public void DeleteCar(string carname)
        {

        }
    }
}
