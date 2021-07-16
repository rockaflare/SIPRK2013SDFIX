using System;
using System.Collections.Generic;
using System.Data;
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
using MahApps.Metro.Controls.Dialogs;
using SIPRK2013SDFIX.RaportDb;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for RaportView.xaml
    /// </summary>
    public partial class RaportView : MetroWindow
    {
        public RaportView()
        {
            InitializeComponent();
            LoadNamaSiswa();
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

        private async void SetButt_Click(object sender, RoutedEventArgs e)
        {
            if (SNama.SelectedIndex != -1 && SSemester.SelectedIndex != -1)
            {
                HasilRaport hs = new HasilRaport();
                hs.SetRaport(SNama.SelectedValue.ToString(), SSemester.Text);
                hs.Show();
                this.Close();
            }
            else
            {
                await this.ShowMessageAsync("Data Raport", "Silahkan pilih nama dan semester!");
            }
            
        }
    }
}
