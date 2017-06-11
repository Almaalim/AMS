<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="Pages_Config_Notifications" meta:resourcekey="PageResource1" %>

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
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">


                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.gif" LastPageText="Last" LastPageImageUrl="~/images/last.gif"
                            NextPageText="Next" NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.gif" />
                        <Columns>
                            <asp:BoundField HeaderText="Name(Ar)" DataField="MailNameAr" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:BoundField HeaderText="Name(En)" DataField="MailNameEn" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                            <asp:BoundField HeaderText="MailID" DataField="MailID" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField HeaderText="MailCode" DataField="MailCode" meta:resourcekey="BoundFieldResource4" />
                            <asp:TemplateField HeaderText="Status" SortExpression="SchActive" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("SchActive"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                        CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsSaveResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name(Ar) :" meta:resourcekey="lblNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server" meta:resourcekey="txtNameArResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvNameArResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name(En) :" meta:resourcekey="lblNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server" meta:resourcekey="txtNameEnResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;" ValidationGroup="vgSave" meta:resourcekey="cvNameEnResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblDescription" runat="server" Text="Description :" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDescription" runat="server" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblMailSendType" runat="server" Text="Send To :" meta:resourcekey="lblMailSendTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlMailSendType" runat="server" Enabled="False" meta:resourcekey="ddlMailSendTypeResource1">
                            <asp:ListItem Value="EMP" Text="Employees" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Value="USR" Text="Users" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="False" meta:resourcekey="txtIDResource1"></asp:TextBox>
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkStatus" runat="server" Text="Active" meta:resourcekey="chkStatusResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblScheduleType" runat="server" Text="Schedule Type :" meta:resourcekey="lblScheduleTypeResource1"></asp:Label>
                    </div>
                    <div class="col10">
                        <fieldset>
                            <legend>
                                <asp:DropDownList ID="ddlScheduleType" runat="server" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged" AutoPostBack="True" Width="150px" meta:resourcekey="ddlScheduleTypeResource1">
                                    <asp:ListItem Value="Daily1" Text="Daily - Shift 1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    <asp:ListItem Value="Daily2" Text="Daily - Shift 2" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    <asp:ListItem Value="Daily3" Text="Daily - Shift 3" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                    <asp:ListItem Value="Weekly" Text="Weekly" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                    <asp:ListItem Value="Monthly" Text="Monthly" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                </asp:DropDownList>
                            </legend>
                            <div id="divDaysOfWeek" runat="server" visible="False">
                                <div id="tblDaysOfWeek">
                                    <div class="row">
                                        <div class="col12">
                                            <asp:Label ID="lblOnthefollowingdays" runat="server" Text="On the following days:" meta:resourcekey="lblOnthefollowingdaysResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col12">
                                            <asp:CheckBoxList ID="cblDaysOfWeek" runat="server"  RepeatDirection="Horizontal" RepeatColumns="7" meta:resourcekey="cblDaysOfWeekResource1">
                                                <asp:ListItem Value="1" Text="Sunday"    meta:resourcekey="SundayResource1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Monday"    meta:resourcekey="MondayResource1"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Tuesday"   meta:resourcekey="TuesdayResource1"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Wednesday" meta:resourcekey="WednesdayResource1"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Thursday"  meta:resourcekey="ThursdayResource1"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Friday"    meta:resourcekey="FridayResource1"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="Saturday"  meta:resourcekey="SaturdayResource1"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divCalendar" runat="server" visible="False">
                                <div id="tblCalendar">
                                    <div class="row">
                                        <div class="col12">
                                            <asp:Label ID="lblCalendardays" runat="server" Text="Calendar days:" meta:resourcekey="lblCalendardaysResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col12">
                                            <asp:CheckBoxList ID="cblCalendardays" runat="server"   RepeatDirection="Horizontal" RepeatColumns="8" meta:resourcekey="cblCalendardaysResource1">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <asp:Label ID="lblStartTime" runat="server" Text="Start Time:" meta:resourcekey="lblStartTimeResource1"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" CssClass="TimeCss" meta:resourcekey="tpStartTimeResource1" TimePickerValidationGroup="" TimePickerValidationText="" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

