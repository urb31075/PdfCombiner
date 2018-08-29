<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PdfCombinerWeb.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="PdfCombiner.css"/>
</head>
<body>
    <form id="PdfCombinerWeb" runat="server">
        <asp:scriptmanager id="ScriptManager1" runat="server" />
        <div>
            <label>WEB-интерфейс для PdfCombiner</label>
        </div>
        <div>
            <asp:Label ID="StartTimeLabel" runat="server" Text="XX:XX:XX"></asp:Label>
        </div>
        <div>
            <asp:Label ID="PostBackTimeLabel" runat="server" Text="XX:XX:XX"></asp:Label>
        </div>
        <div>
            <label>Arguments:</label> <input type="text" runat="server" id="MyArgument" /><br />
            <label>Command:</label> <input type="text" runat="server" id="MyCommand" />
        </div>
        <div>
            <button type ="submit">Submit</button>
            <br />
            <select id="willattend" runat="server">
                <option value="">chouse Page</option>    
                <option value="page1">Page1</option>    
                <option value="page2">Page2</option>    
            </select>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button1" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button2" />
            <asp:Button ID="SummaryButton" runat="server" OnClick="SummaryButton_Click" Text="Summary" />
        </div>
    </form>
</body>
</html>
