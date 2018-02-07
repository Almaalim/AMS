<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ApplicationSetting.aspx.cs" Inherits="ApplicationSetting" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                        EnableClientScript="False" ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                        CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsSaveResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"   
                        OnClick="btnSave_Click"  
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                               
                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                            Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                                     
                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                        ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppCompany" runat="server" Text="Company Name :" meta:resourcekey="lblAppCompanyResource1"></asp:Label>
                    </div>
                <div class="col4">
                    <asp:TextBox ID="txtAppCompany" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppCompanyResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvAppCompany" runat="server" 
                        ControlToValidate="txtAppCompany" EnableClientScript="False" CssClass="CustomValidator"
                        Text="&lt;img src='../images/Exclamation.gif' title='Company Name is required' /&gt;" 
                        ValidationGroup="vgSave" meta:resourcekey="rvAppCompanyResource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col2">
                    <asp:Label ID="lblAppDisplay" runat="server" Text="Display Name :" meta:resourcekey="lblAppDisplayResource1"></asp:Label>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtAppDisplay" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppDisplayResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvAppDisplay" runat="server" 
                    ControlToValidate="txtAppDisplay" EnableClientScript="False" CssClass="CustomValidator"
                    Text="&lt;img src='../images/Exclamation.gif' title='Display Name is required' /&gt;" 
                    ValidationGroup="vgSave" meta:resourcekey="rvAppDisplayResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppCountry" runat="server" Text="Country :" meta:resourcekey="lblAppCountryResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppCountry" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppCountryResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblAppCity" runat="server" Text="City :" meta:resourcekey="lblAppCityResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppCity" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppCityResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppAddress1" runat="server" Text="Address 1 :" meta:resourcekey="lblAppAddress1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppAddress1" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppAddress1Resource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblAppAddress2" runat="server" Text="Address 2 :" meta:resourcekey="lblAppAddress2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppAddress2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppAddress2Resource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppTelNo1" runat="server" Text="Telephone No. 1 :" meta:resourcekey="lblAppTelNo1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppTelNo1" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppTelNo1Resource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblAppTelNo2" runat="server" Text="Telephone No. 2 :" meta:resourcekey="lblAppTelNo2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppTelNo2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppTelNo2Resource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppPOBox" runat="server" Text="P.o.box :" meta:resourcekey="lblAppPOBoxResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtAppPOBox" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtAppPOBoxResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblAppSessionDuration" runat="server" Text="Session timeout :" Visible="False" meta:resourcekey="lblAppSessionDurationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtSessionDuration" runat="server" FormatTime="mm" Visible="False" meta:resourcekey="txtSessionDurationResource1" TextTimeValidationGroup="" TextTimeValidationText=""/>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppCalendar" runat="server" Text="Calendar :" meta:resourcekey="lblAppCalendarResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlAppCalendar" runat="server" meta:resourcekey="ddlAppCalendarResource1">
                            <asp:ListItem Selected="True" Value="G" Text="Gregorian" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Hijri" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblAppDateFormat" runat="server" Text="Date Format :" meta:resourcekey="lblAppDateFormatResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlAppDateFormat" runat="server" Enabled="False" meta:resourcekey="ddlAppDateFormatResource1">
                            <asp:ListItem Selected="True" Value="dd/MM/yyyy" Text="dd/MM/yyyy" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppDataLangRequired" runat="server" Text="Required Data Language :" meta:resourcekey="lblAppDataLangRequiredResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:RadioButtonList ID="rdlAppDataLangRequired" runat="server" Enabled="False" RepeatDirection="Horizontal" meta:resourcekey="rdlAppDataLangRequiredResource1">
                            <asp:ListItem Value="B" Text ="Arabic - English" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            <asp:ListItem Value="A" Text ="Arabic" meta:resourcekey="ListItemResource5"></asp:ListItem>
                            <asp:ListItem Value="E" Text ="English" meta:resourcekey="ListItemResource6"></asp:ListItem>
                        </asp:RadioButtonList>
                    
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1" Enabled="True"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1" Enabled="True"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" meta:resourcekey="lnkShow1Resource1" />
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" meta:resourcekey="lnkClose1Resource1" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="Specify the language of the input data required and can not be conservation without entered." meta:resourcekey="lblHint1Resource1"></asp:Label>
                            </p>
                        </div>
                    
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblAppMiniLogger_VerificationMethod" runat="server" Text="Mini Logger Verification Method:" meta:resourcekey="lblAppMiniLogger_VerificationMethodResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlAppMiniLogger_VerificationMethod" runat="server" meta:resourcekey="ddlAppMiniLogger_VerificationMethodResource1">
                            <asp:ListItem Selected="True" Value="FP" Text="Fingerprint" meta:resourcekey="ListItemResource7"></asp:ListItem>
                            <asp:ListItem Value="PW" Text="Password" meta:resourcekey="ListItemResource8"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                       
                    </div>
                    <div class="col4">
                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>

