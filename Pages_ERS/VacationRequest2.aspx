<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="VacationRequest2.aspx.cs" Inherits="VacationRequest2" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

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
    <link href="../CSS/Metro/Metro.css" rel="stylesheet" />
    <title>Vacation Request </title>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <%--script--%>

    <%--stylesheet--%>
    <%--<link href="../CSS/General.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../CSS/VSStyle.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../CSS/ModalPopup.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MasterPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />--%>
    <%--stylesheet--%>
</head>
<body>
    <form id="form1" runat="server">
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
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
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="errorValidation" EnableClientScript="False" ValidationGroup="vgShowMsg" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">

                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblReqType" runat="server" meta:resourcekey="lblReqTypeResource1" Text="Request Type :"></asp:Label>

                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlReqType" runat="server"
                                AutoPostBack="true" meta:resourcekey="ddlReqTypeResource1"
                                OnSelectedIndexChanged="ddlReqType_SelectedIndexChanged">
                                <asp:ListItem Text="Vacation" Value="VAC" meta:resourcekey="liVACResource1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Commission" Value="COM" meta:resourcekey="liCOMResource1"></asp:ListItem>
                                <asp:ListItem Text="Job Assignment" Value="JOB" meta:resourcekey="liJOBResource1"></asp:ListItem>
                                <asp:ListItem Text="Administrative License" Value="LIC" meta:resourcekey="liLICResource1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <caption>
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblStartDate" runat="server" meta:resourcekey="lblStartDateResource1" Text="Start Date :"></asp:Label>
                            </caption>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate" />

                            <asp:CustomValidator ID="cvHaveTrans" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="HaveTrans_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False"
                                ControlToValidate="txtValid">
                            </asp:CustomValidator>

                            <asp:CustomValidator ID="cvDays" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="Days_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvDaysResource1">
                            </asp:CustomValidator>

                            <asp:CustomValidator ID="cvReqDate" runat="server" CssClass="CustomValidator"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="Days_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvDaysResource1">
                            </asp:CustomValidator>
                        </div>

                        <div class="col2">
                            <caption>
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblEndDate" runat="server" meta:resourcekey="lblEndDateResource1" Text="End Date :"></asp:Label>
                            </caption>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />

                        </div>
                    </div>
                    <div id="divVACType" runat="server" class="row">
                        <div class="col2">
                            <caption>
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblVacType" runat="server" meta:resourcekey="lblVacTypeResource1" Text="Vacation Type :"></asp:Label>
                            </caption>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlVacType" runat="server" AutoPostBack="true"
                                meta:resourcekey="ddlVacTypeResource1"
                                OnSelectedIndexChanged="ddlVacType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvExcType" runat="server" CssClass="CustomValidator"
                                ControlToValidate="ddlVacType" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='Vacation Type is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rfvExcTypeResource1">
                            </asp:RequiredFieldValidator>

                            <asp:CustomValidator ID="cvMaxDays" runat="server" CssClass="CustomValidator"
                                Text="&lt;img src='../images/message_exclamation.png' title='MaxDays!' /&gt;"
                                ValidationGroup="vgSave"
                                ErrorMessage="MaxDays!"
                                OnServerValidate="MaxDays_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid"
                                meta:resourcekey="cvMaxDaysResource1">
                            </asp:CustomValidator>
                        </div>

                        <div class="col2">
                            <span id="spVacHospitalType" runat="server" class="RequiredField" visible="false">*</span>
                            <asp:Label ID="lblVacHospitalType" runat="server" Text="Hospital Type :" Visible="false" meta:resourcekey="lblVacHospitalTypeResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlVacHospitalType" runat="server" Visible="false" meta:resourcekey="ddlVacHospitalTypeResource1">
                                <asp:ListItem Text="-Select Hospital Type-" Value="N" meta:resourcekey="liSelectHospitalTypeResource"></asp:ListItem>
                                <asp:ListItem Text="Government Hospital" Value="GH" meta:resourcekey="liGHResource"></asp:ListItem>
                                <asp:ListItem Text="Private Hospital" Value="PH" meta:resourcekey="liPHResource"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvVacHospitalType" runat="server" Enabled="false"
                                ControlToValidate="ddlVacHospitalType" EnableClientScript="False" CssClass="CustomValidator"
                                Text="&lt;img src='../images/Exclamation.gif' title='Hospital Type is required!' /&gt;"
                                ValidationGroup="vgSave" InitialValue="N" meta:resourcekey="rvVacHospitalTypeResource1">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                                TextMode="MultiLine" Enabled="False"
                                meta:resourcekey="txtDescResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtDesc" EnableClientScript="False" CssClass="CustomValidator"
                                Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;"
                                ValidationGroup="vgSave"
                                meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col2">
                            <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                            <asp:Label ID="lblReqFile" runat="server" Text="Request File :"
                                meta:resourcekey="lblReqFileResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:FileUpload ID="fudReqFile" runat="server" meta:resourcekey="fudReqFileResource1" Width="450px" />
                            <asp:CustomValidator ID="cvReqFile" runat="server"
                                Text="&lt;img src='../images/Exclamation.gif' title='Request File is required!' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="ReqFile_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                        </div>
                    </div>

                    <div id="divDetail" runat="server" class="row">
                        <div class="col2">
                            <asp:Label ID="lblAvailabitily" runat="server" Text="Availabitily Address :"
                                meta:resourcekey="lblAvailabitilyResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtAvailabitily" runat="server" AutoCompleteType="Disabled"
                                TextMode="MultiLine" Enabled="False"
                                meta:resourcekey="txtAvailabitilyResource1"></asp:TextBox>
                        </div>

                        <div class="col2">
                            <asp:Label ID="lblPhone" runat="server" Text="Phone. No :"
                                meta:resourcekey="lblPhoneResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtPhone" runat="server" AutoCompleteType="Disabled"
                                Enabled="False" meta:resourcekey="txtPhoneResource1"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col2"></div>
                        <div class="col4">
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton  glyphicon glyphicon-floppy-disk"
                                OnClick="btnSave_Click" ValidationGroup="vgSave"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                meta:resourcekey="btnSaveResource1" OnClientClick="javascript:showWait();"></asp:LinkButton>

                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                                OnClick="btnCancel_Click" OnClientClick="hideparentPopup('');"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="16px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
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
