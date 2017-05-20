<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="SpErrors.aspx.cs" Inherits="SpErrors" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table class="rp_Search">
                            <tr>
                                <td valign="middle" style="width:85px">
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Procedure:" ></asp:Label>
                                </td>
                                <td valign="middle" style="width:255px">   
                                    <asp:DropDownList ID="ddlProcedure" runat="server" Width="250px"></asp:DropDownList>
                                </td>
                                <td valign="middle" style="width:55px">  
                                    &nbsp;
                                    <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
                                </td>
                                <td valign="middle"style="width:105px">      
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList>
                                </td>
                                <td valign="middle" style="width:45px"> 
                                    <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                                 </td>
                                <td valign="middle" style="width:75px">  
                                     <asp:DropDownList ID="ddlYear" runat="server" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle">     
                                    &nbsp;&nbsp;
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png"/>
                                </td>  
                                
                                <td></td>  
                                
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="container">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <asp:Literal ID="Literal1" runat="server" Text="Procedure Errors"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid">
                                                <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                                                    PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp" 
                                                    PreviousPageKey="PageDown" />
                                                
                                                <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                    AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                                                    GridLines="None" DataKeyNames="ErrorLogID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                    OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                    OnPreRender="grdData_PreRender" Width="800px"
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
                                                    <RowStyle CssClass="row" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom-outer">
                                        <div class="bottom-inner">
                                            <div class="bottom">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>