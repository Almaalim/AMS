<%@ Page Title="Worktime Page" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" CodeFile="WorkTime.aspx.cs" Inherits="WorkTime" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="TextTimeServerControl" Namespace="TextTimeServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"  meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Work Time Name (Ar)" Value="WktNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Work Time Name (En)" Value="WktNameEn" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Work Type Name (Ar)" Value="WtpNameAr" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Work Type Name (En)" Value="WtpNameEn" meta:resourcekey="ListItemResource5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="WktID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True" meta:resourcekey="grdDataResource1">
                        <%--PagerStyle-CssClass="pgr"--%>

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="WktNameAr" SortExpression="WktNameAr"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="WktID" HeaderText="ID" SortExpression="WktID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="WktNameEn" HeaderText="Name (En)" SortExpression="WktNameEn"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="WtpNameAr" HeaderText="Work Type (Ar)" SortExpression="WtpNameAr"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField HeaderText="Work Type (En)" DataField="WtpNameEn" SortExpression="WtpNameEn"
                                meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("WktID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
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
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                        CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary runat="server" ID="vsCalShift" ValidationGroup="vgCalShift"
                        EnableClientScript="False" CssClass="MsgValidation" meta:resourcekey="vsumCalShiftResource1" />
                </div>
                 </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        ValidationGroup="vgWarning" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton  glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvIsModify" runat="server" Display="None" ValidationGroup="vgWarning" CssClass="CustomValidator"
                        OnServerValidate="Warning_ServerValidate" EnableClientScript="False" ControlToValidate="txtValid"
                        meta:resourcekey="cvIsModifyResource1"></asp:CustomValidator>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col12">

                        <asp:Label ID="lblWorkTimeSetting" runat="server" Text="WorkTime Setting" meta:resourcekey="lblWorkTimeSettingResource1" CssClass="h4"></asp:Label>

                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar):" meta:resourcekey="lblArWorkTimeNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled"
                            ToolTip="Work Time Name" meta:resourcekey="txtArWorkTimeNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="WorkTimeValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvArWorkTimeNameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :" meta:resourcekey="lblEnWorkTimeNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtEnWorkTimeNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="WorkTimeValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvEnWorkTimeNameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnWktInitialAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblWktInitialAr" runat="server" Text="Initial (Ar) :" meta:resourcekey="lblWktInitialArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtWktInitialAr" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtWktInitialArResource1" MaxLength="3"></asp:TextBox>
                        <asp:CustomValidator ID="cvInitialAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="WorkTimeValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvInitialArResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnWktInitialEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblWktInitialEn" runat="server" Text="Initial (En) :" meta:resourcekey="lblWktInitialEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtWktInitialEn" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtWktInitialEnResource1" MaxLength="3"></asp:TextBox>
                        <asp:CustomValidator ID="cvInitialEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="WorkTimeValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvInitialEnResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>
                

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="Label19" runat="server" Text="Work Type :" meta:resourcekey="Label19Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlWtpID" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlWtpID_SelectedIndexChanged" meta:resourcekey="ddlWtpIDResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvWtpID" runat="server" ControlToValidate="ddlWtpID"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvWtpIDResource1" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkWrtStatus" runat="server" Text="Active" meta:resourcekey="chkWrtStatusResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="Label9" runat="server" Text="Overtime Interval (Max/Day) :" meta:resourcekey="Label9Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtMaxPercOT" runat="server" FormatTime="hhmm" CssClass="TimeCss" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="You can initialize here" meta:resourcekey="lblHint1Resource"></asp:Label>
                            </p>
                        </div>
                    </div>

                    <div class="col2">
                        <asp:Label ID="lblWorkTimeDesc" runat="server" meta:resourcekey="lblWorkTimeDescResource1" CssClass="">Description :</asp:Label>
                    </div>
                    <div class="col4">

                        <asp:TextBox ID="txtWorkTimeDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine" Style="resize: none;" meta:resourcekey="txtWorkTimeDescResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col12">

                        <asp:Label ID="Label2" runat="server" Text="Shift 1 Setting" meta:resourcekey="Label2Resource1" CssClass="h4"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnShift1NameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblArShift1Name" runat="server" Text="Name (Ar):" meta:resourcekey="lblArShift1NameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtArShift1Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtArShift1NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvArShift1Name" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 1 Name Arabic is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvArShift1NameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift1Overnight" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='shift 1 must not be Overnight' /&gt;"
                            ErrorMessage="shift 1 must not be Overnight!" ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift1OvernightResource1">
                        </asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnShift1NameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblShift1Name" runat="server" Text="Name (En) :" meta:resourcekey="lblShift1NameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEnShift1Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtEnShift1NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEnShift1Name" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 1 Name English is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvEnShift1NameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift1From" runat="server" Text="From :" meta:resourcekey="lblShift1FromResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift1From" runat="server" FormatTime="HHmm" style="direction:ltr;"
                            CssClass="TimeCss" TimePickerValidationGroup="vgSave" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='Time Shift 1 From is required!' /&gt;" />
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift1To" runat="server" Text="To :" meta:resourcekey="lblShift1ToResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift1To" runat="server" FormatTime="HHmm" meta:resourcekey="tpShift1ToResource1"
                            CssClass="TimeCss" TimePickerValidationGroup="vgSave" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='Time Shift 1 To is required!' /&gt;" />
                        <asp:CustomValidator ID="cvShift1Time" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 1 Time' /&gt;"
                            ErrorMessage="Enter correct Shift 1 Time!" ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift1TimeResource1">
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift1Cal" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 1 Time' /&gt;"
                            ErrorMessage="Enter correct Shift 1 Time!" ValidationGroup="vgCalShift" OnServerValidate="Shift1Validate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift1TimeResource1">
                        </asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift1Duration" runat="server" Text="Duration :" meta:resourcekey="lblShift1DurationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift1Duration" runat="server" FormatTime="hhmm" meta:resourcekey="txtShift1DurationResource1"
                            CssClass="TimeCss" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint2" runat="server" Text="You can initialize here" meta:resourcekey="lblHint2Resource"></asp:Label>
                            </p>
                        </div>

                        <asp:ImageButton ID="btnCalShift1Duration" runat="server" OnClick="btnCalShift1Duration_Click" CssClass="LeftOverlay1"
                            ImageUrl="~/images/Button_Icons/button_calculator.png" Enabled="False" ValidationGroup="vgCalShift" meta:resourcekey="btnCalShift1DurationResource1"
                            ToolTip="Calculater Shift 1 Duration" />

                         <asp:CustomValidator ID="cvShift1Duration" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 1 Duration is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvShift1DurationResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="row" runat="server" visible="false" id="divFT1">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblWktShift1FTHours" runat="server" Text="Flexible Time Hours :" meta:resourcekey="lblWktShift1FTHoursResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtWktShift1FTHours" runat="server" FormatTime="hhmm" meta:resourcekey="WktShift1FTHoursResource1" CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvWktShift1FTHours" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Flexible Time Hours is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift1Validate_ServerValidate" EnableClientScript="False"
                            Enabled="false"
                            ControlToValidate="txtValid" meta:resourcekey="cvWktShift1FTHoursResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                        </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift1Grace" runat="server" Text="In Grace :" meta:resourcekey="lblShift1GraceResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift1GraceMin" runat="server" FormatTime="mm" meta:resourcekey="txtShift1GraceMinResource1" />
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblShift1MiddleGrace" runat="server" Text="Middle Grace :" meta:resourcekey="lblShift1MiddleGraceResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift1MidGraceMin" runat="server" FormatTime="mm" meta:resourcekey="txtShift1MidGraceMinResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift1EndGrace" runat="server" Text="Out Grace :" meta:resourcekey="lblShift1EndGraceResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift1EndGrace" runat="server" FormatTime="mm" meta:resourcekey="txtShift1EndGraceResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkWktShift1IsOverNight" runat="server" Text="OverNight" meta:resourcekey="chkWktShift1IsOverNightResource1" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow3" runat="server" TargetControlID="lnkShow3"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose3" runat="server" TargetControlID="lnkClose3"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow3" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo3" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose3" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint3" runat="server" Text="You can initialize here" meta:resourcekey="lblHint3Resource"></asp:Label>
                            </p>
                        </div>
                        &nbsp;&nbsp;
                                                <asp:CheckBox ID="chkWktShift1IsOptional" runat="server" Text="Optional" meta:resourcekey="chkWktShift1IsOptionalResource1" />
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow4" runat="server" TargetControlID="lnkShow4"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose4" runat="server" TargetControlID="lnkClose4"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow4" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo4" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose4" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint4" runat="server" Text="You can initialize here" meta:resourcekey="lblHint4Resource"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col12">
                        

                        <asp:Label ID="Label11" runat="server" Text="Shift 2 Setting" meta:resourcekey="Label11Resource1" CssClass="h4"></asp:Label>

                        <asp:CheckBox ID="chkShift2Setting" runat="server" Text="Shift2" AutoPostBack="True"
                            Enabled="False" OnCheckedChanged="chkShift2Setting_CheckedChanged" meta:resourcekey="chkShift2SettingResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnShift2NameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblArShift2Name" runat="server" Text="Name (Ar):" meta:resourcekey="lblArShift2NameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtArShift2Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtArShift2NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvArShift2Name" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 2 Name Arabic is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvArShift2NameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvOrderShfit2" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shifts must be ordered by time' /&gt;"
                            ErrorMessage="Shifts must be ordered by time!" ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvOrderShfit2Resource1">
                        </asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnShift2NameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblShift2Name" runat="server" Text=" Name (En) :" meta:resourcekey="lblShift2NameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEnShift2Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtEnShift2NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEnShift2Name" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 2 Name English is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvEnShift2NameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift2From" runat="server" Text="From :" meta:resourcekey="lblShift2FromResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift2From" runat="server" FormatTime="HHmm" meta:resourcekey="tpShift2FromResource1"
                            CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvShift2FromRequired" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Time Shift 2 From is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvShift2FromRequiredResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift2To" runat="server" Text="To :" meta:resourcekey="lblShift2ToResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift2To" runat="server" FormatTime="HHmm" meta:resourcekey="tpShift2ToResource1"
                            CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvShift2ToRequired" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Time Shift 2 To is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvShift2ToRequiredResource1" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift2Time" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 2 Time' /&gt;"
                            ErrorMessage="Enter correct Shift 2 Time!" ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift2TimeResource1"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift2Cal" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 2 Time' /&gt;"
                            ErrorMessage="Enter correct Shift 2 Time!" ValidationGroup="vgCalShift" OnServerValidate="Shift2Validate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift2TimeResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift2Duration" runat="server" Text="Duration :" meta:resourcekey="lblShift2DurationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift2Duration" runat="server" FormatTime="hhmm" meta:resourcekey="txtShift2DurationResource1"
                            CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvShift2Duration" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shift 2 Duration is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvShift2DurationResource1" CssClass="CustomValidator"></asp:CustomValidator>

                        <asp:ImageButton ID="btnCalShift2Duration" runat="server" OnClick="btnCalShift2Duration_Click"
                            ImageUrl="~/images/Button_Icons/button_calculator.png" Enabled="False" ValidationGroup="vgCalShift" meta:resourcekey="btnCalShift2DurationResource1"
                            ToolTip="Calculater Shift 2 Duration" CssClass="LeftOverlay1" />
                    </div>
                </div>
                <div id="divFT2" runat="server" visible="false" class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblWktShift2FTHours" runat="server" Text="Flexible Time Hours :" meta:resourcekey="lblWktShift2FTHoursResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtWktShift2FTHours" runat="server" FormatTime="hhmm" meta:resourcekey="WktShift2FTHoursResource1" CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvWktShift2FTHours" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Flexible Time Hours is required!' /&gt;"
                            ValidationGroup="Users" OnServerValidate="Shift2Validate_ServerValidate" EnableClientScript="False"
                            Enabled="false"
                            ControlToValidate="txtValid" meta:resourcekey="cvWktShift2FTHoursResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>



                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift2Grace" runat="server" Text="In Grace :" meta:resourcekey="lblShift2GraceResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift2GraceMin" runat="server" FormatTime="mm" meta:resourcekey="txtShift2GraceMinResource1" />
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblShift2MiddleGrace" runat="server" Text="Middle Grace :" meta:resourcekey="lblShift2MiddleGraceResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift2MidGraceMin" runat="server" FormatTime="mm" meta:resourcekey="txtShift2MidGraceMinResource1" />
                    </div>
                </div>


                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift2EndGrace" runat="server" Text="Out Grace :" meta:resourcekey="lblShift2EndGraceResource1"></asp:Label>
                    </div>
                    <div class="col4">

                        <Almaalim:TextTime ID="txtShift2EndGrace" runat="server" FormatTime="mm" meta:resourcekey="txtShift2EndGraceResource1" />

                    </div>
                    <div class="col2"></div>
                    <div class="col4">
                        <asp:CheckBox ID="chkWktShift2IsOverNight" runat="server" Text="OverNight" meta:resourcekey="chkWktShift2IsOverNightResource1" />

                        <asp:CheckBox ID="chkWktShift2IsOptional" runat="server" Text="Optional" meta:resourcekey="chkWktShift2IsOptionalResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col12">
                        

                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label13Resource1" CssClass="h4"
                            Text="Shift 3 Setting"></asp:Label>
                        <asp:CheckBox ID="chkShift3Setting" runat="server" AutoPostBack="True"
                            Enabled="False" meta:resourcekey="chkShift3SettingResource1"
                            OnCheckedChanged="chkShift3Setting_CheckedChanged" Text="Shift3" />
                    </div>
                </div>



                <div class="row">
                    <div class="col2">
                        <span id="spnShift3NameAr" runat="server" class="RequiredField" visible="False">*</span>
                        <asp:Label ID="lblArShift3Name" runat="server"
                            meta:resourcekey="lblArShift3NameResource1" Text="Name (Ar):"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtArShift3Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtArShift3NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvArShift3Name" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            meta:resourcekey="cvArShift3NameResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Shift 3 Name Arabic is required!' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvOrderShfit3" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Shifts must be ordered by time' /&gt;"
                            ErrorMessage="Shifts must be ordered by time!" ValidationGroup="vgSave" OnServerValidate="Shift3Validate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvOrderShfit3Resource1">
                        </asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnShift3NameEn" runat="server" class="RequiredField" visible="False">*</span>
                        <asp:Label ID="lblShift3Name" runat="server"
                            meta:resourcekey="lblShift3NameResource1" Text="Name (En) :"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEnShift3Name" runat="server" AutoCompleteType="Disabled"
                            meta:resourcekey="txtEnShift3NameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEnShift3Name" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            meta:resourcekey="cvEnShift3NameResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Shift 3 Name English is required!' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift3From" runat="server" meta:resourcekey="lblShift3FromResource1" Text="From :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift3From" runat="server" CssClass="TimeCss" FormatTime="HHmm" meta:resourcekey="tpShift3FromResource1" />
                        <asp:CustomValidator ID="cvShift3FromRequired" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            meta:resourcekey="cvShift3FromRequiredResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time Shift 3 From is required!' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblShift3To" runat="server"
                            meta:resourcekey="lblShift3ToResource1" Text="To :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpShift3To" runat="server" CssClass="TimeCss"
                            FormatTime="HHmm" meta:resourcekey="tpShift3ToResource1" />
                        <asp:CustomValidator ID="cvShift3ToRequired" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            meta:resourcekey="cvShift3ToRequiredResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time Shift 3 To is required!' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift3Time" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            ErrorMessage="Enter correct Shift 3 Time!"
                            meta:resourcekey="cvShift3TimeResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 3 Time' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:CustomValidator ID="cvShift3Cal" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            ErrorMessage="Enter correct Shift 3 Time!"
                            meta:resourcekey="cvShift3TimeResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Enter correct Shift 3 Time' /&gt;"
                            ValidationGroup="vgCalShift" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift3Duration" runat="server"
                            meta:resourcekey="lblShift3DurationResource1" Text="Duratoin :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift3Duration" runat="server" CssClass="TimeCss"
                            FormatTime="hhmm" meta:resourcekey="txtShift3DurationResource1" />
                        <asp:CustomValidator ID="cvShift3Duration" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            meta:resourcekey="cvShift3DurationResource1"
                            OnServerValidate="Shift3Validate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Shift 3 Duration is required!' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>

                        <asp:ImageButton ID="btnCalShift3Duration" runat="server" OnClick="btnCalShift3Duration_Click"
                            ImageUrl="~/images/Button_Icons/button_calculator.png" Enabled="False" ValidationGroup="vgCalShift" meta:resourcekey="btnCalShift3DurationResource1"
                            ToolTip="Calculater Shift 3 Duration" CssClass="LeftOverlay1" />
                    </div>
                </div>
                <div id="divFT3" runat="server" visible="false" class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblWktShift3FTHours" runat="server" Text="Flexible Time Hours :" meta:resourcekey="lblWktShift3FTHoursResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtWktShift3FTHours" runat="server" FormatTime="hhmm" meta:resourcekey="WktShift3FTHoursResource1" CssClass="TimeCss" />
                        <asp:CustomValidator ID="cvWktShift3FTHours" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Flexible Time Hours is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Shift3Validate_ServerValidate" EnableClientScript="False"
                            Enabled="false"
                            ControlToValidate="txtValid" meta:resourcekey="cvWktShift3FTHoursResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift3Grace" runat="server"
                            meta:resourcekey="lblShift3GraceResource1" Text="In Grace :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift3GraceMin" runat="server" FormatTime="mm"
                            meta:resourcekey="txtShift3GraceMinResource1" />
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblShift3MidGrace" runat="server"
                            meta:resourcekey="lblShift3MidGraceResource1" Text=" Middle Grace :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift3MidGraceMin" runat="server" FormatTime="mm"
                            meta:resourcekey="txtShift3MidGraceMinResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblShift3EndGrace" runat="server"
                            meta:resourcekey="lblShift3EndGraceResource1" Text="Out Grace :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtShift3EndGrace" runat="server" FormatTime="mm"
                            meta:resourcekey="txtShift3EndGraceResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkWktShift3IsOverNight" runat="server"
                            meta:resourcekey="chkWktShift3IsOverNightResource1" Text="OverNight" />

                        <asp:CheckBox ID="chkWktShift3IsOptional" runat="server"
                            meta:resourcekey="chkWktShift3IsOptionalResource1" Text="Optional" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
