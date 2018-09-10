// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableOfContentExtractor.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   The table of content extractor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The table of content extractor.
    /// </summary>
    public class TableOfContentExtractor
    {
        /// <summary>
        /// The extract table of content.
        /// </summary>
        /// <returns>
        /// The <see cref="TableOfContentResult"/>.
        /// </returns>
        public TableOfContentResult ExtractTableOfContent(string fileName)
        {
            var tableOfContentResult = new TableOfContentResult();
            if (!File.Exists(fileName))
            {
                tableOfContentResult.Result = false;
                tableOfContentResult.LastError = "File not found!";
                return tableOfContentResult;
            }
            Microsoft.Office.Interop.Word.Application wordApplication = null;
            Microsoft.Office.Interop.Word.Document document = null;
            bool breakPoint = false;
            try
            {
                object templatePath = fileName;
                object missingObj = Missing.Value;
                wordApplication = new Microsoft.Office.Interop.Word.Application();
                document = wordApplication.Documents.Open(ref templatePath, ref missingObj, ref missingObj, ref missingObj);

                var content = string.Empty;
                var hyperlinksCount = document.TablesOfContents[1].Range.Hyperlinks.Count;
                int NextStart = 0;
                for (var i = hyperlinksCount; i > 0; i--)
                {
                    var tableOfContentData = new TableOfContentData();
                    var myRange = document.TablesOfContents[1].Range.Hyperlinks[i].Range;

                    if (myRange.Text.Contains("Защита оборудования ТСО от воздействия молниевых разрядов"))
                    {
                        var xxx = myRange.CharacterStyle;
                        var yyy = myRange.FormattedText;
                        breakPoint = true;
                    }

                    var splites = myRange.Text.Split('\t');
                    if (splites.Length > 1)
                    {
                        int page;
                        var result = int.TryParse(splites[splites.Length - 1], out page);
                        if (result)
                        {
                            for (var j = 0; j < splites.Length - 1; j++)
                            {
                                content += splites[j] + " ";
                            }
                            tableOfContentData.Page = page;
                            tableOfContentData.Content = content.Trim().Replace("  ", " ");
                            content = string.Empty;
                        }
                        else
                        {
                            content += myRange.Text;
                        }
                    }
                    else
                    {
                        content += myRange.Text;
                    }

                    var rd = new RangeDetails();

                    rd.HyperLinkStart = myRange.Start;
                    rd.HyperLinkEnd = myRange.End;
                    rd.LinkText = myRange.Text;

                    try
                    {
                        var sadd = document.TablesOfContents[1].Range.Hyperlinks[i].SubAddress;
                        var wb = document.Bookmarks[sadd];
                        if (wb != null)
                        {
                            Microsoft.Office.Interop.Word.Style style = wb.Range.get_Style();
                            tableOfContentData.StyleInfo = style.NameLocal;
                            if (breakPoint)
                            {
                                var ss = wb.Range.Text;
                                breakPoint = false;
                            }

                            rd.ContentRangeStart = wb.Range.End + 1; // content starts after the Heading so we take the end of heading
                            if (i == document.TablesOfContents[1].Range.Hyperlinks.Count)
                            {
                                // then it is the last Range and the "End" will be the endofDoc.
                                object oEndOfDoc = @"\endofdoc";
                                rd.ContentRangeEnd = document.Bookmarks.get_Item(ref oEndOfDoc).Range.End;
                            }
                            else
                            {
                                rd.ContentRangeEnd = NextStart - 1;
                            }

                            NextStart = rd.ContentRangeStart;
                            string text = document.Range(rd.ContentRangeStart, rd.ContentRangeEnd).Text;
                            var xxx = document.Range(rd.ContentRangeStart, rd.ContentRangeEnd).Fields;
                            tableOfContentResult.Items.Add(tableOfContentData);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    //contRange.Ranges.Add(rd);
                }

                document.Close();
                document = null;
                wordApplication.Quit();
                wordApplication = null;
            }
            catch (Exception ex)
            {
                tableOfContentResult.Result = false;
                tableOfContentResult.LastError = ex.Message;
            }
            finally
            {
                document?.Close();
                wordApplication?.Quit();
            }

            tableOfContentResult.Items.Reverse();
            return tableOfContentResult;
        }

        /// <summary>
        /// The table of content result.
        /// </summary>
        public class TableOfContentResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TableOfContentResult"/> class.
            /// </summary>
            public TableOfContentResult()
            {
                this.Items = new List<TableOfContentData>();
            }

            /// <summary>
            /// Gets or sets a value indicating whether result.
            /// </summary>
            public bool Result { get; set; }

            /// <summary>
            /// Gets or sets the last error.
            /// </summary>
            public string LastError { get; set; }

            /// <summary>
            /// Gets or sets the items.
            /// </summary>
            public List<TableOfContentData> Items { get; set; }
        }

        /// <summary>
        /// The table of content data.
        /// </summary>
        public class TableOfContentData
        {
            /// <summary>
            /// Gets or sets the content.
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// Gets or sets the page.
            /// </summary>
            public int Page { get; set; }

            /// <summary>
            /// Gets or sets the page amount.
            /// </summary>
            public int PageAmount { get; set; }

            public string  StyleInfo{ get; set; }
    }

        internal class RangeDetails
        {
            public int HyperLinkStart { get; set; }

            public int HyperLinkEnd { get; set; }

            public string LinkText { get; set; }

            public int ContentRangeStart { get; set; }

            public int ContentRangeEnd { get; set; }
        }
    }
}
