<%@ Page Title="Shift Swap User" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ShiftSwapUser.aspx.cs" Inherits="ShiftSwapUser" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <%--script--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table runat="server" id="MainTable">                             
                
                <tr>
                    <td>
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <div style="width: 800px; overflow: scroll;">
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowPaging="True"
                                                CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="SwpID" ShowFooter="True"
                                                OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                                                OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                OnRowCommand="grdData_RowCommand" onprerender="grdData_PreRender" 
                                                Width="800px" 
                                                meta:resourcekey="grdDataResource1">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                                    NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                                                    PreviousPageImageUrl="~/images/prev.png" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="EmpID" 
                                                        SortExpression="EmpID" />
                                                    <asp:BoundField DataField="SwpID" HeaderText="ID" SortExpression="SwpID" 
                                                        meta:resourcekey="BoundFieldResource2" />
                                                    <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" 
                                                        SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource1" />
                                                    <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)" 
                                                        SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource3" />
                                                    
                                                    <asp:TemplateField HeaderText="Start Date" SortExpression="SwpStartDate" 
                                                        meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("SwpStartDate"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date" SortExpression="SwpEndDate" 
                                                        meta:resourcekey="TemplateFieldResource2">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("SwpEndDate"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:BoundField HeaderText="ID" DataField="EmpID2" 
                                                        SortExpression="EmpID2" />
                                                    <asp:BoundField DataField="EmpName2Ar" HeaderText="Name (Ar)" 
                                                        SortExpression="EmpName2Ar" meta:resourcekey="BoundFieldResource4" />
                                                    <asp:BoundField HeaderText="Name (En)" DataField="EmpName2En" 
                                                        SortExpression="EmpName2En" meta:resourcekey="BoundFieldResource5" />
                                                    <asp:TemplateField HeaderText="Start Date" SortExpression="SwpStartDate2" 
                                                        meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("SwpStartDate2"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date" SortExpression="SwpEndDate2" 
                                                        meta:resourcekey="TemplateFieldResource2">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("SwpEndDate2"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="           " 
                                                        meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete"  CommandName="Delete1" CommandArgument='<%# Eval("SwpID") %>'
                                                                runat="server" ImageUrl="../images/Button_Icons/button_delete.png" 
                                                                meta:resourcekey="imgbtnDeleteResource1" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="row" />
                                            </asp:GridView>
                                            </div>
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
                   
                <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </td>
                </tr>
                <tr>
                    <td class="td2Allalign" colspan="2">
                        <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                            EnableClientScript="False" CssClass="MsgValidation" 
                            meta:resourcekey="vsumAllResource1" />
                    </td>
                </tr>     
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="bottom">
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton" 
                                        OnClick="btnAdd_Click" Width="70px"  Height="18px" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" 
                                        meta:resourcekey="btnAddResource1"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton" 
                                        OnClick="btnModify_Click" Width="70px" Height="18px" Visible="False" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" 
                                        meta:resourcekey="btnModifyResource1"></asp:LinkButton> 
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" 
                                        OnClick="btnSave_Click" Width="70px" Height="18px" ValidationGroup="vgSave" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                                </td>
                                <td>
                                &nbsp;
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" 
                                        OnClick="btnCancel_Click" Width="70px" Height="18px" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" 
                                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
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
                    <td valign="top">
                        <table>                                 
                            <tr>
                                <td class="td1Allalign" valign="middle">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="Label1" runat="server" Text="Employee ID:" 
                                        meta:resourcekey="lblEmpIDResource1"></asp:Label>
                                </td>
                                <td valign="middle" class="td2Allalign">
                                    <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" 
                                        Width="167px" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtEmpID" EnableClientScript="False" 
                                        Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rfvEmpID2Resource1"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator id="cvEmployee1" runat="server"
                                        Text="&lt;img src='../images/message_exclamation.png' title='No Employee with ID!' /&gt;" 
                                        ValidationGroup="vgSave"
                                        ErrorMessage="No Employee with ID!"
                                        OnServerValidate="Employee_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" 
                                        meta:resourcekey="cvEmployeeResource1"></asp:CustomValidator>
                                </td>
                            </tr>
                                
                            <tr>
                                <td class="td1Allalign" valign="middle">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="lblType" runat="server" Text="Type :" 
                                        meta:resourcekey="lblTypeResource1"></asp:Label>
                                </td>
                                <td valign="middle" class="td2Allalign">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="172px" 
                                        meta:resourcekey="ddlTypeResource1">
                                        <asp:ListItem Text="Select Type" Value="Select Type" Selected="True" 
                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                        <asp:ListItem Text="Working Day(s)" Value="Work" 
                                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Text="Off Day(s)" Value="Off" 
                                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlType" runat="server" 
                                        ControlToValidate="ddlType" EnableClientScript="False" 
                                        Text="&lt;img src='../images/Exclamation.gif' title='Type is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rfvddlTypeResource1"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator id="cvWorkTime" runat="server"
                                        Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;" 
                                        ValidationGroup="vgSave"
                                        OnServerValidate="Days_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" 
                                        meta:resourcekey="cvWorkTimeResource1"></asp:CustomValidator>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtWorktime" runat="server" AutoCompleteType="Disabled" 
                                        Width="180px" Visible="False" meta:resourcekey="txtWorktimeResource1"></asp:TextBox> 
                                </td>
                            </tr>
                                            
                            <tr>
                                <td class="td1Allalign">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblStartDate1" runat="server" Text="Start Date :" 
                                        meta:resourcekey="lblStartDate1Resource1"></asp:Label>
                                </td>
                                <td class="td2Allalign" valign="middle">                                   
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <Cal:Calendar2 ID="calStartDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate1"/>
                                            </td>
                                            <td> 
                                                <asp:CustomValidator id="cvDaysCount" runat="server"
                                                    Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;" 
                                                    ValidationGroup="vgSave"
                                                    OnServerValidate="DaysCount_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid" 
                                                    meta:resourcekey="cvDaysCountResource1"></asp:CustomValidator>
                                            </td>
                                            <td> 
                                                <asp:CustomValidator id="cvDays1" runat="server"
                                                    Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;" 
                                                    ValidationGroup="vgSave"
                                                    OnServerValidate="Days_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid" 
                                                    meta:resourcekey="cvDays1Resource1"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="td1Allalign" valign="middle">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="lblEndDate1" runat="server" Text="End Date :" 
                                        meta:resourcekey="lblEndDate1Resource1" ></asp:Label>
                                </td>
                                <td class="td2Allalign" valign="middle">
                                    <Cal:Calendar2 ID="calEndDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate"/>
                                </td>
                            </tr>                                           
                                                                                   
                            <tr>
                                <td class="td1Allalign" valign="middle">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="lblEmpID2" runat="server" Text="swap With Employee ID:" 
                                        meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                                </td>
                                <td valign="middle" class="td2Allalign">
                                    <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled" 
                                        Width="167px" meta:resourcekey="txtEmpID2Resource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmpID2" runat="server" 
                                        ControlToValidate="txtEmpID2" EnableClientScript="False" 
                                        Text="&lt;img src='../images/Exclamation.gif' title='swap With Employee ID is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="rfvEmpID2Resource1"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator id="cvEmployee2" runat="server"
                                        Text="&lt;img src='../images/message_exclamation.png' title='No Employee with ID!' /&gt;" 
                                        ValidationGroup="vgSave"
                                        ErrorMessage="No Employee with ID!"
                                        OnServerValidate="Employee_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" 
                                        meta:resourcekey="cvEmployeeResource1"></asp:CustomValidator>
                                </td>
                            </tr>
                                            
                            <tr>
                                <td class="td1Allalign">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="lblStartDate2" runat="server"  Text="Start Date :" 
                                        meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                                </td>
                                <td class="td2Allalign" valign="middle">                                   
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate2"/>
                                            </td>
                                            <td> 
                                                <asp:CustomValidator id="cvDays2" runat="server"
                                                    Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;" 
                                                    ValidationGroup="vgSave"
                                                    OnServerValidate="Days_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid" 
                                                    meta:resourcekey="cvDays2Resource1"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="td1Allalign" valign="middle">
                                <span class="RequiredField">*</span>
                                    <asp:Label ID="lblEndDate2" runat="server" Text="End Date :" 
                                        meta:resourcekey="lblEndDate2Resource1" ></asp:Label>
                                </td>
                                <td class="td2Allalign" valign="middle">
                                    <Cal:Calendar2 ID="calEndDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave"/>
                                </td>
                            </tr>        
                            <tr>
                                <td colspan="4">
                                    <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" 
                                        Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


