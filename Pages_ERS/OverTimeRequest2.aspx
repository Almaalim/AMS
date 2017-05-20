<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverTimeRequest2.aspx.cs"
    Inherits="OverTimeRequest2" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl"
    TagPrefix="Almaalim" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        function showWait() {
            //if ($get('fudReqFile').value.length > 0) {
            $get('upWaiting').style.display = 'block';
            //}
        }
    </script>
    <title>Overtime Request </title>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
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
    <form id="form1" runat="server" style="background: #4E6877">
    <div>
        <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="divWaiting">
                    <center>
                        <iframe id="ifrmProgress" runat="server" src="~/Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="600px" width="650px" ></iframe> 
                    </center>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <%--UpdateMode="Conditional"--%>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
            <ContentTemplate>
                <table>
                    <div id="divName" runat="server" style="width: 100%">
                        <tr class="rp_Search">
                            <td class="td1Allalign">
                                <center>
                                    <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1"></asp:Label>
                                </center>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td class="td2Allalign" colspan="2">
                            <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                                EnableClientScript="False" CssClass="MsgValidation" 
                                meta:resourcekey="vsumAllResource1" />
                        </td>
                    </tr>
                    <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </td>
                </tr>
                    <caption>
                        <tr>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblGapID" runat="server" Text="Overtime ID :" meta:resourcekey="lblGapIDResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtOvtID" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                                Width="167px" meta:resourcekey="txtOvtIDResource1"></asp:TextBox>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:CustomValidator ID="cvMaxOverTime" runat="server" Display="None" ValidationGroup="vgSave"
                                                OnServerValidate="MaxOverTime_ServerValidate" EnableClientScript="False" ControlToValidate="txtValid"
                                                meta:resourcekey="cvMaxOverTimeResource1"></asp:CustomValidator>
                                        </td>
                                        <td class="td2Allalign">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblDate" runat="server" Text="Date :" meta:resourcekey="lblDateResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                                Width="167px" meta:resourcekey="txtDateResource1"></asp:TextBox>
                                        </td>
                                        <td class="td1Allalign">
                                        </td>
                                        <td class="td2Allalign">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblTimeFrom" runat="server" Text="Time from :" meta:resourcekey="lblTimeFromResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <Almaalim:TimePicker ID="tpFrom" runat="server" FormatTime="HHmmss" CssClass="TimeCss" />
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblTimeTo" runat="server" Text="Time To :" meta:resourcekey="lblTimeToResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <Almaalim:TimePicker ID="tpTo" runat="server" FormatTime="HHmmss" CssClass="TimeCss" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                                            <asp:Label ID="lblReqFile" runat="server" Text="Request File :" meta:resourcekey="lblReqFileResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign" colspan="3">
                                            <input id="fudReqFile" type="File" runat="Server" meta:resourcekey="fudReqFileResource1" />
                                            <%--<asp:FileUpload ID="fudReqFile" runat="server" />--%>
                                            <asp:CustomValidator ID="cvReqFile" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Request File is required!' /&gt;"
                                                ValidationGroup="vgSave" OnServerValidate="ReqFile_ServerValidate" EnableClientScript="False"
                                                ControlToValidate="txtValid" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td2Allalign" colspan="4">
                                            <span class="RequiredField">*</span>
                                            <asp:Label ID="lblDesc" runat="server" Text="Overtime Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td2Allalign" colspan="4">
                                            <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine"
                                                Width="600px" Enabled="False" meta:resourcekey="txtDescResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDesc"
                                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Request Reason is required!' /&gt;"
                                                ValidationGroup="vgSave" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <center>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" OnClick="btnSave_Click"
                                                                Width="70px" Height="18px" ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                                                meta:resourcekey="btnSaveResource1" OnClientClick="javascript:showWait();"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" OnClick="btnCancel_Click"
                                                                OnClientClick="hideparentPopup('divPopup2');" Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                                                meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                                EnableClientScript="False" ControlToValidate="txtValid">
                                                            </asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </caption>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
