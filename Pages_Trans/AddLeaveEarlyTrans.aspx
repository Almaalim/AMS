<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="AddLeaveEarlyTrans.aspx.cs" Inherits="AddLeaveEarlyTrans" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>

<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td valign="top" colspan="3">
                        <table  class="rp_Search">
                            <tr>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Employee ID:" Height="18px" 
                                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                                    
                                    <asp:TextBox ID="txtEmpIDSearch" runat="server" AutoCompleteType="Disabled" 
                                        Width="168px" ToolTip="Employee ID" meta:resourcekey="txtEmpIDSearchResource1"></asp:TextBox>
                                    <asp:CustomValidator id="cvFindEmp" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Employee Not found' /&gt;" 
                                        ErrorMessage="Employee Not found"
                                        ValidationGroup="Filter"
                                        OnServerValidate="FindEmp_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" meta:resourcekey="cvFindEmpResource1"></asp:CustomValidator>
                                    
                                    <asp:Panel runat="server" ID="pnlauID" Height="200px"  ScrollBars="Vertical" 
                                        meta:resourcekey="pnlauIDResource1" />
                                    <ajaxToolkit:AutoCompleteExtender
                                        runat="server" 
                                        ID="auID" 
                                        TargetControlID="txtEmpIDSearch"
                                        ServicePath="~/Service/AutoComplete.asmx" 
                                        ServiceMethod="GetEmployeeIDList"
                                        MinimumPrefixLength="1"
                                        OnClientItemSelected="AutoCompleteIDSearchItemSelected"
                                        CompletionListElementID="pnlauID"
                                        CompletionListCssClass="AutoExtender" 
                                        CompletionListItemCssClass="AutoExtenderList" 
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                                    &nbsp;
                                    <asp:Label ID="lblNameFilter" runat="server" Text="Employee Name:" Height="18px" 
                                        meta:resourcekey="lblNameFilterResource1"></asp:Label>
                                    
                                    <asp:TextBox ID="txtEmpNameSearch" runat="server" AutoCompleteType="Disabled" 
                                        Width="168px" ToolTip="Employee Name" meta:resourcekey="txtEmpNameSearchResource1"></asp:TextBox>
                                    <asp:CustomValidator id="cvFindEmpName" runat="server"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Employee Not found' /&gt;" 
                                        ErrorMessage="Employee Not found"
                                        ValidationGroup="Filter"
                                        OnServerValidate="FindEmp_ServerValidate"
                                        EnableClientScript="False" 
                                        ControlToValidate="txtValid" meta:resourcekey="cvFindEmpNameResource1"></asp:CustomValidator>
                                    
                                    <asp:Panel runat="server" ID="pnlauName" Height="200px"  ScrollBars="Vertical" 
                                        meta:resourcekey="pnlauNameResource1" />
                                    <ajaxToolkit:AutoCompleteExtender
                                        runat="server" 
                                        ID="auName" 
                                        TargetControlID="txtEmpNameSearch"
                                        ServicePath="~/Service/AutoComplete.asmx" 
                                        ServiceMethod="GetEmployeeNameList"
                                        MinimumPrefixLength="1"
                                        OnClientItemSelected="AutoCompleteNameSearchItemSelected"
                                        CompletionListElementID="pnlauName"
                                        CompletionListCssClass="AutoExtender" 
                                        CompletionListItemCssClass="AutoExtenderList" 
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                        CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />
                                    &nbsp;
                                    <asp:Label ID="lblTodayDate" runat="server" Text="Today Date:" Height="18px" 
                                        meta:resourcekey="lblTodayDateResource1"></asp:Label>
                                    &nbsp;
                                    <asp:TextBox ID="txtTodayDate" runat="server" Width="110px" 
                                        ToolTip="Today Date" Enabled="False" meta:resourcekey="txtTodayDateResource1"></asp:TextBox>
                                    &nbsp;
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                                        ImageUrl="../images/Button_Icons/button_magnify.png" Width="16px" 
                                        ValidationGroup="Filter" meta:resourcekey="btnFilterResource1"/>
                                </td>
                            </tr>
                      </table>   
                    </td>                                
                </tr>

                <tr>
                    <td  style="height:10px"></td>             
                </tr>

                <tr>
                    <td>
                        <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid">
                                            <asp:UpdatePanel ID="updPanel" runat="server">
                                                <ContentTemplate>
                                                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" 
                                                        TargetControlID="grdData" NextPageKey="PageUp" NextRowSelectKey="Add" 
                                                        PreviousPageKey="PageDown" PrevRowSelectKey="Subtract" />
                                                     <div style="width: 800px;">
                                                     
                                                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                                                        SelectedIndex="1" AutoGenerateColumns="False"
                                                        AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EmpID"
                                                        ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                        OnPreRender="grdData_PreRender" Width="800px" 
                                                             meta:resourcekey="grdDataResource1">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                                            NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                                                            PreviousPageImageUrl="~/images/prev.png" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Time" InsertVisible="False" 
                                                                SortExpression="TrnTime" meta:resourcekey="TemplateFieldResource1">
                                                                <ItemTemplate>
                                                                    <%# DisplayFun.GrdDisplayTime(Eval("TrnTime"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type" InsertVisible="False" 
                                                                SortExpression="TrnType" meta:resourcekey="TemplateFieldResource2">
                                                                <ItemTemplate>
                                                                    <%# DisplayFun.GrdDisplayTypeTrans(Eval("TrnType"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Location(En)" DataField="LocationEn" 
                                                                ReadOnly="True" SortExpression="Location" 
                                                                meta:resourcekey="BoundFieldResource1">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Location(Ar)" DataField="LocationAr" 
                                                                ReadOnly="True" SortExpression="Location" 
                                                                meta:resourcekey="BoundFieldResource2">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="User Name" DataField="UsrName" ReadOnly="True" 
                                                                SortExpression="UsrName" meta:resourcekey="BoundFieldResource3">
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Description" DataField="TrnDesc" ReadOnly="True" 
                                                                SortExpression="TrnDesc" meta:resourcekey="BoundFieldResource4">
                                                            </asp:BoundField>

                                                            <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID" 
                                                                meta:resourcekey="BoundFieldResource5" >
                                                                <HeaderStyle CssClass="first" />
                                                                <ItemStyle CssClass="first" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Date" InsertVisible="False" 
                                                                SortExpression="TrnDate" meta:resourcekey="TemplateFieldResource3">
                                                                <ItemTemplate>
                                                                    <%# DisplayFun.GrdDisplayDate(Eval("TrnDate"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="row" />
                                                    </asp:GridView>
                                                    <!-- Notice this is outside the GridView --> 
                                                    </div>  
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                    <td colspan="2">
                        <asp:ValidationSummary runat="server" ID="vsADD" ValidationGroup="vgADD" 
                            EnableClientScript="False" CssClass="errorValidation" 
                            meta:resourcekey="vsADDResource1"/>
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
                    <td style="height:20px" valign="top" colspan="2"> 
                        <table>
                            <tr>
                                <td valign="bottom">
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton" ValidationGroup="vgADD"
                                        OnClick="btnAdd_Click" Width="70px"  Height="18px" 
                                        
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" 
                                        meta:resourcekey="btnAddResource1"></asp:LinkButton>
                                </td>
                    
                                <td>
                                    &nbsp;
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" 
                                       OnClick="btnSave_Click" Width="70px" Height="18px" ValidationGroup="vgSave" 
                                       
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                                </td>
                                <td style="width: 94px">
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
                                    <asp:CustomValidator ID="cvIsAdd" runat="server"  ValidationGroup="vgADD" Display="None"
                                        ErrorMessage="You must be the last Transactions is a IN Transactions"
                                        OnServerValidate="IsAdd_ServerValidate" EnableClientScript="False" 
                                        ControlToValidate="txtValid" meta:resourcekey="cvIsAddResource1"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td class="td1Allalign">
                                                <span class="RequiredField">*</span>
                                                <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID :" 
                                                    meta:resourcekey="lblEmployeeIDResource1"></asp:Label>    
                                            </td>     
                                            <td class="td2Allalign">
                                                <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" 
                                                    Width="168px" ToolTip="Employee ID" Enabled="False" 
                                                    meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtEmpID" runat="server" 
                                                    ControlToValidate="txtEmpID" EnableClientScript="False" 
                                                    Text="&lt;img src='../images/Exclamation.gif' title='Employee ID is required!' /&gt;" 
                                                    ValidationGroup="vgSave" meta:resourcekey="rfvtxtEmpIDResource1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="td1Allalign">
                                                <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :" 
                                                    meta:resourcekey="lblEmpNameResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled" 
                                                    Width="168px" Enabled="False" meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td1Allalign">
                                                <span class="RequiredField">*</span>
                                                <asp:Label ID="lblDaye" runat="server" Text="Date :" 
                                                    meta:resourcekey="lblDayeResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                                            </td>
                                            <td class="td1Allalign">
                                              <span class="RequiredField">*</span>  
                                              <asp:Label ID="lblType" runat="server" Text="Type :" 
                                                    meta:resourcekey="lblTypeResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:DropDownList ID="ddlType" runat="server" Enabled="False" Width="173px" 
                                                    meta:resourcekey="ddlTypeResource1">
                                                    <asp:ListItem Text="OUT" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td1Allalign">
                                               <span class="RequiredField">*</span>  <asp:Label ID="lblTime" runat="server" 
                                                    Text="Time :" meta:resourcekey="lblTimeResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <Almaalim:TimePicker ID="tpickerTime" runat="server" FormatTime="HHmmss"  
                                                    CssClass="TimeCss" meta:resourcekey="tpickerTimeResource1" 
                                                    TimePickerValidationGroup="" TimePickerValidationText=""/>
                                                <asp:CustomValidator id="cvtpickerTime" runat="server"
                                                    Text="&lt;img src='../images/Exclamation.gif' title='Time is required!' /&gt;" 
                                                    ValidationGroup="vgSave"
                                                    OnServerValidate="TpickerTime_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid" 
                                                    meta:resourcekey="cvtpickerTimeResource1"></asp:CustomValidator>
                                            </td>
                                            <td class="td1Allalign">
                                               <span class="RequiredField">*</span>  
                                               <asp:Label ID="lblLocation" runat="server" Text="Location :" 
                                                    meta:resourcekey="lblLocationResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:DropDownList ID="ddlLocation" runat="server" Enabled="False" Width="173px" 
                                                    meta:resourcekey="ddlLocationResource1">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvLocation" ControlToValidate="ddlLocation"
                                                    InitialValue="Select Location" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Language is required!' />"
                                                    ErrorMessage="Location is required!" ValidationGroup="vgSave" 
                                                    meta:resourcekey="rfvLocationResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

