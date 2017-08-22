<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RequestSurprise.aspx.cs" Inherits="Pages_Trans_RequestSurprise" %>

<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>

    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server">
                        <asp:ListItem Selected="True">[None]</asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (Ar)" Value="WktNameAr"></asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (En)" Value="WktNameEn"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png"/>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server"
                        TargetControlID="grdData" NextRowSelectKey="Add"
                        PrevRowSelectKey="Subtract" />
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" BorderWidth="0px" CellPadding="0"
                        CssClass="datatable" DataKeyNames="RsmID" GridLines="None"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnPreRender="grdData_PreRender"
                        OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        SelectedIndex="0" ShowFooter="True">

                        <PagerSettings FirstPageImageUrl="~/images/first.png" FirstPageText="First"
                            LastPageImageUrl="~/images/last.png" LastPageText="Last"
                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/next.png"
                            NextPageText="Next" PreviousPageImageUrl="~/images/prev.png"
                            PreviousPageText="Prev" />
                        <Columns>
                            <asp:BoundField DataField="RsmNameAr" HeaderText="Name (Ar)" SortExpression="RsmNameAr"></asp:BoundField>
                            <asp:BoundField DataField="RsmID" HeaderText="ID" SortExpression="RsmID"/>
                            <asp:BoundField DataField="RsmNameEn" HeaderText="Name (En)" SortExpression="RsmNameEn"/>
                            <asp:TemplateField HeaderText="Date" SortExpression="RsmDate">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("RsmDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Time" SortExpression="RsmTimeFrom">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("RsmTimeFrom"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Time" SortExpression="RsmTimeTo">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("RsmTimeTo"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Is Send" SortExpression="RsmIsSend">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("RsmIsSend"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="           ">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server"
                                        CommandArgument='<%# Eval("RsmID") %>' CommandName="Delete1"
                                        ImageUrl="../images/Button_Icons/button_delete.png"/>
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
                        CssClass="MsgValidation" ShowSummary="False"/>
                </div>
            </div>
            
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px"></asp:TextBox>
                    &nbsp;
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>

            <div class="GreySetion">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viwShow" runat="server">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblRsmNameAr" runat="server" Text="Name (Ar) :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtRsmNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col2">

                                <asp:Label ID="lblRsmNameEn" runat="server" Text="Name (En)"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtRsmNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblRsmDate" runat="server" Text="Date :"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calRsmDate" runat="server" CalendarType="System" />
                            </div>
                            <div class="col2">
                                
                            </div>
                            <div class="col4">
                                
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblRsmTimeFrom" runat="server" Text="From :"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpRsmTimeFrom" runat="server" FormatTime="HHmmss" CssClass="TimeCss"/>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblRsmTimeTo" runat="server" Text="To :"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpRsmTimeTo" runat="server" FormatTime="HHmmss" CssClass="TimeCss"/>
                            </div>
                        </div>

                        <div class="row" id="DivMac" runat="server">
                            <div class="col2">
                                <asp:Label ID="lblRsmMacType" runat="server" Text="Machine Method :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:DropDownList ID="ddlRsmMacType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRsmMacType_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text ="Transaction Machine" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text ="Surprise Machine"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col2" id="DivMac1" runat="server" visible="false">
                                <asp:Label ID="lblRsmMacIDs" runat="server" Text="Machine :"></asp:Label>
                            </div>
                            <div class="col4" id="DivMac2" runat="server" visible="false">
                                <asp:DropDownList ID="ddlRsmMacIDs" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblRsmDescription" runat="server" Text="Description :" ></asp:Label>
                            </div>
                            <div class="col4">
                                  <asp:TextBox ID="txtRsmDescription" runat="server" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblRsmEmpIDs" runat="server" Text="Employees :"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:GridView ID="grdRsmEmpIDs" runat="server" AutoGenerateColumns="False"  
                                    DataKeyNames="EmpID" TabIndex="100" GridLines="Horizontal" EnableModelValidation="True">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField HeaderText="Name" DataField="EmpName" SortExpression="EmpName" ReadOnly="True"></asp:BoundField>
                                        <asp:BoundField HeaderText="Attend" DataField="isAttend" SortExpression="isAttend" ReadOnly="True"></asp:BoundField>
                                    </Columns>     
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="viwAdd" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="block">
                            <ContentTemplate>

                                <asp:Wizard ID="WizardData" runat="server" ActiveStepIndex="0"
                                    DisplaySideBar="False" OnActiveStepChanged="WizardData_ActiveStepChanged"
                                    OnPreRender="WizardData_PreRender">
                                    <StartNavigationTemplate>
                                        <div class="row">
                                            <div class="col12">
                                                <asp:ImageButton ID="btnStartStep" runat="server" OnClick="btnStartStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="vgStart" ToolTip="Next"/>
                                            </div>
                                        </div>
                                    </StartNavigationTemplate>
                                    <StepNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnBackStep" runat="server" OnClick="btnBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back"/>

                                                <asp:ImageButton ID="btnNextStep" runat="server" OnClick="btnNextStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="VGStep1" ToolTip="Next"/>
                                            </div>
                                        </div>
                                    </StepNavigationTemplate>
                                    <FinishNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnFinishBackStep" runat="server"
                                                    OnClick="btnFinishBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back"/>

                                                <asp:ImageButton ID="btnFinishStep" runat="server"
                                                    OnClick="btnSaveFinish_Click" ImageUrl="../images/Wizard_Image/save.png"
                                                    ToolTip="Save" ValidationGroup="VGFinish"/>
                                            </div>
                                        </div>
                                    </FinishNavigationTemplate>
                                    <WizardSteps>
                                        <asp:WizardStep ID="WizardStepStart" runat="server" Title="1-Option">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="vsStart" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="vgStart"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span id="spnRsmNameEn_WZ" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmNameEn_WZ" runat="server" Text="Name (En) :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtRsmNameEn_WZ" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvRsmNameEn_WZ" runat="server"
                                                        ControlToValidate="txtValid_WZ" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        ValidationGroup="vgStart"></asp:CustomValidator>
                                                    <asp:TextBox ID="txtValid_WZ" runat="server" Text="02120" Visible="False" Width="10px"></asp:TextBox>
                                                </div>
                                                <div class="col2">
                                                    <span id="spnRsmNameAr_WZ" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmNameAr_WZ" runat="server" Text="Name (Ar) :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtRsmNameAr_WZ" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvRsmNameAr_WZ" runat="server"
                                                        ControlToValidate="txtCustomValidator" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="txtValid_WZ"
                                                        ValidationGroup="vgStart"></asp:CustomValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmDate_WZ" runat="server" Text="Date :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="calRsmDate_WZ" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgStart" />
                                                </div>
                                                <div class="col2">
                                                    
                                                </div>
                                                <div class="col4">
  
                                                </div> 
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmTimeFrom_WZ" runat="server" Text="From :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Almaalim:TimePicker ID="tpRsmTimeFrom_WZ" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                                           TimePickerValidationGroup="vgStart" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='From Time is required' /&gt;"/>
                                                </div>
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmTimeTo_WZ" runat="server" Text="To :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Almaalim:TimePicker ID="tpRsmTimeTo_WZ" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                                            TimePickerValidationGroup="vgStart" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='To Time is required' /&gt;"/>
                                                </div>
                                            </div>

                                            <div class="row" id="DivMac_WZ" runat="server">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmMacType_WZ" runat="server" Text="Machine Method :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="ddlRsmMacType_WZ" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRsmMacType_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text ="Transaction Machine" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text ="Surprise Machine"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col2" id="DivMac1_WZ" runat="server" visible="false">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblRsmMacIDs_WZ" runat="server" Text="Machine :"></asp:Label>
                                                </div>
                                                <div class="col4" id="DivMac2_WZ" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlRsmMacIDs_WZ" runat="server">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rvRsmMacIDs_WZ" runat="server" ControlToValidate="ddlRsmMacIDs_WZ"
                                                        EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Machine is required' /&gt;"
                                                        ValidationGroup="vgStart" CssClass="CustomValidator" Enabled="false">
                                                     </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col2">
                                                    <asp:Label ID="lblRsmDescription_WZ" runat="server" Text="Description :" ></asp:Label>
                                                </div>
                                                <div class="col4">
                                                      <asp:TextBox ID="txtRsmDescription_WZ" runat="server" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                                                </div>

                                                <div class="col2">
                                
                                                </div>

                                                <div class="col4">
                               
                                                </div>
                                            </div>

                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStepFinish" runat="server" Title="2-Select Employees">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSFinish" runat="server" CssClass="MsgValidation" EnableClientScript="False" ValidationGroup="vgFinish"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <ucEmp:EmployeeSelected ID="ucEmployeeSelected" runat="server" ValidationGroupName="vgFinish" />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                    </WizardSteps>
                                    <HeaderTemplate>
                                        <ul id="wizHeader<%=Session["Language"]%>">
                                            <asp:Repeater ID="SideBarList" runat="server">
                                                <ItemTemplate>
                                                    <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>"
                                                        title='<%# Eval("Name") %>'><%# Eval("Name")%></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </HeaderTemplate>
                                </asp:Wizard>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:View>
                </asp:MultiView>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


