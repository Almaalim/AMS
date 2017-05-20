<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calendar3.ascx.cs" Inherits="Control_Calendar3" %>

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
    }
</style>


<script type="text/javascript">
    function showHideG(Gdiv,Hdiv) {
        document.getElementById(Hdiv).style.display = "none";

        if (document.getElementById(Gdiv).style.display == "none") {
            document.getElementById(Gdiv).style.display = "block";
        }
        else { document.getElementById(Gdiv).style.display = "none"; }
    }

    function showHideH(Gdiv, Hdiv) {
        document.getElementById(Gdiv).style.display = "none";

        if (document.getElementById(Hdiv).style.display == "none") {
            document.getElementById(Hdiv).style.display = "block";
        }
        else { document.getElementById(Hdiv).style.display = "none"; }
    }
</script>


<asp:UpdatePanel ID="CalendarUpdatePanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblCalendar" runat="server" BorderColor="#C4CBD1" BorderStyle="Solid" BorderWidth="1px" Height="21px" BackColor="White" Width="220px">
                        <table width="100%">
                            <tr>
                                <td valign="middle">
                                    <asp:TextBox ID="txtGDate" runat="server" Width="75px" Height="16" CssClass="CalTextStyle" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td runat="server" id="tdGCal"  class="td1Allalign" valign="middle">
                                    <asp:Image ID="imgG" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/G.png" />
                                </td>
                                
                                <td class="td1Allalign" valign="middle">
                                    <asp:TextBox ID="txtHDate" runat="server" Width="75px" Height="16" CssClass="CalTextStyle" ReadOnly="true"></asp:TextBox>
                                </td>

                                <td runat="server" id="tdHCal" class="td1Allalign" valign="middle">
                                    <asp:Image ID="imgH" runat="server" Width="16px" Height="16" ImageUrl="~/images/Control_Images/H.png" />
                                </td>

                                <td class="td1Allalign" valign="middle" >
                                    <asp:ImageButton ID="imgbtnClearCalendar" runat="server" Width="12px" Height="12" ImageUrl="~/images/Control_Images/cross.png" OnClick="imgbtnClearCalendar_Click" />
                                </td>  
                            </tr>
                        </table>
                    </asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtGDate" Enabled="false"
                        EnableClientScript="False" Text="&lt;img src='images/Exclamation.gif' title='Date is required' /&gt;">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:CustomValidator ID="cvCompareDates" runat="server" Text="&lt;img src='images/message_exclamation.png' title='Start date more than End date' /&gt;"
                        ValidationGroup="vgSave" ErrorMessage="Start date more than End date" Enabled="false"
                        OnServerValidate="DateValidate_ServerValidate" EnableClientScript="False" ControlToValidate="ddlGMonths">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="DivGCal" runat="server" class="whole_calendar">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlGMonths" runat="server" AutoPostBack="True" 
                                        Width="125px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlGYears" runat="server" AutoPostBack="True" Width="95px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Calendar ID="CalGDate" runat="server" BackColor="White" Width="220px" DayNameFormat="FirstLetter"
                                        ForeColor="Black" Height="160px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#CCCCCC"
                                        CellPadding="4" OnSelectionChanged="CalDate_SelectionChanged" 
                                        ShowNextPrevMonth="False" ShowTitle="False">
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
                    <div id="DivHCal" runat="server" class="whole_calendar">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlHMonths" runat="server" AutoPostBack="True" 
                                        Width="125px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlHYears" runat="server" AutoPostBack="True" Width="95px" CssClass="CallstItemStyle"
                                        OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Calendar ID="CalHDate" runat="server" BackColor="White" Width="220px" DayNameFormat="FirstLetter"
                                        ForeColor="Black" Height="160px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#CCCCCC"
                                        CellPadding="4" OnSelectionChanged="CalDate_SelectionChanged" 
                                        ShowNextPrevMonth="False" ShowTitle="False">
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
