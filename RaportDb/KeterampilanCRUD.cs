using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SIPRK2013SDFIX.Model;

namespace SIPRK2013SDFIX.RaportDb
{
    public class KeterampilanCRUD
    {
        public bool Tambah(NilaiKeterampilan nk)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO nilai_keterampilan VALUES(@idket, @nisn, @idmapel, @kd_tertinggi, @kd_terendah, @nilai_tertinggi, @nilai_terendah, @nilaiakhir, @predikatket, @desket, @semester)";
            var args = new Dictionary<string, object>
            {
                {"@idket", nk.IdKet },
                {"@nisn", nk.Nisn },
                {"@idmapel", nk.IdMapel },
                {"@kd_tertinggi", nk.KdTertinggi },
                {"@kd_terendah", nk.KdTerendah },
                {"@nilai_tertinggi", nk.NilaiTertinggi },
                {"@nilai_terendah", nk.NilaiTerendah },
                {"@nilaiakhir", nk.NilaiAkhir },
                {"@predikatket", nk.PredikatKeterampilan },
                {"@desket", nk.DeskripsiKeterampilan },
                {"@semester", nk.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Ubah(NilaiKeterampilan nk)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE nilai_keterampilan SET kd_tertinggi = @kd_tertinggi, kd_terendah = @kd_terendah, nilai_tertinggi = @nilai_tertinggi, nilai_terendah = @nilai_terendah, nilaiakhir = @nilaiakhir, predikatketerampilan = @predikatket, deskripsi_keterampilan = @desket, semester = @semester WHERE id_ket = @idket";
            var args = new Dictionary<string, object>
            {
                {"@idket", nk.IdKet },
                {"@kd_tertinggi", nk.KdTertinggi },
                {"@kd_terendah", nk.KdTerendah },
                {"@nilai_tertinggi", nk.NilaiTertinggi },
                {"@nilai_terendah", nk.NilaiTerendah },
                {"@nilaiakhir", nk.NilaiAkhir },
                {"@predikatket", nk.PredikatKeterampilan },
                {"@desket", nk.DeskripsiKeterampilan },
                {"@semester", nk.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(NilaiKeterampilan nk)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_keterampilan WHERE id_ket = @idket";
            var args = new Dictionary<string, object>
            {
                {"@idket", nk.IdKet }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool HapusAll(NilaiKeterampilan nk)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_keterampilan WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", nk.Nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public NilaiKeterampilan GetNilaiKeterampilan(string idket)
        {
            NilaiKeterampilan nilaiKeterampilan;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM nilai_keterampilan WHERE id_ket = '{idket}'";
            DataTable dt = dB.GetDataRaport(query);
            nilaiKeterampilan = new NilaiKeterampilan
            {
                IdKet = idket,
                Nisn = dt.Rows[0][1].ToString(),
                IdMapel = Convert.ToInt32(dt.Rows[0][2]),
                KdTertinggi = Convert.ToInt32(dt.Rows[0][3]),
                KdTerendah = Convert.ToInt32(dt.Rows[0][4]),
                NilaiTertinggi = Convert.ToInt32(dt.Rows[0][5]),
                NilaiTerendah = Convert.ToInt32(dt.Rows[0][6]),
                NilaiAkhir = Convert.ToInt32(dt.Rows[0][7]),
                PredikatKeterampilan = dt.Rows[0][8].ToString(),
                DeskripsiKeterampilan = dt.Rows[0][9].ToString(),
                Semester = dt.Rows[0][10].ToString()
            };
            return nilaiKeterampilan;
        }
    }
}
