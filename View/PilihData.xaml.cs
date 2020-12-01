using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SIPRK2013SDFIX.RaportDb;
using System.Data;
using System.Text.RegularExpressions;
using MahApps.Metro.Controls.Dialogs;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for PilihData.xaml
    /// </summary>
    public partial class PilihData : MetroWindow
    {
        public PilihData()
        {
            InitializeComponent();
            LoadNamaSiswa();
        }
        private int KKM = 60;
        private async void SetButt_Click(object sender, RoutedEventArgs e)
        {
            if (STipeNilai.SelectedIndex == 0)
            {
                if (SNama.SelectedValue != null && SSemester.SelectedValue != null)
                {
                    NilaiDanAbsen NdaForm = new NilaiDanAbsen();
                    NdaForm.SetData(SNama.SelectedValue.ToString(), SNama.Text, SSemester.Text, GetNamaPanggilan(SNama.SelectedValue.ToString()));
                    NdaForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("Pilih Data", "Data belum lengkap!");
                }
            }
            else if (STipeNilai.SelectedIndex == 1)
            {
                if (SNama.SelectedValue != null && SSemester.SelectedValue != null && SMapel.SelectedValue != null)
                {
                    PengetahuanDanKeterampilan PenForm = new PengetahuanDanKeterampilan();
                    PenForm.SetData(SNama.SelectedValue.ToString(), SNama.Text, Convert.ToInt32(SMapel.SelectedValue), SMapel.Text, SSemester.Text, GetKelas(), GetNamaPanggilan(SNama.SelectedValue.ToString()), KKM);
                    PenForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("Pilih Data", "Data belum lengkap!");
                }
            }
        }
        private void LoadNamaSiswa()
        {
            var query = "SELECT nisn, nm_siswa FROM data_siswa";
            RaportDB dBRaport = new RaportDB();
            DataTable dtNama = dBRaport.GetDataRaport(query);
            SNama.ItemsSource = dtNama.DefaultView;
            SNama.DisplayMemberPath = "nm_siswa";
            SNama.SelectedValuePath = "nisn";
        }
        private void LoadMapel(int agama)
        {
            var query = $"SELECT nm_mapel, id_mapel FROM data_mapel WHERE tingkat_kls = 'all' OR agama = {agama}";
            RaportDB dBRaport = new RaportDB();
            DataTable dtmapel = dBRaport.GetDataRaport(query);
            SMapel.ItemsSource = dtmapel.DefaultView;
            SMapel.DisplayMemberPath = "nm_mapel";
            SMapel.SelectedValuePath = "id_mapel";
        }
        private void GetAgama(string nisn)
        {
            var query = $"SELECT agama FROM data_siswa WHERE nisn={nisn}";
            RaportDB dBRaport = new RaportDB();
            DataTable dtNama = dBRaport.GetDataRaport(query);
            if (dtNama.Rows[0][0].ToString() == "Islam")
            {
                LoadMapel(1);
            }
            else if (dtNama.Rows[0][0].ToString() == "Kristen")
            {
                LoadMapel(2);
            }
            else if (dtNama.Rows[0][0].ToString() == "Katolik")
            {
                LoadMapel(3);
            }
            else if (dtNama.Rows[0][0].ToString() == "Hindu")
            {
                LoadMapel(4);
            }
            else if (dtNama.Rows[0][0].ToString() == "Buddha")
            {
                LoadMapel(5);
            }
            else if (dtNama.Rows[0][0].ToString() == "Konghuchu")
            {
                LoadMapel(6);
            }
            
        }
        private string GetNamaPanggilan(string nisn)
        {
            string hasil;
            var query = $"SELECT nm_panggilan FROM data_siswa WHERE nisn = {nisn}";
            RaportDB dBRaport = new RaportDB();
            DataTable dt = dBRaport.GetDataRaport(query);
            hasil = dt.Rows[0][0].ToString();
            return hasil;
        }
        private string GetKelas()
        {
            var query = $"SELECT kelas FROM data_sekolah WHERE id_sek = 1";
            RaportDB dBRaport = new RaportDB();
            DataTable dt = dBRaport.GetDataRaport(query);
            string hasil = Regex.Match(dt.Rows[0][0].ToString(), @"\d+").Value;
            return hasil ;
        }
        

        private void SNama_DropDownClosed(object sender, EventArgs e)
        {
            if (SNama.SelectedValue != null)
            {
                GetAgama(SNama.SelectedValue.ToString());
            }
        }

        private void STipeNilai_DropDownClosed(object sender, EventArgs e)
        {
            if (STipeNilai.SelectedIndex == 0)
            {
                SMapel.IsEnabled = false;
            }
        }

        private void SMapel_DropDownClosed(object sender, EventArgs e)
        {
            RumusNilai rn = new RumusNilai();
            if (SMapel.SelectedValue != null)
            {
                KKM = rn.GetKKM(Convert.ToInt32(SMapel.SelectedValue));
            }
        }
    }
}
