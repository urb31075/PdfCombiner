// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the MainForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfMerger
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Wordprocessing;

    using FastReport;
    using FastReport.Export.Pdf;

    using Xfinium.Pdf;

    using File = System.IO.File;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The path.
        /// </summary>
        private const string SrcPath = @"D:\URB31075\InteractivePdfCreator\DocSource\SoderganieBlanc.docx";
        //private const string SrcPath = @"D:\URB31075\InteractivePdfCreator\DocSource\TestDoc.docx";
        private const string OutDocx   = @"D:\out.docx";
        private const string OutPdf    = @"D:\out.pdf";
        private const string OutMerged = @"D:\outmerged.pdf";
        private const string A1Format  = @"D:\PdfStorage\A1Format.pdf";
        private const string A2Format  = @"D:\PdfStorage\A2Format.pdf";
        private const string AxFormat  = @"D:\PdfStorage\AxFormat.pdf";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The main form_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            this.InfoListBox.Items.Clear();
        }

        private Paragraph addLinkToParagraph(WordprocessingDocument doc, Paragraph paragraph1, int id)
        {
            Body body = doc.MainDocumentPart.Document.Body;

            BookmarkStart bookmarkStart = new BookmarkStart { Name = @"text_of_tips_"+id, Id = id.ToString() };
            BookmarkEnd bookmarkEnd = new BookmarkEnd() { Id = id.ToString() };
            body.InsertBefore<BookmarkStart>(bookmarkStart, paragraph1);
            body.InsertAfter<BookmarkEnd>(bookmarkEnd, paragraph1);

            Paragraph paragraph3 = new Paragraph();

            Run run2 = new Run { RsidRunAddition = "009B0519" };
            FieldChar fieldChar1 = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            run2.Append(fieldChar1);
            Run run3 = new Run { RsidRunAddition = "009B0519" };
            FieldCode fieldCode1 = new FieldCode(); // { Space = SpaceProcessingModeValues.Preserve };
            fieldCode1.Text = string.Format(" REF text_of_tips_{0} \\h ", id); // Link to bookmark p_1
            run3.Append(fieldCode1);
            Run run4 = new Run() { RsidRunAddition = "009B0519" };
            FieldChar fieldChar2 = new FieldChar { FieldCharType = FieldCharValues.Separate };
            run4.Append(fieldChar2);
            Run run5 = new Run() { RsidRunAddition = "009B0519" };

            Text text2 = new Text();
            //paragraph1.InnerText;
            text2.Text = paragraph1.InnerText; //"name of smeta from output documents of Investor " + id;
            run5.Append(text2);

            Run run6 = new Run();
            FieldChar fieldChar3 = new FieldChar() { FieldCharType = FieldCharValues.End };
            run6.Append(fieldChar3);
            paragraph3.Append(run2);
            paragraph3.Append(run3);
            paragraph3.Append(run4);
            paragraph3.Append(run5);
            paragraph3.Append(run6);
            return paragraph3;
        }

        /// <summary>
        /// The create toc button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CreateTocButtonClick(object sender, EventArgs e)
        {
            if (File.Exists(OutDocx))
            {
              File.Delete(OutDocx);    
            }

            WordprocessingDocument.Open(SrcPath, true).SaveAs(OutDocx).Close();

            using (var wordprocessingDocument = WordprocessingDocument.Open(OutDocx, true))
            {
                var body = wordprocessingDocument.MainDocumentPart.Document.Body;

                //body.AppendChild(new Paragraph(new Run(new Break { Type = BreakValues.Page })));
                var tocItemsParagraphList = new List<Paragraph>();
                var pagenumberItemsParagraphList = new List<Paragraph>();
                for (var i = 0; i < 20; i++)
                {
                    body.AppendChild(new Paragraph(new Run(new Break { Type = BreakValues.Page })));
                    var p1 = body.AppendChild(new Paragraph(new Run(new Text("name of smeta from output documents of Investor " + (i + 1)))));
                    tocItemsParagraphList.Add(p1);

                    var p2 = body.AppendChild(new Paragraph(new Run(new Text((i + 4).ToString()))));
                    pagenumberItemsParagraphList.Add(p2);

                    body.AppendChild(new Paragraph(new Run(new Text("added text1 of paragraph " + (i + 1)))));
                    body.AppendChild(new Paragraph(new Run(new Text("added text2 of paragraph " + (i + 1)))));
                    body.AppendChild(new Paragraph(new Run(new Text("added text3 of paragraph " + (i + 1)))));
                    body.AppendChild(new Paragraph(new Run(new Text("added text4 of paragraph " + (i + 1)))));
                }

                //https://social.msdn.microsoft.com/Forums/sqlserver/en-US/09010c46-a3f8-42a8-b53c-c3394dc46e37/create-bookmarks-open-xml?forum=oxmlsdk

                var myTable = body.Descendants<Table>().First();
                var theRow = myTable.Elements<TableRow>().Last();
                for (var i = 0; i < 20; i++)
                {
                    TableRow rowCopy = (TableRow)theRow.CloneNode(true);

                    var runProperties = GetRunPropertyFromTableCell(rowCopy, 0);
                    var run = new Run(new Text(i + " 1"));
                    run.PrependChild<RunProperties>(runProperties);

                    rowCopy.Descendants<TableCell>().ElementAt(0).RemoveAllChildren<Paragraph>();
                    rowCopy.Descendants<TableCell>().ElementAt(0).Append(new Paragraph(new Run(new Text(string.Format("№ {0}", i + 1)))));

                    rowCopy.Descendants<TableCell>().ElementAt(1).RemoveAllChildren<Paragraph>();
                    var p1 = this.addLinkToParagraph(wordprocessingDocument, tocItemsParagraphList[i], i);
                    rowCopy.Descendants<TableCell>().ElementAt(1).Append(p1);

                    rowCopy.Descendants<TableCell>().ElementAt(2).RemoveAllChildren<Paragraph>();
                    //var p2 = this.addLinkToParagraph(wordprocessingDocument, pagenumberItemsParagraphList[i], i);
                    //rowCopy.Descendants<TableCell>().ElementAt(2).Append(p2);
                    rowCopy.Descendants<TableCell>().ElementAt(2).Append(new Paragraph(new Run(new Text(string.Format("Прим. {0}", i + 1)))));

                    myTable.AppendChild(rowCopy);
                }

                myTable.RemoveChild(theRow); //// you may want to remove this line. I have it because in my code i always have a empty row last in the table that i copy.

                wordprocessingDocument.Close();
            }



            Process.Start(OutDocx);
        }

        private static RunProperties GetRunPropertyFromTableCell(TableRow rowCopy, int cellIndex)
        {
            var runProperties = new RunProperties();
            var fontname = "Calibri";
            var fontSize = "18";
            try
            {
                fontname =
                    rowCopy.Descendants<TableCell>()
                       .ElementAt(cellIndex)
                       .GetFirstChild<Paragraph>()
                       .GetFirstChild<ParagraphProperties>()
                       .GetFirstChild<ParagraphMarkRunProperties>()
                       .GetFirstChild<RunFonts>()
                       .Ascii;
            }
            catch
            {
                //swallow
            }
            try
            {
                fontSize =
                       rowCopy.Descendants<TableCell>()
                          .ElementAt(cellIndex)
                          .GetFirstChild<Paragraph>()
                          .GetFirstChild<ParagraphProperties>()
                          .GetFirstChild<ParagraphMarkRunProperties>()
                          .GetFirstChild<FontSize>()
                          .Val;
            }
            catch
            {
                //swallow
            }
            runProperties.AppendChild(new RunFonts() { Ascii = fontname });
            runProperties.AppendChild(new FontSize() { Val = fontSize });
            return runProperties;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var startdDateTime = DateTime.Now;
            this.XfiniumPageInserting(OutPdf, OutMerged);
            this.InfoListBox.Items.Add("Finish OutMerged" + (DateTime.Now - startdDateTime));
            Process.Start(OutMerged);
        }

        void XfiniumPageInserting(string inputFileName, string outFileName)
        {
            if (File.Exists(outFileName))
            {
                File.Delete(outFileName);
            }

            using (var inputFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            {
                var formated = new PdfFixedDocument(inputFileStream);
                var document = new PdfFixedDocument();
                for (var i = 0; i < 10; i++)
                {
                    var page = formated.Pages[i];
                    document.Pages.Add(page);
                }

                using (var a1St = new FileStream(A1Format, FileMode.Open, FileAccess.Read))
                {
                    var a1Fd = new PdfFixedDocument(a1St);
                    document.Pages.Add(a1Fd.Pages[0]);
                }

                using (var a2St = new FileStream(A2Format, FileMode.Open, FileAccess.Read))
                {
                    var a2Fd = new PdfFixedDocument(a2St);
                    document.Pages.Add(a2Fd.Pages[0]);
                }

                using (var axSt = new FileStream(AxFormat, FileMode.Open, FileAccess.Read))
                {
                    var axFd = new PdfFixedDocument(axSt);
                    document.Pages.Add(axFd.Pages[0]);
                }

                for (var i = 13; i < formated.Pages.Count; i++)
                {
                    var page = formated.Pages[i];
                    document.Pages.Add(page);
                }

                using (var fileStream = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
                {
                    document.Save(fileStream);
                }
            }
        }

        /// <summary>
        /// The convert button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ConvertButtonClick(object sender, EventArgs e)
        {
            var wordApplication = new Microsoft.Office.Interop.Word.Application();

            var wordDocument = wordApplication.Documents.Open(OutDocx, Visible: false, ReadOnly: true);
            wordDocument.ExportAsFixedFormat(OutPdf, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            // ReSharper disable once RedundantCast
            ((Microsoft.Office.Interop.Word._Document)wordDocument).Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);

            // ReSharper disable once RedundantCast
            ((Microsoft.Office.Interop.Word._Application)wordApplication).Quit();
            Marshal.ReleaseComObject(wordApplication);
            Process.Start(OutPdf);
        }

        private void FrTocButton_Click(object sender, EventArgs e)
        {
            var tocDataList1 = new List<TocData>();
            for (var i = 0; i < 46; i++)
            {
                tocDataList1.Add(new TocData {PageNumber = 2*i + 2, PageTitle = $"PageTitle{i + 2}" });
            }

            var tocDataList2 = new List<TocData>();
            for (var i = 46; i < 120; i++)
            {
                tocDataList2.Add(new TocData { PageNumber = 2*i + 2, PageTitle = $"PageTitle{i + 2}" });
            }

            var srcData = new List<SrcData>();
            for (var i = 0; i < 500; i++)
            {
                srcData.Add(new SrcData {PageLabel = $"page {i}"});
            }

            var report = new Report();
            report.Load(@"D:\TocReport.frx");
            report.RegisterData(tocDataList1, "Toc1");
            report.RegisterData(tocDataList2, "Toc2");
            report.RegisterData(srcData, "Src");

            report.Design(false);
        }

        public class TocData
        {
            public int PageNumber { get; set; }

            public string PageTitle { get; set; }
        }

        public class SrcData
        {
            public string PageLabel { get; set; }
        }

        private void FrTovPdfButtonClick(object sender, EventArgs e)
        {
            var probeTocDataList = new List<TocData>();
            var tocDataList1 = new List<TocData>();
            var tocDataList2 = new List<TocData>();
            var maxItemAmount = 0;
            for (var i = 0; i < 150; i++)
            {
                var probeReport = new Report();
                probeReport.Load(@"D:\TocReport.frx");
                probeReport.Pages[1].Visible = false;
                probeReport.Pages[2].Visible = false;
                probeTocDataList.Add(new TocData { PageNumber = i + 2, PageTitle = $"PageTitle{i + 1}" });
                probeReport.RegisterData(probeTocDataList, "Toc1");
                probeReport.Prepare();
                var count = Convert.ToInt32(probeReport.GetParameter("TotalPages").Value);
                this.InfoListBox.Refresh();
                if (count > 1)
                {
                    maxItemAmount = i - 1;
                    break;
                }

                probeReport.Dispose();
            }

            var report = new Report();
            report.Load(@"D:\TocReport.frx");
            report.Pages[1].Visible = true;
            report.Pages[2].Visible = true;

            for (var i = 0; i < maxItemAmount; i++)
            {
                tocDataList1.Add(new TocData { PageNumber = 2 * i + 2, PageTitle = $"PageTitle{i + 1}" });
            }

            for (var i = maxItemAmount; i < 150; i++)
            {
                tocDataList2.Add(new TocData { PageNumber = 2 * i + 2, PageTitle = $"PageTitle{i + 1}" });
            }

            var srcData = new List<SrcData>();
            for (var i = 0; i < 500; i++)
            {
                srcData.Add(new SrcData { PageLabel = $"page {i}" });
            }

            report.RegisterData(tocDataList1, "Toc1");
            report.RegisterData(tocDataList2, "Toc2");
            report.RegisterData(srcData, "Src");
            report.Prepare();

            var excelExport = new PDFExport { OpenAfterExport = true };
            report.Export(excelExport, "D:\\out.pdf");

        }

        private void SelectDirButtonClick(object sender, EventArgs e)
        {

        }
    }
}
