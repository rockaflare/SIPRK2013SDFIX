using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;

namespace SIPRK2013SDFIX.RaportDb
{
    public class SekolahCRUD
    {
        public bool Ubah(DataSekolah data)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE data_sekolah SET npsn = @npsn, kelas = @kelas, guru_kelas = @guru_kelas, nip_guru = @nip_guru, desa = @desa, kecamatan = @kecamatan, kota = @kota, provinsi = @provinsi, nm_sekolah = @nm_sekolah, alamat = @alamat, nm_kepsek = @nm_kepsek, nip_kepsek = @nip_kepsek, tgl_raport = @tgl_raport, semester = @semester, tahunajar = @tahunajar WHERE id_sek = @id_sek";
            var args = new Dictionary<string, object>
            {
                {"@id_sek", data.IdSek },
                {"@npsn", data.Npsn },
                {"@kelas", data.Kelas },
                {"@guru_kelas", data.GuruKelas },
                {"@nip_guru", data.NipGuru },
                {"@desa", data.Desa },
                {"@kecamatan", data.Kecamatan },
                {"@kota", data.Kota },
                {"@provinsi", data.Provinsi },
                {"@nm_sekolah", data.NmSekolah },
                {"@alamat", data.Alamat },
                {"@nm_kepsek", data.NmKepsek },
                {"@nip_kepsek", data.NipKepsek },
                {"@tgl_raport", data.TglRaport },
                {"@semester", data.Semester },
                {"@tahunajar", data.Tahunajar }
            };
            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public DataSekolah GetDataSekolah()
        {
            DataSekolah dataSekolah;
            RaportDB dB = new RaportDB();
            const string query = "SELECT * FROM data_sekolah WHERE id_sek = 1";
            DataTable dT = dB.GetDataRaport(query);
            dataSekolah = new DataSekolah
            {
                Npsn = Convert.ToInt32(dT.Rows[0][1]),
                Kelas = dT.Rows[0][2].ToString(),
                GuruKelas = dT.Rows[0][3].ToString(),
                NipGuru = dT.Rows[0][4].ToString(),
                Desa = dT.Rows[0][5].ToString(),
                Kecamatan = dT.Rows[0][6].ToString(),
                Kota = dT.Rows[0][7].ToString(),
                Provinsi = dT.Rows[0][8].ToString(),
                NmSekolah = dT.Rows[0][9].ToString(),
                Alamat = dT.Rows[0][10].ToString(),
                NmKepsek = dT.Rows[0][11].ToString(),
                NipKepsek = dT.Rows[0][12].ToString(),
                TglRaport = dT.Rows[0][13].ToString(),
                Semester = dT.Rows[0][14].ToString(),
                Tahunajar = dT.Rows[0][15].ToString()
            };
            return dataSekolah;
        }
    }
}
