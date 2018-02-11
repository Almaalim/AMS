<%@ Page Title="Add Transactions" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AppendingTransactions.aspx.cs" Inherits="AppendingTransactions" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Employee ID:" Height="18px"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtEmployeeID" runat="server" AutoCompleteType="Disabled" ToolTip="Employee ID" meta:resourcekey="txtEmployeeIDResource1"></asp:TextBox>
                    <asp:Panel runat="server" ID="pnlauID" ScrollBars="Vertical"
                        meta:resourcekey="pnlauIDResource1" />
                    <ajaxToolkit:AutoCompleteExtender
                        runat="server"
                        ID="auID"
                        TargetControlID="txtEmployeeID"
                        ServicePath="~/Service/AutoComplete.asmx"
                        ServiceMethod="GetEmployeeIDList"
                        MinimumPrefixLength="1"
                        OnClientItemSelected="AutoCompleteIDItemSelected"
                        CompletionListElementID="pnlauID"
                        CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
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
                    <asp:ImageButton ID="btnFilter" runat="server"
                        OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnMonthFilterResource1" />
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
                                SelectedIndex="1" AutoGenerateColumns="False"
                                AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                                ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                OnPreRender="grdData_PreRender"
                                meta:resourcekey="grdDataResource1">

                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                    NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                    PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="EmpID" InsertVisible="False" ReadOnly="True"
                                        SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="first" />
                                        <ItemStyle CssClass="first" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date" InsertVisible="False"
                                        SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayDate(Eval("TrnDate"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time" InsertVisible="False"
                                        SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" InsertVisible="False"
                                        SortExpression="TrnType" meta:resourcekey="TemplateFieldResource3">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Location(En)" DataField="MacLocationEn" InsertVisible="False"
                                        ReadOnly="True" SortExpression="MacLocationEn"
                                        meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                    <asp:BoundField HeaderText="Location(Ar)" DataField="MacLocationAr" InsertVisible="False"
                                        ReadOnly="True" SortExpression="MacLocationAr"
                                        meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                    <asp:BoundField HeaderText="User Name" DataField="UsrName" InsertVisible="False"
                                        ReadOnly="True" SortExpression="UsrName"
                                        meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="row" />
                            </asp:GridView>
                            <!-- Notice this is outside the GridView -->

                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
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
                        <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" ToolTip="Employee ID" Enabled="False"
                            meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:Panel runat="server" ID="pnlauID1"  
                            meta:resourcekey="pnlauID1Resource1" />
                        <ajaxToolkit:AutoCompleteExtender
                            runat="server"
                            ID="AutoCompleteExtender1"
                            TargetControlID="txtEmpID"
                            ServicePath="~/Service/AutoComplete.asmx"
                            ServiceMethod="GetEmployeeIDList"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="AutoCompleteIDItemSelected1"
                            CompletionListElementID="pnlauID1"
                            CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                        <asp:RequiredFieldValidator ID="rfvtxtEmpID" runat="server"
                            ControlToValidate="txtEmpID" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvtxtEmpIDResource1"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvFindEmp" runat="server"
                            Text="&lt;img src='../images/message_exclamation.png' title='Employee Not found' /&gt;"
                            ErrorMessage="Employee Not found" CssClass="CustomValidator"
                            ValidationGroup="vgSave"
                            OnServerValidate="FindEmp_ServerValidate"
                            EnableClientScript="False"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvFindEmpResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblDate" runat="server" Text="Date :"
                            meta:resourcekey="lblDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                        <asp:CheckBox ID="chkIN" runat="server" Text="In Transaction" AutoPostBack="True"
                            Enabled="False" OnCheckedChanged="chkIN_CheckedChanged" Width="500px"
                            meta:resourcekey="chkINResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblINTime" runat="server" Text="Time :"
                            meta:resourcekey="lblINTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpINTime" runat="server" FormatTime="HHmmss"/>
                        <asp:CustomValidator ID="cvINTime" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False" CssClass="CustomValidator"
                            OnServerValidate="Time_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvINTimeResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblINLocation" runat="server" Text="Location :"
                            meta:resourcekey="lblINLocationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlINLocation" runat="server" Enabled="False"
                             meta:resourcekey="ddlINLocationResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvINLocation" runat="server"
                            ControlToValidate="ddlINLocation" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Location is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvINLocationResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblINDesc" runat="server" Text="Reason :"
                            meta:resourcekey="lblINDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtINDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine" Enabled="False"
                            meta:resourcekey="txtINDescResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvINDesc" runat="server"
                            ControlToValidate="txtINDesc" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvINDescResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span id="spnINReqFile" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblINReqFile" runat="server" Text="Attachment File :"
                            meta:resourcekey="lblINReqFileResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:FileUpload ID="fudINReqFile" runat="server"
                            meta:resourcekey="fudINReqFileResource1" />
                        <asp:CustomValidator ID="cvINReqFile" runat="server"
                            Text="&lt;img src='../images/Exclamation.gif' title='Attachment File is required!' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="ReqFile_ServerValidate"
                            EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvINReqFileResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">
                        <asp:CheckBox ID="chkOUT" runat="server" Text="Out Transaction" AutoPostBack="True"
                            Enabled="False" OnCheckedChanged="chkOUT_CheckedChanged" Width="500px"
                            meta:resourcekey="chkOUTResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblOUTTime" runat="server" Text="Time :"
                            meta:resourcekey="lblOUTTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpOUTTime" runat="server" FormatTime="HHmmss"/>
                        <asp:CustomValidator ID="cvOUTTime" runat="server"
                            ControlToValidate="txtValid" EnableClientScript="False"
                            OnServerValidate="Time_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time is required' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvOUTTimeResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblOUTLocation" runat="server" Text="Location :"
                            meta:resourcekey="lblOUTLocationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlOUTLocation" runat="server" Enabled="False"
                            meta:resourcekey="ddlOUTLocationResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvOUTLocation" runat="server"
                            ControlToValidate="ddlOUTLocation" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Location is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvOUTLocationResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblOUTDesc" runat="server" Text="Reason :"
                            meta:resourcekey="lblOUTDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtOUTDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine" Enabled="False"
                            meta:resourcekey="txtOUTDescResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvOUTDesc" runat="server"
                            ControlToValidate="txtOUTDesc" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvOUTDescResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span id="spnOUTReqFile" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblOUTReqFile" runat="server" Text="Attachment File :"
                            meta:resourcekey="lblOUTReqFileResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:FileUpload ID="fudOUTReqFile" runat="server"
                            meta:resourcekey="fudOUTReqFileResource1" />
                        <asp:CustomValidator ID="cvOUTReqFile" runat="server"
                            Text="&lt;img src='../images/Exclamation.gif' title='Attachment File is required!' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="ReqFile_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvOUTReqFileResource1"></asp:CustomValidator>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
