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
using SIPRK2013SDFIX.RaportDb;
using SIPRK2013SDFIX.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;
using System.Threading;
using System.Data.SQLite;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for Informasi.xaml
    /// </summary>
    public partial class Informasi : MetroWindow
    {
        public Informasi()
        {
            InitializeComponent();
            CultureInfo ci = new CultureInfo("id_ID");
            ci.DateTimeFormat.LongDatePattern = "dd MMMM yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            LoadDataSekolah();
        }
        
        public void SetNisn(string nisn)
        {
            _nisn = nisn;
            GetDataSiswa();
            Tambah.IsEnabled = false;
            EditS.IsEnabled = true;
            TabSiswa.IsSelected = true;
        }

        #region method sekolah
        private async void UbahDataSekolah()
        {
            SekolahCRUD SekCrud = new SekolahCRUD();
            DataSekolah Ds = new DataSekolah();
            Ds.IdSek = 1;
            Ds.Npsn = Convert.ToInt32(Npsn.Text);
            Ds.NmSekolah = NmSekolah.Text;
            Ds.NmKepsek = NmKepsek.Text;
            Ds.NipKepsek = NipKepsek.Text;
            Ds.GuruKelas = NmGuru.Text;
            Ds.NipGuru = NipGuru.Text;
            Ds.Kelas = Kelas.Text;
            Ds.Alamat = Alamat.Text;
            Ds.Desa = Desa.Text;
            Ds.Kecamatan = Kecamatan.Text;
            Ds.Kota = Kota.Text;
            Ds.Provinsi = Provinsi.Text;
            Ds.Semester = Semester.Text;
            Ds.Tahunajar = Tahunajar.Text;
            Ds.TglRaport = TglRaport.Text;
            try
            {
                if (SekCrud.Ubah(Ds))
                {
                    await this.ShowMessageAsync("Data Sekolah", "Berhasil menyimpan data!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadDataSekolah()
        {
            SekolahCRUD sekolahCRUD = new SekolahCRUD();
            DataSekolah NewDS = sekolahCRUD.GetDataSekolah();

            Npsn.Text = NewDS.Npsn.ToString();
            NmSekolah.Text = NewDS.NmSekolah;
            Alamat.Text = NewDS.Alamat;
            NmKepsek.Text = NewDS.NmKepsek;
            NipKepsek.Text = NewDS.NipKepsek;
            NmGuru.Text = NewDS.GuruKelas;
            NipGuru.Text = NewDS.NipGuru;
            Kelas.Text = NewDS.Kelas;
            Desa.Text = NewDS.Desa;
            Kecamatan.Text = NewDS.Kecamatan;
            Kota.Text = NewDS.Kota;
            Provinsi.Text = NewDS.Provinsi;
            Semester.Text = NewDS.Semester;
            Tahunajar.Text = NewDS.Tahunajar;
            TglRaport.Text = NewDS.TglRaport;
        }
        private void SetTanggal()
        {
            CultureInfo ci = new CultureInfo("id_ID");
            ci.DateTimeFormat.LongDatePattern = "dd MMMM yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
        #endregion method sekolah

        #region method siswa
        private string _nisn { get; set; }
        private string SetTanggalLahir()
        {
            string hasil;
            if (TglLahir.SelectedDate.HasValue)
            {
                hasil = TglLahir.SelectedDate.Value.ToString("dd MMMM yyyy");
            }
            else
            {
                hasil = "";
            }
            return hasil;
        }
        private async void SimpanDataSiswa()
        {
            SiswaCRUD scrd = new SiswaCRUD();
            DataSiswa ds = new DataSiswa();
            ds.Nisn = Nisn.Text;
            ds.NoInduk = NoInduk.Text;
            ds.NmSiswa = NmSiswa.Text;
            ds.NmPanggilan = NmPanggilan.Text;
            ds.Gender = Gender.Text;
            ds.Agama = Agama.Text;
            ds.TempatLahir = TempatLahir.Text;
            ds.TglLahir = SetTanggalLahir();
            ds.PendidikanSeb = PendidikanSeb.Text;
            ds.Alamat = AlamatSiswa.Text;
            ds.NmAyah = NmAyah.Text;
            ds.NmIbu = NmIbu.Text;
            ds.JobAyah = JobAyah.Text;
            ds.JobIbu = JobIbu.Text;
            ds.Jalan = Jalan.Text;
            ds.Kecamatan = Camat.Text;
            ds.Desa = DesaSiswa.Text;
            ds.Kota = KotaSiswa.Text;
            ds.Provinsi = ProvSiswa.Text;
            ds.NmWali = NmWali.Text;
            ds.JobWali = JobWali.Text;
            ds.AlamatWali = AlamatWali.Text;
            ds.Pendengaran = Pendengaran.Text;
            ds.Penglihatan = Penglihatan.Text;
            ds.Gigi = Gigi.Text;
            ds.Berat1 = Berat1.Text;
            ds.Berat2 = Berat2.Text;
            ds.Tinggi1 = Tinggi1.Text;
            ds.Tinggi2 = Tinggi2.Text;
            try
            {
                if (scrd.Tambah(ds))
                {
                    await this.ShowMessageAsync("Data Siswa", "Berhasil menambahkan data!");
                }
            }
            catch (SQLiteException ex)
            {
                await this.ShowMessageAsync("Data Siswa", $"Terjadi kesalahan, {ex.Message}");
            }
        }
        private async void SimpanEditDataSiswa()
        {
            SiswaCRUD scrd = new SiswaCRUD();
            DataSiswa ds = new DataSiswa();
            ds.Nisn = Nisn.Text;
            ds.NoInduk = NoInduk.Text;
            ds.NmSiswa = NmSiswa.Text;
            ds.NmPanggilan = NmPanggilan.Text;
            ds.Gender = Gender.Text;
            ds.Agama = Agama.Text;
            ds.TempatLahir = TempatLahir.Text;
            ds.TglLahir = SetTanggalLahir();
            ds.PendidikanSeb = PendidikanSeb.Text;
            ds.Alamat = AlamatSiswa.Text;
            ds.NmAyah = NmAyah.Text;
            ds.NmIbu = NmIbu.Text;
            ds.JobAyah = JobAyah.Text;
            ds.JobIbu = JobIbu.Text;
            ds.Jalan = Jalan.Text;
            ds.Kecamatan = Camat.Text;
            ds.Desa = DesaSiswa.Text;
            ds.Kota = KotaSiswa.Text;
            ds.Provinsi = ProvSiswa.Text;
            ds.NmWali = NmWali.Text;
            ds.JobWali = JobWali.Text;
            ds.AlamatWali = AlamatWali.Text;
            ds.Pendengaran = Pendengaran.Text;
            ds.Penglihatan = Penglihatan.Text;
            ds.Gigi = Gigi.Text;
            ds.Berat1 = Berat1.Text;
            ds.Berat2 = Berat2.Text;
            ds.Tinggi1 = Tinggi1.Text;
            ds.Tinggi2 = Tinggi2.Text;
            try
            {
                if (scrd.Ubah(ds, _nisn))
                {
                    await this.ShowMessageAsync("Data Siswa", "Berhasil menambahkan data!");
                    EditDataWindow edw = new EditDataWindow();
                    edw.Show();
                    this.Close();
                }
            }
            catch (SQLiteException ex)
            {
                await this.ShowMessageAsync("Data Siswa", $"Terjadi kesalahan, {ex.Message}");
            }
        }
        public void GetDataSiswa()
        {
            SiswaCRUD scrd = new SiswaCRUD();
            DataSiswa ds = scrd.GetDataSiswa(_nisn);
            Nisn.Text = ds.Nisn;
            NoInduk.Text = ds.NoInduk;
            NmSiswa.Text = ds.NmSiswa;
            NmPanggilan.Text = ds.NmPanggilan;
            Agama.Text = ds.Agama;
            Gender.Text = ds.Gender;
            TempatLahir.Text = ds.TempatLahir;
            if (ds.TglLahir == "")
            {
                TglLahir.SelectedDate = null;
            }
            else
            {
                TglLahir.SelectedDate = DateTime.ParseExact(ds.TglLahir, "dd MMMM yyyy", null);
            }
            
            PendidikanSeb.Text = ds.PendidikanSeb;
            AlamatSiswa.Text = ds.Alamat;
            NmAyah.Text = ds.NmAyah;
            NmIbu.Text = ds.NmIbu;
            JobAyah.Text = ds.JobAyah;
            JobIbu.Text = ds.JobIbu;
            Jalan.Text = ds.Jalan;
            DesaSiswa.Text = ds.Desa;
            Camat.Text = ds.Kecamatan;
            KotaSiswa.Text = ds.Kota;
            ProvSiswa.Text = ds.Provinsi;
            NmWali.Text = ds.NmWali;
            JobWali.Text = ds.JobWali;
            AlamatWali.Text = ds.AlamatWali;
            Pendengaran.Text = ds.Pendengaran;
            Penglihatan.Text = ds.Penglihatan;
            Gigi.Text = ds.Gigi;
            Berat1.Text = ds.Berat1;
            Berat2.Text = ds.Berat2;
            Tinggi1.Text = ds.Tinggi1;
            Tinggi2.Text = ds.Tinggi2;
        }
        #endregion method siswa


        #region button methods
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            UbahDataSekolah();
            LoadDataSekolah();
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            SimpanDataSiswa();
        }
        #endregion button methods

        private void EditS_Click(object sender, RoutedEventArgs e)
        {
            SimpanEditDataSiswa();
            
        }
    }
}
