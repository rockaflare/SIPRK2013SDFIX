using System;
using System.Collections.Generic;

namespace SIPRK2013SDFIX.Model
{
    public partial class NilaiSikap
    {
        public string IdSikap { get; set; }
        public string Nisn { get; set; }
        public int Beribadah { get; set; }
        public int Bersyukur { get; set; }
        public int Berdoa { get; set; }
        public int Toleransi { get; set; }
        public int Jujur { get; set; }
        public int Disiplin { get; set; }
        public int TanggungJawab { get; set; }
        public int Peduli { get; set; }
        public int Santun { get; set; }
        public int PercayaDiri { get; set; }
        public int Kerjasama { get; set; }
        public string DeskripsiKi1 { get; set; }
        public string DeskripsiKi2 { get; set; }
        public string Semester { get; set; }
    }
}
