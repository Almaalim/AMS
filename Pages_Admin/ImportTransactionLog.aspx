<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ImportTransactionLog.aspx.cs" Inherits="Pages_Admin_ImportTransactionLog" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col3">
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col3">
                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1"/>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <%--<as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"/>--%>
                    <AM:GridView  ID="grdData" runat="server" CellPadding="0" BorderWidth="0px" CssClass="datatable" GridLines="None" 
                        AutoGenerateColumns="False" AllowPaging="True"  DataKeyNames="IplID" ShowFooter="True"
                        EnableModelValidation="True"

                        DataSourceID="odsGrdData"
                        OnDataBound="grdData_DataBound"  
                        OnRowCreated="grdData_RowCreated" 
                        OnRowDataBound="grdData_RowDataBound" 
                        OnPreRender="grdData_PreRender"  
                        
                        meta:resourcekey="grdDataResource1"> 
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="IplID" SortExpression="IplID" meta:resourcekey="BoundFieldResource1"/>
                            <asp:TemplateField HeaderText="Date" SortExpression="IplDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("IplDate")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time" SortExpression="IplStartTime" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("IplStartTime")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time" SortExpression="IplEndTime" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("IplEndTime")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration" SortExpression="IplDuration" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("IplDuration")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Import Method" SortExpression="IplType" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# GetType(Eval("IplType")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </AM:GridView>

                    <asp:ObjectDataSource ID="odsGrdData" runat="server" 
                        TypeName="LargDataGridView.DAL.LargDataGrid" 
                        SelectMethod="GetDataSortedPage" 
                        SelectCountMethod="GetDataCount"  
                        EnablePaging="True" 
                        SortParameterName="sortExpression" OnSelected="odsGrdData_Selected">
                        <SelectParameters>
                            <asp:ControlParameter  ControlID="hfSearchCriteria"  Name="searchCriteria" Direction="Input"  />
                            <asp:ControlParameter ControlID="HfRefresh" Name="Refresh" Direction="Input"  />
                            <asp:Parameter Name="CacheKey" Direction="Input" DefaultValue="ImportLog" />
                            <asp:Parameter Name="DataID" Direction="Input" DefaultValue="ImportLog" />
                            <asp:Parameter Name="sortID" Direction="Input" DefaultValue="IplDate DESC" />
                            <asp:Parameter Name="DT" Direction="Output" DefaultValue="" Type="Object" />
                        </SelectParameters>                                            
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSearchCriteria" runat="server" />
                    <asp:HiddenField ID="HfRefresh" runat="server" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

