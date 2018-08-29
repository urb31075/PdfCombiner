<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="PdfCombinerWeb.Summary" %>
<%@ Import Namespace="PdfCombinerWeb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Summary</title>
    <link rel="stylesheet" href="PdfCombiner.css"/>
</head>
<body>
    <h1>Summary</h1>
    <h2><% this.Response.Write(DateTime.Now); %></h2>
    <h3><% this.Response.Write(Default.GetTitle());%></h3>
    <table>
        <thead>
        <tr><th>X</th><th>Y</th><th>Z</th></tr>
        </thead>
        <tbody>
        <%= this.GetTable()%>
        </tbody>
    </table>
</body>
</html>
