<%@ Page Title="Employee Transaction" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeTransactions.aspx.cs" Inherits="EmployeeTransactions" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete1.js"></script>

    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Employee ID:" Height="18px"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">

                    <asp:TextBox ID="txtEmployeeID" runat="server" AutoCompleteType="Disabled"
                        ToolTip="Employee ID"></asp:TextBox>
                    <asp:Panel runat="server" ID="pnlauID" />
                    <ajaxToolkit:AutoCompleteExtender
                        runat="server"
                        ID="auID"
                        TargetControlID="txtEmployeeID"
                        ServicePath="~/Service/AutoComplete.asmx"
                        ServiceMethod="GetEmployeeIDList"
                        MinimumPrefixLength="1"
                        CompletionInterval="1000"
                        EnableCaching="true"
                        OnClientItemSelected="AutoCompleteIDItemSelected"
                        CompletionListElementID="pnlauID"
                        CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        CompletionSetCount="12" />
                </div>
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" Height="18px"
                        meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server"
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" Height="18px"
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server"
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnMonthFilter" runat="server"
                        OnClick="btnMonthFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png"
                        meta:resourcekey="btnMonthFilterResource1" />

                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdShift" />

                    <%--<div style="width: 800px; height: 230px; overflow: scroll">--%>
                    <%--    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                                                        SelectedIndex="1" AutoGenerateColumns="False"
                                                        AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                                                        ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                        OnPreRender="grdData_PreRender" Width="800px" EnableModelValidation="True" 
                                                             meta:resourcekey="grdDataResource1">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                                            NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                                                            PreviousPageImageUrl="~/images/prev.png" />--%>

                    <asp:GridView ID="grdShift" runat="server" CssClass="datatable"
                        SelectedIndex="1" AutoGenerateColumns="False"
                        BorderWidth="0px" GridLines="None" ShowFooter="True"
                        OnPageIndexChanging="grdShift_PageIndexChanging" OnRowCreated="grdShift_RowCreated"
                        OnRowDataBound="grdShift_RowDataBound"
                        OnSelectedIndexChanged="grdShift_SelectedIndexChanged"
                        OnPreRender="grdShift_PreRender"
                        EnableModelValidation="True" meta:resourcekey="grdShiftDetailsResource1"
                        AllowPaging="True" CellPadding="0">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:TemplateField HeaderText="Shift Date" InsertVisible="False"
                                SortExpression="SsmDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblShiftDate" runat="server"
                                        Text='<%# DisplayFun.GrdDisplayDate(Eval("SsmDate")) %>'
                                        meta:resourcekey="lblShiftDateResource1"></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Shift Number" DataField="SsmShift"
                                InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource1"></asp:BoundField>

                            <asp:TemplateField HeaderText="Shift Duration"
                                SortExpression="SsmShiftDuration" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmShiftDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Duration" SortExpression="SsmWorkDuration"
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmWorkDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Actual Duration"
                                SortExpression="SsmActualWorkDuration"
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmActualWorkDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gap Total" SortExpression="SsmGapDuration"
                                meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gap UnPaid"
                                SortExpression="SsmGapUnpaidDuration" meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapUnpaidDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gap Paid" SortExpression="SsmGapPaidDuration"
                                meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapPaidDuration"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Extratime" SortExpression="SsmExtratime"
                                meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmExtratime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Overtime" SortExpression="SsmOverTime"
                                meta:resourcekey="TemplateFieldResource9">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmOverTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>


                    </asp:GridView>

                    <%--</div>--%>


                    <asp:HiddenField ID="hdnShiftID" runat="server" />



                </div>
            </div>
            <div class="GreySetion">
            <div class="row">
                <div class="col6"><span class="h4">
                    <asp:Literal ID="Literal2" runat="server" Text=" Transaction"
                        meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </span>
                    <div class="clearfix"></div>
                    <asp:UpdatePanel ID="updPanel" runat="server">
                        <ContentTemplate>
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="GridViewKeyBoardPagerExtender1" TargetControlID="grdTrans" />

                            <asp:Panel ID="pnlGrdTrans" runat="server">
                                <asp:GridView ID="grdTrans" runat="server" CssClass="datatable"
                                    SelectedIndex="1" AutoGenerateColumns="False"
                                    PageSize="3" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="UsrName"
                                    ShowFooter="True" OnPageIndexChanging="grdTrans_PageIndexChanging"
                                    OnRowDataBound="grdTrans_RowDataBound"
                                    OnSelectedIndexChanged="grdTrans_SelectedIndexChanged"
                                    EnableModelValidation="True" meta:resourcekey="grdTransResource1">

                                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                        FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                        NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                        PreviousPageImageUrl="~/images/prev.png" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" InsertVisible="False"
                                            SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDate" runat="server"
                                                    Text='<%# DisplayFun.GrdDisplayDate(Eval("TrnDate")) %>'
                                                    meta:resourcekey="lblTransDateResource1"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time" InsertVisible="False"
                                            SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource11">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTranstime" runat="server"
                                                    Text='<%# DisplayFun.GrdDisplayTime(Eval("TrnTime")) %>'
                                                    meta:resourcekey="lblTranstimeResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" InsertVisible="False"
                                            SortExpression="TrnType" meta:resourcekey="TemplateFieldResource12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTranstype" runat="server"
                                                    Text='<%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType")) %>'
                                                    meta:resourcekey="lblTranstypeResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="User Name" DataField="UsrName"
                                            InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                    </Columns>


                                </asp:GridView>

                            </asp:Panel>

                            <!-- Notice this is outside the GridView -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col6">
                    <span class="h4">
                    <asp:Literal ID="Literal3" runat="server" Text="Gap" 
                        meta:resourcekey="Literal3Resource1"></asp:Literal></span>
                    <div class="clearfix"></div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="GridViewKeyBoardPagerExtender2" TargetControlID="grdGap" />

                            <asp:GridView ID="grdGap" runat="server" CssClass="datatable"
                                SelectedIndex="1" AutoGenerateColumns="False"
                                PageSize="3" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="UsrName"
                                ShowFooter="True" OnPageIndexChanging="grdGap_PageIndexChanging" OnRowCreated="grdGap_RowCreated"
                                OnRowDataBound="grdGap_RowDataBound"
                                OnSelectedIndexChanged="grdGap_SelectedIndexChanged"
                                EnableModelValidation="True" meta:resourcekey="grdGapResource1">

                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                    NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                    PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" InsertVisible="False"
                                        SortExpression="GapDate" meta:resourcekey="TemplateFieldResource13">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGapDate1" runat="server"
                                                Text='<%# DisplayFun.GrdDisplayDate(Eval("GapDate")) %>'
                                                meta:resourcekey="lblGapDate1Resource1"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="first" />
                                        <ItemStyle CssClass="first" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Time" InsertVisible="False"
                                        SortExpression="GapStartTime" meta:resourcekey="TemplateFieldResource14">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGapStartTime1" runat="server"
                                                Text='<%# DisplayFun.GrdDisplayTime(Eval("GapStartTime")) %>'
                                                meta:resourcekey="lblGapStartTime1Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Time" InsertVisible="False"
                                        SortExpression="GapEndTime" meta:resourcekey="TemplateFieldResource15">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGapEndTime1" runat="server"
                                                Text='<%# DisplayFun.GrdDisplayTime(Eval("GapEndTime")) %>'
                                                meta:resourcekey="lblGapEndTime1Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Duration" SortExpression="GapDuration"
                                        meta:resourcekey="TemplateFieldResource16">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayDuration(Eval("GapDuration"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField HeaderText="GapID" DataField="GapID" ReadOnly="True"
                                        meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                </Columns>


                            </asp:GridView>



                            <!-- Notice this is outside the GridView -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>



            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                Height="120px" Width="100%" meta:resourcekey="TabContainer1Resource1">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Shift Details"
                    meta:resourcekey="TabPanel1Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabDate" runat="server" Text="Date :" Font-Bold="True"
                                    meta:resourcekey="lblShtTabDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabDateV" runat="server"
                                    meta:resourcekey="lblShtTabDateVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblShtTabWorkingtime" runat="server" Text="Working Time : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabWorkingtimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabWorkingtimeV" runat="server"
                                    meta:resourcekey="lblShtTabWorkingtimeVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabPeriod" runat="server" Text="Shift Period : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabPeriodResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabPeriodV" runat="server"
                                    meta:resourcekey="lblShtTabPeriodVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabActDuration" runat="server" Text="Actual Duration :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabActDurationResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabActDurationV" runat="server"
                                    meta:resourcekey="lblShtTabActDurationVResource1"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lblShtTabWorkDuration" runat="server" Text="Work Duration :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabWorkDurationResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabWorkDurationV" runat="server"
                                    meta:resourcekey="lblShtTabWorkDurationVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabShiftDuration" runat="server" Text="Shift Duration : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabShiftDurationResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabShiftDurationV" runat="server"
                                    meta:resourcekey="lblShtTabShiftDurationVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabBeginEarly" runat="server" Text="Begin Early :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabBeginEarlyResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabBeginEarlyV" runat="server"
                                    meta:resourcekey="lblShtTabBeginEarlyVResource1"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lblShtTabBeginLate" runat="server" Text="BeginLate : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabBeginLateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabBeginLateV" runat="server"
                                    meta:resourcekey="lblShtTabBeginLateVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabExtraTime" runat="server" Text="ExtraTime : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabExtraTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabExtraTimeV" runat="server"
                                    meta:resourcekey="lblShtTabExtraTimeVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabOutEarly" runat="server" Text="Out Early :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabOutEarlyResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabOutEarlyV" runat="server"
                                    meta:resourcekey="lblShtTabOutEarlyVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblShtTabOutLate" runat="server" Text="Out Late : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabOutLateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabOutLateV" runat="server"
                                    meta:resourcekey="lblShtTabOutLateVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShtTabOvertime" runat="server" Text="Overtime :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabOvertimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblShtTabOvertimeV" runat="server"
                                    meta:resourcekey="lblShtTabOvertimeVResource1"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server"
                    HeaderText="Transaction Details" meta:resourcekey="TabPanel2Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblTrnTabDate" runat="server" Text="Date :" Font-Bold="True"
                                    meta:resourcekey="lblTrnTabDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblTrnTabDateV" runat="server"
                                    meta:resourcekey="lblTrnTabDateVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblTrnTabTime" runat="server" Text="Time : " Font-Bold="True"
                                    meta:resourcekey="lblTrnTabTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblTrnTabTimeV" runat="server"
                                    meta:resourcekey="lblTrnTabTimeVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblTrnTabType" runat="server" Text="Type :" Font-Bold="True"
                                    meta:resourcekey="lblTrnTabTypeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblTrnTabTypeV" runat="server"
                                    meta:resourcekey="lblTrnTabTypeVResource1"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Gap Details"
                    Height="100px" meta:resourcekey="TabPanel3Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblGapTabDate" runat="server" Text="Date :" Font-Bold="True"
                                    meta:resourcekey="lblGapTabDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblGapTabDateV" runat="server"
                                    meta:resourcekey="lblGapTabDateVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabStartTime" runat="server" Text="Start Time : "
                                    Font-Bold="True" meta:resourcekey="lblGapTabStartTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblGapTabStartTimeV" runat="server"
                                    meta:resourcekey="lblGapTabStartTimeVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblGapTabEndTime" runat="server" Text="End Time : "
                                    Font-Bold="True" meta:resourcekey="lblGapTabEndTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblGapTabEndTimeV" runat="server"
                                    meta:resourcekey="lblGapTabEndTimeVResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblGapTabGapDuration" runat="server" Text="Gap Duration :"
                                    Font-Bold="True" meta:resourcekey="lblGapTabGapDurationResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblGapTabGapDurationV" runat="server"
                                    meta:resourcekey="lblGapTabGapDurationVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabExcuseType" runat="server" Text="Excuse Type :"
                                    Font-Bold="True" meta:resourcekey="lblGapTabExcuseTypeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Label ID="lblGapTabExcuseTypeV" runat="server"
                                    meta:resourcekey="lblGapTabExcuseTypeVResource1"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
</div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
