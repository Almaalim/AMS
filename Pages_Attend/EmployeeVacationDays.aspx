<%@ Page Title="Employee Vacation Days" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeVacationDays.aspx.cs" Inherits="EmployeeVacationDays" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <span class="h3">
                        <asp:Literal ID="LitEmpName" runat="server" Text="Employee name"
                            meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </span>
                </div>
            </div>
            
            
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" 
                        AutoGenerateColumns="False" PageSize="15" AllowPaging="True"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="VtpID" ShowFooter="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnPreRender="grdData_PreRender"
                        EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField DataField="VtpNameAr" HeaderText="Vacation Name (Ar)"
                                SortExpression="VtpNameAr" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="VtpNameEn" HeaderText="Vacation Name (En)"
                                SortExpression="VtpNameEn" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="VtpID" HeaderText="VtpID" SortExpression="VtpID"
                                meta:resourcekey="BoundFieldResource3" />

                            <asp:TemplateField HeaderText="MaxDays" SortExpression="MaxDays"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# GrdMaxDays(Eval("VtpID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vacation Days" SortExpression="VacationDays"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# GrdVacDays(Eval("VtpID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>







