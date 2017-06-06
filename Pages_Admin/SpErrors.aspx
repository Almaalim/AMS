<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="SpErrors.aspx.cs" Inherits="SpErrors" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                            <div class="col1">
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Procedure:" ></asp:Label>
                               </div>
                                <div class="col2">   
                                    <asp:DropDownList ID="ddlProcedure" runat="server"></asp:DropDownList>
                               </div>
                                <div class="col1">
                                    <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
                                </div>
                                <div class="col2">      
                                    <asp:DropDownList ID="ddlMonth" runat="server" ></asp:DropDownList>
                                </div>
                                <div class="col1">
                                    <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                                 </div>
                                <div class="col2">
                                     <asp:DropDownList ID="ddlYear" runat="server" >
                                    </asp:DropDownList>
                               </div>
                                <div class="col1">
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png"/>
                               </div>
                        </div>

                        <div class="row">
                             <div class="col12">
                                                <asp:Literal ID="Literal1" runat="server" Text="Procedure Errors"></asp:Literal>
                                            </div>
                        </div>

                        <div class="row">
                             <div class="col12">
                                                <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                                                    PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp" 
                                                    PreviousPageKey="PageDown" />
                                                
                                                <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                    AutoGenerateColumns="False" AllowPaging="True"  
                                                    GridLines="None" DataKeyNames="ErrorLogID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                    OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                    OnPreRender="grdData_PreRender"  
                                                    EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                                                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                                        LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                                        NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                                                    <Columns>
                                                        <asp:BoundField DataField="ErrorLogID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ErrorLogID" />
                                                        
                                                        <asp:TemplateField HeaderText="Date" SortExpression="ErrorDate">
                                                            <ItemTemplate>
                                                                <%# DisplayFun.GrdDisplayDate(Eval("ErrorDate")) + " " + DisplayFun.GrdDisplayTime(Eval("ErrorDate"))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:BoundField DataField="ErrorDate" HeaderText="Date" SortExpression="ErrorDate" />--%>
                                                        <asp:BoundField DataField="ErrorProcedure" HeaderText="Procedure" SortExpression="ErrorProcedure" />
                                                        <asp:BoundField DataField="ErrorLine"      HeaderText="Line"      SortExpression="ErrorLine" />
                                                        <asp:BoundField DataField="ErrorMessage"   HeaderText="Message"   SortExpression="ErrorMessage" />

                                                        <%--<asp:BoundField DataField="ErrorNumber"   HeaderText="ErrorNumber" SortExpression="ErrorNumber" />
                                                        <asp:BoundField DataField="ErrorSeverity" HeaderText="ErrorSeverity" SortExpression="ErrorSeverity" />
                                                        <asp:BoundField DataField="ErrorState"    HeaderText="ErrorState" SortExpression="ErrorState" />
                                                        <asp:BoundField DataField="DatabaseName"  HeaderText="DatabaseName" SortExpression="DatabaseName" />--%>
                                                    </Columns>
                                                   
                                                </asp:GridView>
                                          </div>
                        </div>

                      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>