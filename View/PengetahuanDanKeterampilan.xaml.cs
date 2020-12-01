using System;
using System.Collections.Generic;
using System.Data;
using MahApps.Metro.Controls;
using SIPRK2013SDFIX.RaportDb;
using SIPRK2013SDFIX.Model;
using MahApps.Metro.Controls.Dialogs;
using System.Data.SQLite;
using System.Windows;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for PengetahuanDanKeterampilan.xaml
    /// </summary>
    public partial class PengetahuanDanKeterampilan : MetroWindow
    {
        public PengetahuanDanKeterampilan()
        {
            InitializeComponent();
        }
        public void SetData(string SetNisn, string SetNama, int SetIdMapel, string SetMapel, string SetSemester, string SetKelas, string SetNPanggilan, int SetKkm)
        {
            _nisnSet = SetNisn;
            _namaSet = SetNama;
            _idmapelSet = SetIdMapel;
            _mapelSet = SetMapel;
            _semesterSet = SetSemester;
            _kelasSet = SetKelas;
            _nmpanggilanSet = SetNPanggilan;
            _kkmSet = SetKkm;

            NamaBox.Text = _namaSet;
            SemBox.Text = _semesterSet;
            MapelBox.Text = _mapelSet;
            KetMapelBox.Text = _mapelSet;
            LoadKDKet(_kelasSet);
            LoadKDPeng(_kelasSet);
        }
        public void SetEdit(string EditNisn, string EditNama, string EditNPanggilan, int EditMapel, string EditNMapel, string EditSemester, string IdEditPeng, string IdEditKet, string EditKelas, int EditKKM)
        {
            _nisnSet = EditNisn;
            _namaSet = EditNama;
            _nmpanggilanSet = EditNPanggilan;
            _idmapelSet = EditMapel;
            _semesterSet = EditSemester;
            _editketSet = IdEditKet;
            _editpengSet = IdEditPeng;
            _mapelSet = EditNMapel;
            _kelasSet = EditKelas;
            _kkmSet = EditKKM;

            NamaBox.Text = _namaSet;
            SemBox.Text = _semesterSet;
            MapelBox.Text = _mapelSet;
            KetMapelBox.Text = _mapelSet;
            LoadKDKet(_kelasSet);
            LoadKDPeng(_kelasSet);

            GetPengetahuan();
            GetKeterampilan();
            Simpan.IsEnabled = false;
            EditSimpan.IsEnabled = true;
        }
        #region Variabel
        private string _nisnSet { get; set; }
        private string _namaSet { get; set; }
        private int _idmapelSet { get; set; }
        private string _mapelSet { get; set; }
        private string _semesterSet { get; set; }
        private string _kelasSet { get; set; }
        private int _kkmSet { get; set; }
        private string _nmpanggilanSet { get; set; }
        private string _editpengSet { get; set; }
        private string _editketSet { get; set; }
        #endregion Variabel

        #region KeterampilanPengetahuan
        private void LoadKDPeng(string kelas)
        {
            var query = $"SELECT id_kd, no_kd FROM kompetensi_dasar WHERE id_mapel = {_idmapelSet} AND ki = 'peng' AND kelas={kelas}";
            RaportDB dBRaport = new RaportDB();
            DataTable dt = dBRaport.GetDataRaport(query);
            KDTinggiBox.ItemsSource = dt.DefaultView;
            KDRendahBox.ItemsSource = dt.DefaultView;
            KDTinggiBox.DisplayMemberPath = "no_kd";
            KDRendahBox.DisplayMemberPath = "no_kd";
            KDTinggiBox.SelectedValuePath = "id_kd";
            KDRendahBox.SelectedValuePath = "id_kd";
        }
        private void LoadKDKet(string kelas)
        {
            var query = $"SELECT id_kd, no_kd FROM kompetensi_dasar WHERE id_mapel = {_idmapelSet} AND ki = 'ket' AND kelas={kelas}";
            RaportDB dBRaport = new RaportDB();
            DataTable dt = dBRaport.GetDataRaport(query);
            KetKDTinggiBox.ItemsSource = dt.DefaultView;
            KetKDRendahBox.ItemsSource = dt.DefaultView;
            KetKDTinggiBox.DisplayMemberPath = "no_kd";
            KetKDRendahBox.DisplayMemberPath = "no_kd";
            KetKDTinggiBox.SelectedValuePath = "id_kd";
            KetKDRendahBox.SelectedValuePath = "id_kd";
        }
        #endregion KeterampilanPengetahuan

        #region RegNilaiPengetahuan
        private void GetPengetahuan()
        {
            PengetahuanCRUD pcrd = new PengetahuanCRUD();
            NilaiPengetahuan np = pcrd.GetNilaiPengetahuan(_editpengSet);
            KDTinggiBox.SelectedValue = np.KdTertinggi;
            KDRendahBox.SelectedValue = np.KdTerendah;
            NTinggiBox.Value = np.NilaiTertinggi;
            NRendahBox.Value = np.NilaiTerendah;
            NAkhirPengBox.Value = np.NilaiAkhir;
            PredikatPengBox.Text = np.PredikatPengetahuan;
            DPengBox.Text = np.DeskripsiPengetahuan;
        }
        private int SimpanPengetahuan()
        {
            int hasil = 0;
            PengetahuanCRUD pcrd = new PengetahuanCRUD();
            NilaiPengetahuan np = new NilaiPengetahuan();
            RumusNilai rn = new RumusNilai();
            np.IdPeng = "PENG" + _nisnSet + _idmapelSet + _semesterSet;
            np.Nisn = _nisnSet;
            np.IdMapel = _idmapelSet;
            np.Semester = _semesterSet;
            np.KdTertinggi = Convert.ToInt32(KDTinggiBox.SelectedValue);
            np.KdTerendah = Convert.ToInt32(KDRendahBox.SelectedValue);
            np.NilaiTertinggi = Convert.ToInt32(NTinggiBox.Value);
            np.NilaiTerendah = Convert.ToInt32(NRendahBox.Value);
            np.NilaiAkhir = Convert.ToInt32(NAkhirPengBox.Value);
            np.PredikatPengetahuan = PredikatPengBox.Text;
            np.DeskripsiPengetahuan = DPengBox.Text;
            try
            {
                if (pcrd.Tambah(np))
                {
                    hasil = 181197;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    hasil = 19;
                }
                else
                {
                    hasil = ex.ErrorCode;
                }
            }
            return hasil;
        }
        private int EditPengetahuan()
        {
            int hasil = 0;
            PengetahuanCRUD pcrd = new PengetahuanCRUD();
            NilaiPengetahuan np = new NilaiPengetahuan();
            RumusNilai rn = new RumusNilai();
            np.IdPeng = _editpengSet;
            np.Nisn = _nisnSet;
            np.IdMapel = _idmapelSet;
            np.Semester = _semesterSet;
            np.KdTertinggi = Convert.ToInt32(KDTinggiBox.SelectedValue);
            np.KdTerendah = Convert.ToInt32(KDRendahBox.SelectedValue);
            np.NilaiTertinggi = Convert.ToInt32(NTinggiBox.Value);
            np.NilaiTerendah = Convert.ToInt32(NRendahBox.Value);
            np.NilaiAkhir = Convert.ToInt32(NAkhirPengBox.Value);
            np.PredikatPengetahuan = PredikatPengBox.Text;
            np.DeskripsiPengetahuan = DPengBox.Text;
            try
            {
                if (pcrd.Ubah(np))
                {
                    hasil = 181197;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    hasil = 19;
                }
                else
                {
                    hasil = ex.ErrorCode;
                }
            }
            return hasil;
        }
        #endregion RegNilaiPengetahuan

        #region RegNilaiKeterampilan
        private void GetKeterampilan()
        {
            KeterampilanCRUD kcrd = new KeterampilanCRUD();
            NilaiKeterampilan nk = kcrd.GetNilaiKeterampilan(_editketSet);
            KetKDTinggiBox.SelectedValue = nk.KdTertinggi;
            KetKDRendahBox.SelectedValue = nk.KdTerendah;
            KetNTinggiBox.Value = nk.NilaiTertinggi;
            KetNRendahBox.Value = nk.NilaiTerendah;
            NAkhirKetBox.Value = nk.NilaiAkhir;
            PredikatKetBox.Text = nk.PredikatKeterampilan;
            DKetBox.Text = nk.DeskripsiKeterampilan;
        }
        private int SimpanKeterampilan()
        {
            int hasil = 0;
            KeterampilanCRUD kcrd = new KeterampilanCRUD();
            NilaiKeterampilan nk = new NilaiKeterampilan();
            RumusNilai rn = new RumusNilai();
            nk.IdKet = "KET" + _nisnSet + _idmapelSet + _semesterSet;
            nk.Nisn = _nisnSet;
            nk.IdMapel = _idmapelSet;
            nk.Semester = _semesterSet;
            nk.KdTertinggi = Convert.ToInt32(KetKDTinggiBox.SelectedValue);
            nk.KdTerendah = Convert.ToInt32(KetKDRendahBox.SelectedValue);
            nk.NilaiTertinggi = Convert.ToInt32(KetNTinggiBox.Value);
            nk.NilaiTerendah = Convert.ToInt32(KetNRendahBox.Value);
            nk.NilaiAkhir = Convert.ToInt32(NAkhirKetBox.Value);
            nk.PredikatKeterampilan = PredikatKetBox.Text;
            nk.DeskripsiKeterampilan = DKetBox.Text;
            try
            {
                if (kcrd.Tambah(nk))
                {
                    hasil = 181197;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    hasil = 19;
                }
                else
                {
                    hasil = ex.ErrorCode;
                }
            }
            return hasil;
        }
        private int EditKeterampilan()
        {
            int hasil = 0;
            KeterampilanCRUD kcrd = new KeterampilanCRUD();
            NilaiKeterampilan nk = new NilaiKeterampilan();
            RumusNilai rn = new RumusNilai();
            nk.IdKet = _editketSet;
            nk.Nisn = _nisnSet;
            nk.IdMapel = _idmapelSet;
            nk.Semester = _semesterSet;
            nk.KdTertinggi = Convert.ToInt32(KetKDTinggiBox.SelectedValue);
            nk.KdTerendah = Convert.ToInt32(KetKDRendahBox.SelectedValue);
            nk.NilaiTertinggi = Convert.ToInt32(KetNTinggiBox.Value);
            nk.NilaiTerendah = Convert.ToInt32(KetNRendahBox.Value);
            nk.NilaiAkhir = Convert.ToInt32(NAkhirKetBox.Value);
            nk.PredikatKeterampilan = PredikatKetBox.Text;
            nk.DeskripsiKeterampilan = DKetBox.Text;
            try
            {
                if (kcrd.Ubah(nk))
                {
                    hasil = 181197;
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 19)
                {
                    hasil = 19;
                }
                else
                {
                    hasil = ex.ErrorCode;
                }
            }
            return hasil;
        }
        #endregion RegNilaiKeterampilan

        private void DesKetButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RumusNilai rn = new RumusNilai();
            string predikatHigh = rn.Predikat(Convert.ToInt32(KetNTinggiBox.Value), _kkmSet);
            NAkhirKetBox.Value = (Convert.ToInt32(KetNTinggiBox.Value) + Convert.ToInt32(KetNRendahBox.Value)) / 2;
            PredikatKetBox.Text = rn.Predikat(Convert.ToInt32(NAkhirKetBox.Value), _kkmSet);
            DKetBox.Text = rn.Deskripsi(rn.DeskripsiKD(Convert.ToInt32(KetKDRendahBox.SelectedValue)), rn.DeskripsiKD(Convert.ToInt32(KetKDTinggiBox.SelectedValue)), _nmpanggilanSet, predikatHigh);
        }

        private void DesPengButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RumusNilai rn = new RumusNilai();
            string predikatHigh = rn.Predikat(Convert.ToInt32(NTinggiBox.Value), _kkmSet);
            NAkhirPengBox.Value = (Convert.ToInt32(NTinggiBox.Value) + Convert.ToInt32(NRendahBox.Value)) / 2;
            PredikatPengBox.Text = rn.Predikat(Convert.ToInt32(NAkhirPengBox.Value), _kkmSet);
            DPengBox.Text = rn.Deskripsi(rn.DeskripsiKD(Convert.ToInt32(KDRendahBox.SelectedValue)), rn.DeskripsiKD(Convert.ToInt32(KDTinggiBox.SelectedValue)), _nmpanggilanSet, predikatHigh);
        }

        private async void Simpan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SimpanKeterampilan() == 181197 && SimpanPengetahuan() == 181197)
            {
                await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Data nilai berhasil disimpan!");
                this.Close();
            }
            else if (SimpanKeterampilan() == 19 && SimpanPengetahuan() == 19)
            {
                await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Data yang sama sudah ada!");
            }
            else
            {
                await this.ShowMessageAsync("Error", "Terjadi kesalahan! Silahkan periksa kembali!");
            }
        }

        private async void EditSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (EditKeterampilan() == 181197 && EditPengetahuan() == 181197)
            {
                await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Data nilai berhasil disimpan!");
                EditDataWindow edw = new EditDataWindow();
                edw.TabPengket.IsSelected = true;
                edw.Show();
                this.Close();
            }
            else if (EditKeterampilan() == 19 && EditPengetahuan() == 19)
            {
                await this.ShowMessageAsync("Pengetahuan dan Keterampilan", "Data yang sama sudah ada!");
            }
            else
            {
                await this.ShowMessageAsync("Error", "Terjadi kesalahan! Silahkan periksa kembali!");
            }
        }
    }
}
