<%@ Page Title="Employee Type" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeType.aspx.cs" Inherits="EmployeeType" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by:"
                        meta:resourcekey="lblSearchByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlSearchByResource1">
                        <asp:ListItem Selected="True" Text="[None]"
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Employee Type Name (Ar)" Value="EtpNameAr"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Type Name (En)" Value="EtpNameEn"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server"
                        meta:resourcekey="txtSearchResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BorderWidth="0px" CellPadding="0" CssClass="datatable" DataKeyNames="EtpID" GridLines="None"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowCommand"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnSorting="grdData_Sorting" ShowFooter="True" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField DataField="EtpID" HeaderText="ID" SortExpression="EtpID"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="EtpNameAr" HeaderText="Name (Ar)"
                                SortExpression="EtpNameAr" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="EtpNameEn" HeaderText="Name (En)"
                                SortExpression="EtpNameEn" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="EtpDesc" HeaderText="Description"
                                SortExpression="EtpDesc" meta:resourcekey="BoundFieldResource4" />
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("EtpID") %>'
                                        CommandName="Delete1" Enabled="False"
                                        ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
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
                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                        EnableClientScript="False" CssClass="MsgValidation"
                        meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
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
                    <div class="col2">
                        <span id="spnEmpTypeAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblEtpNameAr" runat="server" Text="Employment Name (Ar) :"
                            meta:resourcekey="lblEtpNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEtpNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtEtpNameArResource1"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="reqBranchManager1" runat="server" ControlToValidate="txtEtpNameAr"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Emloyee type Name (Ar) is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="reqBranchManager1Resource1"></asp:RequiredFieldValidator>--%>

                        <asp:CustomValidator ID="cvNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnEmpTypeEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblEtpNameEn" runat="server" Text="Employment Name (En) :"
                            meta:resourcekey="lblEtpNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEtpNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtEtpNameEnResource1"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEtpNameEn"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Emloyee type Name (En) is required!' /&gt;"
                            ValidationGroup="vgSave" 
                            meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>--%>

                        <asp:CustomValidator ID="cvNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid" meta:resourcekey="cvEnBranchNameResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblEtpDesc" runat="server" Text="Description:"
                            meta:resourcekey="lblEtpDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEtpDesc" runat="server" TextMode="MultiLine"
                            meta:resourcekey="txtEtpDescResource1" Height="50px"></asp:TextBox>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
