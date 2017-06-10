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
    <%--<link href="../CSS/ModalPopup.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MasterPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="../CSS/Metro/Metro.css" rel="stylesheet" />
    <%--stylesheet--%>
</head>
<body>
    <form id="form1" runat="server" >
        <div>
            <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

            <asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="row">
                        <div class="col12">
                            <iframe id="ifrmProgress" runat="server" src="~/Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="600px" width="650px"></iframe>
                        </div>
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

                    <div id="divName" runat="server" class="row">
                        <div class="col12">
                            <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1" CssClass="h3"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                                EnableClientScript="False" CssClass="MsgValidation"
                                meta:resourcekey="vsumAllResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                                EnableClientScript="False" ValidationGroup="ShowMsg" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <asp:Label ID="lblGapID" runat="server" Text="Overtime ID :" meta:resourcekey="lblGapIDResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtOvtID" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                meta:resourcekey="txtOvtIDResource1"></asp:TextBox>
                        </div>
                        <div class="col4">
                            <asp:CustomValidator ID="cvMaxOverTime" runat="server" Display="None" ValidationGroup="vgSave"
                                OnServerValidate="MaxOverTime_ServerValidate" EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"
                                meta:resourcekey="cvMaxOverTimeResource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <asp:Label ID="lblDate" runat="server" Text="Date :" meta:resourcekey="lblDateResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                meta:resourcekey="txtDateResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <asp:Label ID="lblTimeFrom" runat="server" Text="Time from :" meta:resourcekey="lblTimeFromResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Almaalim:TimePicker ID="tpFrom" runat="server" FormatTime="HHmmss" CssClass="TimeCss" />
                        </div>
                        <div class="col2">
                            <asp:Label ID="lblTimeTo" runat="server" Text="Time To :" meta:resourcekey="lblTimeToResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Almaalim:TimePicker ID="tpTo" runat="server" FormatTime="HHmmss" CssClass="TimeCss" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                            <asp:Label ID="lblReqFile" runat="server" Text="Request File :" meta:resourcekey="lblReqFileResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <input id="fudReqFile" type="File" runat="Server" meta:resourcekey="fudReqFileResource1" />
                            <%--<asp:FileUpload ID="fudReqFile" runat="server" />--%>
                            <asp:CustomValidator ID="cvReqFile" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Request File is required!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="ReqFile_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                                ControlToValidate="txtValid" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblDesc" runat="server" Text="Overtime Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine"
                                Enabled="False" meta:resourcekey="txtDescResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDesc" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Request Reason is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col8">
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                                ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                meta:resourcekey="btnSaveResource1" OnClientClick="javascript:showWait();"></asp:LinkButton>

                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                                OnClientClick="hideparentPopup('divPopup2');"  Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                             </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False" ControlToValidate="txtValid">
                            </asp:CustomValidator>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
