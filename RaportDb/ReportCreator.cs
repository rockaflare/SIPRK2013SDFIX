using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Xps.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;

namespace SIPRK2013SDFIX.RaportDb
{
    public class ReportCreator
    {
        private const string semesterGanjil = @"RaportTemplate\RaportTemplateS1.dotx";
        private const string semesterGenap = @"RaportTemplate\RaportTemplateS2.dotx";

        public string GetFileDirectory(string semester)
        {
            string hasil = "";
            var projectDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (semester == "Ganjil")
            {
                hasil = Path.Combine(projectDir, semesterGanjil);
            }
            else if (semester == "Genap")
            {
                hasil = Path.Combine(projectDir, semesterGenap);
            }
            return hasil;
        }
        public static MemoryStream ReadAllBytesToMemoryStream(string path)
        {
            byte[] buffer = File.ReadAllBytes(path);
            var destStream = new MemoryStream(buffer.Length);
            destStream.Write(buffer, 0, buffer.Length);
            destStream.Seek(0, SeekOrigin.Begin);
            return destStream; 
        }
        public bool GantiMergeField(string docPath, string savePath, Dictionary<string, string> raportDict)
        {
            try
            {
                byte[] byteArray = File.ReadAllBytes(docPath);
                using (var stream = new MemoryStream())
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                    using (WordprocessingDocument document = WordprocessingDocument.Open(stream, true))
                    {
                        document.ChangeDocumentType(WordprocessingDocumentType.Document);
                        foreach (var item in raportDict)
                        {
                            document.GetMergeFields(item.Key).ReplaceWithText(item.Value);
                        }
                        document.MainDocumentPart.Document.Save();
                    }
                    stream.Position = 0;
                    File.WriteAllBytes(savePath, stream.ToArray());
                }                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName)
        {
            Application wordApplication = new Application();

            wordApplication.Documents.Add(wordDocName);

            Microsoft.Office.Interop.Word.Document doc = wordApplication.ActiveDocument;
            try
            {
                doc.SaveAs(xpsDocName, WdSaveFormat.wdFormatXPS);
                wordApplication.Quit();
                XpsDocument xpsDoc = new XpsDocument(xpsDocName, FileAccess.Read);
                return xpsDoc;
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
            return null;
        }

    }
}
