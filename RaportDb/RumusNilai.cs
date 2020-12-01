using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPRK2013SDFIX.RaportDb
{
    public class RumusNilai
    {
        public string Deskripsi(string rendah, string tinggi, string nmpanggilan, string prehigh)
        {
            string hasil;
            if (rendah != "" || tinggi != "")
            {
                if (prehigh == "A")
                {
                    hasil = $"Ananda {nmpanggilan} sangat baik dalam {tinggi}, dan cukup dalam {rendah}";
                }
                else if (prehigh == "B")
                {
                    hasil = $"Ananda {nmpanggilan} sudah baik dalam {tinggi}, dan cukup dalam {rendah}";
                }
                else
                {
                    hasil = $"Ananda {nmpanggilan} sudah cukup dalam {tinggi}, dan {rendah}";
                }
            }
            else
            {
                hasil = "Tidak ada deskripsi!";
            }
            
            return hasil;
        }

        public string Predikat(int nilaiakhir, int kkm)
        {
            string hasil = "";
            int rentangkkm = (100 - kkm) / 3;
            int predikatB = 100 - rentangkkm;
            int predikatC = predikatB - rentangkkm;
            if (nilaiakhir == kkm || nilaiakhir < kkm)
            {
                hasil = "D";
            }
            else if (nilaiakhir <= predikatC && nilaiakhir > kkm)
            {
                hasil = "C";
            }
            else if (nilaiakhir <= predikatB && nilaiakhir > predikatC)
            {
                hasil = "B";
            }
            else if(nilaiakhir == 100 && nilaiakhir > predikatB)
            {
                hasil = "A";
            }            
            return hasil;
        }
        public string DeskripsiKD(int id_kd)
        {
            string hasil = "";
            RaportDB dB = new RaportDB();
            if (id_kd != 0)
            {
                string query = $"SELECT deskripsi_kd FROM kompetensi_dasar WHERE id_kd = {id_kd}";
                DataTable dt = dB.GetDataRaport(query);
                hasil = dt.Rows[0][0].ToString();
            }
            return hasil;
        }
        public int GetKKM(int id_mapel)
        {
            int hasil;
            RaportDB dB = new RaportDB();
            string query = $"SELECT kkm FROM data_mapel WHERE id_mapel = {id_mapel}";
            DataTable dt = dB.GetDataRaport(query);
            hasil = Convert.ToInt32(dt.Rows[0][0]);
            return hasil;
        }
    }
}
