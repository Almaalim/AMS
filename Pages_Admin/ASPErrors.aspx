<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ASPErrors.aspx.cs" Inherits="ASPErrors" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table class="rp_Search">
                            <tr>
                                <td valign="middle" style="width:55px">  
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
                                                <asp:Literal ID="Literal1" runat="server" Text="ASP.NET Errors"></asp:Literal>
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
                                                    GridLines="None" DataKeyNames="ErrorId" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                    OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                    OnPreRender="grdData_PreRender" Width="800px"
                                                    EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                                                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                                        LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                                        NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" ItemStyle-Wrap="False" SortExpression="ErrorId" Visible="false">
                                                            <ItemTemplate><%# Server.HtmlEncode(Eval("ErrorId").ToString()) %></ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Application" ItemStyle-Wrap="False" SortExpression="Application" Visible="false">
                                                            <ItemTemplate><%# Server.HtmlEncode(Eval("Application").ToString()) %></ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Host" ItemStyle-Wrap="False" SortExpression="Host">
                                                            <ItemTemplate><%# Server.HtmlEncode(Eval("Host").ToString()) %></ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Code" ItemStyle-Wrap="False" SortExpression="StatusCode">
                                                            <ItemTemplate><%# Server.HtmlEncode(Eval("StatusCode").ToString()) %></ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Type" ItemStyle-Wrap="False" SortExpression="Type">
                                                            <ItemTemplate>
                                                                <span title="<%# Server.HtmlEncode(Eval("Type").ToString()) %>">
                                                                    <%# Server.HtmlEncode(Elmah.ErrorDisplay.HumaneExceptionErrorType(Eval("Type").ToString())) %>
                                                                </span>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Message" SortExpression="Message">
                                                            <ItemTemplate>
                                                                <%# Server.HtmlEncode(Eval("Message").ToString()) %>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="More&hellip;" NavigateUrl='<%# "~/elmah.axd/detail?id=" + Eval("ErrorId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="User" ItemStyle-Wrap="False" SortExpression="User">
                                                            <ItemTemplate><%# Server.HtmlEncode(Eval("User").ToString())%></ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="False" SortExpression="Date">
                                                            <ItemTemplate>
                                                                <%# ShowLocalDate(Eval("TimeUtc"))%>
                                                                <%--<%# Server.HtmlEncode(Eval("TimeUtc", "{0:dd/MM/yyyy}"))%>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time" ItemStyle-Wrap="False" SortExpression="Time">
                                                            <ItemTemplate>
                                                                <%# ShowLocalTime(Eval("TimeUtc"))%>
                                                                <%--<%# Server.HtmlEncode(Eval("TimeUtc", "{0:t}"))%>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Date" SortExpression="ErrorDate">
                                                            <ItemTemplate>
                                                                <%# General.GrdDisplayDate(Eval("ErrorDate"), Session["dateType"], Session["DateFormat"] + " " + General.GrdDisplayTime(Eval("ErrorDate")))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
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

