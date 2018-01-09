<%@ Page Title="ShiftSwap_EmployeeApproval" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ShiftSwap_EmployeeApproval.aspx.cs" Inherits="ShiftSwap_EmployeeApproval" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div runat="server" id="MainTable">
        <div class="row">
            <div class="col12">
                <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                    EnableClientScript="False" ValidationGroup="vgShowMsg" />
            </div>
        </div>

        <div class="row">
            <div class="col4">
                <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                    ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                    EnableClientScript="False" ControlToValidate="txtValid">
                </asp:CustomValidator>
            </div>
        </div>

        <div class="row">
            <div class="col12">
                <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                    AutoGenerateColumns="False" AllowPaging="True"
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
                        <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr2"
                            SortExpression="EmpNameAr2" meta:resourcekey="BoundFieldResource568" />
                        <asp:BoundField HeaderText="Name (En)" DataField="EmpNameEn2"
                            SortExpression="EmpNameEn2" meta:resourcekey="BoundFieldResource569" />
                        <asp:TemplateField HeaderText="Date 1" InsertVisible="False"
                            SortExpression="ErqStartDate" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqStartDate"))%>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Date 2" InsertVisible="False"
                            SortExpression="ErqStartDate2" meta:resourcekey="TemplateFieldResource3">
                            <ItemTemplate>
                                <%# DisplayFun.GrdDisplayDate(Eval("ErqStartDate2"))%>
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

