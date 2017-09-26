<%@ Page Title="Employee Gaps" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeGaps.aspx.cs" Inherits="EmployeeGaps" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <%--script--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" PageSize="3" DataKeyNames="GapID"
                        CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnRowCommand="grdData_RowCommand"
                        meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="GapID" HeaderText="Gap ID" SortExpression="GapID"
                                Visible="False" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:TemplateField HeaderText="DayName" SortExpression="DayName"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# GrdDisplayDayName(Eval("GapDate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="GapDate" HeaderText="GapDate"
                                SortExpression="GapDate" Visible="False"
                                meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:TemplateField HeaderText="Date" SortExpression="Date"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("GapDate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Time" SortExpression="GapStartTime"
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("GapStartTime"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time" SortExpression="GapEndTime"
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("GapEndTime"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Duration" SortExpression="GapDuration"
                                meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("GapDuration"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <asp:ImageButton ID="SendReq" CommandName="SendReq_Command" CommandArgument='<%# Eval("GapID") %>'
                                        runat="server" ImageUrl="../images/del.gif" ToolTip="SendRequest"
                                        meta:resourcekey="SendReqResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <asp:ImageButton ID="GapStatusRequest" CommandName="GapStatusRequest_Command" CommandArgument='<%# Eval("GapID") %>'
                                        runat="server" Height="16px" Width="16px" Enabled="False"
                                        meta:resourcekey="GapStatusRequestResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="ExcID" HeaderText="Exc ID" SortExpression="ExcID"
                                Visible="False" meta:resourcekey="BoundFieldResource3"></asp:BoundField>

                            <asp:TemplateField SortExpression="ExcType"
                                meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <%# GrdDisplayExcuseType(Eval("ExcID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                </div>
            </div>

            <div class="h3 collapseToggle">Summery</div>
            <div class="row" id="collapse">
                <div class="GapSummeryDiv">
                    <asp:Label ID="lblGaps" runat="server" Text="Gaps"
                        meta:resourcekey="lblGapsResource1"></asp:Label>
                    <span>
                        <asp:Label ID="lblTotalGaps" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalGapsResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapsDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapsDurResource1"></asp:Label></span>
                </div>
                <div class="GapSummeryDiv">
                    <asp:Label ID="lblGapWithoutExcuse" runat="server"
                        Text="Gaps Without Excuse" meta:resourcekey="lblGapWithoutExcuseResource1"></asp:Label>

                    <span>
                        <asp:Label ID="lblTotalGapWithoutExcuse" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalGapWithoutExcuseResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithoutExcuseDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithoutExcuseDurResource1"></asp:Label>
                    </span>
                </div>
                <div class="GapSummeryDiv">
                    <asp:Label ID="lblGapWithExcuse" runat="server" Text="Gaps With Excuse "
                        meta:resourcekey="lblGapWithExcuseResource1"></asp:Label>

                    <span>
                        <asp:Label ID="lblTotalGapWithExcuse" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalGapWithExcuseResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcuseDurResource1"></asp:Label>


                        <asp:Label ID="lblTotalGapWithExcusePaid" runat="server" Text="Paid :"
                            meta:resourcekey="lblTotalGapWithExcusePaidResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcusePaidDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcusePaidDurResource1"></asp:Label>


                        <asp:Label ID="lblTotalGapWithExcuseUnPaid" runat="server" Text="Unpaid :"
                            meta:resourcekey="lblTotalGapWithExcuseUnPaidResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseUnPaidDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcuseUnPaidDurResource1"></asp:Label>
                    </span>
                </div>

                <div class="GapSummeryDiv">
                    <asp:Label ID="lblGapWithExcuseByUser" runat="server"
                        Text="Gaps With Excuse By User "
                        meta:resourcekey="lblGapWithExcuseByUserResource1"></asp:Label>

                    <span>
                        <asp:Label ID="lblTotalGapWithExcuseByUser" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalGapWithExcuseByUserResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseByUserDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcuseByUserDurResource1"></asp:Label>


                        <asp:Label ID="lblTotalGapWithExcusePaidByUser" runat="server" Text="Paid :"
                            meta:resourcekey="lblTotalGapWithExcusePaidByUserResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcusePaidByUserDur" runat="server"
                            Text="00:00:00" meta:resourcekey="lblTotalGapWithExcusePaidByUserDurResource1"></asp:Label>


                        <asp:Label ID="lblTotalGapWithExcuseUnPaidByUser" runat="server"
                            Text="Unpaid :" meta:resourcekey="lblTotalGapWithExcuseUnPaidByUserResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseUnPaidByUserDur" runat="server"
                            Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcuseUnPaidByUserDurResource1"></asp:Label>
                    </span>
                </div>

                <div class="GapSummeryDiv">
                    <asp:Label ID="lblGapWithExcuseByReq" runat="server"
                        Text="Gaps With Excuse By Request "
                        meta:resourcekey="lblGapWithExcuseByReqResource1"></asp:Label>

                    <span>
                        <asp:Label ID="lblTotalGapWithExcuseByReq" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalGapWithExcuseByReqResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseByReqDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalGapWithExcuseByReqDurResource1"></asp:Label>


                        <asp:Label ID="lblTotalGapWithExcusePaidByReq" runat="server" Text="Paid :"
                            meta:resourcekey="lblTotalGapWithExcusePaidByReqResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcusePaidByReqDur" runat="server"
                            Text="00:00:00" meta:resourcekey="lblTotalGapWithExcusePaidByReqDurResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseUnPaidByReq" runat="server"
                            Text="Unpaid :" meta:resourcekey="lblTotalGapWithExcuseUnPaidByReqResource1"></asp:Label>
                        &nbsp;
                                                <asp:Label ID="lblTotalGapWithExcuseUnPaidByReqDur" runat="server"
                                                    Text="00:00:00" meta:resourcekey="lblTotalGapWithExcuseUnPaidByReqDurResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseWaitByReq" runat="server"
                            Text="Waiting :" meta:resourcekey="lblTotalGapWithExcuseWaitByReqResource1"></asp:Label>

                        <asp:Label ID="lblTotalGapWithExcuseWaitByReqDur" runat="server"
                            Text="00:00:00" meta:resourcekey="lblTotalGapWithExcuseWaitByReqDurResource1"></asp:Label></span>
                </div>

                <div class="GapSummeryDiv">
                    <asp:Label ID="lblDeduction" runat="server" Text="Deduction"
                        meta:resourcekey="lblDeductionResource1"></asp:Label>

                    <span>
                        <asp:Label ID="lblTotalDeduction" runat="server" Text="Total :"
                            meta:resourcekey="lblTotalDeductionResource1"></asp:Label>

                        <asp:Label ID="lblTotalDeductionDur" runat="server" Text="00:00:00"
                            meta:resourcekey="lblTotalDeductionDurResource1"></asp:Label>
                    </span>
                </div>
            </div>

            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" style="height: 500px; width: 680px;">
                <div id='divPopupHead' class="divPopupHead">
                    <asp:Label ID="lblNamePopup"
                        runat="server" CssClass="lblNamePopup"
                        meta:resourcekey="lblNamePopupResource1"></asp:Label>
                </div>
                <div id='divClosePopup' class="divClosePopup" onclick="hidePopup('divPopup')"><a href='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup" runat="server"  height="500px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopup2' class="divPopup" style="height: 500px; width: 680px;">
                <div id='divPopupHead2' class="divPopupHead">
                    <asp:Label ID="lblNamePopup2"
                        runat="server" CssClass="lblNamePopup"
                        meta:resourcekey="lblNamePopup2Resource1"></asp:Label>
                </div>
                <div id='divClosePopup2' class="divClosePopup" onclick="hidePopup('divPopup2')"><a href='#'>X</a></div>
                <div id='divPopupContent2' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup2" runat="server"  height="500px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>
            <script>
                var acc = document.getElementsByClassName("collapseToggle ");
                var i;

                for (i = 0; i < acc.length; i++) {
                    acc[i].onclick = function () {
                        this.classList.toggle("open");
                        var panel = this.nextElementSibling;
                        if (panel.style.maxHeight) {
                            panel.style.maxHeight = null;
                        } else {
                            panel.style.maxHeight = panel.scrollHeight + "px";
                        }
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>








