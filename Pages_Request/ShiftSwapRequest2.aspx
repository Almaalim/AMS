<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftSwapRequest2.aspx.cs" Inherits="ShiftSwapRequest2" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript">
        function showWait() {
            //if ($get('fudReqFile').value.length > 0) {
            $get('upWaiting').style.display = 'block';
            //}
        }
    </script>


    <title>Shift Swap Request </title>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/jquery-1.7.1.min.js"></script>
    <%--script--%>

    <%--stylesheet--%>
    <%-- <link href="../CSS/ModalPopup.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MasterPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/CSS/Metro/Metro.css" rel="stylesheet" runat="server" id="LanguageSwitch" />
    <%--stylesheet--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("div[class*='col']").each(function () {
                if ($(this).children(".RequiredField").length > 0) {
                    $(this).addClass("RequiredFieldDiv");
                }
                var $this = $(this);

                $this.html($this.html().replace(/&nbsp;/g, ''));

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

            <asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="row">
                        <div class="col12">
                            <iframe id="ifrmProgress" runat="server" src="../Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="600px" width="100%"></iframe>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div id="divName" runat="server" class="row">
                        <div class="col12">
                            <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                                EnableClientScript="False" ValidationGroup="ShowMsg" />
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
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblType" runat="server" Text="Type :" meta:resourcekey="lblTypeResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlType" runat="server"
                                meta:resourcekey="ddlTypeResource1">
                                <asp:ListItem Text="Select Type" Value="Select Type" Selected="True"
                                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Working Day(s)" Value="Work"
                                    meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="Off Day(s)" Value="Off"
                                    meta:resourcekey="ListItemResource3"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlType" runat="server" CssClass="CustomValidator"
                                ControlToValidate="ddlType" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='Type is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rfvddlTypeResource1"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvWorkTime" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="Days_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False"
                                ControlToValidate="txtValid"
                                meta:resourcekey="cvWorkTimeResource1"></asp:CustomValidator>
                        </div>
                        <div class="col2">
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtWorktime" runat="server" AutoCompleteType="Disabled"
                                Visible="False" meta:resourcekey="txtWorktimeResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblStartDate1" runat="server" Text="Start Date :"
                                meta:resourcekey="lblStartDate1Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate1" />

                            <asp:CustomValidator ID="cvDaysCount" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="DaysCount_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" CssClass="CustomValidator"
                                meta:resourcekey="cvDaysCountResource1"></asp:CustomValidator>

                            <asp:CustomValidator ID="cvDays1" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave" CssClass="CustomValidator"
                                OnServerValidate="Days_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid"
                                meta:resourcekey="cvDays1Resource1"></asp:CustomValidator>
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblEndDate1" runat="server" Text="End Date :"
                                meta:resourcekey="lblEndDate1Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calEndDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblEmpID2" runat="server" Text="swap With Employee ID:"
                                meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled"
                                meta:resourcekey="txtEmpID2Resource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmpID2" runat="server" CssClass="CustomValidator"
                                ControlToValidate="txtEmpID2" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='swap With Employee ID is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rfvEmpID2Resource1"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvEmployee" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='No Employee with ID!' /&gt;"
                                ValidationGroup="vgSave"
                                ErrorMessage="No Employee with ID!" CssClass="CustomValidator"
                                OnServerValidate="Employee_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid"
                                meta:resourcekey="cvEmployeeResource1"></asp:CustomValidator>
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblStartDate2" runat="server" Text="Start Date :"
                                meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate2" />

                            <asp:CustomValidator ID="cvDays2" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="Days_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False"
                                ControlToValidate="txtValid"
                                meta:resourcekey="cvDays2Resource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblEndDate2" runat="server" Text="End Date :"
                                meta:resourcekey="lblEndDate2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calEndDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblDesc" runat="server" Text="Request Reason :"
                                meta:resourcekey="lblDescResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                                TextMode="MultiLine" Width="450px" Enabled="False"
                                meta:resourcekey="txtDescResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtDesc" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='Description is required!' /&gt;"
                                ValidationGroup="vgSave" CssClass="CustomValidator"
                                meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2"></div>
                        <div class="col4">
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                                OnClick="btnSave_Click" ValidationGroup="vgSave"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                                OnClick="btnCancel_Click"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid"
                                meta:resourcekey="cvShowMsgResource1">
                            </asp:CustomValidator>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
