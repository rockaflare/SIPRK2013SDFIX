using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SIPRK2013SDFIX.RaportDb
{
    public class RaportMailMerge
    {
        public void ChangeMergeFields(string docName, Dictionary<string, string> raportDict, string targetfile)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(docName, true))
            {
                foreach (var item in raportDict)
                {
                    document.GetMergeFields(item.Key).ReplaceWithText(item.Value);
                }

            }
        }
    }
}
