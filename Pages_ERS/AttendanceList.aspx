<%@ Page Title="Attendance List" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceList.aspx.cs" Inherits="AttendanceList" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css"> 
        .dataTable { text-align:left; font-size:10pt; font-family:Verdana, Sans-Serif; border:solid 1px rgb(210,210,210); color:gray;   } 
        .dataTable CAPTION { color:Black; text-align:left; font-size:12pt; font-weight:bold; padding-bottom:5px; padding-top:15px; } 
        .dataTable TH { text-decoration:none; background-color:rgb(210,210,210); font-family:Tahoma, Sans-Serif, Arial; font-size:11pt; font-weight:normal; color:Black; border:solid 0px; padding:2px 4px 2px 2px; } 
        .dataTable TD { padding-left:6px; border:solid 0px; min-width:100px; } 
        .dataTable TR { border:solid 0px;   } 
        .dataTableAlt TD { font-size: 10pt; color:rgb(75,75,75); font-family:Verdana; border: solid 0px; padding:2px 0px 2px 8px; background-color:rgb(245,245,245); min-width:100px; } 
        .dataTableRow { color:rgb(75,75,75); font-family:Verdana; padding:2px 0px 2px 8px; border:solid 0px; background-color:White; } 
        .dataTable A:Link, .dataTable A:Visited { text-decoration:none; color:black; } 
        .dataTable A:Hover { color:Red; text-decoration:none; } 
    
    
    </style>--%>
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>

    <script type="text/javascript">
        function switchViews(obj, row, Image) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../images/ERS_Images/expand_button_down.png";
            }
            else {
                div.style.display = "none";
                img.src = Image;
            }
        }

        function switchViews2(obj1, obj2, row, RowImage1, NoRowImage1, RowImage2) {
            var div1 = document.getElementById(obj1);
            var div2 = document.getElementById(obj2);
            var img1 = document.getElementById('img' + obj1);
            var img2 = document.getElementById('img' + obj2);
            if (div1.style.display == "none") {
                div1.style.display = "inline";
                img1.src = NoRowImage1;
                div2.style.display = "none";
                if (img2 != null) { img2.src = RowImage2; }
            }
            else {
                div1.style.display = "none";
                if (img1 != null) { img1.src = RowImage1; }
            }
        }
    </script>

    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" runat="server" id="MainTable">
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
                        meta:resourcekey="btnFilterResource1" CssClass="LeftOverlay" />
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <span class="h3">
                                            <asp:Literal ID="Literal1" runat="server" Text="Attendance List" 
                                                meta:resourcekey="Literal1Resource1"></asp:Literal>
                        </span>
                                         </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" PageSize="3" DataKeyNames="DayGDate"
                        CellPadding="0" BorderWidth="0px" GridLines="None" ShowFooter="True" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnRowCommand="grdData_RowCommand" >
                        <Columns>
                            <asp:BoundField DataField="DayName" HeaderText="Day"
                                SortExpression="DayName">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DayGDate" HeaderText="G.Date" SortExpression="DayGDate">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DayHDate" HeaderText="H.Date" SortExpression="DayHDate">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SsmShift" HeaderText="Shift ID" HtmlEncode="False">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Punch In" SortExpression="SsmPunchIn">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("SsmPunchIn"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Punch Out" SortExpression="SsmPunchOut">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("SsmPunchOut"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Begin Late" SortExpression="SsmBeginLate">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmBeginLate"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Early" SortExpression="SsmOutEarly">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmOutEarly"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Middle Gaps" SortExpression="SsmGapDur_MG">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDuration(Eval("SsmGapDur_MG"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="SendVacDay" CommandName="SendVacDay_Command" CommandArgument='<%# Eval("DayGDate") + "-" + Eval("AnyStatus") + "-" + Eval("SsmShift") %>'
                                        runat="server" Height="16px" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="StatusRequest" CommandName="StatusRequest_Command" CommandArgument='<%# Eval("DayGDate") %>'
                                        runat="server" Height="16px" Width="16px" Enabled="False" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="AnyStatus" HeaderText="AnyStatus" SortExpression="AnyStatus"></asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                </div>
            </div>

            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" style="height: 350px; width: 680px;">
                <div id='divPopupHead' class="divPopupHead">
                    <asp:Label ID="lblNamePopup"
                        runat="server" CssClass="lblNamePopup"></asp:Label>
                </div>
                <div id='divClosePopup' class="divClosePopup" onclick="hidePopup('divPopup')"><a href='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup" runat="server"  height="350px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopup2' class="divPopup" style="height: 250px; width: 680px;">
                <div id='divPopupHead2' class="divPopupHead">
                    <asp:Label ID="lblNamePopup2"
                        runat="server" CssClass="lblNamePopup"></asp:Label>
                </div>
                <div id='divClosePopup2' class="divClosePopup" onclick="hidePopup('divPopup2')"><a href='#'>X</a></div>
                <div id='divPopupContent2' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopup2" runat="server"  height="250px" width="670px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>

            <div id='divPopupAlert' class="divPopup" style="height: 200px; width: 580px;">
                <div id='divPopupHeadAlert' class="divPopupHead">
                    <asp:Label ID="lblNamePopupAlert"
                        runat="server" CssClass="lblNamePopup"></asp:Label>
                </div>
                <div id='divClosePopupAlert' class="divClosePopup" onclick="hidePopup('divPopupAlert')"><a href='#'>X</a></div>
                <div id='divPopupContentAlert' class="divPopupContent">
                    <center>
                      <iframe id="ifrmPopupAlert" runat="server"  height="200px" width="500px"  scrolling="no" frameborder="0" style="margin-left:10px; background-color:#4E6877"></iframe> 
                   </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>






