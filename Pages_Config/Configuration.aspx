<%@ Page Title="General Configuration" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="Configuration.aspx.cs" Inherits="Configuration" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<%@ Register Assembly="TextTimeServerControl" Namespace="TextTimeServerControl" TagPrefix="Almaalim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>

    <%--<link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnAdd_Click" Visible="False"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit"
                        OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

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

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col12">
                        <asp:Label ID="lblWorkTimeSetting" runat="server" Text="Shift Setting" CssClass="h4"
                            meta:resourcekey="lblWorkTimeSettingResource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblcfgOrderTransType" runat="server" Text="Order Trans Type :"
                            meta:resourcekey="lblcfgOrderTransTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlcfgOrderTransType" runat="server" Enabled="False" AutoPostBack="True" meta:resourcekey="ddllblcfgOrderTransTypeResource1"
                            OnSelectedIndexChanged="ddlcfgOrderTransType_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="MT" meta:resourcekey="ListItemResource8">Machine Type</asp:ListItem>
                            <asp:ListItem Value="FISO" meta:resourcekey="ListItemResource9">First IN Second OUT</asp:ListItem>
                            <asp:ListItem Value="FILO" meta:resourcekey="ListItemResource10">First IN Last OUT</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        </div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkAutoIn" runat="server" Enabled="False" Text="Auto In" 
                            AutoPostBack="True" OnCheckedChanged="chkAutoIn_CheckedChanged"
                                        meta:resourcekey="chkAutoInResource1" />

                                        <%--<ajaxToolkit:AnimationExtender ID="AnimationExtenderShow?" runat="server" TargetControlID="lnkShow?"></ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose?" runat="server" TargetControlID="lnkClose?"></ajaxToolkit:AnimationExtender>
                                        <asp:ImageButton ID="lnkShow?" runat="server" OnClientClick="return false;" ImageUrl = "~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                        <div id="pnlInfo?" class="flyOutDiv">
                                            <asp:LinkButton ID="lnkClose?" runat="server" Text="X" OnClientClick="return false;" CssClass= "flyOutDivCloseX glyphicon glyphicon-remove" />
                                            <p>
                                                <br />
                                                <asp:Label ID="lblHint?" runat="server" Text="You can initialize here" meta:resourcekey="lblHint?Resource"></asp:Label>
                                            </p>
                                        </div>--%>

                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl = "~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                            <p>
                                                <br />
                                                <asp:Label ID="lblHint1" runat="server" Text="This option determines whether this feature can be selected on the Employee page or not, it means the ability to add movement automatic entry automatically" meta:resourcekey="lblHint1Resource"></asp:Label>
                                            </p>
                                        </div>
                        </div>
                        </div>
                               <div class="col2">
                        </div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkIsTakeAutoIn" runat="server" Enabled="False" 
                                        Text="Auto In Priority" meta:resourcekey="chkIsTakeAutoInResource1" />
                        <div class="flyoutWrap">
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"></ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                            <p>
                                                <br />
                                                <asp:Label ID="lblHint2" runat="server" Text="Identify priority choice between regular entry movement, if any, automatic login movement at treatment, when selecting priority will be given to automatic login movement." meta:resourcekey="lblHint2Resource"></asp:Label>
                                            </p>
                                        </div>
                               </div>
            </div>
            </div>
            <div class="row">
                    <div class="col2">
                        </div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkAutoOut" runat="server" Enabled="False" Text="Auto Out" 
                            AutoPostBack="True" OnCheckedChanged="chkAutoOut_CheckedChanged"
                                        meta:resourcekey="chkAutoOutResource1" />
                        <div class="flyoutWrap">
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow3" runat="server" TargetControlID="lnkShow3"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose3" runat="server" TargetControlID="lnkClose3"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow3" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo3" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose3" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint3" runat="server" Text="This option determines whether this feature can be selected on the employee page or not, it means the ability to add automatic movement out automatically." meta:resourcekey="lblHint3Resource"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                        </div>
                               <div class="col2">
                        </div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkIsTakeAutoOut" runat="server" Enabled="False" 
                                        Text="Auto Out Priority" meta:resourcekey="chkIsTakeAutoOutResource1" />
                        <div class="flyoutWrap">
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow4" runat="server" TargetControlID="lnkShow4"></ajaxToolkit:AnimationExtender>
                                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose4" runat="server" TargetControlID="lnkClose4"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow4" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                        <div id="pnlInfo4" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose4" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                            <p>
                                                <br />
                                                <asp:Label ID="lblHint4" runat="server" Text="Identify priority choice between regular exit transaction, if any, and automatic exit transaction at treatment, when selecting priority will be given to automatic exit transaction." meta:resourcekey="lblHint4Resource"></asp:Label>
                                            </p>
                                        </div>
                              </div>
                </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblMiddleGapCount" runat="server" Text="Middle Gap (No) :" 
                                        meta:resourcekey="lblMiddleGapCountResource1"></asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtMiddleGapCount" runat="server" AutoCompleteType="Disabled" 
                                        Enabled="False" onkeypress="return OnlyNumber(event);" 
                                        meta:resourcekey="txtMiddleGapCountResource1"></asp:TextBox>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow5" runat="server" TargetControlID="lnkShow5"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose5" runat="server" TargetControlID="lnkClose5"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow5" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo5" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose5" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint5" runat="server" Text="The number of gaps allowed in shift the grace which duration is specified in the page working times." meta:resourcekey="lblHint5Resource"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMiddleGapCount"
                                    EnableClientScript="False" ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator"
                                    ValidationGroup="vgSave" 
                                        meta:resourcekey="RegularExpressionValidator1Resource1"><img src="../images/Exclamation.gif" title="Enter Only Numbers!" />
                                    </asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="reqBranchManager1" runat="server" 
                                        ControlToValidate="txtMiddleGapCount" EnableClientScript="False" CssClass="CustomValidator"
                                        ErrorMessage="Middle Gap Period is required!" 
                                        Text="&lt;img src='../images/Exclamation.gif' title='Middle Gap Period is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="reqBranchManager1Resource1"></asp:RequiredFieldValidator>
                                </div>

                               <div class="col2">
                                    <asp:Label ID="lblICApprovalPercent" runat="server" 
                                        Text="incomplete Approval Percent :" 
                                        meta:resourcekey="lblICApprovalPercentResource1"></asp:Label>
                               </div>
                    <div class="col4">
                                   <asp:TextBox ID="txtICApprovalPercent" runat="server" 
                                        AutoCompleteType="Disabled" Enabled="False" MaxLength="3" 
                                        onkeypress="return OnlyNumber(event);" 
                                        meta:resourcekey="txtICApprovalPercentResource1"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server" Text=" % " CssClass="LeftOverlay3"
                                        meta:resourcekey="Label1Resource1"></asp:Label>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow6" runat="server" TargetControlID="lnkShow6"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose6" runat="server" TargetControlID="lnkClose6"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow6" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo6" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose6" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint6" runat="server" Text="This property specifies the percentage allowed for incomplete movement, when you choose 50%, for example, then you can not close the movement completed only a period equal to or less than half the duration of the Shift." meta:resourcekey="lblHint6Resource"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtICApprovalPercent"
                                    EnableClientScript="False" ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator"
                                    ValidationGroup="vgSave" 
                                        meta:resourcekey="RegularExpressionValidator3Resource1"><img src="../images/Exclamation.gif" 
                                    title="Enter Only Numbers!" /></asp:RegularExpressionValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtICApprovalPercent" EnableClientScript="False" CssClass="CustomValidator"
                                        ErrorMessage="incomplete Approval Percent is required!" 
                                        Text="&lt;img src='../images/Exclamation.gif' title='incomplete Approval Percent is required!' /&gt;" 
                                        ValidationGroup="vgSave" 
                                        meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>  
                                </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblRedundantInSelection" runat="server" 
                                        meta:resourcekey="lblRedundantInSelectionResource1">Redundant In Selection :</asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:DropDownList ID="ddlRedundantInSelection" runat="server" Enabled="False" meta:resourcekey="ddlRedundantInSelectionResource1">
                                        <asp:ListItem Selected="True" Value="F" meta:resourcekey="ListItemResource1">First</asp:ListItem>
                                        <asp:ListItem Value="L" meta:resourcekey="ListItemResource2">Last</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow7" runat="server" TargetControlID="lnkShow7"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose7" runat="server" TargetControlID="lnkClose7"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow7" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo7" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose7" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint7" runat="server" Text="This option determines access movement, which will be processed (first or last) in the case of more of the entry in a row and ignore the other movements." meta:resourcekey="lblHint7Resource"></asp:Label>
                                        </p>
                                    </div>
                              </div>

                               <div class="col2">
                                    <asp:Label ID="lblRedundantOutSelection" runat="server" 
                                        meta:resourcekey="lblRedundantOutSelectionResource1">Redundant Out Selection :</asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:DropDownList ID="ddlRedundantOutSelection" runat="server" Enabled="False" meta:resourcekey="ddlRedundantOutSelectionResource1">
                                        <asp:ListItem Value="F" meta:resourcekey="ListItemResource3">First</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="L" meta:resourcekey="ListItemResource4">Last</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                </div>
                <div class="row">
                    <div class="col12">
                                        <asp:Label ID="Label2" runat="server" Text="Overtime Setting" CssClass="h4"
                                            meta:resourcekey="Label2Resource1"></asp:Label>
                                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblMaxPercOT" runat="server" 
                                        Text="Overtime Interval (Max/Day) :" 
                                        meta:resourcekey="lblMaxPercOTResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtMaxPercOT" runat="server" FormatTime="hhmm" 
                                        meta:resourcekey="txtMaxPercOTResource1" />
                        <div class="flyoutWrap">
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow8" runat="server" TargetControlID="lnkShow8"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose8" runat="server" TargetControlID="lnkClose8"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow8" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo8" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose8" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint8" runat="server" Text="Limit the time allowed a maximum of overtime a day, which the employee can not provide additional job applications when crossed." meta:resourcekey="lblHint8Resource"></asp:Label>
                                        </p>
                                    </div>
                               </div>
                </div>
                </div>
               
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblOTBeginEarlyInterval" runat="server" 
                                        Text="Begin Early Interval :" 
                                        meta:resourcekey="lblOTBeginEarlyIntervalResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtOTBeginEarlyInterval" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtOTBeginEarlyIntervalResource1" />
                        <div class="flyoutWrap">
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow10" runat="server" TargetControlID="lnkShow10"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose10" runat="server" TargetControlID="lnkClose10"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow10" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo10" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose10" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint10" runat="server" Text="Less duration can be calculated as overtime early attendance and who is after this period." meta:resourcekey="lblHint10Resource"></asp:Label>
                                        </p>
                                    </div>
                                 </div>
                        </div>
                    
                    <div class="col4">
                                    <asp:CheckBox ID="chkOTBeginEarlyFlag" runat="server" Enabled="False" 
                                        Text="Begin Early overtime" 
                                        meta:resourcekey="chkOTBeginEarlyFlagResource1" />
                        <div class="flyoutWrap">
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow9" runat="server" TargetControlID="lnkShow9"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose9" runat="server" TargetControlID="lnkClose9"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow9" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo9" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose9" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint9" runat="server" Text="When selected early attendance is calculated as overtime for employees" meta:resourcekey="lblHint9Resource"></asp:Label>
                                        </p>
                                    </div>
                                 </div>
                </div>
                </div>
                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkOTOutLateFlag" runat="server" Enabled="False" 
                                        Text="Out Late overtime" meta:resourcekey="chkOTOutLateFlagResource1" />
                               </div>

                               <div class="col2">
                                    <asp:Label ID="lblOTOutLateInterval" runat="server" Text="Out Late Interval :" 
                                        meta:resourcekey="lblOTOutLateIntervalResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtOTOutLateInterval" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtOTOutLateIntervalResource1" />
                               </div>
                </div>
                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkOTOutOfShiftFlag" runat="server" Enabled="False" 
                                        Text="Out Of Shift overtime" 
                                        meta:resourcekey="chkOTOutOfShiftFlagResource1" />
                               </div>

                               <div class="col2">
                                    <asp:Label ID="lblOTOutOfShiftInterval" runat="server" 
                                        Text="Out Of Shift Interval :" 
                                        meta:resourcekey="lblOTOutOfShiftIntervalResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtOTOutOfShiftInterval" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtOTOutOfShiftIntervalResource1" />
                                </div>
                </div>
                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkOTNoShiftFlag" runat="server" Enabled="False" 
                                        Text="No Shift overtime" meta:resourcekey="chkOTNoShiftFlagResource1" />
                             </div>

                               <div class="col2">
                                    <asp:Label ID="lblOTNoShiftInterval" runat="server" Text="No Shift Interval :" 
                                        meta:resourcekey="lblOTNoShiftIntervalResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtOTNoShiftInterval" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtOTNoShiftIntervalResource1" />
                                 </div>
                </div>
                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                                    <asp:CheckBox ID="chkOTVacationFlag" runat="server" Enabled="False" 
                                        Text="Vacation overtime" meta:resourcekey="chkOTVacationFlagResource1" />
                               </div>

                               <div class="col2">
                                    <asp:Label ID="lblOTVacationInterval" runat="server" Text="Vacation Interval :" 
                                        meta:resourcekey="lblOTVacationIntervalResource1"></asp:Label>
                              </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtOTVacationInterval" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtOTVacationIntervalResource1" />
                                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                                        <asp:Label ID="Label4" runat="server" Text="Request Setting" CssClass="h4"
                                            meta:resourcekey="Label4Resource1"></asp:Label>
                                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="Label12" runat="server" Text="Vacation Reset Date :" 
                                        meta:resourcekey="Label12Resource1"></asp:Label>
                                </div>
                    <div class="col4">
                                               <Cal:Calendar2 ID="calVacResetDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave"/>
                                            
                                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow11" runat="server" TargetControlID="lnkShow11"></ajaxToolkit:AnimationExtender>
                                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose11" runat="server" TargetControlID="lnkClose11"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow11" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                                <div id="pnlInfo11" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose11" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                                    <p>
                                                        <br />
                                                        <asp:Label ID="lblHint11" runat="server" Text="The date on which he has been re-calculating vacation days that are of the types of vacations that are re-calculated." meta:resourcekey="lblHint11Resource"></asp:Label>
                                                    </p>
                                                </div>
                                          </div>
                                          </div>

                               <div class="col2">
                                    <asp:Label ID="lblDaysLimitReqVac" runat="server" 
                                        Text="Days limit to Request Vacation :" 
                                        meta:resourcekey="lblDaysLimitReqVacResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                   <asp:TextBox ID="txtDaysLimitReqVac" runat="server" 
                                        AutoCompleteType="Disabled" Enabled="False" Width="60px" MaxLength="3" 
                                        onkeypress="return OnlyNumber(event);" 
                                        meta:resourcekey="txtDaysLimitReqVacResource1"></asp:TextBox>
                                    <asp:Label ID="lblDayType" runat="server" Text=" Days " 
                                        meta:resourcekey="lblDayTypeResource1"></asp:Label>

                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow14" runat="server" TargetControlID="lnkShow14"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose14" runat="server" TargetControlID="lnkClose14"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow14" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo14" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose14" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint14" runat="server" Text="This property specifies the Count Days Limit allowe for employee to request vacation for absent, when you left Text zero No limit for Days Request, but if you entrie write any number the Employee can't Request vacation after passing days limit." meta:resourcekey="lblHint14Resource"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="Label3" runat="server" Text="Form Request :" 
                                        meta:resourcekey="Label3Resource1"></asp:Label>
                                </div>
                    <div class="col4">
                                              <asp:CheckBoxList ID="cblFormRequest" runat="server" Width="336px" 
                                                  meta:resourcekey="cblFormRequestResource1" RepeatColumns="2">
                                              </asp:CheckBoxList>
                                        
                                             <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow12" runat="server" TargetControlID="lnkShow12"></ajaxToolkit:AnimationExtender>
                                             <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose12" runat="server" TargetControlID="lnkClose12"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow12" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                             <div id="pnlInfo12" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose12" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                                <p>
                                                    <br />
                                                    <asp:Label ID="lblHint12" runat="server" Text="This option specifies the form you will be asked to type the request is compulsory or not, and choose the types of applications required form." meta:resourcekey="lblHint12Resource"></asp:Label>
                                                </p>
                                             </div>
                                         </div>
                </div>
                <div class="row">
                    <div class="col12">
                                        <asp:Label ID="Label5" runat="server" Text="General Setting" CssClass="h4" 
                                            meta:resourcekey="Label5Resource1"></asp:Label>
                                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblSessionDuration" runat="server" Text="Session timeout :" 
                                        meta:resourcekey="lblSessionDurationResource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <Almaalim:TextTime ID="txtSessionDuration" runat="server" FormatTime="mm" 
                                        meta:resourcekey="txtSessionDurationResource1" />
                                </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblcfgApprovalsMonthCount" runat="server" Text="Approvals Per Month Count for (Report) :" 
                                        meta:resourcekey="lblcfgApprovalsMonthCountResource1"></asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtcfgApprovalsMonthCount" runat="server" Width="30px" MaxLength="2" 
                                        onkeypress="return OnlyNumber(event);" 
                            meta:resourcekey="txtcfgApprovalsMonthCountResource1"></asp:TextBox>
                              </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblcfgTransInDaysCount" runat="server" Text="Transaction Per Day Count (Report) :" 
                                        meta:resourcekey="lblcfgTransInDaysCountResource1"></asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtcfgTransInDaysCount" runat="server" Width="30px" MaxLength="2" 
                                        onkeypress="return OnlyNumber(event);" 
                            meta:resourcekey="txtcfgTransInDaysCountResource1"></asp:TextBox>
                                 </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="Label6" runat="server" Text="Required Data Language :" 
                                        meta:resourcekey="Label6Resource1"></asp:Label>
                               </div>
                    <div class="col4">
                                             <asp:RadioButtonList ID="rdlDataLang" runat="server" Enabled="False" 
                                                RepeatDirection="Horizontal" meta:resourcekey="rdlDataLangResource1">
                                                <asp:ListItem Value="B" meta:resourcekey="ListItemResource5">Arabic - English</asp:ListItem>
                                                <asp:ListItem Value="A" meta:resourcekey="ListItemResource6">Arabic</asp:ListItem>
                                                <asp:ListItem Value="E" meta:resourcekey="ListItemResource7">English</asp:ListItem>
                                             </asp:RadioButtonList>
                                          
                                             <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow13" runat="server" TargetControlID="lnkShow13"></ajaxToolkit:AnimationExtender>
                                             <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose13" runat="server" TargetControlID="lnkClose13"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow13" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                             <div id="pnlInfo13" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose13" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                                <p>
                                                    <br />
                                                    <asp:Label ID="lblHint13" runat="server" Text="Specify the language of the input data required and can not be conservation without entered." meta:resourcekey="lblHint13Resource"></asp:Label>
                                                </p>
                                            </div>
                                            </div>
                </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


