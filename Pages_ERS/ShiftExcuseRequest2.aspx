<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftExcuseRequest2.aspx.cs" Inherits="ShiftExcuseRequest2" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
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
    
    <title> Shift Excuse Request </title>

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
    <form id="form1" runat="server"  >
         
        <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        
        <asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="row">
                        <div class="col12">
                        <iframe id="ifrmProgress" runat="server" src="../Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="600px" width="100%" ></iframe> 
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
                                    <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1"></asp:Label>
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
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="errorValidation" EnableClientScript="False" ValidationGroup="ShowMsg"/>
                        </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblStartDate" runat="server"  Text="Date :" 
                                            meta:resourcekey="lblStartDateResource1"></asp:Label>
                                    </div>
                            <div class="col4">                                 
                                        <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave"/>
                                    </div>
                            <div class="col2">
                                        <asp:LinkButton ID="btnFillWorkTime" runat="server" CssClass="GenButton GenButtonsmall" 
                                            OnClick="btnFillWorkTime_Click"  Text="Fill Worktime" 
                                            meta:resourcekey="btnFillWorkTimeResource1"></asp:LinkButton>
                                     </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblWorkTimeName" runat="server" Text="Worktime Name :" 
                                            meta:resourcekey="lblWorkTimeNameResource1"></asp:Label>
                                   </div>
                            <div class="col4">
                                        <asp:TextBox ID="txtWorkTimeName" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False"  meta:resourcekey="txtWorkTimeNameResource1" ></asp:TextBox>
                                    </div>
                            <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblWorkTimeID" runat="server" Text="Worktime ID :" 
                                            meta:resourcekey="lblWorkTimeIDResource1" ></asp:Label>
                                    </div>
                            <div class="col4">
                                        <asp:TextBox ID="txtWorkTimeID" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False"   meta:resourcekey="txtWorkTimeIDResource1" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvWorkTimeID" runat="server" 
                                            ControlToValidate="txtWorkTimeID" EnableClientScript="False" CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='Worktime ID is required!' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rfvWorkTimeIDResource1"></asp:RequiredFieldValidator>
                                    </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblShiftName" runat="server" Text="Shift Name :" 
                                            meta:resourcekey="lblShiftNameResource1" ></asp:Label>
                                     </div>
                            <div class="col4">
                                        <asp:DropDownList ID="ddlShiftName" runat="server"   
                                            meta:resourcekey="ddlShiftNameResource1" AutoPostBack="True" 
                                            onselectedindexchanged="ddlShiftName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvShiftName" runat="server" 
                                            ControlToValidate="ddlShiftName" EnableClientScript="False"  CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='Shift Name is required!' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rfvShiftNameResource1"></asp:RequiredFieldValidator>
                                   </div>
                            <div class="col2">
                                        <asp:Label ID="lblShiftID" runat="server" Text="Shift ID :" 
                                            meta:resourcekey="lblShiftIDResource1"></asp:Label>
                                     </div>
                            <div class="col4">
                                        <asp:TextBox ID="txtShiftID" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False"   meta:resourcekey="txtShiftIDResource1" ></asp:TextBox>
                                        </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblTimeFrom" runat="server" Text="Start Time :" 
                                            meta:resourcekey="lblTimeFromResource1" ></asp:Label>
                                    </div>
                            <div class="col4">
                                        <Almaalim:TimePicker ID="tpFrom" runat="server" FormatTime="HHmm" 
                                            meta:resourcekey="tpFromResource1" Enabled="False" CssClass="TimeCss"/>
                                    </div>
                            <div class="col2"> 
                                    <span class="RequiredField">*</span>
                                        <asp:Label ID="lblTimeTo" runat="server" Text="End Time :" 
                                            meta:resourcekey="lblTimeToResource1" ></asp:Label>
                                    </div>
                            <div class="col4">
                                        <Almaalim:TimePicker ID="tpTo" runat="server" FormatTime="HHmm" 
                                            meta:resourcekey="tpToResource1" Enabled="False" CssClass="TimeCss"/>
                                   </div>
                        </div>
                        <div class="row">
                            <div class="col2"> 
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblExcType" runat="server" Text="Excuse Type :" 
                                            meta:resourcekey="lblExcTypeResource1" ></asp:Label>
                                      </div>
                            <div class="col4">
                                        <asp:DropDownList ID="ddlExcType" runat="server"   
                                            meta:resourcekey="ddlExcTypeResource1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExcType" runat="server" 
                                            ControlToValidate="ddlExcType" EnableClientScript="False" CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='Excuse Type is required!' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rfvExcTypeResource1"
                                            ></asp:RequiredFieldValidator>
                                   </div>
                        </div>
                        <div class="row">
                            <div class="col2"> 
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblDesc" runat="server" Text="Request Reason :" meta:resourcekey="lblDescResource1" ></asp:Label>
                                    </div>
                            <div class="col4">
                                        <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled" 
                                            TextMode="MultiLine"   Enabled="False" 
                                            meta:resourcekey="txtDescResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                            ControlToValidate="txtDesc" EnableClientScript="False" CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='Request Reason is required!' /&gt;" 
                                            ValidationGroup="vgSave" 
                                            meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                   </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                        <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                                        <asp:Label ID="lblReqFile" runat="server" Text="Request File :" 
                                            meta:resourcekey="lblReqFileResource1"></asp:Label>
                                    </div>
                            <div class="col4">
                                        <asp:FileUpload ID="fudReqFile" runat="server" meta:resourcekey="fudReqFileResource1"  />
                                        <asp:CustomValidator id="cvReqFile" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Request File is required!' /&gt;" 
                                        ValidationGroup="vgSave" CssClass="CustomValidator"
                                        OnServerValidate="ReqFile_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                                      </div>
                        </div>

                        <div class="row">
                            <div class="col2"></div>
                            <div class="col4">
                                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton  glyphicon glyphicon-floppy-disk" 
                                                            OnClick="btnSave_Click"   ValidationGroup="vgSave" 
                                                            Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                                            meta:resourcekey="btnSaveResource1" OnClientClick="javascript:showWait();"></asp:LinkButton>
                                                    
                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" 
                                                            OnClick="btnCancel_Click" OnClientClick="hideparentPopup('');"  
                                                            Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" 
                                                            meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                                                    </div>
                            <div class="col4">
                                                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                                            Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                                                        <asp:CustomValidator id="cvShowMsg" runat="server" Display="None"  CssClass="CustomValidator"
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