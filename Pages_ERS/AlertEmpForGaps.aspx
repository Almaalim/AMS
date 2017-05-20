<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertEmpForGaps.aspx.cs" Inherits="AlertEmpForGaps" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alert !</title>

    <%--script--%>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <%--script--%>
    <%--stylesheet--%>  
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />  
    <link href="../CSS/ModalPopup.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MasterPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--stylesheet--%>

</head>
<body>
    <form id="form1" runat="server" style="background:#4E6877">
    <div>
        <table cellspacing = "10" runat="server" width="100%">
            <tr>
                <td>
                    <br />
                    <div id="divAbs" runat="server" visible="false">
                        <asp:Label ID="lblMsgWrgAbs" runat="server" Text=""></asp:Label>
                    </div>
                    <br />
                    <br />
                    <div id="divGaps" runat="server" visible="false">
                        <asp:Label ID="lblMsgWrgGaps" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
