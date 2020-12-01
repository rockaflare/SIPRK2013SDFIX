using System;
using System.Collections.Generic;

namespace SIPRK2013SDFIX.Model
{
    public partial class DataSekolah
    {
        public int IdSek { get; set; }
        public int Npsn { get; set; }
        public string Kelas { get; set; }
        public string GuruKelas { get; set; }
        public string NipGuru { get; set; }
        public string Desa { get; set; }
        public string Kecamatan { get; set; }
        public string Kota { get; set; }
        public string Provinsi { get; set; }
        public string NmSekolah { get; set; }
        public string Alamat { get; set; }
        public string NmKepsek { get; set; }
        public string NipKepsek { get; set; }
        public string TglRaport { get; set; }
        public string Semester { get; set; }
        public string Tahunajar { get; set; }
    }
}
