<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="AddLeaveEarlyTrans.aspx.cs" Inherits="AddLeaveEarlyTrans" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Employee ID:"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtEmpIDSearch" runat="server" AutoCompleteType="Disabled"
                        ToolTip="Employee ID" meta:resourcekey="txtEmpIDSearchResource1"></asp:TextBox>
                    <asp:CustomValidator ID="cvFindEmp" runat="server"
                        Text="&lt;img src='../images/Exclamation.gif' title='Employee Not found' /&gt;" CssClass="CustomValidator"
                        ErrorMessage="Employee Not found"
                        ValidationGroup="Filter"
                        OnServerValidate="FindEmp_ServerValidate"
                        EnableClientScript="False"
                        ControlToValidate="txtValid" meta:resourcekey="cvFindEmpResource1"></asp:CustomValidator>

                    <asp:Panel runat="server" ID="pnlauID" Height="200px" ScrollBars="Vertical"
                        meta:resourcekey="pnlauIDResource1" />
                    <ajaxToolkit:AutoCompleteExtender
                        runat="server"
                        ID="auID"
                        TargetControlID="txtEmpIDSearch"
                        ServicePath="~/Service/AutoComplete.asmx"
                        ServiceMethod="GetEmployeeIDList"
                        MinimumPrefixLength="1"
                        OnClientItemSelected="AutoCompleteID_txtEmpIDSearch_ItemSelected"
                        CompletionListElementID="pnlauID"
                        CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                </div>
                <div class="col1">
                    <asp:Label ID="lblNameFilter" runat="server" Text="Employee Name:"
                        meta:resourcekey="lblNameFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtEmpNameSearch" runat="server" AutoCompleteType="Disabled"
                        ToolTip="Employee Name" meta:resourcekey="txtEmpNameSearchResource1"></asp:TextBox>
                    <asp:CustomValidator ID="cvFindEmpName" runat="server"
                        Text="&lt;img src='../images/Exclamation.gif' title='Employee Not found' /&gt;"
                        ErrorMessage="Employee Not found" CssClass="CustomValidator"
                        ValidationGroup="Filter"
                        OnServerValidate="FindEmp_ServerValidate"
                        EnableClientScript="False"
                        ControlToValidate="txtValid" meta:resourcekey="cvFindEmpNameResource1"></asp:CustomValidator>

                    <asp:Panel runat="server" ID="pnlauName" Height="200px" ScrollBars="Vertical"
                        meta:resourcekey="pnlauNameResource1" />
                    <ajaxToolkit:AutoCompleteExtender
                        runat="server"
                        ID="auName"
                        TargetControlID="txtEmpNameSearch"
                        ServicePath="~/Service/AutoComplete.asmx"
                        ServiceMethod="GetEmployeeNameList"
                        MinimumPrefixLength="1"
                        OnClientItemSelected="AutoCompleteID_txtEmpNameSearch_ItemSelected"
                        CompletionListElementID="pnlauName"
                        CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" 
                        />
                </div>
                <div class="col1">
                    <asp:Label ID="lblTodayDate" runat="server" Text="Today Date:"  
                        meta:resourcekey="lblTodayDateResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtTodayDate" runat="server"  
                        ToolTip="Today Date" Enabled="False" meta:resourcekey="txtTodayDateResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        ValidationGroup="Filter" meta:resourcekey="btnFilterResource1" />
                </div>

            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>

            </div>
            <div class="row">
                <div class="col12">
                    <asp:UpdatePanel ID="updPanel" runat="server">
                        <ContentTemplate>
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>
                            <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                                AutoGenerateColumns="False"
                                AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                                ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                OnPreRender="grdData_PreRender"  
                                meta:resourcekey="grdDataResource1">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                    NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                    PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Time" InsertVisible="False"
                                        SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Type" InsertVisible="False"
                                        SortExpression="TrnType" meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="Location(En)" DataField="MacLocationEn"
                                        ReadOnly="True" SortExpression="MacLocationEn"
                                        meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                                    <asp:BoundField HeaderText="Location(Ar)" DataField="MacLocationAr"
                                        ReadOnly="True" SortExpression="MacLocationAr"
                                        meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                    <asp:BoundField HeaderText="User Name" DataField="UsrName" ReadOnly="True"
                                        SortExpression="UsrName" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                    <asp:BoundField HeaderText="Description" DataField="TrnDesc" ReadOnly="True"
                                        SortExpression="TrnDesc" meta:resourcekey="BoundFieldResource4"></asp:BoundField>

                                    <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                        meta:resourcekey="BoundFieldResource5">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date" InsertVisible="False"
                                        SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource3">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayDate(Eval("TrnDate"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                            <!-- Notice this is outside the GridView -->

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary runat="server" ID="vsADD" ValidationGroup="vgADD"
                        EnableClientScript="False" CssClass="errorValidation"
                        meta:resourcekey="vsADDResource1" />
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
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" ValidationGroup="vgADD"
                        OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

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
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate" CssClass="CustomValidator"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvIsAdd" runat="server" ValidationGroup="vgADD" Display="None"
                        CssClass="CustomValidator"
                        OnServerValidate="IsAdd_ServerValidate" EnableClientScript="False"
                        ControlToValidate="txtValid"></asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID :"
                            meta:resourcekey="lblEmployeeIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled"
                            ToolTip="Employee ID" Enabled="False"
                            meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtEmpID" runat="server"
                            ControlToValidate="txtEmpID" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvtxtEmpIDResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :"
                            meta:resourcekey="lblEmpNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblDaye" runat="server" Text="Date :"
                            meta:resourcekey="lblDayeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblType" runat="server" Text="Type :"
                            meta:resourcekey="lblTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlType" runat="server" Enabled="False"
                            meta:resourcekey="ddlTypeResource1">
                            <asp:ListItem Text="OUT" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblTime" runat="server"
                            Text="Time :" meta:resourcekey="lblTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpOUTTime" runat="server" FormatTime="HHmmss"
                            CssClass="TimeCss" meta:resourcekey="tpickerTimeResource1"
                            TimePickerValidationGroup="" TimePickerValidationText="" />
                        <asp:CustomValidator ID="cvtpOUTTime" runat="server" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time is required' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="tpOUTTime_ServerValidate"
                            EnableClientScript="False"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvtpickerTimeResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblLocation" runat="server" Text="Location :"
                            meta:resourcekey="lblLocationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlLocation" runat="server" Enabled="False"
                            meta:resourcekey="ddlLocationResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvLocation" ControlToValidate="ddlLocation" CssClass="CustomValidator"
                            InitialValue="Select Location" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Language is required!' />"
                            ErrorMessage="Location is required!" ValidationGroup="vgSave"
                            meta:resourcekey="rfvLocationResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

