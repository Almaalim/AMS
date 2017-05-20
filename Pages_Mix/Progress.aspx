<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Progress.aspx.cs" Inherits="Progress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table>
                <tr>
                    <td> <img src="../images/Progress/Progress128.gif" alt=""/> </td>
                </tr>
                <tr>
                    <td> <asp:Label ID="lblUpdateProgress" runat="server" Text="...الرجاء الإنتظار" Font-Bold="True" Font-Size="XX-Large" ForeColor="#22EB17"></asp:Label> </td>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
