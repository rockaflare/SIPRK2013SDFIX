using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;
using System.Data;

namespace SIPRK2013SDFIX.RaportDb
{
    public class EkskulCRUD
    {
        public bool Tambah(NilaiEkskul eks)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO nilai_ekskul VALUES(@ideks, @nisn, @ekskul1, @ekskul2, @ekskul3, @nilai1, @nilai2, @nilai3, @semester)";
            var args = new Dictionary<string, object>
            {
                {"@ideks", eks.IdEks },
                {"@nisn", eks.Nisn },
                {"@ekskul1", eks.Eskul1 },
                {"@ekskul2", eks.Eskul2 },
                {"@ekskul3", eks.Eskul3 },
                {"@nilai1", eks.Nilai1 },
                {"@nilai2", eks.Nilai2 },
                {"@nilai3", eks.Nilai3 },
                {"@semester", eks.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Ubah(NilaiEkskul eks)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE nilai_ekskul SET eskul1 = @ekskul1, eskul2 = @ekskul2, eskul3 = @ekskul3, nilai1 = @nilai1, nilai2 = @nilai2, nilai3 = @nilai3, semester = @semester WHERE id_eks = @ideks";
            var args = new Dictionary<string, object>
            {
                {"@ideks", eks.IdEks },
                {"@ekskul1", eks.Eskul1 },
                {"@ekskul2", eks.Eskul2 },
                {"@ekskul3", eks.Eskul3 },
                {"@nilai1", eks.Nilai1 },
                {"@nilai2", eks.Nilai2 },
                {"@nilai3", eks.Nilai3 },
                {"@semester", eks.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(NilaiEkskul eks)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_ekskul WHERE id_eks = @ideks";
            var args = new Dictionary<string, object>
            {
                {"@ideks", eks.IdEks }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool HapusAll(NilaiEkskul eks)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_ekskul WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", eks.Nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public NilaiEkskul GetNilaiEkskul(string ideks)
        {
            NilaiEkskul nilaiEkskul;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM nilai_ekskul WHERE id_eks = '{ideks}'";
            DataTable dt = dB.GetDataRaport(query);
            nilaiEkskul = new NilaiEkskul
            {
                IdEks = ideks,
                Nisn = dt.Rows[0][1].ToString(),
                Eskul1 = dt.Rows[0][2].ToString(),
                Eskul2 = dt.Rows[0][3].ToString(),
                Eskul3 = dt.Rows[0][4].ToString(),
                Nilai1 = dt.Rows[0][5].ToString(),
                Nilai2 = dt.Rows[0][6].ToString(),
                Nilai3 = dt.Rows[0][7].ToString(),
                Semester = dt.Rows[0][8].ToString()
            };
            return nilaiEkskul;
        }
    }
}
