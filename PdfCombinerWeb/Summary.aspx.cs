using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PdfCombinerWeb
{
    public partial class Summary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetTable()
        {
            var x = 5;
            var y = 10;
            var z = 20;
            var rows = string.Empty;
            for (var i = 0; i < 10; i++)
            {
                var line = $"<tr><th>XXX={x++}</th><th>YYY={y++}</th><th>ZZZ={z++}</th></tr>";
                rows += line;
            }
            return rows;
        }
    }
}