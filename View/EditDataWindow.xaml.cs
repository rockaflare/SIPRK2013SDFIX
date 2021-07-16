using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SIPRK2013SDFIX.Model;
using SIPRK2013SDFIX.RaportDb;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for EditDataWindow.xaml
    /// </summary>
    public partial class EditDataWindow : MetroWindow
    {
        public EditDataWindow()
        {
            InitializeComponent();
            LoadSiswa();
            LoadSikapDll();
            LoadPengKet();
            LoadMapel();
        }

        #region Load Data
        private void LoadSiswa()
        {
            SiswaCRUD scrd = new SiswaCRUD();
            GridSiswa.ItemsSource = scrd.SiswaDataTable.AsDataView();
        }
        private void LoadSikapDll()
        {
            RaportDB dB = new RaportDB();
            string query = "SELECT * FROM view_editsikap";
            DataTable dts = dB.GetDataRaport(query);
            GridSikap.ItemsSource = dts.AsDataView();
        }
        private void LoadPengKet(int idmapel = 0)
        {
            RaportDB dB = new RaportDB();
            string query = "SELECT * FROM view_editpengket";
            if (idmapel == 0)
            {
                query = "SELECT * FROM view_editpengket";
            }
            else if(idmapel > 0)
            {
                query = $"SELECT * FROM view_editpengket WHERE id_mapel = {idmapel}";
            }
            DataTable dtp = dB.GetDataRaport(query);
            GridPengKet.ItemsSource = dtp.AsDataView();
        }
        private void LoadMapel()
        {
            var query = $"SELECT nm_mapel, id_mapel FROM data_mapel WHERE tingkat_kls = 'all'";
            RaportDB dBRaport = new RaportDB();
            DataTable dtmapel = dBRaport.GetDataRaport(query);
            CBMapel.ItemsSource = dtmapel.DefaultView;
            CBMapel.DisplayMemberPath = "nm_mapel";
            CBMapel.SelectedValuePath = "id_mapel";
        }
        private string GetKelas()
        {
            var query = $"SELECT kelas FROM data_sekolah WHERE id_sek = 1";
            RaportDB dBRaport = new RaportDB();
            DataTable dt = dBRaport.GetDataRaport(query);
            string hasil = Regex.Match(dt.Rows[0][0].ToString(), @"\d+").Value;
            return hasil;
        }
        
        #endregion Load Data

        private void EditSiswa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rv = (DataRowView)((Button)e.Source).DataContext;
            Informasi informasi = new Informasi();
            informasi.SetNisn(rv.Row[0].ToString());
            informasi.Show();
            this.Close();
        }

        private void EditSikapDll_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rv = (DataRowView)((Button)e.Source).DataContext;
            NilaiDanAbsen nilaiDanAbsen = new NilaiDanAbsen();
            nilaiDanAbsen.SetEdit(rv.Row[0].ToString(), rv.Row[1].ToString(), rv.Row[2].ToString(), rv.Row[13].ToString(), rv.Row[3].ToString(), rv.Row[6].ToString(), rv.Row[9].ToString());
            nilaiDanAbsen.Show();
            this.Close();
        }

        private void EditPengket_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rd = (DataRowView)((Button)e.Source).DataContext;
            PengetahuanDanKeterampilan pdk = new PengetahuanDanKeterampilan();
            pdk.SetEdit(rd.Row[0].ToString(), rd.Row[1].ToString(), rd.Row[2].ToString(), Convert.ToInt32(rd.Row[4]), rd.Row[5].ToString(), rd.Row[3].ToString(), rd.Row[6].ToString(), rd.Row[9].ToString(), GetKelas(), 60);
            pdk.Show();
            this.Close();
        }

        private void TampilkanMapel_Click(object sender, RoutedEventArgs e)
        {
            if (CBMapel.SelectedIndex > -1)
            {
                LoadPengKet(Convert.ToInt32(CBMapel.SelectedValue));
            }
        }

        private async void DelPengket_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            PengetahuanCRUD pcrd = new PengetahuanCRUD();
            KeterampilanCRUD kcrd = new KeterampilanCRUD();
            MessageDialogResult res = await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Anda akan menghapus data pengetahuan dan keterampilan untuk siswa ini. Apa anda yakin ingin melanjutkan?", MessageDialogStyle.AffirmativeAndNegative);
            if (res == MessageDialogResult.Affirmative)
            {
                NilaiPengetahuan np = new NilaiPengetahuan();
                NilaiKeterampilan nk = new NilaiKeterampilan();
                np.IdPeng = drv.Row[6].ToString();
                nk.IdKet = drv.Row[9].ToString();
                try
                {
                    if (pcrd.Hapus(np) && kcrd.Hapus(nk))
                    {
                        await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Data berhasil dihapus!");
                        LoadPengKet();
                    }
                }
                catch (SQLiteException ex)
                {
                    await this.ShowMessageAsync("Pengetahuan dan Keterampilan", $"Error! {ex.Message}");
                }
            }
        }

        private async void DelSikapDll_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            SikapCRUD scrd = new SikapCRUD();
            EkskulCRUD ecrd = new EkskulCRUD();
            AbsensiCRUD acrd = new AbsensiCRUD();
            MessageDialogResult res = await this.ShowMessageAsync("Sikap, Ekskul dan Absensi", "Anda akan menghapus data Sikap, Ekskul, dan Absensi. Apa anda yakin ingin melanjutkan?", MessageDialogStyle.AffirmativeAndNegative);
            if (res == MessageDialogResult.Affirmative)
            {
                NilaiSikap ns = new NilaiSikap();
                NilaiEkskul ne = new NilaiEkskul();
                Absensi ab = new Absensi();
                ns.IdSikap = drv.Row[3].ToString();
                ne.IdEks = drv.Row[6].ToString();
                ab.IdAbsen = drv.Row[9].ToString();
                try
                {
                    if (scrd.Hapus(ns) && ecrd.Hapus(ne) && acrd.Hapus(ab))
                    {
                        await this.ShowMessageAsync("Sikap, Ekskul dan Absensi", "Data berhasil dihapus!");
                        LoadSikapDll();
                    }
                }
                catch (SQLiteException ex)
                {
                    await this.ShowMessageAsync("Sikap, Ekskul dan Absensi", $"Error! {ex.Message}");
                }
            }
        }

        private async void DelSiswa_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)e.Source).DataContext;
            SiswaCRUD sicrud = new SiswaCRUD();
            PengetahuanCRUD pcrd = new PengetahuanCRUD();
            KeterampilanCRUD kcrd = new KeterampilanCRUD();
            SikapCRUD scrd = new SikapCRUD();
            EkskulCRUD ecrd = new EkskulCRUD();
            AbsensiCRUD acrd = new AbsensiCRUD();
            RumusNilai rn = new RumusNilai();
            MessageDialogResult res = await this.ShowMessageAsync("Data Siswa", "Anda akan menghapus seluruh data yang berkaitan dengan siswa ini! Anda yakin ingin melanjutkan?", MessageDialogStyle.AffirmativeAndNegative);
            if (res == MessageDialogResult.Affirmative)
            {
                DataSiswa ds = new DataSiswa();
                string DN = drv.Row[0].ToString();
                ds.Nisn = DN;
                try
                {
                    if (rn.IsRowExist(DN, 0))
                    {
                        if (pcrd.HapusAll(DN) && kcrd.HapusAll(DN) && scrd.HapusAll(DN))
                        {
                            if (rn.IsRowExist(DN, 1))
                            {
                                if (ecrd.HapusAll(DN) && acrd.HapusAll(DN))
                                {
                                    if (sicrud.Hapus(ds))
                                    {
                                        await this.ShowMessageAsync("Data Siswa", "Data siswa dan nilai yang berkaitan sudah terhapus!");
                                        LoadSiswa();
                                        LoadSikapDll();
                                        LoadPengKet();
                                    }
                                }
                            }
                        }
                    }
                    else if (rn.IsRowExist(DN, 1))
                    {
                        if (ecrd.HapusAll(DN) && acrd.HapusAll(DN))
                        {
                            if (sicrud.Hapus(ds))
                            {
                                await this.ShowMessageAsync("Data Siswa", "Data siswa dan nilai yang berkaitan sudah terhapus!");
                                LoadSiswa();
                                LoadSikapDll();
                                LoadPengKet();
                            }
                        }
                    }
                    else
                    {
                        if (sicrud.Hapus(ds))
                        {
                            await this.ShowMessageAsync("Data Siswa", "Data siswa dan nilai yang berkaitan sudah terhapus!");
                            LoadSiswa();
                            LoadSikapDll();
                            LoadPengKet();
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    await this.ShowMessageAsync("Data Siswa", $"Error! {ex.Message}");
                }                
            }
        }
    }
}
