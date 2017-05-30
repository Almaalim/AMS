<%@ Page Title="Pending Transaction" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="PendingTransaction.aspx.cs" Inherits="PendingTransaction" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete1.js"></script>

    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblFilter" runat="server" Text="Employee ID:" Height="18px" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtEmployeeID" runat="server" AutoCompleteType="Disabled"
                        ToolTip="Employee ID" meta:resourcekey="txtEmployeeIDResource1"></asp:TextBox>
                    <asp:Panel runat="server" ID="pnlauID" ScrollBars="Vertical" meta:resourcekey="pnlauIDResource1" />
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
                        CompletionSetCount="12" DelimiterCharacters=""
                        Enabled="True" />
                </div>
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" 
                        meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server"  
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:"  
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server" 
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server"
                        OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png"
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
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender"
                                TargetControlID="grdData" NextPageKey="PageUp" NextRowSelectKey="Add"
                                PreviousPageKey="PageDown" PrevRowSelectKey="Subtract" />



                            <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                                SelectedIndex="1" AutoGenerateColumns="False"
                                AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                                ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                OnPreRender="grdData_PreRender" EnableModelValidation="True"
                                meta:resourcekey="grdDataResource1">

                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                    NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                    PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="EmpID" ReadOnly="True"
                                        SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="first" />
                                        <ItemStyle CssClass="first" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" SortExpression="EmpNameAr"
                                        meta:resourcekey="EmpNameArBoundFieldResource3">
                                        <HeaderStyle CssClass="first" />
                                        <ItemStyle CssClass="first" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)"
                                        SortExpression="EmpNameEn" meta:resourcekey="EmpNameEnBoundFieldResource4">
                                        <HeaderStyle CssClass="first" />
                                        <ItemStyle CssClass="first" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date" InsertVisible="False"
                                        SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrnDate1" runat="server"
                                                Text='<%# DisplayFun.GrdDisplayDate(Eval("TrnDate")) %>'
                                                meta:resourcekey="lblTrnDate1Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time" InsertVisible="False"
                                        SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="TrnType" DataField="TrnType"
                                        ReadOnly="True" SortExpression="TrnType"
                                        meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Type" InsertVisible="False"
                                        SortExpression="TrnType1" meta:resourcekey="TemplateFieldResource3">
                                        <ItemTemplate>
                                            <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Location(En)" DataField="LocationEn"
                                        InsertVisible="False" ReadOnly="True"
                                        meta:resourcekey="BoundFieldResource3"></asp:BoundField>

                                    <asp:BoundField HeaderText="Location(Ar)" DataField="LocationAr"
                                        InsertVisible="False" ReadOnly="True"
                                        meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                    <asp:BoundField HeaderText="User Name" DataField="UsrName"
                                        InsertVisible="False" ReadOnly="True" SortExpression="UsrName"
                                        Visible="False" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                                </Columns>

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
                        meta:resourcekey="btnAddResource1">
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" ValidationGroup="vgSave"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1">
                    </asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    &nbsp;
                                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :"
                            meta:resourcekey="lblEmpIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server"
                            Enabled="False"   meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvEmpID" runat="server"
                            ControlToValidate="txtEmpID" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Emloyee ID is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvEmpIDResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :"
                            meta:resourcekey="lblEmpNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled"
                              Enabled="false"
                            meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="Label1" runat="server"
                            Text="Date :" meta:resourcekey="Label1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblTime" runat="server" Text="Time :" meta:resourcekey="lblTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpickerTime" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                            Enabled="False" meta:resourcekey="tpickerTimeResource1"
                            TimePickerValidationGroup="" TimePickerValidationText="" />
                        <asp:CustomValidator ID="cvtpickerTime" runat="server"
                            Text="&lt;img src='../images/Exclamation.gif' title='Time is required!' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="Time_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False"
                            ControlToValidate="txtValid"
                            meta:resourcekey="cvtpickerTimeResource1"></asp:CustomValidator>

                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblType" runat="server" Text="Type :" meta:resourcekey="lblTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True"
                            Enabled="False"   meta:resourcekey="ddlTypeResource1">
                            <asp:ListItem meta:resourcekey="ListItemResource1">-Select Type-</asp:ListItem>
                            <asp:ListItem Text="IN" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Text="OUT" Value="0" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvType" ControlToValidate="ddlType"
                            InitialValue="Select Type" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Language is required!' />"
                            ErrorMessage="Transaction type is required!" ValidationGroup="vgSave" CssClass="CustomValidator"
                            meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblLocation" runat="server"
                            Text="Location :" meta:resourcekey="lblLocationResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlLocation" runat="server"
                            Enabled="False"   meta:resourcekey="ddlLocationResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvLocation" ControlToValidate="ddlLocation"
                            InitialValue="Select Location" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Language is required!' />"
                            ErrorMessage="Location is required!" ValidationGroup="vgSave" CssClass="CustomValidator"
                            meta:resourcekey="rfvLocationResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine"  
                            meta:resourcekey="txtDescResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvDesc" runat="server"
                            ControlToValidate="txtDesc" EnableClientScript="False" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Reason is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rvDescResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span id="spnReqFile" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblReqFile" runat="server" Text="Attachment File :"
                            meta:resourcekey="lblReqFileResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:FileUpload ID="fudReqFile" runat="server"   meta:resourcekey="fudReqFileResource1" />
                        <asp:CustomValidator ID="cvReqFile" runat="server"
                            Text="&lt;img src='../images/Exclamation.gif' title='Attachment File is required!' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="ReqFile_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvReqFileResource1"></asp:CustomValidator>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
