using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.Model;

namespace SIPRK2013SDFIX.RaportDb
{
    public class SikapCRUD
    {
        public bool Tambah(NilaiSikap ns)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "INSERT INTO nilai_sikap VALUES(@idsikap, @nisn, @beribadah, @bersyukur, @berdoa, @toleransi, @jujur, @disiplin, @tanggungjawab, @peduli, @santun, @pd, @kerjasama, @deskripsik1, @deskripsik2, @semester)";
            var args = new Dictionary<string, object>
            {
                {"@idsikap", ns.IdSikap },
                {"@nisn", ns.Nisn },
                {"@beribadah", ns.Beribadah },
                {"@bersyukur", ns.Bersyukur },
                {"@berdoa", ns.Berdoa },
                {"@toleransi", ns.Toleransi },
                {"@jujur", ns.Jujur },
                {"@disiplin", ns.Disiplin },
                {"@tanggungjawab", ns.TanggungJawab },
                {"@peduli", ns.Peduli },
                {"@santun", ns.Santun },
                {"@pd", ns.PercayaDiri },
                {"@kerjasama", ns.Kerjasama },
                {"@deskripsik1", ns.DeskripsiKi1 },
                {"@deskripsik2", ns.DeskripsiKi2 },
                {"@semester", ns.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }

        public bool Ubah(NilaiSikap ns)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "UPDATE nilai_sikap SET beribadah = @beribadah, bersyukur = @bersyukur, berdoa = @berdoa, toleransi = @toleransi, jujur = @jujur, disiplin = @disiplin, tanggung_jawab = @tanggungjawab, peduli = @peduli, santun = @santun, percaya_diri = @pd, kerjasama = @kerjasama, deskripsi_ki1 = @deskripsik1, deskripsi_ki2 = @deskripsik2, semester = @semester WHERE id_sikap = @idsikap";
            var args = new Dictionary<string, object>
            {
                {"@idsikap", ns.IdSikap },
                {"@beribadah", ns.Beribadah },
                {"@bersyukur", ns.Bersyukur },
                {"@berdoa", ns.Berdoa },
                {"@toleransi", ns.Toleransi },
                {"@jujur", ns.Jujur },
                {"@disiplin", ns.Disiplin },
                {"@tanggungjawab", ns.TanggungJawab },
                {"@peduli", ns.Peduli },
                {"@santun", ns.Santun },
                {"@pd", ns.PercayaDiri },
                {"@kerjasama", ns.Kerjasama },
                {"@deskripsik1", ns.DeskripsiKi1 },
                {"@deskripsik2", ns.DeskripsiKi2 },
                {"@semester", ns.Semester }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool Hapus(NilaiSikap ns)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_sikap WHERE id_sikap = @idsikap";
            var args = new Dictionary<string, object>
            {
                {"@idsikap", ns.IdSikap }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public bool HapusAll(string nisn)
        {
            bool isNice = false;
            RaportDB dB = new RaportDB();

            const string query = "DELETE FROM nilai_sikap WHERE nisn = @nisn";
            var args = new Dictionary<string, object>
            {
                {"@nisn", nisn }
            };

            return isNice = dB.ExecuteWrite(query, args) > 0 ? true : false;
        }
        public NilaiSikap GetNilaiSikap(string idsikap)
        {
            NilaiSikap nilaiSikap;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM nilai_sikap WHERE id_sikap = '{idsikap}'";
            DataTable dt = dB.GetDataRaport(query);
            nilaiSikap = new NilaiSikap
            {
                IdSikap = idsikap,
                Nisn = dt.Rows[0][1].ToString(),
                Beribadah = Convert.ToInt32(dt.Rows[0][2]),
                Bersyukur = Convert.ToInt32(dt.Rows[0][3]),
                Berdoa = Convert.ToInt32(dt.Rows[0][4]),
                Toleransi = Convert.ToInt32(dt.Rows[0][5]),
                Jujur = Convert.ToInt32(dt.Rows[0][6]),
                Disiplin = Convert.ToInt32(dt.Rows[0][7]),
                TanggungJawab = Convert.ToInt32(dt.Rows[0][8]),
                Peduli = Convert.ToInt32(dt.Rows[0][9]),
                Santun = Convert.ToInt32(dt.Rows[0][10]),
                PercayaDiri = Convert.ToInt32(dt.Rows[0][11]),
                Kerjasama = Convert.ToInt32(dt.Rows[0][12]),
                DeskripsiKi1 = dt.Rows[0][13].ToString(),
                DeskripsiKi2 = dt.Rows[0][14].ToString(),
                Semester = dt.Rows[0][15].ToString()
            };
            return nilaiSikap;
        }
        public NilaiSikap GetNilaiSikapSiswa(string nisn, string semester)
        {
            NilaiSikap nilaiSikap;
            RaportDB dB = new RaportDB();
            string query = $"SELECT * FROM nilai_sikap WHERE nisn = '{nisn}' AND semester = '{semester}'";
            DataTable dt = dB.GetDataRaport(query);
            if (dt.Rows.Count > 0 )
            {
                nilaiSikap = new NilaiSikap
                {
                    IdSikap = dt.Rows[0][0].ToString(),
                    Nisn = dt.Rows[0][1].ToString(),
                    Beribadah = Convert.ToInt32(dt.Rows[0][2]),
                    Bersyukur = Convert.ToInt32(dt.Rows[0][3]),
                    Berdoa = Convert.ToInt32(dt.Rows[0][4]),
                    Toleransi = Convert.ToInt32(dt.Rows[0][5]),
                    Jujur = Convert.ToInt32(dt.Rows[0][6]),
                    Disiplin = Convert.ToInt32(dt.Rows[0][7]),
                    TanggungJawab = Convert.ToInt32(dt.Rows[0][8]),
                    Peduli = Convert.ToInt32(dt.Rows[0][9]),
                    Santun = Convert.ToInt32(dt.Rows[0][10]),
                    PercayaDiri = Convert.ToInt32(dt.Rows[0][11]),
                    Kerjasama = Convert.ToInt32(dt.Rows[0][12]),
                    DeskripsiKi1 = dt.Rows[0][13].ToString(),
                    DeskripsiKi2 = dt.Rows[0][14].ToString(),
                    Semester = dt.Rows[0][15].ToString()
                };
            }
            else
            {
                nilaiSikap = new NilaiSikap
                {
                    IdSikap = "",
                    Nisn = "",
                    Beribadah = 0,
                    Bersyukur = 0,
                    Berdoa = 0,
                    Toleransi = 0,
                    Jujur = 0,
                    Disiplin = 0,
                    TanggungJawab = 0,
                    Peduli = 0,
                    Santun = 0,
                    PercayaDiri = 0,
                    Kerjasama = 0,
                    DeskripsiKi1 = "",
                    DeskripsiKi2 = "",
                    Semester = ""
                };
            }
            
            return nilaiSikap;
        }
        public string SpiritualDes(int ib, int sy, int doa, int tole, string nm)
        {
            string hasil = $"Andanda {nm} ";
            string[] arr = { "baik dalam", " dan sudah mampu meningkatkan sikap ", "sangat baik dalam" };
            string[] ki = { " ketaatan beribadah ", " berperilaku syukur ", " berdoa sebelum dan sesudah kegiatan ", " toleransi dalam beribadah " };
            int[] nKi = { ib, sy, doa, tole };
            List<string> lKi = new List<string>();
            List<string> lKo = new List<string>();
            List<string> lKt = new List<string>();
            for (int i = 0; i < nKi.Length; i++)
            {
                if (nKi[i] == 0)
                {
                    lKi.Add(ki[i]);
                }
                else if (nKi[i] == 1)
                {
                    lKo.Add(ki[i]);
                }
                else if (nKi[i] == 2)
                {
                    lKt.Add(ki[i]);
                }
            }
            if (lKi.Count() < 4 && lKi.Count > 0)
            {
                hasil += arr[0];
                for (int i = 0; i < lKi.Count(); i++)
                {
                    hasil += lKi[i] + ",";
                }
            }
            else if (lKi.Count() == 4)
            {
                hasil += arr[0];
                for (int i = 0; i < lKi.Count(); i++)
                {
                    if (lKi[i] == lKi[lKi.Count - 1])
                    {
                        hasil += lKi[i] + ".";
                    }
                    else
                    {
                        hasil += lKi[i] + ",";
                    }
                }
            }

            if (lKt.Count() < 4 && lKt.Count() > 0)
            {
                hasil += arr[2];
                for (int i = 0; i < lKt.Count(); i++)
                {
                    hasil += lKt[i] + ",";
                }
            }
            else if (lKt.Count() == 4)
            {
                hasil += arr[2];
                for (int i = 0; i < lKt.Count(); i++)
                {
                    if (lKt[i] == lKt[lKt.Count - 1])
                    {
                        hasil += lKt[i] + ".";
                    }
                    else
                    {
                        hasil += lKt[i] + ",";
                    }
                }
            }

            if (lKo.Count() > 0)
            {
                hasil += arr[1];
                for (int i = 0; i < lKo.Count(); i++)
                {
                    if (lKo[i] == lKo[lKo.Count - 1])
                    {
                        hasil += lKo[i] + ".";
                    }
                    else
                    {
                        hasil += lKo[i] + ",";
                    }
                }
            }
            return hasil;
        }
        public string SosialDes(int j, int d, int t, int p, int s, int pd, int k, string nm)
        {
            string hasil = $"Ananda {nm} ";
            string[] arr = { "baik dalam sikap ", " dan sudah mampu meningkatkan sikap ", "sangat " };
            string[] ki = { "jujur", "disiplin", "tanggung jawab", "peduli", "santun", "percaya diri", "kerjasama" };
            int[] nKi = { j, d, t, p, s, pd, k };
            List<string> b = new List<string>();
            List<string> sb = new List<string>();
            List<string> pb = new List<string>();
            for (int i = 0; i < nKi.Length; i++)
            {
                if (nKi[i] == 0)
                {
                    b.Add(ki[i]);
                }
                else if (nKi[i] == 1)
                {
                    pb.Add(ki[i]);
                }
                else if (nKi[i] == 2)
                {
                    sb.Add(ki[i]);
                }
            }
            if (b.Count() > 0 && b.Count < 7)
            {
                hasil += arr[0];
                for (int i = 0; i < b.Count(); i++)
                {
                    hasil += b[i] + ", ";
                }
            }
            else if (b.Count() == 7)
            {
                hasil += arr[0];
                for (int i = 0; i < b.Count(); i++)
                {
                    if (b[i] == b[b.Count() - 1])
                    {
                        hasil += b[i] + ".";
                    }
                    else
                    {
                        hasil += b[i] + ", ";
                    }
                }
            }

            if (sb.Count() > 0 && sb.Count < 7)
            {
                hasil += arr[2];
                for (int i = 0; i < sb.Count(); i++)
                {
                    hasil += sb[i] + ", ";
                }
            }
            else if (sb.Count() == 7)
            {
                hasil += arr[2];
                for (int i = 0; i < sb.Count(); i++)
                {
                    if (sb[i] == sb[sb.Count() - 1])
                    {
                        hasil += sb[i] + ".";
                    }
                    else
                    {
                        hasil += sb[i] + ", ";
                    }
                }
            }

            if (pb.Count() > 0)
            {
                hasil += arr[1];
                for (int i = 0; i < pb.Count(); i++)
                {
                    if (pb[i] == pb[pb.Count() - 1])
                    {
                        hasil += pb[i] + ".";
                    }
                    else
                    {
                        hasil += pb[i] + ", ";
                    }
                }
            }
            return hasil;
        }
    }
}
