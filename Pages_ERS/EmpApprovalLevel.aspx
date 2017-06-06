<%@ Page Title="Employee Approval Level" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmpApprovalLevel.aspx.cs" Inherits="EmpApprovalLevel" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

<%@ Register Src="~/Control/ManagersSelected.ascx" TagName="ManagersSelected" TagPrefix="uc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <%--script--%>

    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:Label runat="server" ID="CurrentTab"
                        meta:resourcekey="CurrentTabResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Label ID="Messages" runat="server" meta:resourcekey="MessagesResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server"
                        meta:resourcekey="lblIDFilterResource1" Text="Search By:"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlFilterResource1" Width="300px">
                        <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True">[None]</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource2" Value="EmpID">Employee ID</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource3" Text="Employee Name (Ar)"
                            Value="EmpNameAr"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource4" Text="Employee Name (En)"
                            Value="EmpNameEn"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource5" Value="EalMgrID">Manager</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled"
                        meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" OnClick="btnFilter_Click" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server"
                        TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" BorderWidth="0px"
                        CellPadding="0" CssClass="datatable" DataKeyNames="EmpID"
                        EnableModelValidation="True" GridLines="None"
                        meta:resourcekey="grdDataResource1"
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
                            <asp:BoundField DataField="EmpID" HeaderText="Employee ID"
                                meta:resourcekey="BoundFieldResource1" SortExpression="EmpID"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameAr" HeaderText="Emp. Name (Ar)"
                                meta:resourcekey="BoundFieldResource2" SortExpression="EmpNameAr"></asp:BoundField>
                            <asp:BoundField DataField="EmpNameEn" HeaderText="Emp. Name (En)"
                                meta:resourcekey="BoundFieldResource3" SortExpression="EmpNameEn" />
                            <asp:BoundField DataField="EalLevelCount" HeaderText="Level Count"
                                meta:resourcekey="BoundFieldResource4" SortExpression="EalLevelCount" />
                            <asp:BoundField DataField="RetID" HeaderText="RetID"
                                meta:resourcekey="BoundFieldResource5" SortExpression="RetID" />
                            <asp:TemplateField HeaderText="Request Type"
                                meta:resourcekey="TemplateFieldResource1" SortExpression="RequestType">
                                <ItemTemplate>
                                    <%# getRequestType(Eval("RetID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server"
                                        CommandArgument='<%# Eval("EmpID") + "," + Eval("RetID") %>'
                                        CommandName="Delete1" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsWarning" runat="server"
                        CssClass="warningValidation" EnableClientScript="False" ForeColor="#9F6000"
                        meta:resourcekey="vsWarningResource1" ValidationGroup="vgWarning" />
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
                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                        EnableClientScript="False" CssClass="MsgValidation"
                        meta:resourcekey="vsumAllResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        meta:resourcekey="btnAddResource1" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" meta:resourcekey="btnModifyResource1" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        ValidationGroup="vgWarning"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        meta:resourcekey="btnSaveResource1" OnClick="btnSave_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        ValidationGroup="vgSave"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                    &nbsp;
                                            <asp:CustomValidator ID="cvDeleteReq" runat="server"
                                                ControlToValidate="txtValid" Display="None" CssClass="CustomValidator"
                                                EnableClientScript="False" meta:resourcekey="cvDeleteReqResource1"
                                                OnServerValidate="Warning_ServerValidate" ValidationGroup="vgWarning">
                                            </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viw" runat="server">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblEmpID" runat="server" meta:resourcekey="lblEmpIDResource1"
                                    Text="Employee ID :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblEmpNameAr" runat="server"
                                    meta:resourcekey="lblEmpNameArResource1" Text="Employee Name (Ar) :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtEmpNameAr" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtEmpNameArResource1"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblEmpNameEn" runat="server"
                                    meta:resourcekey="lblEmpNameEnResource1" Text="Employee Name (En) :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtEmpNameEn" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" meta:resourcekey="txtEmpNameEnResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblRetID" runat="server" meta:resourcekey="lblRetIDResource1"
                                    Text="Request Type :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:DropDownList ID="ddlRetID" runat="server" Enabled="False"
                                    meta:resourcekey="ddlRetIDResource1">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                             <div class="col12">
                                <uc:ManagersSelected runat="server" ID="ucManagersSelected" LevelCount="0" ValidationGroupName="Users" />
                             </div>
                        </div>
                    </asp:View>
                    <asp:View ID="viw0" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <asp:Wizard ID="WizardData" runat="server" ActiveStepIndex="0"
                                    DisplaySideBar="False" meta:resourcekey="WizardDataResource1"
                                    OnActiveStepChanged="WizardData_ActiveStepChanged"
                                    OnPreRender="WizardData_PreRender" Width="100%">
                                    <StartNavigationTemplate>
                                        <div class="row">
                                            <div class="col12">
                                                <asp:ImageButton ID="btnStartStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/step_next.png"
                                                    meta:resourcekey="btnStartStepResource1" OnClick="btnStartStep_Click"
                                                    ToolTip="Next" ValidationGroup="VGStart" />
                                            </div>
                                        </div>
                                    </StartNavigationTemplate>
                                    <StepNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnNextStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/step_next.png"
                                                    meta:resourcekey="btnNextStepResource1" OnClick="btnNextStep_Click"
                                                    ToolTip="Next" ValidationGroup="VGStep1" />

                                                <asp:ImageButton ID="btnBackStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png"
                                                    meta:resourcekey="btnBackStepResource1" OnClick="btnBackStep_Click"
                                                    ToolTip="Back" />
                                            </div>
                                        </div>
                                    </StepNavigationTemplate>
                                    <FinishNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnFinishBackStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png"
                                                    meta:resourcekey="btnFinishBackStepResource1" OnClick="btnFinishBackStep_Click"
                                                    ToolTip="Back" />

                                                <asp:ImageButton ID="btnFinishStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/save.png"
                                                    meta:resourcekey="btnFinishStepResource1" OnClick="btnFinishStep_Click"
                                                    ToolTip="Save" ValidationGroup="VGFinish" />
                                            </div>
                                        </div>
                                    </FinishNavigationTemplate>
                                    <WizardSteps>
                                        <asp:WizardStep ID="WizardStep1" runat="server" meta:resourcekey="WizardStep1Resource1" Title="1-Options">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="VSStart" runat="server" CssClass="errorValidation"
                                                        EnableClientScript="False" meta:resourcekey="VSStartResource1"
                                                        ValidationGroup="VGStart" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <asp:Label ID="lblRetID_viw0" runat="server"
                                                        meta:resourcekey="lblRetID_viw0Resource1" Text="Request Type :"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="ddlRetID_viw0" runat="server" AutoPostBack="True"
                                                        meta:resourcekey="ddlRetID_viw0Resource1"
                                                        OnSelectedIndexChanged="ddlRetID_viw0_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRetID_viw0" runat="server" CssClass="CustomValidator"
                                                        ControlToValidate="ddlRetID_viw0" EnableClientScript="False"
                                                        meta:resourcekey="rfvRetID_viw0Resource1"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Request Type is required!' /&gt;"
                                                        ValidationGroup="VGStart"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtCustomValidator_viw0" runat="server"
                                                        meta:resourcekey="txtCustomValidator_viw0Resource1" Text="02120"
                                                        Visible="False" Width="10px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <uc:ManagersSelected runat="server" ID="ucManagersSelectedWizard" LevelCount="0" ValidationGroupName="VGStart" />
                                                </div>
                                            </div>

                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStep2" runat="server"
                                            meta:resourcekey="WizardStep2Resource1" Title="2-Select Employees">
                                            </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <asp:ValidationSummary ID="VSFinish" runat="server" CssClass="errorValidation"
                                        EnableClientScript="False" meta:resourcekey="VSFinishResource1"
                                        ValidationGroup="VGFinish" />
                                </div>
                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <ucEmp:EmployeeSelected ID="ucEmployeeSelected" runat="server"
                                                        ValidationGroupName="VGFinish" />
                                                </div>
                                            </div>

                                        </asp:WizardStep>
                                    </WizardSteps>
                                    <HeaderTemplate>
                                        <ul id="wizHeader<%=Session["Language"]%>" style="background-color: #C2C2C2;">
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
