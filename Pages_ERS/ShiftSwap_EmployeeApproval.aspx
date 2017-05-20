<%@ Page Title="ShiftSwap_EmployeeApproval" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ShiftSwap_EmployeeApproval.aspx.cs" Inherits="ShiftSwap_EmployeeApproval" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="MainTable">
        <div class="row">
            <div class="col12">
                <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                    EnableClientScript="False" ValidationGroup="ShowMsg" />
            </div>
        </div>

        <div class="row">
            <div class="col4">
                <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                    ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                    EnableClientScript="False" ControlToValidate="txtValid">
                </asp:CustomValidator>
            </div>
        </div>

        <div class="row">
            <div class="col12">
                <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                    SelectedIndex="0" AutoGenerateColumns="False" AllowPaging="True"
                    CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True"
                    OnRowCommand="grdData_RowCommand" EnableModelValidation="True"
                    meta:resourcekey="grdDataResource1" OnRowCreated="grdData_RowCreated">
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                        FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                        NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                        PreviousPageImageUrl="~/images/prev.png" />
                    <Columns>
                        <asp:BoundField HeaderText="Requested by" DataField="EmpID" HeaderStyle-CssClass="first"
                            ItemStyle-CssClass="first" InsertVisible="False" ReadOnly="True"
                            SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle CssClass="first" />
                            <ItemStyle CssClass="first" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ErqID" HeaderText="ErqID" SortExpression="ErqID"
                            Visible="False" meta:resourcekey="BoundFieldResource2" />
                        <asp:TemplateField HeaderText="Start Date 1" InsertVisible="False"
                            SortExpression="ErqStartDate" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqStartDate"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date 1" InsertVisible="False"
                            SortExpression="ErqEndDate" meta:resourcekey="TemplateFieldResource2">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqEndDate"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Requested To" DataField="EmpID2"
                            SortExpression="EmpID2" meta:resourcekey="BoundFieldResource3" />
                        <asp:TemplateField HeaderText="Start Date 2" InsertVisible="False"
                            SortExpression="ErqStartDate2" meta:resourcekey="TemplateFieldResource3">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqStartDate2"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date2" InsertVisible="False"
                            SortExpression="ErqEndDate2" meta:resourcekey="TemplateFieldResource4">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqEndDate2"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Description" DataField="ErqReason"
                            SortExpression="ErqReason" meta:resourcekey="BoundFieldResource4" />
                        <asp:TemplateField HeaderText="           "
                            meta:resourcekey="TemplateFieldResource5">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnApprove" CommandName="Approve" CommandArgument='<%# Eval("ErqID") + "," + Eval("EmpID") %>'
                                    runat="server" ImageUrl="~/images/ERS_Images/approve.png"
                                    meta:resourcekey="imgbtnApproveResource1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="           "
                            meta:resourcekey="TemplateFieldResource6">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnCancel" CommandName="Reject" CommandArgument='<%# Eval("ErqID") + "," + Eval("EmpID") %>'
                                    runat="server" ImageUrl="~/images/ERS_Images/reject.png"
                                    meta:resourcekey="imgbtnCancelResource1" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>

