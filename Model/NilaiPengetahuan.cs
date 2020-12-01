using System;
using System.Collections.Generic;

namespace SIPRK2013SDFIX.Model
{
    public class NilaiPengetahuan
    {
        public string IdPeng { get; set; }
        public string Nisn { get; set; }
        public int IdMapel { get; set; }
        public int KdTertinggi { get; set; }
        public int KdTerendah { get; set; }
        public int NilaiTertinggi { get; set; }
        public int NilaiTerendah { get; set; }
        public int NilaiAkhir { get; set; }
        public string PredikatPengetahuan { get; set; }
        public string DeskripsiPengetahuan { get; set; }
        public string Semester { get; set; }
    }
}
