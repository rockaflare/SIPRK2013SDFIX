using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SIPRK2013SDFIX.RaportDb;
using SIPRK2013SDFIX.Model;
using System.Data.SQLite;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for NilaiDanAbsen.xaml
    /// </summary>
    public partial class NilaiDanAbsen : MetroWindow
    {   
        public NilaiDanAbsen()
        {
            InitializeComponent();
        }
        public void SetData(string SetNisn, string SetNama, string SetSemester, string SetNPanggilan)
        {
            _nisnSet = SetNisn;
            _namaSet = SetNama;
            _semesterSet = SetSemester;
            _nmpanggilanSet = SetNPanggilan;

            NamaBox.Text = _namaSet;
            SemBox.Text = _semesterSet;
        }
        public void SetEdit(string EditNisn, string EditNama, string EditSemester, string IdSikapEdit, string IdEksEdit, string IdAbsenEdit)
        {
            _idsikapSet = IdSikapEdit;
            _ideksSet = IdEksEdit;
            _idabsenSet = IdAbsenEdit;
            _nisnSet = EditNisn;
            _namaSet = EditNama;
            _semesterSet = EditSemester;

            NamaBox.Text = _namaSet;
            SemBox.Text = _semesterSet;
            GetDataSikap();
            GetDataEkskul();
            GetDataAbsen();
            SimpanAll.IsEnabled = false;
            SimpanEditAll.IsEnabled = true;
        }
        #region Variabel
        private string _nisnSet { get; set; }
        private string _namaSet { get; set; }
        private string _semesterSet { get; set; }
        private string _nmpanggilanSet { get; set; }
        private string ErrorMessage = "";
        #endregion Variabel

        #region VariabelEdit
        private string _idsikapSet { get; set; }
        private string _ideksSet { get; set; }
        private string _idabsenSet { get; set; }
        #endregion VariabelEdit

        #region CekData        
        private void CekData()
        {
            
        }
        #endregion CekData

        #region SaveData
        private bool SimpanSikap()
        {
            bool isWork = false;
            SikapCRUD scrd = new SikapCRUD();
            NilaiSikap ns = new NilaiSikap();
            ns.IdSikap = "SIK" + _nisnSet + _semesterSet;
            ns.Nisn = _nisnSet;
            ns.Semester = _semesterSet;
            ns.Beribadah = Convert.ToInt32(IbadahBox.Value);
            ns.Bersyukur = Convert.ToInt32(SyukurBox.Value);
            ns.Berdoa = Convert.ToInt32(DoaBox.Value);
            ns.Toleransi = Convert.ToInt32(ToleransiBox.Value);
            ns.DeskripsiKi1 = DSpiritBox.Text;
            ns.Jujur = Convert.ToInt32(JujurBox.Value);
            ns.Disiplin = Convert.ToInt32(DisiplinBox.Value);
            ns.TanggungJawab = Convert.ToInt32(TJBox.Value);
            ns.Peduli = Convert.ToInt32(PeduliBox.Value);
            ns.Santun = Convert.ToInt32(SantunBox.Value);
            ns.PercayaDiri = Convert.ToInt32(PDBox.Value);
            ns.Kerjasama = Convert.ToInt32(KerjasamaBox.Value);
            ns.DeskripsiKi2 = DSosialBox.Text;
            try
            {
                if (scrd.Tambah(ns))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Nilai Sikap Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private bool SimpanEkskul()
        {
            bool isWork = false;
            EkskulCRUD ecrd = new EkskulCRUD();
            NilaiEkskul ne = new NilaiEkskul();
            ne.IdEks = "EKS" + _nisnSet + _semesterSet;
            ne.Nisn = _nisnSet;
            ne.Semester = _semesterSet;
            ne.Eskul1 = Ekskul1Box.Text;
            ne.Eskul2 = Ekskul2Box.Text;
            ne.Eskul3 = Ekskul3Box.Text;
            ne.Nilai1 = NEkskul1Box.Text;
            ne.Nilai2 = NEkskul2Box.Text;
            ne.Nilai3 = NEkskul3Box.Text;
            try
            {
                if (ecrd.Tambah(ne))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Nilai Ekskul Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private bool SimpanAbsensi()
        {
            bool isWork = false;
            AbsensiCRUD acrd = new AbsensiCRUD();
            Absensi ab = new Absensi();
            ab.IdAbsen = "ABS" + _nisnSet + _semesterSet;
            ab.Nisn = _nisnSet;
            ab.Semester = _semesterSet;
            ab.Sakit = Convert.ToInt32(SakitBox.Value);
            ab.Ijin = Convert.ToInt32(IzinBox.Value);
            ab.Alpha = Convert.ToInt32(AlphaBox.Value);
            try
            {
                if (acrd.Tambah(ab))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Absen Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private async void SimpanAllFunc()
        {
            if (SimpanSikap() && SimpanEkskul() && SimpanAbsensi())
            {
                await this.ShowMessageAsync("Nilai dan Absen", "Seluruh data berhasil tersimpan!");
            }
            else
            {
                await this.ShowMessageAsync("Nilai dan Absen", ErrorMessage);
            }
        }
        #endregion SaveData

        #region EditData
        private void GetDataSikap()
        {
            SikapCRUD scrd = new SikapCRUD();
            NilaiSikap ns = scrd.GetNilaiSikap(_idsikapSet);
            IbadahBox.Value = Convert.ToInt32(ns.Beribadah);
            SyukurBox.Value = Convert.ToInt32(ns.Bersyukur);
            DoaBox.Value = Convert.ToInt32(ns.Berdoa);
            ToleransiBox.Value = Convert.ToInt32(ns.Toleransi);
            DSpiritBox.Text = ns.DeskripsiKi1;
            JujurBox.Value = Convert.ToInt32(ns.Jujur);
            DisiplinBox.Value = Convert.ToInt32(ns.Disiplin);
            TJBox.Value = Convert.ToInt32(ns.TanggungJawab);
            PeduliBox.Value = Convert.ToInt32(ns.Peduli);
            SantunBox.Value = Convert.ToInt32(ns.Santun);
            PDBox.Value = Convert.ToInt32(ns.PercayaDiri);
            KerjasamaBox.Value = Convert.ToInt32(ns.Kerjasama);
            DSosialBox.Text = ns.DeskripsiKi2;
        }
        private void GetDataEkskul()
        {
            EkskulCRUD ecrd = new EkskulCRUD();
            NilaiEkskul ne = ecrd.GetNilaiEkskul(_ideksSet);
            Ekskul1Box.Text = ne.Eskul1;
            Ekskul2Box.Text = ne.Eskul2;
            Ekskul3Box.Text = ne.Eskul3;
            NEkskul1Box.Text = ne.Nilai1;
            NEkskul2Box.Text = ne.Nilai2;
            NEkskul3Box.Text = ne.Nilai3;
        }
        private void GetDataAbsen()
        {
            AbsensiCRUD acrd = new AbsensiCRUD();
            Absensi ab = acrd.GetAbsensi(_idabsenSet);
            SakitBox.Value = Convert.ToInt32(ab.Sakit);
            IzinBox.Value = Convert.ToInt32(ab.Ijin);
            AlphaBox.Value = Convert.ToInt32(ab.Alpha);
        }
        private bool SimpanEditSikap()
        {
            bool isWork = false;
            SikapCRUD scrd = new SikapCRUD();
            NilaiSikap ns = new NilaiSikap();
            ns.IdSikap = _idsikapSet;
            ns.Nisn = _nisnSet;
            ns.Semester = _semesterSet;
            ns.Beribadah = Convert.ToInt32(IbadahBox.Value);
            ns.Bersyukur = Convert.ToInt32(SyukurBox.Value);
            ns.Berdoa = Convert.ToInt32(DoaBox.Value);
            ns.Toleransi = Convert.ToInt32(ToleransiBox.Value);
            ns.DeskripsiKi1 = DSpiritBox.Text;
            ns.Jujur = Convert.ToInt32(JujurBox.Value);
            ns.Disiplin = Convert.ToInt32(DisiplinBox.Value);
            ns.TanggungJawab = Convert.ToInt32(TJBox.Value);
            ns.Peduli = Convert.ToInt32(PeduliBox.Value);
            ns.Santun = Convert.ToInt32(SantunBox.Value);
            ns.PercayaDiri = Convert.ToInt32(PDBox.Value);
            ns.Kerjasama = Convert.ToInt32(KerjasamaBox.Value);
            ns.DeskripsiKi2 = DSosialBox.Text;
            try
            {
                if (scrd.Ubah(ns))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Nilai Sikap Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private bool SimpanEditEkskul()
        {
            bool isWork = false;
            EkskulCRUD ecrd = new EkskulCRUD();
            NilaiEkskul ne = new NilaiEkskul();
            ne.IdEks = _ideksSet;
            ne.Nisn = _nisnSet;
            ne.Semester = _semesterSet;
            ne.Eskul1 = Ekskul1Box.Text;
            ne.Eskul2 = Ekskul2Box.Text;
            ne.Eskul3 = Ekskul3Box.Text;
            ne.Nilai1 = NEkskul1Box.Text;
            ne.Nilai2 = NEkskul2Box.Text;
            ne.Nilai3 = NEkskul3Box.Text;
            try
            {
                if (ecrd.Ubah(ne))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Nilai Ekskul Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private bool SimpanEditAbsensi()
        {
            bool isWork = false;
            AbsensiCRUD acrd = new AbsensiCRUD();
            Absensi ab = new Absensi();
            ab.IdAbsen = _idabsenSet;
            ab.Nisn = _nisnSet;
            ab.Semester = _semesterSet;
            ab.Sakit = Convert.ToInt32(SakitBox.Value);
            ab.Ijin = Convert.ToInt32(IzinBox.Value);
            ab.Alpha = Convert.ToInt32(AlphaBox.Value);
            try
            {
                if (acrd.Ubah(ab))
                {
                    isWork = true;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    ErrorMessage = "Absen Siswa sudah ada!";
                }
                else
                {
                    ErrorMessage = "Terjadi kesalahan! Silahkan periksa data kembali!";
                }
            }
            return isWork;
        }
        private async void SimpanAllEditFunc()
        {
            if (SimpanEditSikap() && SimpanEditEkskul() && SimpanEditAbsensi())
            {
                await this.ShowMessageAsync("Nilai dan Absen", "Seluruh data berhasil tersimpan!");
                EditDataWindow edw = new EditDataWindow();
                edw.TabNilai.IsSelected = true;
                edw.Show();
                this.Close();
            }
            else
            {
                await this.ShowMessageAsync("Nilai dan Absen", ErrorMessage);
            }
        }
        #endregion EditData

        private void SimpanAll_Click(object sender, RoutedEventArgs e)
        {
            SimpanAllFunc();
            this.Close();
        }

        private void DeskripsiButt_Click(object sender, RoutedEventArgs e)
        {
            SikapCRUD sk = new SikapCRUD();
            DSpiritBox.Text = sk.SpiritualDes(Convert.ToInt32(IbadahBox.Value), Convert.ToInt32(SyukurBox.Value), Convert.ToInt32(DoaBox.Value), Convert.ToInt32(ToleransiBox.Value), _nmpanggilanSet);
            DSosialBox.Text = sk.SosialDes(Convert.ToInt32(JujurBox.Value), Convert.ToInt32(DisiplinBox.Value), Convert.ToInt32(TJBox.Value), Convert.ToInt32(PeduliBox.Value), Convert.ToInt32(SantunBox.Value), Convert.ToInt32(PDBox.Value), Convert.ToInt32(KerjasamaBox.Value), _nmpanggilanSet);
        }

        private void SimpanEditAll_Click(object sender, RoutedEventArgs e)
        {
            SimpanAllEditFunc();
        }
    }
}
