<%@ Page Title="Shift Swap User" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeSwapShift.aspx.cs" Inherits="EmployeeSwapShift" culture="auto" uiculture="auto" meta:resourcekey="PageResource1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" AutoGenerateColumns="False" AllowPaging="True"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="SwpID" ShowFooter="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" onprerender="grdData_PreRender" meta:resourcekey="grdDataResource1">
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="ID 1" DataField="SwpEmpID1" SortExpression="SwpEmpID1" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField HeaderText="ID"   DataField="SwpID"  SortExpression="SwpID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr1" SortExpression="EmpNameAr1" meta:resourcekey="BoundFieldResource3"/>
                            <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn1" SortExpression="EmpNameEn1" meta:resourcekey="BoundFieldResource4"/>
                                                    
                            <asp:TemplateField HeaderText="Date" SortExpression="SwpStartDate1" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("SwpStartDate1"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                                                                        
                            <asp:BoundField HeaderText="ID 2" DataField="SwpEmpID2" SortExpression="SwpEmpID2" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr2" SortExpression="EmpNameAr2" meta:resourcekey="BoundFieldResource6"/>
                            <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn2" SortExpression="EmpNameEn2" meta:resourcekey="BoundFieldResource7"/>
                            <asp:TemplateField HeaderText="Date" SortExpression="SwpStartDate2" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("SwpStartDate2"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete"  CommandName="Delete1" CommandArgument='<%# Eval("SwpID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave" EnableClientScript="False" CssClass="MsgValidation" meta:resourcekey="vsSaveResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"
                        ></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"
                        ></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" CssClass="GenButton  glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" meta:resourcekey="btnSaveResource1"
                        ></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton  glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"
                        ></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblType" runat="server" Text="Type :" meta:resourcekey="lblTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlType" runat="server" meta:resourcekey="ddlTypeResource1">
                            <asp:ListItem Text="-Select Type-" Value="0" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Text="Working Day with Working Day" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Text="Working Day with Off Day"     Value="2" meta:resourcekey="ListItemResource3"></asp:ListItem>
                            <asp:ListItem Text="Off Day with Working Day"     Value="3" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfddlType" runat="server" 
                            ControlToValidate="ddlType" EnableClientScript="False" InitialValue="0"
                            Text="&lt;img src='../images/Exclamation.gif' title='Type is required' /&gt;" 
                            ValidationGroup="vgSave" meta:resourcekey="rfddlTypeResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2"></div>
                    <div class="col4"></div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpID1" runat="server" Text="Employee ID :" meta:resourcekey="lblEmpID1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID1" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtEmpID1Resource1"></asp:TextBox>
                        <asp:Panel runat="server" ID="pnlauID1" meta:resourcekey="pnlauID1Resource1" />
                        <ajaxToolkit:AutoCompleteExtender
                            ID="auID1" runat="server"
                            TargetControlID="txtEmpID1"
                            ServicePath="~/Service/AutoComplete.asmx"
                            ServiceMethod="GetEmployeeIDList"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="AutoCompleteID_txtEmpID1_ItemSelected"
                            CompletionListElementID="pnlauID1"
                            CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                        
                        <asp:CustomValidator ID="cvEmployee1" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Employee_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid" meta:resourcekey="cvEmployee1Resource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartDate1" runat="server" Text="Date 1:" meta:resourcekey="lblStartDate1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calStartDate2"/>

                        <asp:CustomValidator ID="cvDays1" runat="server"
                            Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator"
                            OnServerValidate="Days1_ServerValidate"
                            EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvDays1Resource1"
                            ></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEmpID2" runat="server" Text="swap With Employee ID:" meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpID2Resource1"></asp:TextBox>
                        <asp:Panel runat="server" ID="pnlauID2" meta:resourcekey="pnlauID2Resource1" />
                        <ajaxToolkit:AutoCompleteExtender
                            ID="auID2" runat="server"
                            TargetControlID="txtEmpID2"
                            ServicePath="~/Service/AutoComplete.asmx"
                            ServiceMethod="GetEmployeeIDList"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="AutoCompleteID_txtEmpID2_ItemSelected"
                            CompletionListElementID="pnlauID2"
                            CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                                                   
                        <asp:CustomValidator ID="cvEmployee2" runat="server" ValidationGroup="vgSave"
                            Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                            CssClass="CustomValidator"
                            OnServerValidate="Employee_ServerValidate"
                            EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvEmployee2Resource1"
                            ></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartDate2" runat="server" Text="Date 2:" meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />

                        <asp:CustomValidator ID="cvDays2" runat="server"
                            Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                            ValidationGroup="vgSave"
                            OnServerValidate="Days2_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvDays2Resource1"
                            ></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine" Enabled="False" meta:resourcekey="txtDescResource1"
                            ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvtxtDesc" runat="server"
                            ControlToValidate="txtDesc" EnableClientScript="False"
                            Text="&lt;img src='../images/Exclamation.gif' title='Reason is required' /&gt;"
                            ValidationGroup="vgSave" CssClass="CustomValidator" meta:resourcekey="rvtxtDescResource1"
                           ></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2"></div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="False" meta:resourcekey="txtIDResource1"></asp:TextBox>
                    </div>
                </div>           
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


