// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the Default type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerWeb
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.ModelBinding;

    /// <summary>
    /// The default.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// The my data model.
        /// </summary>
        public class MyDataModel
        {
            /// <summary>
            /// Gets or sets the arguments.
            /// </summary>
            [Required]
            public string MyArgument { get; set; }

            /// <summary>
            /// Gets or sets the Command.
            /// </summary>
            [Required]
            public string MyCommand { get; set; }
        }

        public static string GetTitle()
        {
            return "2123123123123123123123";
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.PostBackTimeLabel.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                var myDataModel = new MyDataModel { MyArgument = "arg", MyCommand = "cmd" };

                if (this.TryUpdateModel(myDataModel, new FormValueProvider(this.ModelBindingExecutionContext)))
                {
                    var message = myDataModel.MyArgument + " " + myDataModel.MyCommand;
                    var allertBody = $@"<script language='javascript'>alert('{message}')</script>";
                    this.Response.Write(allertBody);
                }
            }
            else
            {
                this.StartTimeLabel.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("HtmlPage1.html");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("HtmlPage2.html");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (this.willattend.Value=="page1")
            {
                this.Response.Redirect("HtmlPage1.html");
            }

            if (this.willattend.Value == "page2")
            {
                this.Response.Redirect("HtmlPage2.html");
            }
        }

        protected void SummaryButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Summary.aspx");
        }
    }
}