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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SIPRK2013SDFIX.View;
using MahApps.Metro.Controls.Dialogs;

namespace SIPRK2013SDFIX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Informasi_Click(object sender, RoutedEventArgs e)
        {
            var Informasi = new Informasi();
            Informasi.ShowDialog();
        }

        private void Nilai_Click(object sender, RoutedEventArgs e)
        {
            var PilihDataWindow = new PilihData();
            PilihDataWindow.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var EditData = new EditDataWindow();
            EditData.ShowDialog();
        }

        private void Raport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
