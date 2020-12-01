using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;
using System.Data;

namespace SIPRK2013SDFIX.RaportDb
{
    public class PengetahuanCRUD
    {
        public bool Tambah(NilaiPengetahuan np)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO nilai_pengetahuan VALUES(@idpeng, @nisn, @idmapel, @kd_tertinggi, @kd_terendah, @nilai_tertinggi, @nilai_terendah, @nilaiakhir, @predikatpeng, @despeng, @semester)";
            var args = new Dictionary<string, object>
            {
                {"@idpeng", np.IdPeng },
                {"@nisn", np.Nisn },
                {"@idmapel", np.IdMapel },
                {"@kd_tertinggi", np.KdTertinggi },
                {"@kd_terendah", np.KdTerendah },
                {"@nilai_tertinggi", np.NilaiTertinggi },
                {"@nilai_terendah", np.NilaiTerendah },
                {"@nilaiakhir", np.NilaiAkhir },
                {"@predikatpeng", np.PredikatPengetahuan },
                {"@despeng", np.DeskripsiPengetahuan },
                {"@semester", np.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Ubah(NilaiPengetahuan np)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE nilai_pengetahuan SET kd_tertinggi = @kd_tertinggi, kd_terendah = @kd_terendah, nilai_tertinggi = @nilai_tertinggi, nilai_terendah = @nilai_terendah, nilaiakhir = @nilaiakhir, predikatpengetahuan = @predikatpeng, deskripsi_pengetahuan = @despeng, semester = @semester WHERE id_peng = @idpeng";
            var args = new Dictionary<string, object>
            {
                {"@idpeng", np.IdPeng },
                {"@kd_tertinggi", np.KdTertinggi },
                {"@kd_terendah", np.KdTerendah },
                {"@nilai_tertinggi", np.NilaiTertinggi },
                {"@nilai_terendah", np.NilaiTerendah },
                {"@nilaiakhir", np.NilaiAkhir },
                {"@predikatpeng", np.PredikatPengetahuan },
                {"@despeng", np.DeskripsiPengetahuan },
                {"@semester", np.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(NilaiPengetahuan np)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_pengetahuan WHERE id_peng = @idpeng";
            var args = new Dictionary<string, object>
            {
                {"@idpeng", np.IdPeng }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool HapusAll(NilaiPengetahuan np)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_pengetahuan WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", np.Nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public NilaiPengetahuan GetNilaiPengetahuan(string idpeng)
        {
            NilaiPengetahuan nilaiPengetahuan;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM nilai_pengetahuan WHERE id_peng = '{idpeng}'";
            DataTable dt = dB.GetDataRaport(query);
            nilaiPengetahuan = new NilaiPengetahuan
            {
                IdPeng = idpeng,
                Nisn = dt.Rows[0][1].ToString(),
                IdMapel = Convert.ToInt32(dt.Rows[0][2]),
                KdTertinggi = Convert.ToInt32(dt.Rows[0][3]),
                KdTerendah = Convert.ToInt32(dt.Rows[0][4]),
                NilaiTertinggi = Convert.ToInt32(dt.Rows[0][5]),
                NilaiTerendah = Convert.ToInt32(dt.Rows[0][6]),
                NilaiAkhir = Convert.ToInt32(dt.Rows[0][7]),
                PredikatPengetahuan = dt.Rows[0][8].ToString(),
                DeskripsiPengetahuan = dt.Rows[0][9].ToString(),
                Semester = dt.Rows[0][10].ToString()
            };
            return nilaiPengetahuan;
        }
    }
}
