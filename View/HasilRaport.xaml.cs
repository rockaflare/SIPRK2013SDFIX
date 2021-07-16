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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SIPRK2013SDFIX.RaportDb;
using Microsoft.Office.Interop.Word;
using System.Windows.Xps.Packaging;
using System.IO;

namespace SIPRK2013SDFIX.View
{
    /// <summary>
    /// Interaction logic for HasilRaport.xaml
    /// </summary>
    public partial class HasilRaport : MetroWindow
    {
        public HasilRaport()
        {
            InitializeComponent();
        }

        private string Nisn { get; set; }
        private string Semester { get; set; }
        
        public void SetRaport(string nisn, string semester)
        {
            Nisn = nisn;
            Semester = semester;

            ShowRaport();
        }

        private async void ShowRaport()
        {
            var DocDir = AppDomain.CurrentDomain.BaseDirectory;
            string newXpsDocs = Path.Combine(DocDir, @"Documents\HasilRaport.xps");
            string newDocPath = Path.Combine(DocDir, @"Documents\HasilRaport.docx");
            if (Nisn != "" && Semester != "")
            {
                AddRaport adr = new AddRaport();
                ReportCreator rc = new ReportCreator();

                string docPath = rc.GetFileDirectory(Semester);
                Dictionary<string, string> raportDict = new Dictionary<string, string>();
                raportDict = adr.GetRaportView(Nisn, Semester);
                try
                {
                    if (rc.GantiMergeField(docPath, newDocPath, raportDict))
                    {
                        Raportviewer.Document = rc.ConvertWordDocToXPSDoc(newDocPath, newXpsDocs).GetFixedDocumentSequence();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Gagal", "Gagal nih");
                        this.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
    }
}
