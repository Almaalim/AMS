<%@ Page Title="Email Schedule" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" ValidateRequest="false" CodeFile="EmailSchedules.aspx.cs"
    Inherits="EmailSchedules" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
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
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender" EnableModelValidation="True"
                        meta:resourcekey="grdSchedulesResource1">
                        <PagerStyle HorizontalAlign="Left" />

                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="SchID" DataField="SchID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField HeaderText="Type" DataField="SchType" meta:resourcekey="BoundFieldResource2" />
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
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Schedule Name(Ar)" meta:resourcekey="lblNameArResource1"></asp:Label>
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
                        <span id="spnNameEn" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Schedule Name(En)" meta:resourcekey="lblNameEnResource1"></asp:Label>
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
                        <asp:Label ID="lblDescription" runat="server" Text="Description" meta:resourcekey="lblDescriptionResource1"></asp:Label>
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
                                    <asp:ListItem Value="Hourly" meta:resourcekey="ListItemResource4">Hourly</asp:ListItem>
                                    <asp:ListItem Value="Daily" meta:resourcekey="ListItemResource5">Daily</asp:ListItem>
                                    <asp:ListItem Value="Weekly" meta:resourcekey="ListItemResource6">Weekly</asp:ListItem>
                                    <asp:ListItem Value="WeeklySkip" meta:resourcekey="ListItemResource7">Weekly skip</asp:ListItem>
                                    <asp:ListItem Value="WeekNumber" meta:resourcekey="ListItemResource8">Week number</asp:ListItem>
                                    <asp:ListItem Value="Calendar" meta:resourcekey="ListItemResource9">Calendar</asp:ListItem>
                                    <asp:ListItem Value="Once" meta:resourcekey="ListItemResource10">Run once</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1">
                                </ajaxToolkit:AnimationExtender>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1">
                                </ajaxToolkit:AnimationExtender>
                                <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay2" />
                                <div id="pnlInfo1" class="flyOutDiv">
                                    <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                        CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                    <p>
                                        <br />
                                        <asp:Label ID="lblHint1" runat="server" Text="As per the type of the schedule select the day(s),Months(s),week(s)"
                                            meta:resourcekey="lblHintResource1"></asp:Label>
                                    </p>
                                </div>
                            </legend>
                            <div class="clearfix"></div>
                            <div id="divHour" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRunthescheduleevery" runat="server" Text="Run the schedule every:"
                                            meta:resourcekey="lblRunthescheduleeveryResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:TextBox ID="txtEveryHour" runat="server" Width="30px" meta:resourcekey="txtEveryHourResource1">1</asp:TextBox>
                                    </div>
                                    <div class="col2">
                                        <asp:Label ID="lblhours" runat="server" Text="hours:" meta:resourcekey="lblhoursResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:DropDownList ID="ddlEveryMinute" runat="server" meta:resourcekey="ddlEveryMinuteResource1">
                                            <asp:ListItem Value="00" meta:resourcekey="ListItemResource11">00</asp:ListItem>
                                            <asp:ListItem Value="01" meta:resourcekey="ListItemResource12">01</asp:ListItem>
                                            <asp:ListItem Value="02" meta:resourcekey="ListItemResource13">02</asp:ListItem>
                                            <asp:ListItem Value="03" meta:resourcekey="ListItemResource14">03</asp:ListItem>
                                            <asp:ListItem Value="04" meta:resourcekey="ListItemResource15">04</asp:ListItem>
                                            <asp:ListItem Value="05" meta:resourcekey="ListItemResource16">05</asp:ListItem>
                                            <asp:ListItem Value="06" meta:resourcekey="ListItemResource17">06</asp:ListItem>
                                            <asp:ListItem Value="07" meta:resourcekey="ListItemResource18">07</asp:ListItem>
                                            <asp:ListItem Value="08" meta:resourcekey="ListItemResource19">08</asp:ListItem>
                                            <asp:ListItem Value="09" meta:resourcekey="ListItemResource20">09</asp:ListItem>
                                            <asp:ListItem Value="10" meta:resourcekey="ListItemResource21">10</asp:ListItem>
                                            <asp:ListItem Value="11" meta:resourcekey="ListItemResource22">11</asp:ListItem>
                                            <asp:ListItem Value="12" meta:resourcekey="ListItemResource23">12</asp:ListItem>
                                            <asp:ListItem Value="13" meta:resourcekey="ListItemResource24">13</asp:ListItem>
                                            <asp:ListItem Value="14" meta:resourcekey="ListItemResource25">14</asp:ListItem>
                                            <asp:ListItem Value="15" meta:resourcekey="ListItemResource26">15</asp:ListItem>
                                            <asp:ListItem Value="16" meta:resourcekey="ListItemResource27">16</asp:ListItem>
                                            <asp:ListItem Value="17" meta:resourcekey="ListItemResource28">17</asp:ListItem>
                                            <asp:ListItem Value="18" meta:resourcekey="ListItemResource29">18</asp:ListItem>
                                            <asp:ListItem Value="19" meta:resourcekey="ListItemResource30">19</asp:ListItem>
                                            <asp:ListItem Value="20" meta:resourcekey="ListItemResource31">20</asp:ListItem>
                                            <asp:ListItem Value="21" meta:resourcekey="ListItemResource32">21</asp:ListItem>
                                            <asp:ListItem Value="22" meta:resourcekey="ListItemResource33">22</asp:ListItem>
                                            <asp:ListItem Value="23" meta:resourcekey="ListItemResource34">23</asp:ListItem>
                                            <asp:ListItem Value="24" meta:resourcekey="ListItemResource35">24</asp:ListItem>
                                            <asp:ListItem Value="25" meta:resourcekey="ListItemResource36">25</asp:ListItem>
                                            <asp:ListItem Value="26" meta:resourcekey="ListItemResource37">26</asp:ListItem>
                                            <asp:ListItem Value="27" meta:resourcekey="ListItemResource38">27</asp:ListItem>
                                            <asp:ListItem Value="28" meta:resourcekey="ListItemResource39">28</asp:ListItem>
                                            <asp:ListItem Value="29" meta:resourcekey="ListItemResource40">29</asp:ListItem>
                                            <asp:ListItem Value="30" meta:resourcekey="ListItemResource41">30</asp:ListItem>
                                            <asp:ListItem Value="31" meta:resourcekey="ListItemResource42">31</asp:ListItem>
                                            <asp:ListItem Value="32" meta:resourcekey="ListItemResource43">32</asp:ListItem>
                                            <asp:ListItem Value="33" meta:resourcekey="ListItemResource44">33</asp:ListItem>
                                            <asp:ListItem Value="34" meta:resourcekey="ListItemResource45">34</asp:ListItem>
                                            <asp:ListItem Value="35" meta:resourcekey="ListItemResource46">35</asp:ListItem>
                                            <asp:ListItem Value="36" meta:resourcekey="ListItemResource47">36</asp:ListItem>
                                            <asp:ListItem Value="37" meta:resourcekey="ListItemResource48">37</asp:ListItem>
                                            <asp:ListItem Value="38" meta:resourcekey="ListItemResource49">38</asp:ListItem>
                                            <asp:ListItem Value="39" meta:resourcekey="ListItemResource50">39</asp:ListItem>
                                            <asp:ListItem Value="40" meta:resourcekey="ListItemResource51">40</asp:ListItem>
                                            <asp:ListItem Value="41" meta:resourcekey="ListItemResource52">41</asp:ListItem>
                                            <asp:ListItem Value="42" meta:resourcekey="ListItemResource53">42</asp:ListItem>
                                            <asp:ListItem Value="43" meta:resourcekey="ListItemResource54">43</asp:ListItem>
                                            <asp:ListItem Value="44" meta:resourcekey="ListItemResource55">44</asp:ListItem>
                                            <asp:ListItem Value="45" meta:resourcekey="ListItemResource56">45</asp:ListItem>
                                            <asp:ListItem Value="46" meta:resourcekey="ListItemResource57">46</asp:ListItem>
                                            <asp:ListItem Value="47" meta:resourcekey="ListItemResource58">47</asp:ListItem>
                                            <asp:ListItem Value="48" meta:resourcekey="ListItemResource59">48</asp:ListItem>
                                            <asp:ListItem Value="49" meta:resourcekey="ListItemResource60">49</asp:ListItem>
                                            <asp:ListItem Value="50" meta:resourcekey="ListItemResource61">50</asp:ListItem>
                                            <asp:ListItem Value="51" meta:resourcekey="ListItemResource62">51</asp:ListItem>
                                            <asp:ListItem Value="52" meta:resourcekey="ListItemResource63">52</asp:ListItem>
                                            <asp:ListItem Value="53" meta:resourcekey="ListItemResource64">53</asp:ListItem>
                                            <asp:ListItem Value="54" meta:resourcekey="ListItemResource65">54</asp:ListItem>
                                            <asp:ListItem Value="55" meta:resourcekey="ListItemResource66">55</asp:ListItem>
                                            <asp:ListItem Value="56" meta:resourcekey="ListItemResource67">56</asp:ListItem>
                                            <asp:ListItem Value="57" meta:resourcekey="ListItemResource68">57</asp:ListItem>
                                            <asp:ListItem Value="58" meta:resourcekey="ListItemResource69">58</asp:ListItem>
                                            <asp:ListItem Value="59" meta:resourcekey="ListItemResource70">59</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblminutes" runat="server" Text="minutes:" meta:resourcekey="lblminutesResource1"></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div id="divDaysOfWeek" runat="server" visible="false">

                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblOnthefollowingdays" runat="server" Text="On the following days:"
                                            meta:resourcekey="lblOnthefollowingdaysResource1"></asp:Label>
                                    </div>
                                    <div class="col10">
                                        <asp:CheckBox ID="chkSun" runat="server" Text="Sun" meta:resourcekey="chkSunResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkMon" runat="server" Text="Mon" meta:resourcekey="chkMonResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkTue" runat="server" Text="Tue" meta:resourcekey="chkTueResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkWed" runat="server" Text="Wed" meta:resourcekey="chkWedResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkThu" runat="server" Text="Thu" meta:resourcekey="chkThuResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkFri" runat="server" Text="Fri" meta:resourcekey="chkFriResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkSat" runat="server" Text="Sat" meta:resourcekey="chkSatResource1"></asp:CheckBox>
                                    </div>
                                </div>

                            </div>
                            <div id="divWeekOfMonth" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblOnweekofmonth" runat="server" Text="On week of month:" meta:resourcekey="lblOnweekofmonthResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:DropDownList ID="ddlWeekOfMonth" runat="server" meta:resourcekey="ddlWeekOfMonthResource1">
                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource71">1st</asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource72">2nd</asp:ListItem>
                                            <asp:ListItem Value="3" meta:resourcekey="ListItemResource73">3rd</asp:ListItem>
                                            <asp:ListItem Value="4" meta:resourcekey="ListItemResource74">4th</asp:ListItem>
                                            <asp:ListItem Value="5" meta:resourcekey="ListItemResource75">Last</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="divDayRepeat" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRepeatDays" runat="server" Text="Repeat after this number of days:"
                                            meta:resourcekey="lblRepeatDaysResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:TextBox ID="txtRepeatDays" runat="server" meta:resourcekey="txtRepeatDaysResource1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divWeekRepeat" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblRepeatweeks" runat="server" Text="Repeat after this number of weeks:"
                                            meta:resourcekey="lblRepeatweeksResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:TextBox ID="txtRepeatWeek" runat="server" meta:resourcekey="txtRepeatWeekResource1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divMonths" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblMonths" runat="server" Text="Months:" meta:resourcekey="lblMonthsResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBox ID="chkJan" runat="server" Text="Jan" meta:resourcekey="chkJanResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkApr" runat="server" Text="Apr" meta:resourcekey="chkAprResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkJul" runat="server" Text="Jul" meta:resourcekey="chkJulResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkOct" runat="server" Text="Oct" meta:resourcekey="chkOctResource1"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col2">
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBox ID="chkFeb" runat="server" Text="Feb" meta:resourcekey="chkFebResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkMay" runat="server" Text="May" meta:resourcekey="chkMayResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkAug" runat="server" Text="Aug" meta:resourcekey="chkAugResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkNov" runat="server" Text="Nov" meta:resourcekey="chkNovResource1"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col2">
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBox ID="chkMar" runat="server" Text="Mar" meta:resourcekey="chkMarResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkJun" runat="server" Text="Jun" meta:resourcekey="chkJunResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkSep" runat="server" Text="Sep" meta:resourcekey="chkSepResource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDec" runat="server" Text="Dec" meta:resourcekey="chkDecResource1"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div id="divCalendar" runat="server" visible="false">
                                <div class="row">
                                    <div class="col2">
                                        <asp:Label ID="lblCalendardays" runat="server" Text="Calendar days:" meta:resourcekey="lblCalendardaysResource1"></asp:Label>
                                    </div>
                                    <div class="col10">
                                        <asp:CheckBox ID="chkDay1" runat="server" Text="1" meta:resourcekey="chkDay1Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay2" runat="server" Text="2" meta:resourcekey="chkDay2Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay3" runat="server" Text="3" meta:resourcekey="chkDay3Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay4" runat="server" Text="4" meta:resourcekey="chkDay4Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay5" runat="server" Text="5" meta:resourcekey="chkDay5Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay6" runat="server" Text="6" meta:resourcekey="chkDay6Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay7" runat="server" Text="7" meta:resourcekey="chkDay7Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay8" runat="server" Text="8" meta:resourcekey="chkDay8Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay9" runat="server" Text="9" meta:resourcekey="chkDay9Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay10" runat="server" Text="10" meta:resourcekey="chkDay10Resource1"></asp:CheckBox>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col2">
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBox ID="chkDay11" runat="server" Text="11" meta:resourcekey="chkDay11Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay12" runat="server" Text="12" meta:resourcekey="chkDay12Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay13" runat="server" Text="13" meta:resourcekey="chkDay13Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay14" runat="server" Text="14" meta:resourcekey="chkDay14Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay15" runat="server" Text="15" meta:resourcekey="chkDay15Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay16" runat="server" Text="16" meta:resourcekey="chkDay16Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay17" runat="server" Text="17" meta:resourcekey="chkDay17Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay18" runat="server" Text="18" meta:resourcekey="chkDay18Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay19" runat="server" Text="19" meta:resourcekey="chkDay19Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay20" runat="server" Text="20" meta:resourcekey="chkDay20Resource1"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col2">
                                    </div>
                                    <div class="col4">
                                        <asp:CheckBox ID="chkDay21" runat="server" Text="21" meta:resourcekey="chkDay21Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay22" runat="server" Text="22" meta:resourcekey="chkDay22Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay23" runat="server" Text="23" meta:resourcekey="chkDay23Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay24" runat="server" Text="24" meta:resourcekey="chkDay24Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay25" runat="server" Text="25" meta:resourcekey="chkDay25Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay26" runat="server" Text="26" meta:resourcekey="chkDay26Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay27" runat="server" Text="27" meta:resourcekey="chkDay27Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay28" runat="server" Text="28" meta:resourcekey="chkDay28Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay29" runat="server" Text="29" meta:resourcekey="chkDay29Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay30" runat="server" Text="30" meta:resourcekey="chkDay30Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDay31" runat="server" Text="31" meta:resourcekey="chkDay31Resource1"></asp:CheckBox>

                                        <asp:CheckBox ID="chkDayLast" runat="server" Text="Last" meta:resourcekey="chkDayLastResource1"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:" meta:resourcekey="lblStartTimeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" CssClass="TimeCss" meta:resourcekey="tpStartTimeResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date" meta:resourcekey="lblStartDateResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationGroup="vgSave" CalTo="calEndDate" />
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date" meta:resourcekey="lblEndDateResource1"></asp:Label>
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
                        <asp:Label ID="lblSubject" runat="server" Text="Subject" meta:resourcekey="lblSubjectResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtSubject" runat="server"  meta:resourcekey="txtSubjectResource1"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator runat="server" ID="rfvSubject1" ControlToValidate="txtSubject"
                            Text="<img src='images/Exclamation.gif' title='Subject is required!' />" ErrorMessage="Subject is required!"
                            ValidationGroup="vgSave" EnableClientScript="False" Display="Dynamic" SetFocusOnError="True"
                            meta:resourcekey="rfvSubjectResource1"></asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvSubject" ControlToValidate="txtSubject" CssClass="CustomValidator"
                            Text="<img src='../images/Exclamation.gif' title='Username is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" meta:resourcekey="rfvSubjectResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblMessage" runat="server" Text="Message" meta:resourcekey="lblMessageResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMessage" runat="server" Columns="50" Rows="10" TextMode="MultiLine"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <%-- <ajaxToolkit:HtmlEditorExtender ID="htmlEditorExtender2" TargetControlID="txtMessage"
                            runat="server" DisplaySourceTab="True">
                            <Toolbar>
                                <ajaxToolkit:Bold />
                                <ajaxToolkit:Italic />
                                <ajaxToolkit:Underline />
                                <ajaxToolkit:HorizontalSeparator />
                                <ajaxToolkit:JustifyLeft />
                                <ajaxToolkit:JustifyCenter />
                                <ajaxToolkit:JustifyRight />
                                <ajaxToolkit:JustifyFull />
                            </Toolbar>
                        </ajaxToolkit:HtmlEditorExtender>--%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvMessage" ControlToValidate="txtMessage" CssClass="CustomValidator"
                            Text="<img src='../images/Exclamation.gif' title='Message is required!' />" ErrorMessage="Message is required!"
                            ValidationGroup="vgSave" EnableClientScript="False" Display="Dynamic" SetFocusOnError="True"
                            meta:resourcekey="rfvMessageResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblReportGroup" runat="server" Text="Report" meta:resourcekey="lblReportGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlReportGroup" runat="server" meta:resourcekey="ddlReportGroupResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rvReportGroup" ControlToValidate="ddlReportGroup" CssClass="CustomValidator"
                            EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Report name is required!' />"
                            ErrorMessage="Report Name is required!" ValidationGroup="vgSave" meta:resourcekey="rfvReportGroupResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpGroup" runat="server" Text="User" meta:resourcekey="lblEmpGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:ListBox ID="lbxUsers" runat="server" ValidationGroup="vgSave" CssClass="widthAuto"
                            meta:resourcekey="lbxUsersResource1" SelectionMode="Multiple" Enabled="False"></asp:ListBox>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2">
                        </ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2">
                        </ajaxToolkit:AnimationExtender>
                        <span Class="LeftOverlay1">
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png"  />
                            </span>
                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint2" runat="server" Text="List of users to whoom mail has to be send as per the type of the schedule"
                                    meta:resourcekey="lblHintResource2"></asp:Label>
                            </p>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="<img src='../images/Exclamation.gif' title='Select Users!' />" CssClass="CustomValidator"
                            ControlToValidate="lbxUsers" ErrorMessage="Select Users!" ValidationGroup="vgSave">  
                        </asp:RequiredFieldValidator>
                        <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="lbxUsers"
                            InitialValue="Select Users" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Select Users!' />"
                            ErrorMessage="Select Users!" ValidationGroup="vgSave" meta:resourcekey="rfvUsersResource1"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblReportFormat" runat="server" Text="Report Format" meta:resourcekey="lblReportFormatResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlReportFormat" runat="server" meta:resourcekey="ddlReportFormatResource1">
                            <asp:ListItem meta:resourcekey="ListItemResource76">Select Format</asp:ListItem>
                            <asp:ListItem Text="Excel" Value="Excel" meta:resourcekey="ListItemResource77"></asp:ListItem>
                            <asp:ListItem Text="Pdf" Value="Pdf" meta:resourcekey="ListItemResource78"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvReportFormat" ControlToValidate="ddlReportFormat" CssClass="CustomValidator"
                            InitialValue="Select Format" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Report Format is required!' />"
                            ErrorMessage="Report Format is required!" ValidationGroup="vgSave" meta:resourcekey="rfvReportFormatResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
