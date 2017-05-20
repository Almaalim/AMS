<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="Pages_Config_Notifications" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellspacing="1" cellpadding="1">
                <%--<tr>
                    <td colspan="4">
                        <table class="rp_Search">
                            <tr>
                                <td>
                                    &nbsp; &nbsp;
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlFilter" runat="server" Width="150px" meta:resourcekey="ddlFilterResource1">
                                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                                        <asp:ListItem Text="Schedule Name(En)" Value="SchNameEn" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Text="Schedule Name(Ar)" Value="SchNameAr" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>
                                    &nbsp;
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png"
                                        meta:resourcekey="btnFilterResource1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="4">
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <%--<asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                                                BorderWidth="0px" GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                                                OnPreRender="grdData_PreRender" Width="800px" EnableModelValidation="True">
                                                <PagerStyle HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                                                    FirstPageImageUrl="~/images/first.gif" LastPageText="Last" LastPageImageUrl="~/images/last.gif"
                                                    NextPageText="Next" NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev"
                                                    PreviousPageImageUrl="~/images/prev.gif" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Name(Ar)" DataField="MailNameAr">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Name(En)" DataField="MailNameEn">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="MailID" DataField="MailID"/>
                                                    <asp:BoundField HeaderText="MailCode" DataField="MailCode"/>
                                                    <asp:TemplateField HeaderText="SchActive" SortExpression="SchActive">
                                                        <ItemTemplate>
                                                            <%--<%# DisplayFun.GrdDisplayDate(Eval("SchStartDate"))%>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                    </td>
                </tr>
                <%--=============================================================================--%>
                <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                            CssClass="MsgValidation" ShowSummary="False"/>
                    </td>
                </tr>
                <%--=============================================================================--%>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton" OnClick="btnModify_Click"
                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                                        ></asp:LinkButton>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" OnClick="btnSave_Click"
                                        Width="70px" Height="18px" ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save">
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" OnClick="btnCancel_Click"
                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                        ></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" ></asp:TextBox>
                                    &nbsp;
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="td1Allalign" width="125px">
                                    <span id="spnNameAr" runat="server" visible="false" class="RequiredField">*</span>
                                    <asp:Label ID="lblNameAr" runat="server" Text="Name(Ar)" ></asp:Label>
                                </td>
                                <td class="td2Allalign">
                                    <asp:TextBox ID="txtNameAr" runat="server" Width="250px"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid"
                                        EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                        ValidationGroup="vgSave"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1Allalign">
                                    <span id="spnNameEn" runat="server" visible="false" class="RequiredField">*</span>
                                    <asp:Label ID="lblNameEn" runat="server" Text="Name(En)"></asp:Label>
                                </td>
                                <td class="td2Allalign">
                                    <asp:TextBox ID="txtNameEn" runat="server" Width="250px"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid"
                                        EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                        ValidationGroup="vgSave"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1Allalign">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                </td>
                                <td class="td2Allalign">
                                    <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td2Allalign">
                                    <asp:Label ID="lblMailSendType" runat="server" Text="Send To :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMailSendType" runat="server" Enabled="False" Width="190px">
                                        <asp:ListItem Value="EMP" Text="Employees"></asp:ListItem>
                                        <asp:ListItem Value="USR" Text="Users"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td1Allalign">
                                    <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                                </td>
                                <td class="td2Allalign">
                                    <asp:CheckBox ID="chkStatus" runat="server" Text= "Active"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td class="td1align" valign="top" width="125px">
                                    <asp:Label ID="lblScheduleType" runat="server" Text="Schedule Type"></asp:Label>
                                </td>
                                <td>
                                    <fieldset>
                                        <legend>
                                            <asp:DropDownList ID="ddlScheduleType" runat="server" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged" AutoPostBack="True" Width="150px">
                                                <asp:ListItem Value="Daily1" Text="Daily - Shift 1"></asp:ListItem>
                                                <asp:ListItem Value="Daily2" Text="Daily - Shift 2"></asp:ListItem>
                                                <asp:ListItem Value="Daily3" Text="Daily - Shift 3"></asp:ListItem>
                                                <asp:ListItem Value="Weekly" Text="Weekly"></asp:ListItem>
                                                <asp:ListItem Value="Monthly" Text="Monthly"></asp:ListItem>
                                            </asp:DropDownList>
                                        </legend>
                                        <div id="divDaysOfWeek" runat="server" visible="false">
                                            <table id="tblDaysOfWeek" cellspacing="1" cellpadding="1" border="0">
                                                <tr>
                                                    <td style="height: 21px" colspan="7">
                                                        <asp:Label ID="lblOnthefollowingdays" runat="server" Text="On the following days:"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="cblDaysOfWeek" runat="server" Width="336px" RepeatDirection="Horizontal" RepeatColumns="7">
                                                            <asp:ListItem Value="1" Text="Sun"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Mon"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Tue"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Wed"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Thu"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Fri"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Sat"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divCalendar" runat="server" visible="false">
                                            <table id="tblCalendar" cellspacing="1" cellpadding="1" width="300" border="0">
                                                <tr>
                                                    <td colspan="10">
                                                        <asp:Label ID="lblCalendardays" runat="server" Text="Calendar days:"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="cblCalendardays" runat="server" Width="336px" RepeatDirection="Horizontal" RepeatColumns="8">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <table cellspacing="1" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:"></asp:Label>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="td2Allalign">
                                                                <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" CssClass="TimeCss"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

