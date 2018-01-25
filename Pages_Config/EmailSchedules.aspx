<%@ Page Title="Email Schedule" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="EmailSchedules.aspx.cs"
    Inherits="EmailSchedules" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="TextTimeServerControl" Namespace="TextTimeServerControl" TagPrefix="Almaalim" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Text="[None]" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Schedule Name(En)" Value="SchNameEn" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Schedule Name(Ar)" Value="SchNameAr" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png"
                        meta:resourcekey="btnFilterResource1" CssClass="LeftOverlay" />
                </div>
                <div class="col2">
                    <asp:HiddenField ID="hdnSchID" runat="server" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender"
                        meta:resourcekey="grdSchedulesResource1">
                        <PagerStyle HorizontalAlign="Left" />

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:TemplateField HeaderText="Type" SortExpression="SchType" meta:resourcekey="BoundFieldResource2">
                                <ItemTemplate>
                                    <%# GrdDisplayType(Eval("SchType"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="SchID" DataField="SchID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField HeaderText="Name(Ar)" DataField="SchNameAr" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                            <asp:BoundField HeaderText="Name(En)" DataField="SchNameEn" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                            <asp:TemplateField HeaderText="Start Date" SortExpression="SchStartDate" InsertVisible="False"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("SchStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="SchEndDate" InsertVisible="False"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("SchEndDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("SchID") %>'
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
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
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

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Schedule Name(Ar) :" meta:resourcekey="lblNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server"
                            meta:resourcekey="txtScheduleNameArResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvNameArResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Schedule Name(En) :" meta:resourcekey="lblNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server"
                            meta:resourcekey="txtScheduleNameEnResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;" CssClass="CustomValidator"
                            ValidationGroup="vgSave" meta:resourcekey="cvNameEnResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblDescription" runat="server" Text="Description :" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDescription" runat="server" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkStatus" runat="server" meta:resourcekey="chkStatusResource1" /><asp:Label
                            ID="lblStatus" runat="server" Text="Active" meta:resourcekey="lblStatusResource1"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="Label1" runat="server" Text="Schedule Type" meta:resourcekey="Label1Resource1"></asp:Label>
                    </div>
                    <div class="col10">
                        <fieldset>
                            <legend>
                                <asp:DropDownList ID="ddlScheduleType" runat="server" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged" 
                                    AutoPostBack="True" meta:resourcekey="ddlScheduleTypeResource1">
                                    <asp:ListItem Value="Hourly" meta:resourcekey="ListItemResource4" Text="Hourly"></asp:ListItem>
                                    <asp:ListItem Value="Daily" meta:resourcekey="ListItemResource5" Text="Daily"></asp:ListItem>
                                    <asp:ListItem Value="Weekly" meta:resourcekey="ListItemResource6" Text="Weekly"></asp:ListItem>
                                    <%--<asp:ListItem Value="WeeklySkip" meta:resourcekey="ListItemResource7" Text="Weekly skip"></asp:ListItem>--%>
                                    <%--<asp:ListItem Value="WeekNumber" meta:resourcekey="ListItemResource8" Text="Week number"></asp:ListItem>--%>
                                    <asp:ListItem Value="Calendar" meta:resourcekey="ListItemResource9" Text="Calendar"></asp:ListItem>
                                    <asp:ListItem Value="Once" meta:resourcekey="ListItemResource10" Text="Run once"></asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1" Enabled="True">
                                </ajaxToolkit:AnimationExtender>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1" Enabled="True">
                                </ajaxToolkit:AnimationExtender>
                                <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay2" meta:resourcekey="lnkShow1Resource1" />
                                <div id="pnlInfo1" class="flyOutDiv">
                                    <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                        CssClass="flyOutDivCloseX glyphicon glyphicon-remove" meta:resourcekey="lnkClose1Resource1" />
                                    <p>
                                        <br />
                                        <asp:Label ID="lblHint1" runat="server" Text="As per the type of the schedule select the day(s),Months(s),week(s)"
                                            meta:resourcekey="lblHintResource1"></asp:Label>
                                    </p>
                                </div>
                            </legend>
                            <div class="clearfix"></div>
                            <div id="divHour" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRunScheduleEvery" runat="server" Text="Run the schedule every:"
                                            meta:resourcekey="lblRunthescheduleeveryResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <Almaalim:TextTime ID="ttRunScheduleEvery" runat="server" FormatTime="hhmm" CssClass="TimeCss" />
                                         <asp:CustomValidator ID="cvRunScheduleEvery" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Run the schedule every is required' /&gt;"
                                            ValidationGroup="vgSave" OnServerValidate="RunScheduleEvery_ServerValidate" EnableClientScript="False" Enabled ="false"
                                            ControlToValidate="txtValid" meta:resourcekey="cvRunScheduleEveryResource1" CssClass="CustomValidator"></asp:CustomValidator>
                                    </div>
                                </div>
                            </div>
                            <div id="divDaysOfWeek" runat="server" visible="False">

                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblOnthefollowingdays" runat="server" Text="On the following days:"
                                            meta:resourcekey="lblOnthefollowingdaysResource1"></asp:Label>
                                    </div>
                                    <div class="col10">
                                        <asp:CheckBoxList ID="cblDaysOfWeek" runat="server"  RepeatDirection="Horizontal" RepeatColumns="7" meta:resourcekey="cblDaysOfWeekResource1">
                                            <asp:ListItem Value="1" Text="Sunday"    meta:resourcekey="SundayResource1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Monday"    meta:resourcekey="MondayResource1"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Tuesday"   meta:resourcekey="TuesdayResource1"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Wednesday" meta:resourcekey="WednesdayResource1"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Thursday"  meta:resourcekey="ThursdayResource1"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Friday"    meta:resourcekey="FridayResource1"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Saturday"  meta:resourcekey="SaturdayResource1"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CustomValidator id="cvDaysOfWeek" runat="server"
                                            Text="&lt;img src='../images/message_exclamation.png' title='Please Select Atleast one Day' /&gt;" 
                                            ValidationGroup="vgSave"
                                            ErrorMessage="Please Select Atleast one Day" CssClass="CustomValidator"
                                            OnServerValidate="DaysOfWeek_ServerValidate" 
                                            EnableClientScript="False" Enabled ="false"
                                            ControlToValidate="txtValid" meta:resourcekey="cvDaysOfWeekResource1"></asp:CustomValidator>
                                    </div>
                                </div>

                            </div>
                            <div id="divWeekOfMonth" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblOnweekofmonth" runat="server" Text="On week of month:" meta:resourcekey="lblOnweekofmonthResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:DropDownList ID="ddlWeekOfMonth" runat="server" meta:resourcekey="ddlWeekOfMonthResource1">
                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource71" Text="1st"></asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource72" Text="2nd"></asp:ListItem>
                                            <asp:ListItem Value="3" meta:resourcekey="ListItemResource73" Text="3rd"></asp:ListItem>
                                            <asp:ListItem Value="4" meta:resourcekey="ListItemResource74" Text="4th"></asp:ListItem>
                                            <asp:ListItem Value="5" meta:resourcekey="ListItemResource75" Text="Last"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="divDayRepeat" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRepeatDays" runat="server" Text="Repeat after this number of days:"
                                            meta:resourcekey="lblRepeatDaysResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:TextBox ID="txtRepeatDays" runat="server" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtRepeatDaysResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvRepeatDays" runat="server" ControlToValidate="txtRepeatDays" CssClass="CustomValidator"
                                            Text="<img src='../images/Exclamation.gif' title='Repeat after this number of days is required' />"
                                            ValidationGroup="vgSave" EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" Enabled ="false"
                                            meta:resourcekey="rvRepeatDaysResource1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div id="divWeekRepeat" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRepeatweeks" runat="server" Text="Repeat after this number of weeks:"
                                            meta:resourcekey="lblRepeatweeksResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:TextBox ID="txtRepeatWeek" runat="server" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtRepeatWeekResource1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divMonths" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblMonths" runat="server" Text="Months:" meta:resourcekey="lblMonthsResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBoxList ID="cblMonth" runat="server"  RepeatDirection="Horizontal" RepeatColumns="7" meta:resourcekey="cblMonthResource1">
                                        </asp:CheckBoxList>
                                        <asp:CustomValidator id="cvMonth" runat="server"
                                            Text="&lt;img src='../images/message_exclamation.png' title='Please Select Atleast one Month' /&gt;" 
                                            ValidationGroup="vgSave"
                                            ErrorMessage="Please Select Atleast one Month" CssClass="CustomValidator"
                                            OnServerValidate="Month_ServerValidate" 
                                            EnableClientScript="False" Enabled ="false"
                                            ControlToValidate="txtValid" meta:resourcekey="cvMonthResource1"></asp:CustomValidator>
                                    </div>
                                </div>
                            </div>
                            <div id="divCalendar" runat="server" visible="False">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblCalendarDays" runat="server" Text="Calendar days:" meta:resourcekey="lblCalendardaysResource1"></asp:Label>
                                    </div>
                                    <div class="col10">
                                        <asp:CheckBoxList ID="cblCalendarDays" runat="server"   RepeatDirection="Horizontal" RepeatColumns="8" meta:resourcekey="cblCalendardaysResource1">
                                        </asp:CheckBoxList>
                                        <asp:CustomValidator id="cvCalendarDays" runat="server"
                                            Text="&lt;img src='../images/message_exclamation.png' title='Please Select Atleast one Day' /&gt;" 
                                            ValidationGroup="vgSave"
                                            ErrorMessage="Please Select Atleast one Day" CssClass="CustomValidator"
                                            OnServerValidate="CalendarDays_ServerValidate" 
                                            EnableClientScript="False" Enabled ="false"
                                            ControlToValidate="txtValid" meta:resourcekey="cvCalendarDaysResource1"></asp:CustomValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:"  meta:resourcekey="lblStartTimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" CssClass="TimeCss"  TimePickerValidationGroup="vgSave" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='Start Time is required' /&gt;" meta:resourcekey="tpStartTimeResource1"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" meta:resourcekey="lblStartDateResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate" />
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date :" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                                </div>
                            </div>
                        </fieldset>

                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblSubject" runat="server" Text="Subject :" meta:resourcekey="lblSubjectResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtSubject" runat="server"  meta:resourcekey="txtSubjectResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvSubject" ControlToValidate="txtSubject" CssClass="CustomValidator"
                            Text="<img src='../images/Exclamation.gif' title='Subject is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" meta:resourcekey="rfvSubjectResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblMessage" runat="server" Text="Message :" meta:resourcekey="lblMessageResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMessage" runat="server" Columns="50" Rows="10" TextMode="MultiLine"
                            BorderStyle="Solid" BorderWidth="1px" meta:resourcekey="txtMessageResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvMessage" ControlToValidate="txtMessage" CssClass="CustomValidator"
                            Text="<img src='../images/Exclamation.gif' title='Message is required' />"
                            ValidationGroup="vgSave" EnableClientScript="False" Display="Dynamic" SetFocusOnError="True"
                            meta:resourcekey="rfvMessageResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblReportGroup" runat="server" Text="Report :" meta:resourcekey="lblReportGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlReportGroup" runat="server" meta:resourcekey="ddlReportGroupResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rvReportGroup" ControlToValidate="ddlReportGroup" CssClass="CustomValidator"
                            EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='The report must be selected' />"
                            ValidationGroup="vgSave" meta:resourcekey="rfvReportGroupResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpGroup" runat="server" Text="Users :" meta:resourcekey="lblEmpGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:ListBox ID="lbxUsers" runat="server" ValidationGroup="vgSave" CssClass="widthAuto"
                            meta:resourcekey="lbxUsersResource1" SelectionMode="Multiple" Enabled="False"></asp:ListBox>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2" Enabled="True">
                        </ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2" Enabled="True">
                        </ajaxToolkit:AnimationExtender>
                        <span Class="LeftOverlay">
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" meta:resourcekey="lnkShow2Resource1"  />
                            </span>
                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" meta:resourcekey="lnkClose2Resource1" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint2" runat="server" Text="List of users to whoom mail has to be send as per the type of the schedule"
                                    meta:resourcekey="lblHintResource2"></asp:Label>
                            </p>
                        </div>
                        <asp:RequiredFieldValidator ID="rvUsers" runat="server" Text="<img src='../images/Exclamation.gif' title='The Users must be selected' />" CssClass="CustomValidator"
                            ControlToValidate="lbxUsers" InitialValue="0" ValidationGroup="vgSave" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblReportFormat" runat="server" Text="Report Format :" meta:resourcekey="lblReportFormatResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlReportFormat" runat="server" meta:resourcekey="ddlReportFormatResource1">
                            <asp:ListItem Text="-Select Format-" Value="0" meta:resourcekey="ListItemResource76"></asp:ListItem>
                            <asp:ListItem Text="Excel" Value="Excel" meta:resourcekey="ListItemResource77"></asp:ListItem>
                            <asp:ListItem Text="Pdf" Value="Pdf" meta:resourcekey="ListItemResource78"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvReportFormat" ControlToValidate="ddlReportFormat" CssClass="CustomValidator"
                            InitialValue="0" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='The Report Format must be selected' />"
                            ValidationGroup="vgSave" meta:resourcekey="rfvReportFormatResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="False" meta:resourcekey="txtIDResource1"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
