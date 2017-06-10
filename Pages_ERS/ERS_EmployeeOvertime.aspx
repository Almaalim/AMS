<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ERS_EmployeeOvertime.aspx.cs" Inherits="ERS_EmployeeOvertime" %>

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
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                     </div>
            </div>
            <div class="row">
                <div class="col12">
                        <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                            Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                         
                        <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
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
                <div class="col2">
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                                        ImageUrl="../images/Button_Icons/button_magnify.png"  
                                        meta:resourcekey="btnFilterResource1" />
                                  </div>
            </div>
            <div class="row">
                <div class="col12">
                    <span class="h3">
                                            <asp:Literal ID="Literal1" runat="server" Text="Overtime List"></asp:Literal>
                                      </div>
            </div>
            <div class="row">
                <div class="col12">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" PageSize="3" DataKeyNames="OvtID"
                                                CellPadding="0" BorderWidth="0px" GridLines="None"  ShowFooter="True" OnRowCreated="grdData_RowCreated"
                                                OnRowDataBound="grdData_RowDataBound" 
                                                OnRowCommand="grdData_RowCommand" >
                                                <Columns>
                                                    <asp:BoundField DataField="OvtID" HeaderText="Ovt ID" SortExpression="OvtID" Visible="False"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="DayName" SortExpression="DayName">
                                                        <ItemTemplate>
                                                            <%# GrdDisplayDayName(Eval("OvtDate"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="OvtDate" HeaderText="OvtDate" SortExpression="OvtDate" Visible = "False"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("OvtDate"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Start Time" SortExpression="OvtStartTime">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayTime(Eval("OvtStartTime"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time" SortExpression="OvtEndTime">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayTime(Eval("OvtEndTime"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration" SortExpression="OvtDuration">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDuration(Eval("OvtDuration"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                               
                                                    <asp:BoundField DataField="OvtTypeFlag" HeaderText="OvtTypeFlag" SortExpression="OvtTypeFlag" Visible="False"></asp:BoundField>
                                                    <asp:TemplateField SortExpression="Type">
                                                        <ItemTemplate>
                                                            <%--<%# DisplayFun.GrdDisplayOverTimeType(Eval("OvtTypeFlag"))%>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                     
                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnReqSend"  CommandName="ReqSend_Command" CommandArgument='<%# Eval("OvtID") %>'
                                                                runat="server" ImageUrl="../images/del.gif" ToolTip="SendRequest"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnReqStatus"  CommandName="ReqStatus_Command" CommandArgument='<%# Eval("OvtID") %>'
                                                                runat="server"  Height="16px" Width="16px" Enabled="False"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                            </asp:GridView>
                                         </div>
            </div>
            <div class="row">
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblBeginEarly" runat="server" Text="Begin Early Overtime"></asp:Label>
                                     <span>
                                                <asp:Label ID="lblBeginEarlyTotal" runat="server" Text="Total :"></asp:Label>
                                              
                                                <asp:Label ID="lblBeginEarlyVTotal" runat="server" Text="00:00:00"></asp:Label>
                                           
                                                <asp:Label ID="lblBeginEarlyPaid" runat="server" Text="Paid :"></asp:Label>
                                              
                                                <asp:Label ID="lblBeginEarlyVPaid" runat="server" Text="00:00:00"></asp:Label>
                                            
                                                <asp:Label ID="lblBeginEarlyUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                              
                                                <asp:Label ID="lblBeginEarlyVUnpaid" runat="server" Text="00:00:00"></asp:Label></span>     
                                                  </div>
             
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblOutLate" runat="server" Text="Out Late Overtime "></asp:Label>
                                         <span>      
                                                <asp:Label ID="lblOutLateTotal" runat="server" Text="Total :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutLateVTotal" runat="server" Text="00:00:00"></asp:Label>
                                              
                                                <asp:Label ID="lblOutLatePaid" runat="server" Text="Paid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutLateVPaid" runat="server" Text="00:00:00"></asp:Label>
                                             
                                                <asp:Label ID="lblOutLateUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutLateVUnpaid" runat="server" Text="00:00:00"></asp:Label></span>
                                                       </div>
             
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblOutShift" runat="server" Text="Out Of Shift Overtime "></asp:Label>
                                               <span>
                                                <asp:Label ID="lblOutShiftTotal" runat="server" Text="Total :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutShiftVTotal" runat="server" Text="00:00:00"></asp:Label>
                                                          
                                                <asp:Label ID="lblOutShiftPaid" runat="server" Text="Paid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutShiftVPaid" runat="server" Text="00:00:00"></asp:Label>
                                                       
                                                <asp:Label ID="lblOutShiftUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOutShiftVUnpaid" runat="server" Text="00:00:00"></asp:Label></span>
                                                       </div>
             
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblNoShift" runat="server" Text="No Shift Overtime "></asp:Label>
                                                   <span>
                                                <asp:Label ID="lblNoShiftTotal" runat="server" Text="Total :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblNoShiftVTotal" runat="server" Text="00:00:00"></asp:Label>
                                             
                                                <asp:Label ID="lblNoShiftPaid" runat="server" Text="Paid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblNoShiftVPaid" runat="server" Text="00:00:00"></asp:Label>
                                             
                                                <asp:Label ID="lblNoShiftUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblNoShiftVUnpaid" runat="server" Text="00:00:00"></asp:Label></span>
                                                          </div>
           
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblInVac" runat="server" Text="Vacation Overtime"></asp:Label>
                                            <span>
                                                <asp:Label ID="lblInVacTotal" runat="server" Text="Total :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblInVacVTotal" runat="server" Text="00:00:00"></asp:Label>
                                               
                                                <asp:Label ID="lblInVacPaid" runat="server" Text="Paid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblInVacVPaid" runat="server" Text="00:00:00"></asp:Label>
                                              
                                                <asp:Label ID="lblInVacUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblInVacVUnpaid" runat="server" Text="00:00:00"></asp:Label></span>
                                                               </div>
             
                <div class=" GapSummeryDiv Overtime">
                                                <asp:Label ID="lblOvt" runat="server" Text="Overtime Total"></asp:Label>
                                              <span>
                                                <asp:Label ID="lbOvtTotal" runat="server" Text="Total :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOvtVTotal" runat="server" Text="00:00:00"></asp:Label>
                                             
                                                <asp:Label ID="lblOvtPaid"    runat="server" Text="Paid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOvtVPaid" runat="server" Text="00:00:00"></asp:Label>
                                             
                                                <asp:Label ID="lblOvtUnpaid" runat="server" Text="Unpaid :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOvtVUnpaid" runat="server" Text="00:00:00"></asp:Label>
                                                  <asp:Label ID="lblOvtWait" runat="server" Text="Waiting :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOvtVWait" runat="server" Text="00:00:00"></asp:Label>

                                              </span>
                                                                      </div>
            </div>
            
            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" style="height:500px; width:680px;">
                <div id='divPopupHead' class="divPopupHead"><asp:Label ID="lblNamePopup" 
                        runat="server"  CssClass="lblNamePopup"></asp:Label></div>
                <div id='divClosePopup' class="divClosePopup" onclick="hidePopup('divPopup')"><ahref='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                   <center>
                      <iframe id="ifrmPopup" runat="server"  height="500px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopup2' class="divPopup" style="height:500px; width:680px;">
                <div id='divPopupHead2' class="divPopupHead">
                    <asp:Label ID="lblNamePopup2" runat="server" CssClass="lblNamePopup"></asp:Label>
                </div>
                <div id='divClosePopup2' class="divClosePopup" onclick="hidePopup('divPopup2')"><ahref='#'>X</a></div>
                <div id='divPopupContent2' class="divPopupContent">
                   <center>
                      <iframe id="ifrmPopup2" runat="server"  height="500px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

