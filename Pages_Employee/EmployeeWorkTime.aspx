<%@ Page Title="Employee Worktime" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeWorkTime.aspx.cs" Inherits="EmployeeWorkTime" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <%--script--%>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
                <div class="row" runat="server" id="MainTable">
                <div class="col1">
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" 
                                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                                </div>
                <div class="col2">
                                    <asp:DropDownList ID="ddlFilter" runat="server"   
                                        meta:resourcekey="ddlFilterResource1">
                                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                                        <asp:ListItem Text="Employee ID" Value="EmpID" 
                                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Text="Employee  Name (Ar)" Value="EmpNameAr" 
                                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn" 
                                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                                        <asp:ListItem Text="Worktime Name (Ar)" Value="WktNameAr" 
                                            meta:resourcekey="ListItemResource5"></asp:ListItem>
                                        <asp:ListItem Text="Worktime Name (En)" Value="WktNameEn" 
                                            meta:resourcekey="ListItemResource6"></asp:ListItem>
                                    </asp:DropDownList>
                             </div>
                <div class="col2">
                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" 
                                        meta:resourcekey="txtFilterResource1"></asp:TextBox>
                             
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay" 
                                        meta:resourcekey="btnFilterResource1" />
                               </div>
            </div>

            <div class="row">
                <div class="col12">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" 
                                                TargetControlID="grdData" NextRowSelectKey="Add" 
                                                PrevRowSelectKey="Subtract" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" 
                                                AutoGenerateColumns="False" AllowPaging="True"
                                                CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="EwrID" ShowFooter="True"
                                                OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                                                OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                OnRowCommand="grdData_RowCommand" onprerender="grdData_PreRender" 
                                                  EnableModelValidation="True" 
                                                meta:resourcekey="grdDataResource1">
                                                 
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                                    NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                                                    PreviousPageImageUrl="~/images/prev.png" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="EmpID" 
                                                        SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EwrID" HeaderText="Ewr ID" SortExpression="EwrID" 
                                                        meta:resourcekey="BoundFieldResource2" />
                                                    <asp:BoundField HeaderText="Name (Ar)" DataField="EmpNameAr" 
                                                        SortExpression="EmpNameAr" InsertVisible="False"
                                                        ReadOnly="True" meta:resourcekey="BoundFieldResource3">
                                                        <HeaderStyle CssClass="first" />
                                                        <ItemStyle CssClass="first" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)" 
                                                        SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource4" />
                                                    <asp:BoundField HeaderText="Worktime ID" DataField="WktID" 
                                                        SortExpression="WktID" Visible="False" 
                                                        meta:resourcekey="BoundFieldResource5" />
                                                    <asp:BoundField HeaderText="Worktime Name (Ar)" DataField="WktNameAr" 
                                                        SortExpression="WktNameAr" meta:resourcekey="BoundFieldResource6" />
                                                    <asp:BoundField DataField="WktNameEn" HeaderText="Worktime Name (En)" 
                                                        SortExpression="WktNameEn" meta:resourcekey="BoundFieldResource7" />
                                                    <asp:TemplateField HeaderText="Start Date" SortExpression="EwrStartDate"
                                                        meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("EwrStartDate"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date" SortExpression="EwrEndDate" 
                                                        meta:resourcekey="TemplateFieldResource2">
                                                        <ItemTemplate>
                                                            <%# DisplayFun.GrdDisplayDate(Eval("EwrEndDate"))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="           " 
                                                        meta:resourcekey="TemplateFieldResource3" >
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("EwrID") %>'
                                                                runat="server" ImageUrl="../images/Button_Icons/button_delete.png" 
                                                                meta:resourcekey="imgbtnDeleteResource1" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                            </asp:GridView>
                                        </div>
            </div>

            <div class="row">
                <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </div>
            </div>
            <div class="row">
                <div class="col12">
                        <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave"
                            EnableClientScript="False" CssClass="MsgValidation" 
                            meta:resourcekey="vsumAllResource1" />
                    </div>
                 </div>
            <div class="row">
                <div class="col8">
                              <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" 
                                   OnClick="btnAdd_Click" 
                                   Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" 
                                   meta:resourcekey="btnAddResource1"></asp:LinkButton>
                          
                              <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" 
                                   OnClick="btnModify_Click" 
                                   Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" 
                                   meta:resourcekey="btnModifyResource1"></asp:LinkButton> 
                      
                              <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton  glyphicon glyphicon-floppy-disk" 
                                   OnClick="btnSave_Click" ValidationGroup="vgSave" 
                                   Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                   meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                          
                              <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" 
                                   OnClick="btnCancel_Click" 
                                   Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" 
                                   meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                           </div>
                <div class="col4">
                                <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                              
                                <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                    ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid">
                                </asp:CustomValidator>
                        </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                   <div class="col2">

                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :" 
                                                        meta:resourcekey="lblEmpIDResource1" ></asp:Label>
                                            </div>
                    <div class="col4">
                                                    <asp:TextBox ID="txtEmpID" runat="server" autocomplete="off"
                                                        Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                                                    <asp:Panel runat="server" ID="pnlauID"   />
                                                    <ajaxToolkit:AutoCompleteExtender
                                                        runat="server" 
                                                        ID="auID" 
                                                        TargetControlID="txtEmpID"
                                                        ServicePath="~/Service/AutoComplete.asmx" 
                                                        ServiceMethod="GetEmployeeIDList"
                                                        MinimumPrefixLength="1" 
                                                        CompletionInterval="1000"
                                                        EnableCaching="true"
                                                        OnClientItemSelected="AutoCompleteIDItemSelected"
                                                        CompletionListElementID="pnlauID"
                                                        CompletionListCssClass="AutoExtender" 
                                                        CompletionListItemCssClass="AutoExtenderList" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                        CompletionSetCount="12" />
                                                    <asp:RequiredFieldValidator ID="reqBranchManager1" runat="server" 
                                                        ControlToValidate="txtEmpID" EnableClientScript="False" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Emloyee ID is required!' /&gt;" 
                                                        ValidationGroup="vgSave" meta:resourcekey="reqBranchManager1Resource1"></asp:RequiredFieldValidator>

                                                    <asp:CustomValidator id="cvFindEmp" runat="server"
                                                    Text="&lt;img src='../images/message_exclamation.png' title='Employee Not found!' /&gt;" 
                                                    ErrorMessage="Employee Not found!"
                                                    ValidationGroup="vgSave" CssClass="CustomValidator"
                                                    OnServerValidate="FindEmp_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtValid" 
                                                    meta:resourcekey="cvFindEmpResource1"></asp:CustomValidator>
                                                </div>
                    <div class="col2">
                                                    <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :" 
                                                         meta:resourcekey="lblEmpNameResource1"></asp:Label>
                                               </div>
                    <div class="col4">
                                                    <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled" 
                                                        meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                                                    <asp:Panel runat="server" ID="pnlauName"   />
                                                    <ajaxToolkit:AutoCompleteExtender
                                                        runat="server" 
                                                        ID="auName" 
                                                        TargetControlID="txtEmpName"
                                                        ServicePath="~/Service/AutoComplete.asmx" 
                                                        ServiceMethod="GetEmployeeNameList"
                                                        MinimumPrefixLength="1" 
                                                        CompletionInterval="1000"
                                                        EnableCaching="true"
                                                        OnClientItemSelected="AutoCompleteNameItemSelected"
                                                        CompletionListElementID="pnlauName"
                                                        CompletionListCssClass="AutoExtender" 
                                                        CompletionListItemCssClass="AutoExtenderList" 
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                                        CompletionSetCount="12" />
                                             </div>
                </div>

                <div class="row">
                    <div class="col2">
                                                <span class="RequiredField">*</span>
                                                <asp:Label ID="lblWorktime" 
                                                        runat="server" Text="Work Time :" meta:resourcekey="lblWorktimeResource1" ></asp:Label>
                                              </div>
                    <div class="col4">
                                                    <asp:DropDownList ID="ddlWktID" runat="server" Enabled="False" 
                                                        AutoPostBack="True" onselectedindexchanged="ddlWktID_SelectedIndexChanged" 
                                                        meta:resourcekey="ddlWktIDResource1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlWktID" runat="server" 
                                                        ControlToValidate="ddlWktID" EnableClientScript="False" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;" 
                                                        ValidationGroup="vgSave" meta:resourcekey="rfvddlWktIDResource1"></asp:RequiredFieldValidator>

                                                </div>
                </div>

                <div class="row">
                    <div class="col2">
                                                <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblStartDate" runat="server" 
                                                        meta:resourcekey="lblStartDateResource1" >Start Date :</asp:Label>
                                                      </div>
                    <div class="col4">                                  
                                                     <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate"/>
                                               </div>
                    <div class="col2">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date :" 
                                                        meta:resourcekey="lblEndDateResource1"></asp:Label>
                                                             </div>
                    <div class="col4"> 
                                                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave"/>
                                                     </div>
                </div>

                <div class="row">
                    <div class="col2">
                                                <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblWorkingdays" runat="server" Text="Working days :" 
                                                        meta:resourcekey="lblWorkingdaysResource1" ></asp:Label>
                                                              </div>
                    <div class="col4">   
                        <span class="daysChk">
                            <asp:CheckBox ID="chkEwrSun" runat="server" Enabled="False" Text="Sunday" 
                                meta:resourcekey="chkEwrSunResource1" />
                            </span>
                        <span class="daysChk">    
                            <asp:CheckBox ID="chkEwrMon" runat="server" Enabled="False" Text="Monday" 
                                meta:resourcekey="chkEwrMonResource1" />
                            </span>
                        <span class="daysChk">    
                            <asp:CheckBox ID="chkEwrTue" runat="server" Enabled="False" Text="Tuesday" 
                                meta:resourcekey="chkEwrTueResource1" />
                            </span>
                        <span class="daysChk">     
                            <asp:CheckBox ID="chkEwrWed" runat="server" Enabled="False" Text="Wednesday" 
                                meta:resourcekey="chkEwrWedResource1" />
                            </span>
                        <span class="daysChk">    
                            <asp:CheckBox ID="chkEwrThu" runat="server" Enabled="False" Text="Thursday" 
                                meta:resourcekey="chkEwrThuResource1" />
                            </span>
                        <span class="daysChk">    
                            <asp:CheckBox ID="chkEwrFri" runat="server" Enabled="False" Text="Friday" 
                                meta:resourcekey="chkEwrFriResource1" />
                        </span>      
                        <span class="daysChk">
                            <asp:CheckBox ID="chkEwrSat" runat="server" Enabled="False" Text="Saturday" 
                                meta:resourcekey="chkEwrSatResource1" />
                            </span>
                        <asp:CustomValidator id="cvSelectWorkDays" runat="server"
                            Text="&lt;img src='../images/message_exclamation.png' title='Select Work Days!' /&gt;" 
                            ValidationGroup="vgSave"
                            ErrorMessage="Select Work Days" CssClass="CustomValidator"
                            OnServerValidate="SelectWorkDays_ServerValidate" 
                            EnableClientScript="False" 
                            ControlToValidate="txtValid" meta:resourcekey="cvSelectWorkDaysResource1"></asp:CustomValidator>
                            </div>
                    <div class="col2">                    </div>
                    <div class="col4"> 
                                                    <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                                                                       </div>
                </div>
                </div>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


