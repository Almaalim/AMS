<%@ Page Title="Attendance List" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceList.aspx.cs" Inherits="AttendanceList" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" runat="server" id="MainTable">
                <div class="col1">
                    <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server"
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:"
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server"
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png"
                        meta:resourcekey="btnFilterResource1" />
                </div>
                <div class="col1">

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                </div>
            </div>

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
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>
            <%--<div class="row">
                <div class="col12">
                    <span class="h3">
                        <asp:Literal ID="Literal1" runat="server" Text="Attendance List"
                            meta:resourcekey="Literal1Resource1"></asp:Literal>
                    </span>
                </div>
            </div>--%>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" PageSize="3" DataKeyNames="DayGDate"
                        CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnRowCommand="grdData_RowCommand" meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="DayName" HeaderText="Day"
                                SortExpression="DayName" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DayGDate" HeaderText="G.Date" SortExpression="DayGDate" meta:resourcekey="BoundFieldResource2">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DayHDate" HeaderText="H.Date" SortExpression="DayHDate" meta:resourcekey="BoundFieldResource3">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SsmShift" HeaderText="Shift ID" HtmlEncode="False" meta:resourcekey="BoundFieldResource4">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Punch In" SortExpression="SsmPunchIn" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("SsmPunchIn"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Punch Out" SortExpression="SsmPunchOut" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("SsmPunchOut"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Begin Late" SortExpression="SsmBeginLate" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmBeginLate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Early" SortExpression="SsmOutEarly" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmOutEarly"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Middle Gaps" SortExpression="SsmGapDur_MG" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapDur_MG"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <asp:ImageButton ID="SendVacDay" CommandName="SendVacDay_Command" CommandArgument='<%# Eval("DayGDate") + "-" + Eval("AnyStatus") + "-" + Eval("SsmShift") %>'
                                        runat="server" Height="16px" Width="16px" meta:resourcekey="SendVacDayResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <asp:ImageButton ID="StatusRequest" CommandName="StatusRequest_Command" CommandArgument='<%# Eval("DayGDate") %>'
                                        runat="server" Height="16px" Width="16px" Enabled="False" meta:resourcekey="StatusRequestResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="AnyStatus" HeaderText="AnyStatus" SortExpression="AnyStatus" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                </div>
            </div>

            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" style="height: 350px; width: 680px;">
                <div id='divPopupHead' class="divPopupHead">
                    <asp:Label ID="lblNamePopup"
                        runat="server" CssClass="lblNamePopup" meta:resourcekey="lblNamePopupResource1"></asp:Label>
                </div>
                <div id='divClosePopup' class="divClosePopup" onclick="hidePopup('divPopup')"><a href='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup" runat="server"  height="350px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopup2' class="divPopup" style="height: 500px; width: 680px;">
                <div id='divPopupHead2' class="divPopupHead">
                    <asp:Label ID="lblNamePopup2"
                        runat="server" CssClass="lblNamePopup" meta:resourcekey="lblNamePopup2Resource1"></asp:Label>
                </div>
                <div id='divClosePopup2' class="divClosePopup" onclick="hidePopup('divPopup2')"><a href='#'>X</a></div>
                <div id='divPopupContent2' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup2" runat="server"  height="500px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopupAlert' class="divPopup" style="height: 500px; width: 580px;">
                <div id='divPopupHeadAlert' class="divPopupHead">
                    <asp:Label ID="lblNamePopupAlert"
                        runat="server" CssClass="lblNamePopup" meta:resourcekey="lblNamePopupAlertResource1"></asp:Label>
                </div>
                <div id='divClosePopupAlert' class="divClosePopup" onclick="hidePopup('divPopupAlert')"><a href='#'>X</a></div>
                <div id='divPopupContentAlert' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopupAlert" runat="server"  height="500px" width="500px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>






