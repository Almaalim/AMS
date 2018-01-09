<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ASPErrors.aspx.cs" Inherits="ASPErrors" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server"  meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server"   meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" 
                        PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp"
                        PreviousPageKey="PageDown" />

                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="ErrorId" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />

                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="ErrorId" Visible="False" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate><%# Server.HtmlEncode(Eval("ErrorId").ToString()) %></ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Host" SortExpression="Host" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate><%# Server.HtmlEncode(Eval("Host").ToString()) %></ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Code" SortExpression="StatusCode" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate><%# Server.HtmlEncode(Eval("StatusCode").ToString()) %></ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type" SortExpression="Type" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <span title="<%# Server.HtmlEncode(Eval("Type").ToString()) %>">
                                        <%# Server.HtmlEncode(Elmah.ErrorDisplay.HumaneExceptionErrorType(Eval("Type").ToString())) %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Message" SortExpression="Message" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("Message").ToString()) %>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="More&hellip;" NavigateUrl='<%# "~/elmah.axd/detail?id=" + Eval("ErrorId") %>' meta:resourcekey="HyperLink1Resource1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date" SortExpression="Date" meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <%# ShowLocalDate(Eval("TimeUtc"))%>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" SortExpression="Time" meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <%# ShowLocalTime(Eval("TimeUtc"))%>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

