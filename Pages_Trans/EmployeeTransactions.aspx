<%@ Page Title="Employee Transaction" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeTransactions.aspx.cs" Inherits="EmployeeTransactions" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        OnClientItemSelected="AutoCompleteID_txtEmpSearch_ItemSelected"
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
                    <asp:GridView ID="grdShift" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False"
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
                            <asp:TemplateField HeaderText="Status" InsertVisible="False"
                                SortExpression="SsmStatus" meta:resourcekey="TemplateFieldStatusResource">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayShiftStatus(Eval("SsmStatus")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:BoundField HeaderText="Shift ID" DataField="SsmShift"
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
                                SortExpression="SsmWorkDurWithET"
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmWorkDurWithET"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gaps" SortExpression="SsmGapDur_WithoutExc"
                                meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapDur_WithoutExc"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Excuse Paid" SortExpression="SsmGapDur_PaidExc"
                                meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapDur_PaidExc"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overtime" SortExpression="SsmOverTimeDur"
                                meta:resourcekey="TemplateFieldResource9">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmOverTimeDur"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

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
                                    AutoGenerateColumns="False"
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
                                AutoGenerateColumns="False"
                                PageSize="3" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="GapID"
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" meta:resourcekey="TabContainer1Resource1">
                <ajaxToolkit:TabPanel ID="TabShift" runat="server" HeaderText="Shift Details" meta:resourcekey="TabPanel1Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_Date" runat="server" Text="Date :" Font-Bold="True"
                                    meta:resourcekey="lblShtTabDateResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_Date" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_ShiftID" runat="server" Text="Shift ID : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_ShiftIDResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_ShiftID" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmStatus" runat="server" Text="Shift Status : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmStatusResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmStatus" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmShiftDuration" runat="server" Text="Shift Duration : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabShiftDurationResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmShiftDuration" runat="server"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmWorkDuration" runat="server" Text="Work Duration :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabWorkDurationResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmWorkDuration" runat="server"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmWorkDurWithET" runat="server" Text="Actual Duration :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabActDurationResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmWorkDurWithET" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmStartShiftTime" runat="server" Text="Start Shift Time :"
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmStartShiftTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmStartShiftTime" runat="server"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmEndShiftTime" runat="server" Text="End Shift Time : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmEndShiftTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmEndShiftTime" runat="server"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>                      
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmPunchIn" runat="server" Text="Punch In :"
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmPunchInTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmPunchIn" runat="server"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmPunchOut" runat="server" Text="Punch Out : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmPunchOutTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmPunchOut" runat="server"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmBeginEarly" runat="server" Text="Begin Early :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabBeginEarlyResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmBeginEarly" runat="server"></asp:Label>
                            </div>

                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmBeginLate" runat="server" Text="BeginLate : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabBeginLateResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmBeginLate" runat="server"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmOutEarly" runat="server" Text="Out Early :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabOutEarlyResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmOutEarly" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmOutLate" runat="server" Text="Out Late : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabOutLateResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmOutLate" runat="server"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>                       
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmExtraTimeDur" runat="server" Text="ExtraTime : "
                                    Font-Bold="True" meta:resourcekey="lblShtTabExtraTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="bl_ST_V_SsmExtraTimeDur" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmOverTimeDur" runat="server" Text="Overtime :"
                                    Font-Bold="True" meta:resourcekey="lblShtTabOvertimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="bl_ST_V_SsmOverTimeDur" runat="server"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmGapDur_WithoutExc" runat="server" Text="Gaps : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmGapDur_WithoutExcResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmGapDur_WithoutExc" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmGapDur_PaidExc" runat="server" Text="Paid Excuse :"
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmGapDur_PaidExcResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmGapDur_PaidExc" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmGapDur_UnPaidExc" runat="server" Text="Unpaid Excuse :"
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmGapDur_UnPaidExcResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmGapDur_UnPaidExc" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmGapDur_Grace" runat="server" Text="Shift Grace : "
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmGapDur_GraceResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmGapDur_Grace" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_SsmGapDur_MG" runat="server" Text="Middle Grace :"
                                    Font-Bold="True" meta:resourcekey="lbl_ST_SsmGapDur_MGResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lbl_ST_V_SsmGapDur_MG" runat="server"></asp:Label>
                            </div>
                            <div class="col2">
                            </div>
                            <div class="col2">
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
                            <div class="col2">
                                <asp:Label ID="lblGapTabDateV" runat="server"
                                    meta:resourcekey="lblGapTabDateVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabExcuseType" runat="server" Text="Excuse Type :"
                                    Font-Bold="True" meta:resourcekey="lblGapTabExcuseTypeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabExcuseTypeV" runat="server"
                                    meta:resourcekey="lblGapTabExcuseTypeVResource1"></asp:Label>
                            </div>
                            <div class="col2"></div>
                            <div class="col2"></div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblGapTabStartTime" runat="server" Text="Start Time : "
                                    Font-Bold="True" meta:resourcekey="lblGapTabStartTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabStartTimeV" runat="server"
                                    meta:resourcekey="lblGapTabStartTimeVResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabEndTime" runat="server" Text="End Time : "
                                    Font-Bold="True" meta:resourcekey="lblGapTabEndTimeResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabEndTimeV" runat="server"
                                    meta:resourcekey="lblGapTabEndTimeVResource1"></asp:Label>
                            </div>
                             <div class="col2">
                                <asp:Label ID="lblGapTabGapDuration" runat="server" Text="Gap Duration :"
                                    Font-Bold="True" meta:resourcekey="lblGapTabGapDurationResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblGapTabGapDurationV" runat="server"
                                    meta:resourcekey="lblGapTabGapDurationVResource1"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
</div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
