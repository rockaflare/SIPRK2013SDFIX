using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;
using System.Data;

namespace SIPRK2013SDFIX.RaportDb
{
    public class AbsensiCRUD
    {
        public bool Tambah(Absensi abs)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO absensi VALUES(@idabsen, @nisn, @sakit, @ijin, @alpha, @semester)";
            var args = new Dictionary<string, object>
            {
                {"@idabsen", abs.IdAbsen },
                {"@nisn", abs.Nisn },
                {"@sakit", abs.Sakit },
                {"@ijin", abs.Ijin },
                {"@alpha", abs.Alpha },
                {"@semester", abs.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Ubah(Absensi abs)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE absensi SET sakit = @sakit, ijin = @ijin, alpha = @alpha, semester = @semester WHERE id_absen = @idabsen";
            var args = new Dictionary<string, object>
            {
                {"@idabsen", abs.IdAbsen },
                {"@sakit", abs.Sakit },
                {"@ijin", abs.Ijin },
                {"@alpha", abs.Alpha },
                {"@semester", abs.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(Absensi abs)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM absensi WHERE id_absen = @idabsen";
            var args = new Dictionary<string, object>
            {
                {"@idabsen", abs.IdAbsen }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool HapusAll(Absensi abs)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM absensi WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", abs.Nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public Absensi GetAbsensi(string idabs)
        {
            Absensi absensi;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM absensi WHERE id_absen = '{idabs}'";
            DataTable dt = dB.GetDataRaport(query);
            absensi = new Absensi
            {
                IdAbsen = idabs,
                Nisn = dt.Rows[0][1].ToString(),
                Sakit = Convert.ToInt32(dt.Rows[0][2]),
                Ijin = Convert.ToInt32(dt.Rows[0][3]),
                Alpha = Convert.ToInt32(dt.Rows[0][4]),
                Semester = dt.Rows[0][5].ToString()
            };
            return absensi;
        }
    }
}
