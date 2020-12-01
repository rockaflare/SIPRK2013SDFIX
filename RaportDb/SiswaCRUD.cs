using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;

namespace SIPRK2013SDFIX.RaportDb
{
    public class SiswaCRUD
    {
        public DataTable SiswaDataTable { get; set; }
        public SiswaCRUD()
        {
            GetSiswaTable();
        }
        public bool Tambah(DataSiswa ds)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO data_siswa VALUES(@nisn, @noinduk, @nmsiswa, @nmpanggilan, @gender, @agama, @tptlahir, @tgllahir, @penseb, @alamat, @nmayah, @nmibu, @jobayah, @jobibu, @jalan, @kecamatan, @desa, @kota, @provinsi, @nmwali, @jobwali, @alamatwali, @pendengaran, @penglihatan, @gigi, @berat1, @berat2, @tinggi1, @tinggi2)";
            var args = new Dictionary<string, object>
            {
                {"@nisn", ds.Nisn },
                {"@noinduk", ds.NoInduk },
                {"@nmsiswa", ds.NmSiswa },
                {"@nmpanggilan", ds.NmPanggilan },
                {"@gender", ds.Gender },
                {"@agama", ds.Agama },
                {"@tptlahir", ds.TempatLahir },
                {"@tgllahir", ds.TglLahir },
                {"@penseb", ds.PendidikanSeb },
                {"@alamat", ds.Alamat },
                {"@nmayah", ds.NmAyah },
                {"@nmibu", ds.NmIbu },
                {"@jobayah", ds.JobAyah },
                {"@jobibu", ds.JobIbu },
                {"@jalan", ds.Jalan },
                {"@kecamatan", ds.Kecamatan },
                {"@desa", ds.Desa },
                {"@kota", ds.Kota },
                {"@provinsi", ds.Provinsi },
                {"@nmwali", ds.NmWali },
                {"@jobwali", ds.JobWali },
                {"@alamatwali", ds.AlamatWali },
                {"@pendengaran", ds.Pendengaran },
                {"@penglihatan", ds.Penglihatan },
                {"@gigi", ds.Gigi },
                {"@berat1", ds.Berat1 },
                {"@berat2", ds.Berat2 },
                {"@tinggi1", ds.Tinggi1 },
                {"@tinggi2", ds.Tinggi2 }
            };
            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Ubah(DataSiswa ds, string oldnisn)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE data_siswa SET nisn = @nisn, no_induk = @noinduk, nm_siswa = @nmsiswa, nm_panggilan = @nmpanggilan, gender = @gender, agama =  @agama, tempat_lahir = @tptlahir, tgl_lahir = @tgllahir, pendidikan_seb = @penseb, alamat =  @alamat, nm_ayah = @nmayah, nm_ibu =  @nmibu, job_ayah =  @jobayah, job_ibu = @jobibu, jalan =  @jalan, kecamatan = @kecamatan, desa = @desa, kota = @kota, provinsi =  @provinsi, nm_wali = @nmwali, job_wali = @jobwali, alamat_wali = @alamatwali, pendengaran = @pendengaran, penglihatan = @penglihatan, gigi = @gigi, berat1 = @berat1, berat2 = @berat2, tinggi1 = @tinggi1, tinggi2 = @tinggi2 WHERE nisn = @oldnisn";
            var args = new Dictionary<string, object>
            {
                {"@oldnisn", oldnisn },
                {"@nisn", ds.Nisn },
                {"@noinduk", ds.NoInduk },
                {"@nmsiswa", ds.NmSiswa },
                {"@nmpanggilan", ds.NmPanggilan },
                {"@gender", ds.Gender },
                {"@agama", ds.Agama },
                {"@tptlahir", ds.TempatLahir },
                {"@tgllahir", ds.TglLahir },
                {"@penseb", ds.PendidikanSeb },
                {"@alamat", ds.Alamat },
                {"@nmayah", ds.NmAyah },
                {"@nmibu", ds.NmIbu },
                {"@jobayah", ds.JobAyah },
                {"@jobibu", ds.JobIbu },
                {"@jalan", ds.Jalan },
                {"@kecamatan", ds.Kecamatan },
                {"@desa", ds.Desa },
                {"@kota", ds.Kota },
                {"@provinsi", ds.Provinsi },
                {"@nmwali", ds.NmWali },
                {"@jobwali", ds.JobWali },
                {"@alamatwali", ds.AlamatWali },
                {"@pendengaran", ds.Pendengaran },
                {"@penglihatan", ds.Penglihatan },
                {"@gigi", ds.Gigi },
                {"@berat1", ds.Berat1 },
                {"@berat2", ds.Berat2 },
                {"@tinggi1", ds.Tinggi1 },
                {"@tinggi2", ds.Tinggi2 }
            };
            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(DataSiswa ds)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM data_siswa WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", ds.Nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }

        public DataSiswa GetDataSiswa(string nisn)
        {
            DataSiswa dataSiswa;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM data_siswa WHERE nisn = {nisn}";
            DataTable dt = dB.GetDataRaport(query);
            dataSiswa = new DataSiswa
            {
                Nisn = dt.Rows[0][0].ToString(),
                NoInduk = dt.Rows[0][1].ToString(),
                NmSiswa = dt.Rows[0][2].ToString(),
                NmPanggilan = dt.Rows[0][3].ToString(),
                Gender = dt.Rows[0][4].ToString(),
                Agama = dt.Rows[0][5].ToString(),
                TempatLahir = dt.Rows[0][6].ToString(),
                TglLahir = dt.Rows[0][7].ToString(),
                PendidikanSeb = dt.Rows[0][8].ToString(),
                Alamat = dt.Rows[0][9].ToString(),
                NmAyah = dt.Rows[0][10].ToString(),
                NmIbu = dt.Rows[0][11].ToString(),
                JobAyah = dt.Rows[0][12].ToString(),
                JobIbu = dt.Rows[0][13].ToString(),
                Jalan = dt.Rows[0][14].ToString(),
                Kecamatan = dt.Rows[0][15].ToString(),
                Desa = dt.Rows[0][16].ToString(),
                Kota = dt.Rows[0][17].ToString(),
                Provinsi = dt.Rows[0][18].ToString(),
                NmWali = dt.Rows[0][19].ToString(),
                JobWali = dt.Rows[0][20].ToString(),
                AlamatWali = dt.Rows[0][21].ToString(),
                Pendengaran = dt.Rows[0][22].ToString(),
                Penglihatan = dt.Rows[0][23].ToString(),
                Gigi = dt.Rows[0][24].ToString(),
                Berat1 = dt.Rows[0][25].ToString(),
                Berat2 = dt.Rows[0][26].ToString(),
                Tinggi1 = dt.Rows[0][27].ToString(),
                Tinggi2 = dt.Rows[0][28].ToString()
            };
            return dataSiswa;
        }
        public void GetSiswaTable()
        {
            var query = "SELECT nisn, no_induk, nm_siswa, gender, agama, tempat_lahir, tgl_lahir FROM data_siswa";
            RaportDB db = new RaportDB();
            SiswaDataTable = db.GetDataRaport(query);
        }
    }
}
