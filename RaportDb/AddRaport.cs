using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPRK2013SDFIX.RaportDb;
using SIPRK2013SDFIX.Model;

namespace SIPRK2013SDFIX.RaportDb
{
    public class AddRaport
    {
        public Dictionary<string, string> GetRaportView(string nisn, string semester)
        {
            //Nama dan Sekolah
            SiswaCRUD sw = new SiswaCRUD();
            SekolahCRUD sk = new SekolahCRUD();
            SikapCRUD si = new SikapCRUD();
            EkskulCRUD es = new EkskulCRUD();
            AbsensiCRUD ab = new AbsensiCRUD();
            PengetahuanCRUD pe = new PengetahuanCRUD();
            KeterampilanCRUD ke = new KeterampilanCRUD();
            DataSiswa dsw = sw.GetDataSiswa(nisn);
            DataSekolah dsk = sk.GetDataSekolah();
            NilaiSikap dns = si.GetNilaiSikapSiswa(nisn, semester);
            NilaiEkskul dne = es.GetNilaiEkskulSiswa(nisn, semester);
            Absensi dab = ab.GetAbsensiSiswa(nisn, semester);
            NilaiPengetahuan nag = new NilaiPengetahuan();
            NilaiKeterampilan kag = new NilaiKeterampilan();
            if (dsw.Agama.ToString() == "Islam")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 1);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 1);
            }
            else if (dsw.Agama.ToString() == "Kristen")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 2);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 2);
            }
            else if (dsw.Agama.ToString() == "Katolik")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 3);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 3);
            }
            else if (dsw.Agama.ToString() == "Hindu")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 4);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 4);
            }
            else if (dsw.Agama.ToString() == "Buddha")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 5);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 5);
            }
            else if (dsw.Agama.ToString() == "Konghuchu")
            {
                nag = pe.GetNilaiPengetahuanSiswa(nisn, semester, 6);
                kag = ke.GetNilaiKeterampilanSiswa(nisn, semester, 6);
            }
            NilaiPengetahuan npkn = pe.GetNilaiPengetahuanSiswa(nisn, semester, 7);
            NilaiKeterampilan kkpkn = ke.GetNilaiKeterampilanSiswa(nisn, semester, 7);

            NilaiPengetahuan nbi = pe.GetNilaiPengetahuanSiswa(nisn, semester, 8);
            NilaiKeterampilan kbi = ke.GetNilaiKeterampilanSiswa(nisn, semester, 8);

            NilaiPengetahuan nmtk = pe.GetNilaiPengetahuanSiswa(nisn, semester, 9);
            NilaiKeterampilan kmtk = ke.GetNilaiKeterampilanSiswa(nisn, semester, 9);

            NilaiPengetahuan nsbk = pe.GetNilaiPengetahuanSiswa(nisn, semester, 12);
            NilaiKeterampilan ksbk = ke.GetNilaiKeterampilanSiswa(nisn, semester, 12);

            NilaiPengetahuan npen = pe.GetNilaiPengetahuanSiswa(nisn, semester, 13);
            NilaiKeterampilan kpen = ke.GetNilaiKeterampilanSiswa(nisn, semester, 13);

            
            var hasil = new Dictionary<string, string>
            {
                {"NamaSiswa", dsw.NmSiswa.ToString() },
                {"Nisn", dsw.Nisn.ToString() },
                {"NoInduk", dsw.NoInduk.ToString() },
                {"Kelas", dsk.Kelas },
                {"NamaSekolah", dsk.NmSekolah },
                {"Alamat", dsk.Alamat },
                {"TahunAjar", dsk.Tahunajar },
                {"DSpiritual", dns.DeskripsiKi1},
                {"DSosial", dns.DeskripsiKi2 },

                {"NPAgama", nag.NilaiAkhir.ToString() },
                {"PPAgama", nag.PredikatPengetahuan },
                {"DPAgama", nag.DeskripsiPengetahuan },
                {"NKAgama", kag.NilaiAkhir.ToString() },
                {"PKAgama", kag.PredikatKeterampilan },
                {"DKAgama", kag.DeskripsiKeterampilan },

                {"NPPkn", npkn.NilaiAkhir.ToString() },
                {"PPPkn", npkn.PredikatPengetahuan },
                {"DPPkn", npkn.DeskripsiPengetahuan },
                {"NKPkn", kkpkn.NilaiAkhir.ToString() },
                {"PKPkn", kkpkn.PredikatKeterampilan },
                {"DKPkn", kkpkn.DeskripsiKeterampilan },

                {"NPBi", nbi.NilaiAkhir.ToString() },
                {"PPBi", nbi.PredikatPengetahuan },
                {"DPBi", nbi.DeskripsiPengetahuan },
                {"NKBi", kbi.NilaiAkhir.ToString() },
                {"PKBi", kbi.PredikatKeterampilan },
                {"DKBi", kbi.DeskripsiKeterampilan },

                {"NPMtk", nmtk.NilaiAkhir.ToString() },
                {"PPMtk", nmtk.PredikatPengetahuan },
                {"DPMtk", nmtk.DeskripsiPengetahuan },
                {"NKMtk", kmtk.NilaiAkhir.ToString() },
                {"PKMtk", kmtk.PredikatKeterampilan },
                {"DKMtk", kmtk.DeskripsiKeterampilan },

                {"NPSbk", nsbk.NilaiAkhir.ToString() },
                {"PPSbk", nsbk.PredikatPengetahuan },
                {"DPSbk", nsbk.DeskripsiPengetahuan },
                {"NKSbk", ksbk.NilaiAkhir.ToString() },
                {"PKSbk", ksbk.PredikatKeterampilan },
                {"DKSbk", ksbk.DeskripsiKeterampilan },

                {"NPPenjas", npen.NilaiAkhir.ToString() },
                {"PPPenjas", npen.PredikatPengetahuan },
                {"DPPenjas", npen.DeskripsiPengetahuan },
                {"NKPenjas", kpen.NilaiAkhir.ToString() },
                {"PKPenjas", kpen.PredikatKeterampilan },
                {"DKPenjas", kpen.DeskripsiKeterampilan },

                {"Eks1", dne.Eskul1 },
                {"Eks2", dne.Eskul2 },
                {"Eks3", dne.Eskul3 },
                {"NEks1", dne.Nilai1 },
                {"NEks2", dne.Nilai2 },
                {"NEks3", dne.Nilai3 },

                {"TB1", dsw.Tinggi1 },
                {"TB2", dsw.Tinggi2 },
                {"BB1", dsw.Berat1 },
                {"BB2", dsw.Berat2 },
                {"Dengar", dsw.Pendengaran },
                {"Lihat", dsw.Penglihatan },
                {"Gigi", dsw.Gigi },

                {"Sakit", dab.Sakit.ToString() },
                {"Ijin", dab.Ijin.ToString() },
                {"Alpa", dab.Alpha.ToString() },

                {"NamaAyah", dsw.NmAyah },

                {"NMGuru", dsk.GuruKelas },
                {"NIPGuru", dsk.NipGuru },

                {"NMKepsek", dsk.NmKepsek },
                {"NIPKepsek", dsk.NipKepsek }
            };
            return hasil;
        }
    }
}
