<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="Pages_Config_Notifications" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender" EnableModelValidation="True">


                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.gif" LastPageText="Last" LastPageImageUrl="~/images/last.gif"
                            NextPageText="Next" NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.gif" />
                        <Columns>
                            <asp:BoundField HeaderText="Name(Ar)" DataField="MailNameAr"></asp:BoundField>
                            <asp:BoundField HeaderText="Name(En)" DataField="MailNameEn"></asp:BoundField>
                            <asp:BoundField HeaderText="MailID" DataField="MailID" />
                            <asp:BoundField HeaderText="MailCode" DataField="MailCode" />
                            <asp:TemplateField HeaderText="SchActive" SortExpression="SchActive">
                                <ItemTemplate>
                                    <%--<%# DisplayFun.GrdDisplayDate(Eval("SchStartDate"))%>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
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
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                        CssClass="MsgValidation" ShowSummary="False" />
                </div>
            </div>

            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save">
                    </asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name(Ar)"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name(En)"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblMailSendType" runat="server" Text="Send To :"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlMailSendType" runat="server" Enabled="False">
                            <asp:ListItem Value="EMP" Text="Employees"></asp:ListItem>
                            <asp:ListItem Value="USR" Text="Users"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkStatus" runat="server" Text="Active" />
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblScheduleType" runat="server" Text="Schedule Type"></asp:Label>
                    </div>
                    <div class="col10">
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
                                <div id="tblDaysOfWeek">
                                    <div class="row">
                                        <div class="col12">
                                            <asp:Label ID="lblOnthefollowingdays" runat="server" Text="On the following days:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col12">
                                            <asp:CheckBoxList ID="cblDaysOfWeek" runat="server"  RepeatDirection="Horizontal" RepeatColumns="7">
                                                <asp:ListItem Value="1" Text="Sun"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Mon"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Tue"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Wed"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Thu"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Fri"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="Sat"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divCalendar" runat="server" visible="false">
                                <div id="tblCalendar">
                                    <div class="row">
                                        <div class="col12">
                                            <asp:Label ID="lblCalendardays" runat="server" Text="Calendar days:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col12">
                                            <asp:CheckBoxList ID="cblCalendardays" runat="server"   RepeatDirection="Horizontal" RepeatColumns="8">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" CssClass="TimeCss" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

