<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RotationShift_SANS.aspx.cs" Inherits="RotationShift_SANS" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/Control/EmployeeSelectedGroup.ascx" TagPrefix="uc" TagName="EmployeeSelectedGroup" %>
<%@ Register Src="~/Control/Control_SelectGroups.ascx" TagPrefix="ucg" TagName="Control_SelectGroups" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--Style--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <%--Style--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" Text="[None]" Value="[None]" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (Ar)" Value="RwtNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Rotation Shift Name (En)" Value="RwtNameEn" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" BorderWidth="0px" CellPadding="0"
                        CssClass="datatable" DataKeyNames="RwtID" GridLines="None"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnPreRender="grdData_PreRender"
                        OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" ShowFooter="True" meta:resourcekey="grdDataResource1">

                        <PagerSettings FirstPageImageUrl="~/images/first.png" FirstPageText="First"
                            LastPageImageUrl="~/images/last.png" LastPageText="Last"
                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/next.png"
                            NextPageText="Next" PreviousPageImageUrl="~/images/prev.png"
                            PreviousPageText="Prev" />
                        <Columns>
                            <asp:BoundField DataField="RwtNameAr" HeaderText="Name (Ar)" SortExpression="WktNameAr" meta:resourcekey="BoundFieldResource1"/>
                            <asp:BoundField DataField="RwtID" HeaderText="ID" SortExpression="RwtID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="RwtNameEn" HeaderText="Name (En)" SortExpression="RwtNameEn" meta:resourcekey="BoundFieldResource3" />
                            <asp:TemplateField HeaderText="Start Date" SortExpression="RwtFromDate" meta:resourcekey="TemplateFieldResource1" >
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("RwtFromDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="RwtToDate" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("RwtToDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RwtWorkDaysCount" HeaderText="Work Days" SortExpression="RwtWorkDaysCount" meta:resourcekey="BoundFieldResource4"/>
                            <asp:BoundField DataField="RwtNotWorkDaysCount" HeaderText="Vacation Days" SortExpression="RwtNotWorkDaysCount" meta:resourcekey="BoundFieldResource5"/>
                            <asp:BoundField DataField="RwtRotationDaysCount" HeaderText="Rotation Days" SortExpression="RwtRotationDaysCount" meta:resourcekey="BoundFieldResource6"/>
                                                        
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server"
                                        CommandArgument='<%# Eval("RwtID") %>' CommandName="Delete1"
                                        ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1"/>
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
                        CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsSaveResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="col8">
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnAddEmp" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAddEmp_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add Employees" meta:resourcekey="btnAddEmpResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnDelEmp" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnDelEmp_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Delete Employees" meta:resourcekey="btnDelEmpResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                    &nbsp;
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>

            <div class="GreySetion">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">                    
                    <asp:View ID="viw1" runat="server">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblRwtNameAr" runat="server" Text="Name (Ar) :" meta:resourcekey="viw1_lblRwtNameArResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="viw1_txtRwtNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="viw1_txtRwtNameArResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="viw1_lblRwtNameEn" runat="server" Text="Name (En)" meta:resourcekey="viw1_lblRwtNameEnResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="viw1_txtRwtNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="viw1_txtRwtNameEnResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblRwtWorkDaysCount" runat="server" Text="No. of Work Days :" meta:resourcekey="viw1_lblRwtWorkDaysCountResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="viw1_txtRwtWorkDaysCount" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="viw1_txtRwtWorkDaysCountResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="viw1_lblRwtNotWorkDaysCount" runat="server" Text="No. of Vacation Days :" meta:resourcekey="viw1_lblRwtNotWorkDaysCountResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="viw1_txtRwtNotWorkDaysCount" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="viw1_txtRwtNotWorkDaysCountResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblRwtRotationDaysCount" runat="server" Text="No. of Rotation Days :" meta:resourcekey="viw1_lblRwtRotationDaysCountResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="viw1_txtRwtRotationDaysCount" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="viw1_txtRwtRotationDaysCountResource1"></asp:TextBox>
                            </div>
                            <div class="col2"></div>
                            <div class="col4"></div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblStartDate" runat="server" Text="Start Date :" meta:resourcekey="viw1_lblStartDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="viw1_calStartDate" runat="server" CalendarType="System" />
                            </div>
                            <div class="col2">
                                <asp:Label ID="viw1_lblEndDate" runat="server" Text="End Date :" meta:resourcekey="viw1_lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="viw1_calEndDate" runat="server" CalendarType="System" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblWorkTime" runat="server" Text="Work Time :" meta:resourcekey="viw1_lblWorkTimeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:ListBox ID="viw1_lstWorkTime" runat="server" Height="200px" meta:resourcekey="viw1_lstWorkTimeResource1"></asp:ListBox>
                            </div>

                            <div class="col2">
                                <asp:Label ID="viw1_lblRotationGroup" runat="server" Text="Rotation Group :" meta:resourcekey="viw1_lblRotationGroupResource1"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:ListBox ID="viw1_lstRotationGroup" runat="server" AutoPostBack="True" Height="200px" OnSelectedIndexChanged="viw1_lstRotationGroup_SelectedIndexChanged" meta:resourcekey="viw1_lstRotationGroupResource1"></asp:ListBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="viw1_lblEmployeeGroup" runat="server" Text="Employee Group :" meta:resourcekey="viw1_lblEmployeeGroupResource1"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:ListBox ID="viw1_lstEmployeeGroup" runat="server" Height="200px" meta:resourcekey="viw1_lstEmployeeGroupResource1"></asp:ListBox>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="viw2" runat="server">
                        <asp:UpdatePanel ID="viw2_UpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:Wizard ID="WizardData" runat="server" ActiveStepIndex="0"
                                    DisplaySideBar="False" OnActiveStepChanged="WizardData_ActiveStepChanged"
                                    OnPreRender="WizardData_PreRender" Width="100%" meta:resourcekey="WizardDataResource1">
                                    <StartNavigationTemplate>
                                        <div class="row">
                                            <div class="col12">
                                                <asp:ImageButton ID="viw2_btnStartStep" runat="server" OnClick="viw2_btnStartStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="vgStart"
                                                    ToolTip="Next" meta:resourcekey="viw2_btnStartStepResource1"/>
                                            </div>
                                        </div>
                                    </StartNavigationTemplate>
                                    <StepNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="viw2_btnBackStep" runat="server" OnClick="viw2_btnBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back" meta:resourcekey="viw2_btnBackStepResource1"/>

                                                <asp:ImageButton ID="viw2_btnNextStep" runat="server" OnClick="viw2_btnNextStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_next.png" ValidationGroup="vgStep1"
                                                    ToolTip="Next" meta:resourcekey="viw2_btnNextStepResource1"/>
                                            </div>
                                        </div>
                                    </StepNavigationTemplate>
                                    <FinishNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="viw2_btnFinishBackStep" runat="server"
                                                    OnClick="viw2_btnFinishBackStep_Click"
                                                    ImageUrl="../images/Wizard_Image/step_previous.png" ToolTip="Back" meta:resourcekey="viw2_btnFinishBackStepResource1"/>

                                                <asp:ImageButton ID="viw2_btnFinishStep" runat="server"
                                                    OnClick="viw2_btnSaveFinish_Click" ImageUrl="../images/Wizard_Image/save.png"
                                                    ToolTip="Save" ValidationGroup="vgFinish" meta:resourcekey="viw2_btnFinishStepResource1"/>
                                            </div>
                                        </div>
                                    </FinishNavigationTemplate>
                                    <WizardSteps>
                                        <asp:WizardStep ID="WizardStepStart" runat="server" Title="1-Option" meta:resourcekey="WizardStepStartResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="viw2_vsStart" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="vgStart" meta:resourcekey="viw2_vsStartResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span id="viw2_spnRwtNameEn" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblRwtNameEn" runat="server" Text="Name (En) :" meta:resourcekey="viw2_lblRwtNameEnResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtRwtNameEn" runat="server" AutoCompleteType="Disabled" meta:resourcekey="viw2_txtRwtNameEnResource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="viw2_cvRwtNameEn" runat="server"
                                                        ControlToValidate="viw2_txtValid" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                                        ValidationGroup="vgStart" meta:resourcekey="viw2_cvRwtNameEnResource1"></asp:CustomValidator>
                                                    <asp:TextBox ID="viw2_txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="viw2_txtValidResource1"></asp:TextBox>
                                                </div>
                                                <div class="col2">
                                                    <span id="viw2_spnRwtNameAr" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblRwtNameAr" runat="server" Text="Name (Ar) :" meta:resourcekey="viw2_lblRwtNameArResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtRwtNameAr" runat="server" AutoCompleteType="Disabled" meta:resourcekey="viw2_txtRwtNameArResource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="viw2_cvRwtNameAr" runat="server"
                                                        ControlToValidate="viw2_txtValid" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                                        ValidationGroup="vgStart" meta:resourcekey="viw2_cvRwtNameArResource1"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblRwtWorkDaysCount" runat="server" Text="No. of Work Days :" meta:resourcekey="viw2_lblRwtWorkDaysCountResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtRwtWorkDaysCount" runat="server" AutoCompleteType="Disabled" onkeypress="return OnlyNumber(event);" meta:resourcekey="viw2_txtRwtWorkDaysCountResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="viw2_rvRwtWorkDaysCount" runat="server"
                                                        ControlToValidate="viw2_txtRwtWorkDaysCount" EnableClientScript="False" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='No. of Work Days is required' /&gt;"
                                                        ValidationGroup="vgStart" meta:resourcekey="viw2_rvRwtWorkDaysCountResource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblRwtNotWorkDaysCount" runat="server" Text="No. of Vacation Days :" meta:resourcekey="viw2_lblRwtNotWorkDaysCountResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtRwtNotWorkDaysCount" runat="server" onkeypress="return OnlyNumber(event);" meta:resourcekey="viw2_txtRwtNotWorkDaysCountResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="viw2_rvRwtNotWorkDaysCount" runat="server"
                                                        ControlToValidate="viw2_txtRwtNotWorkDaysCount" EnableClientScript="False" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='No. of Vacation Days is required' /&gt;"
                                                        ValidationGroup="vgStart" meta:resourcekey="viw2_rvRwtNotWorkDaysCountResource1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblRwtRotationDaysCount" runat="server" Text="No. of Rotation Days :" meta:resourcekey="viw2_lblRwtRotationDaysCountResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtRwtRotationDaysCount" runat="server" AutoCompleteType="Disabled" onkeypress="return OnlyNumber(event);" meta:resourcekey="viw2_txtRwtRotationDaysCountResource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="viw2_cvRwtRotationDaysCount" runat="server"
                                                        ControlToValidate="viw2_txtValid" EnableClientScript="False"
                                                        OnServerValidate="RotationDaysValidate_ServerValidate" CssClass="CustomValidator"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                                        ValidationGroup="vgStart" meta:resourcekey="viw2_cvRwtRotationDaysCountResource1"></asp:CustomValidator>
                                                </div>
                                                <div class="col2"></div>
                                                <div class="col4"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblStartDate" runat="server" Text="Start Date :" meta:resourcekey="viw2_lblStartDateResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="viw2_calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgStart" CalTo="viw2_calEndDate" />
                                                </div>
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblEndDate" runat="server" Text="End Date :" meta:resourcekey="viw2_lblEndDateResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="viw2_calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgStart" />
                                                </div>
                                            </div>                       
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStep1" runat="server" Title="2-Work Time" meta:resourcekey="WizardStep1Resource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="viw2_vsStep1" runat="server" CssClass="MsgValidation" EnableClientScript="False" ValidationGroup="vgStep1" meta:resourcekey="viw2_vsStep1Resource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblWktID" runat="server" Text="Work Time :" meta:resourcekey="viw2_lblWktIDResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="viw2_ddlWktID" runat="server" meta:resourcekey="viw2_ddlWktIDResource1"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2"></div>
                                                <div class="col4">
                                                    <asp:ImageButton ID="viw2_btnAddWkt" runat="server" OnClick="viw2_btnAddWkt_Click"
                                                        ImageUrl="../images/Wizard_Image/add.png" ToolTip="Add" meta:resourcekey="viw2_btnAddWktResource1"/>

                                                    <asp:ImageButton ID="viw2_btnUpWkt" runat="server" OnClick="viw2_btnUpWkt_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_up.png" ToolTip="Up" meta:resourcekey="viw2_btnUpWktResource1"/>

                                                    <asp:ImageButton ID="viw2_btnDownWkt" runat="server" OnClick="viw2_btnDownWkt_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_down.png" ToolTip="Down" meta:resourcekey="viw2_btnDownWktResource1"/>

                                                    <asp:ImageButton ID="viw2_btnRemoveWkt" runat="server"
                                                        OnClick="viw2_btnRemoveWkt_Click" ImageUrl="../images/Wizard_Image/delete.png" ToolTip="Remove" meta:resourcekey="viw2_btnRemoveWktResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                </div>
                                                <div class="col4">
                                                    <asp:ListBox ID="viw2_lstWorkTime" runat="server" Height="200px" meta:resourcekey="viw2_lstWorkTimeResource1"></asp:ListBox>
                                                    <asp:CustomValidator ID="viw2_cvlstWorkTime" runat="server"
                                                        ControlToValidate="viw2_txtValid" EnableClientScript="False"
                                                        OnServerValidate="lstWorkTime_viw1_ServerValidate"
                                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required' /&gt;"
                                                        ValidationGroup="vgStep1" meta:resourcekey="viw2_cvlstWorkTimeResource1"></asp:CustomValidator>
                                                </div>
                                            </div>                                            
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStep2" runat="server" Title="3-Group" meta:resourcekey="WizardStep2Resource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="viw2_vsStep2" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="vgStep2" meta:resourcekey="viw2_vsStep2Resource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblGrpName" runat="server" Text="Group Name :" meta:resourcekey="viw2_lblGrpNameResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="viw2_txtGrpName" runat="server" meta:resourcekey="viw2_txtGrpNameResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="viw2_lblGrpUser" runat="server" Text="Manager :" meta:resourcekey="viw2_lblGrpUserResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="viw2_ddlUser" runat="server" meta:resourcekey="viw2_ddlUserResource1"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2"></div>
                                                <div class="col4">
                                                    <asp:ImageButton ID="viw2_btnAddGrp" runat="server" OnClick="viw2_btnAddGrp_Click"
                                                        ImageUrl="../images/Wizard_Image/add.png" ToolTip="Add" meta:resourcekey="viw2_btnAddGrpResource1"/>
                                                    &nbsp;
                                                    <asp:ImageButton ID="viw2_btnUpGrp" runat="server" OnClick="viw2_btnUpGrp_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_up.png" ToolTip="Up" meta:resourcekey="viw2_btnUpGrpResource1"/>
                                                    &nbsp;
                                                    <asp:ImageButton ID="viw2_btnDownGrp" runat="server" OnClick="viw2_btnDownGrp_Click"
                                                        ImageUrl="../images/Wizard_Image/arrow_down.png" ToolTip="Down" meta:resourcekey="viw2_btnDownGrpResource1"/>
                                                    &nbsp;
                                                    <asp:ImageButton ID="viw2_btnRemoveGrp" runat="server"
                                                        OnClick="viw2_btnRemoveGrp_Click" ImageUrl="../images/Wizard_Image/delete.png"
                                                        ToolTip="Remove" meta:resourcekey="viw2_btnRemoveGrpResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2"></div>
                                                <div class="col4">
                                                    <div class="col6">
                                                        <asp:ListBox ID="viw2_lstRotationGrp" runat="server" Height="200px" meta:resourcekey="viw2_lstRotationGrpResource1"></asp:ListBox>
                                                    </div>
                                                    <div class="col6">
                                                        <asp:ListBox ID="viw2_lstRotationUsr" runat="server" Height="200px" meta:resourcekey="viw2_lstRotationUsrResource1"></asp:ListBox>

                                                        <asp:CustomValidator ID="viw2_cvlstRotationGroup" runat="server"
                                                            ControlToValidate="viw2_txtValid" EnableClientScript="False"
                                                            OnServerValidate="lstRotationGroup_viw2_ServerValidate"
                                                            Text="&lt;img src='../images/Exclamation.gif' title='Group Name is required' /&gt;"
                                                            ValidationGroup="vgStep2" meta:resourcekey="viw2_cvlstRotationGroupResource1"></asp:CustomValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStepFinish" runat="server" Title="4-Select Employee" meta:resourcekey="WizardStepFinishResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="viw2_vsFinish" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="vgFinish" meta:resourcekey="viw2_vsFinishResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <uc:EmployeeSelectedGroup runat="server" ID="viw2_ucEmployeeSelectedGroup" ValidationGroupName="vgFinish" ValidationType="ALL" />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                    </WizardSteps>
                                    <HeaderTemplate>
                                        <ul id="wizHeader<%=Session["Language"]%>">
                                            <asp:Repeater ID="SideBarList" runat="server">
                                                <ItemTemplate>
                                                    <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title='<%# Eval("Name") %>'><%# Eval("Name")%></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </HeaderTemplate>
                                </asp:Wizard>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:View>
                    <asp:View ID="viw3" runat="server">
                        <asp:UpdatePanel ID="viw3_UpdatePanel" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col12">
                                        <asp:ValidationSummary ID="viw3_vsSave" runat="server" CssClass="MsgValidation"
                                            EnableClientScript="False" ValidationGroup="viw3_vgSave" meta:resourcekey="viw3_vsSaveResource1"/>
                                    </div>
                                </div>                            
                                <div class="row">
                                    <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="viw3_lblAddStartDate" runat="server" Text="Start Date :" meta:resourcekey="viw3_lblAddStartDateResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <Cal:Calendar2 ID="viw3_calAddStartDate" runat="server" CalendarType="System" ValidationGroup="viw3_vgSave"/>
                                        <asp:CustomValidator ID="viw3_cvAddStartDate" runat="server"
                                            ControlToValidate="viw3_txtValid" EnableClientScript="False"
                                            OnServerValidate="AddDateValidate_ServerValidate" CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                            ValidationGroup="viw3_vgSave" meta:resourcekey="viw3_cvAddStartDateResource1"></asp:CustomValidator>
                                        <asp:TextBox ID="viw3_txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="viw3_txtValidResource1"></asp:TextBox>
                                    </div>
                                    <div class="col2"></div>
                                    <div class="col4"></div>
                                </div>       
                                <div class="row">
                                    <div class="col12">
                                        <uc:EmployeeSelectedGroup runat="server" ID="viw3_ucEmployeeSelectedGroup" ValidationGroupName="viw3_vgSave" ValidationType="ONE" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col12 center-block">
                                         <asp:ImageButton ID="viw3_btnSave" runat="server"
                                            OnClick="viw3_btnSave_Click" ImageUrl="../images/Wizard_Image/save.png"
                                            ToolTip="Save" ValidationGroup="viw3_vgSave" meta:resourcekey="viw3_btnSaveResource1"/>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:View>
                    <asp:View ID="viw4" runat="server">
                        <asp:UpdatePanel ID="viw4_UpdatePanel" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col12">
                                        <asp:ValidationSummary ID="viw4_vsSave" runat="server" CssClass="MsgValidation"
                                            EnableClientScript="False" ValidationGroup="viw4_vgSave" meta:resourcekey="viw4_vsSaveResource1"/>
                                    </div>
                                </div>                            
                                <div class="row">
                                    <div class="col2">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="viw4_lblAddStartDate" runat="server" Text="Start Date :" meta:resourcekey="viw4_lblAddStartDateResource1"></asp:Label>
                                    </div>
                                    <div class="col4">
                                        <Cal:Calendar2 ID="viw4_calAddStartDate" runat="server" CalendarType="System" ValidationGroup="vgSave_viw2"/>
                                        <asp:CustomValidator ID="viw4_cvAddStartDate" runat="server"
                                            ControlToValidate="viw4_txtValid" EnableClientScript="False"
                                            OnServerValidate="AddDateValidate_viw2_ServerValidate" CssClass="CustomValidator"
                                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                            ValidationGroup="viw4_vgSave" meta:resourcekey="viw4_cvAddStartDateResource1"></asp:CustomValidator>
                                        <asp:TextBox ID="viw4_txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="viw4_txtValidResource1"></asp:TextBox>
                                    </div>
                                    <div class="col2"></div>
                                    <div class="col4"></div>
                                </div>       
                                <div class="row">
                                    <div class="col12">
                                        <ucg:Control_SelectGroups runat="server" ID="viw4_ucGroupsSelected" ValidationGroupName="viw4_vgSave" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col12 center-block">
                                         <asp:ImageButton ID="viw4_btnSave" runat="server"
                                            OnClick="viw4_btnSave_Click" ImageUrl="../images/Wizard_Image/save.png"
                                            ToolTip="Save" ValidationGroup="viw4_vgSave" meta:resourcekey="viw4_btnSaveResource1"/>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
