<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calendar2.ascx.cs" Inherits="Control_Calendar2" %>

<style type="text/css">
   .CalTextStyle    { vertical-align:middle; border-style:none;   border-width:0;  text-align:center;  }
   .CallstItemStyle { border-style: none; }
   
   .whole_calendar /* whole Calendar Header Div Style without textboxes*/
    {
        position:absolute;               
        overflow:auto;   
        background-color: #CCCCCC;
        border-color: #C4CBD1;
        border-style: Solid;
        border-width: 1px;
        z-index:999;
    }
   .CalenderTable
   {
       width:300px;
   }
</style>


<script type="text/javascript">
    //To Show hide the div when user click on calendar image or the date text boxes
    function showHide(div) {
        if (document.getElementById(div) != 'undefined' && document.getElementById(div) != null) {
            if (document.getElementById(div).style.display == "none") {
                document.getElementById(div).style.display = "block";
            }
            else {
                document.getElementById(div).style.display = "none";
            }
        }
    }

    function Clear(div, Gtxt, Htxt, GD, HD) {
        if (document.getElementById(div) != 'undefined' && document.getElementById(div) != null) { document.getElementById(div).style.display = "none"; }
        document.getElementById(Gtxt).value = GD;
        document.getElementById(Htxt).value = HD;
    }

    <%--document.onclick = function (e) {
        e = e || event
        var target = e.target || e.srcElement

        var box      = document.getElementById('<% =this.DivCal.UniqueID %>')
        var txtHijri = document.getElementById('<% =this.txtHDate.UniqueID %>')
        var txtGreg  = document.getElementById('<% =this.txtGDate.UniqueID %>')
        do {
            if (box == target | txtHijri == target | txtGreg == target) {
                // Click occured inside the box, do nothing.
                return
            }
            target = target.parentNode
        }
        while (target)
        // Click was outside the box, hide it.
        box.style.display = "none"
    }--%>
</script>


<asp:UpdatePanel ID="CalendarUpdatePanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0"  class="CalenderTable">
            <tr>
                <td>
                    <asp:Label ID="lblCalendar" runat="server" >
                        <table >
                            <tr>
                                <td runat="server" id="tdGCal"  class="td1Allalign" valign="middle">
                                    <%--<asp:Image ID="imgG" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/G.png" />--%>
                                    <span ID="imgG" runat="server" class="Geo CalenderIco">G</span>
                                </td>
                                <td valign="middle" class="calendarTxt">
                                    <asp:TextBox ID="txtGDate" runat="server"  CssClass="CalTextStyle" Enabled="false" width="70px" ></asp:TextBox>
                                </td>
                                <td runat="server" id="tdHCal"  class="td1Allalign" valign="middle">
                                    <%--<asp:Image ID="imgH" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/H.png" />--%>
                                    <span ID="imgH" runat="server" class="Hijri CalenderIco">هـ</span>
                                </td>
                                <td class="calendarTxt" valign="middle">
                                    <asp:TextBox ID="txtHDate" runat="server"  CssClass="CalTextStyle" Enabled="false" width="70px"></asp:TextBox>
                                </td>
                                <td class="td1Allalign" valign="middle" >
                                    <%--<asp:Image ID="imgCal" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/Calendar.png" />--%>
                                    <span ID="imgCal" runat="server" class="glyphicon glyphicon-calendar CalenderIco"></span>
                                </td>
                                <td class="td1Allalign" valign="middle" >   
                                    <%--<asp:Image ID="imgClear" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/Clear.png" />--%>
                                    <span ID="imgClear" runat="server" class="glyphicon glyphicon-remove-sign CalenderIco"></span>
                                    <%--<asp:ImageButton ID="imgbtnClear" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/Clear.png" OnClick="imgbtnClear_Click" />--%>
                                </td>  
                            </tr>
                        </table>
                    </asp:Label>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtGDate" Enabled="false" CssClass="DateExp"
                        EnableClientScript="False" Text="&lt;img src='images/Exclamation.gif' title='Date is required' /&gt;">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:CustomValidator ID="cvCompareDates" runat="server" Text="&lt;img src='images/message_exclamation.png' title='Start date more than End date' /&gt;"
                        ValidationGroup="vgSave" ErrorMessage="Start date more than End date" Enabled="false" CssClass="CustomValidator"
                        OnServerValidate="DateValidate_ServerValidate" EnableClientScript="False" ControlToValidate="ddlMonths">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="DivCal" runat="server" class="whole_calendar">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlLocaleChoice" runat="server" width="90px" AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddlLocaleChoice_SelectedIndexChanged" CssClass="clsDDL">
                                        <asp:ListItem Value="ar-SA" Text="Hijri"></asp:ListItem>
                                        <asp:ListItem Value="en-US" Text="Gregorian"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMonths" runat="server" AutoPostBack="True" 
                                        Width="101px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlYears" runat="server" AutoPostBack="True" Width="65px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Calendar ID="CalDate" runat="server" BackColor="White" Width="265px" DayNameFormat="Short"
                                        ForeColor="Black" Height="160px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#CCCCCC"
                                        CellPadding="4" OnSelectionChanged="CalDate_SelectionChanged" 
                                        ShowNextPrevMonth="False" ShowTitle="False" FirstDayOfWeek="Sunday" OnDayRender="CalDate_DayRender">
                                        <TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
                                        <SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
                                        <NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
                                        <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
                                        <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
                                        <TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
                                        <WeekendDayStyle BackColor="LightSteelBlue"></WeekendDayStyle>
                                        <OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
                                    </asp:Calendar>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
