<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="TransactionLog.aspx.cs" Inherits="Pages_Admin_TransactionLog" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" class="row" runat="server">
                <div class="col1">
                    <asp:Label ID="lblLogTableName" runat="server" Text="Table Name:" meta:resourcekey="lblLogTableNameResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlLogTableName" runat="server" meta:resourcekey="ddlLogTableNameResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblLogTransactionType" runat="server" Text="Transaction:" meta:resourcekey="lblLogTransactionTypeResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlLogTransactionType" runat="server" meta:resourcekey="ddlLogTransactionTypeResource1">
                        <asp:ListItem Text="-Select Type-" Value="0" Selected ="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Add" Value="I" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Update" Value="U" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Delete" Value="D" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblLogTransactionBy" runat="server" Text="Transaction By:" meta:resourcekey="lblLogTransactionByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlLogTransactionBy" runat="server" meta:resourcekey="ddlLogTransactionByResource1">
                    </asp:DropDownList>
                </div>
            </div>
 
            <div id="Div3" runat="server" class="row">  
                <div class="col1">
                    <asp:Label ID="lblStartDate" runat="server" Text="From:" meta:resourcekey="lblStartDateResource1"></asp:Label>
                </div>
                <div class="col4">
                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationGroup ="vgSearch" CalTo="calEndDate" />
                </div>
                <div class="col1">
                    <asp:Label ID="lblEndDate" runat="server" Text="To:" meta:resourcekey="lblEndDateResource1"></asp:Label>
                </div>
                <div class="col4">
                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                </div> 
            </div>
            <div class="row" runat="server">
                <div class="col8">
                    <asp:LinkButton ID="btnFilter" runat="server" CssClass="GenButton glyphicon glyphicon-search"
                        OnClick="btnFilter_Click" ValidationGroup ="vgSearch"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_magnify.png&quot; /&gt; Search" meta:resourcekey="btnFilterResource1"></asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsSearch" runat="server" ValidationGroup="vgSearch"
                        EnableClientScript="False" CssClass="MsgValidation" meta:resourcekey="vsSearchResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <AM:GridView  ID="grdData" runat="server" CellPadding="0" BorderWidth="0px" CssClass="datatable" GridLines="None" 
                        AutoGenerateColumns="False" AllowPaging="True"  DataKeyNames="LogID" ShowFooter="True"
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
                            <asp:BoundField DataField="LogID" HeaderText="ID" SortExpression="LogID" meta:resourcekey="BoundFieldResource1"/>
                            <asp:BoundField DataField="TableNameAr" HeaderText="Table Name (Ar)" SortExpression="TableNameAr" meta:resourcekey="BoundFieldResource2"/>
                            <asp:BoundField DataField="TableNameEn" HeaderText="Table Name (En)" SortExpression="TableNameEn" meta:resourcekey="BoundFieldResource3"/>
                            <asp:BoundField DataField="LogTransactionBy" HeaderText="Transaction By" SortExpression="LogTransactionBy" meta:resourcekey="BoundFieldResource4"/>
                            <asp:TemplateField HeaderText="Type" SortExpression="LogTransactionType" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# GrdDisplayType(Eval("LogTransactionType"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="LogTransactionDate" DataField="LogTransactionDate" SortExpression="LogTransactionDate" Visible="False" meta:resourcekey="BoundFieldResource5"/>
                            <asp:TemplateField HeaderText="Transaction Date" SortExpression="TransactionDate" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("LogTransactionDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction Time" SortExpression="TransactionTime" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("LogTransactionDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Data ID" DataField="LogPkID" SortExpression="LogPkID" meta:resourcekey="BoundFieldResource6"/>
                            <asp:BoundField HeaderText="Description" DataField="LogTransactionDesc" SortExpression="LogTransactionDesc" meta:resourcekey="BoundFieldResource7"/>
                            
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
                            <asp:Parameter Name="CacheKey" Direction="Input" DefaultValue="TransactionLOG" />
                            <asp:Parameter Name="DataID" Direction="Input" DefaultValue="TransactionLogInfoView" />
                            <asp:Parameter Name="sortID" Direction="Input" DefaultValue="LogID DESC" />
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

