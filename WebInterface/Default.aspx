<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
    MasterPageFile="WebInterface.Master"
    Inherits="WebInterface.Default" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Register Src="~/Controls/IndiControl.ascx" TagPrefix="uc1" TagName="IndiControl" %>


<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
        <div>
            <label>WEB-интерфейс для PdfCombiner</label>
            <a href="https://habr.com/post/311094/" style="font-size: x-large" title="habrahabr">HABR</a>
        </div>    
        <div>
           <% 
               this.Response.Write($"<p>Command Line Parameterd: {this.CommandLineParameter}</p>");
               this.Response.Write($"<p>Route Parameters: {this.RouteParameter}</p>");

               var x = this.GetMain().ToList();
               foreach (var item in x)
               {
                   this.Response.Write($"<p>{item}</p>");
               }
           %>
        </div>
        <div>
            <%
                for (var i = 0; i < 10; i++)
                {
                    if (i == 5)
                    {
                        this.Response.Write($"<div><a href=\"Default.aspx?page=xxx{i}\" style=\"font-size: x-large\">xxx{i}</a></div>");
                    }
                    else
                    {
                        this.Response.Write($"<div><a href=\"Default.aspx?page=xxx{i}\">xxx{i}</a></div>");
                    }
                }
            %>
        </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="footerContent" runat="server">
    <%this.Response.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture)); %>
</asp:Content>
