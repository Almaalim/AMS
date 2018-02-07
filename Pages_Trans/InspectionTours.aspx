<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="InspectionTours.aspx.cs" Inherits="InspectionTours" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/Control/EmployeeSelected.ascx" TagName="EmployeeSelected" TagPrefix="ucEmp" %>

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
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Inspection Tour Name (Ar)" Value="ItmNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Inspection Tour Name (En)" Value="ItmNameEn" meta:resourcekey="ListItemResource3"></asp:ListItem>
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
                        CssClass="datatable" DataKeyNames="ItmID" GridLines="None"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnPreRender="grdData_PreRender"
                        OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        ShowFooter="True" meta:resourcekey="grdDataResource1">

                        <PagerSettings FirstPageImageUrl="~/images/first.png" FirstPageText="First"
                            LastPageImageUrl="~/images/last.png" LastPageText="Last"
                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/next.png"
                            NextPageText="Next" PreviousPageImageUrl="~/images/prev.png"
                            PreviousPageText="Prev" />
                        <Columns>
                            <asp:BoundField DataField="ItmNameAr" HeaderText="Name (Ar)" SortExpression="ItmNameAr" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                            <asp:BoundField DataField="ItmID" HeaderText="ID" SortExpression="ItmID" meta:resourcekey="BoundFieldResource2"/>
                            <asp:BoundField DataField="ItmNameEn" HeaderText="Name (En)" SortExpression="ItmNameEn" meta:resourcekey="BoundFieldResource3"/>
                            <asp:TemplateField HeaderText="Date" SortExpression="ItmDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ItmDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Time" SortExpression="ItmTimeFrom" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ItmTimeFrom"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Time" SortExpression="ItmTimeTo" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ItmTimeTo"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Is Send" SortExpression="ItmIsSend" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayIS(Eval("ItmIsSend"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Is Process" SortExpression="ItmIsProcess" meta:resourcekey="TemplateFieldResource442">
                                <ItemTemplate>
                                    <%# GrdDisplayIsProcess(Eval("ItmIsProcess"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server"
                                        CommandArgument='<%# Eval("ItmID") + "," + Eval("ItmIsSend") %>' CommandName="Delete1"
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnReSend" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnReSend_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/page_refresh.png&quot; /&gt; Resend" meta:resourcekey="btnReSendResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnReProsess" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign"
                        OnClick="btnReProsess_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/page_edit2.png&quot; /&gt; Reprocessing" meta:resourcekey="btnReProsessResource1"></asp:LinkButton>
                </div>
                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                    &nbsp;
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>

            <div class="GreySetion">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viwShow" runat="server">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblItmNameAr" runat="server" Text="Name (Ar) :" meta:resourcekey="lblItmNameArResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtItmNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtItmNameArResource1"></asp:TextBox>
                            </div>
                            <div class="col2">

                                <asp:Label ID="lblItmNameEn" runat="server" Text="Name (En) :" meta:resourcekey="lblItmNameEnResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtItmNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtItmNameEnResource1"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblItmDate" runat="server" Text="Date :" meta:resourcekey="lblItmDateResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Cal:Calendar2 ID="calItmDate" runat="server" CalendarType="System" />
                            </div>
                            <div class="col2">
                                
                            </div>
                            <div class="col2">
                                <asp:CheckBox ID="chkItmIsSend" runat="server" Text="Send" Enabled="False" meta:resourcekey="chkItmIsSendResource1"/>
                                <asp:CheckBox ID="chkItmIsProcess" runat="server" Text="Process" Enabled="False" meta:resourcekey="chkItmIsProcessResource1"/>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblItmTimeFrom" runat="server" Text="From :" meta:resourcekey="lblItmTimeFromResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpItmTimeFrom" runat="server" FormatTime="HHmmss" CssClass="TimeCss" meta:resourcekey="tpItmTimeFromResource1" TimePickerValidationGroup="" TimePickerValidationText=""/>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblItmTimeTo" runat="server" Text="To :" meta:resourcekey="lblItmTimeToResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpItmTimeTo" runat="server" FormatTime="HHmmss" CssClass="TimeCss" meta:resourcekey="tpItmTimeToResource1" TimePickerValidationGroup="" TimePickerValidationText=""/>
                            </div>
                        </div>

                        <div class="row" id="DivMac" runat="server">
                            <div class="col2">
                                <asp:Label ID="lblItmMacType" runat="server" Text="Machine Method :" meta:resourcekey="lblItmMacTypeResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:DropDownList ID="ddlItmMacType" runat="server" meta:resourcekey="ddlItmMacTypeResource1">
                                    <asp:ListItem Value="1" Text ="Transaction Machine" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    <asp:ListItem Value="2" Text ="Inspection Machine" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col2" id="DivMac1" runat="server" visible="False">
                                <asp:Label ID="lblItmMacIDs" runat="server" Text="Machine :" meta:resourcekey="lblItmMacIDsResource1"></asp:Label>
                            </div>
                            <div class="col4" id="DivMac2" runat="server" visible="False">
                                <asp:DropDownList ID="ddlItmMacIDs" runat="server" meta:resourcekey="ddlItmMacIDsResource1">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblItmDescription" runat="server" Text="Description :" meta:resourcekey="lblItmDescriptionResource1" ></asp:Label>
                            </div>
                            <div class="col4">
                                  <asp:TextBox ID="txtItmDescription" runat="server" TextMode="MultiLine" Style="resize: none;" meta:resourcekey="txtItmDescriptionResource1"></asp:TextBox>
                            </div>

                        </div>
                         <div class="row">
                            <div class="col2">
                             
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="False" meta:resourcekey="txtIDResource1"></asp:TextBox>
                                <asp:TextBox ID="txtEmpIDs" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                            </div>

                        </div>


                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblItmEmpIDs" runat="server" Text="Employees :" meta:resourcekey="lblItmEmpIDsResource1"></asp:Label>
                            </div>

                            <div class="col4">
                                <asp:GridView ID="grdItmEmpIDs" runat="server" AutoGenerateColumns="False"  
                                    DataKeyNames="EmpID" TabIndex="100" GridLines="Horizontal" meta:resourcekey="grdItmEmpIDsResource1">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID" ReadOnly="True" meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                        <asp:BoundField HeaderText="Name" DataField="EmpName" SortExpression="EmpName" ReadOnly="True" meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Is Attend" SortExpression="isAttend" meta:resourcekey="TemplateFieldResource6">
                                            <ItemTemplate>
                                                <%# DisplayFun.GrdDisplayIS(Eval("isAttend"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Process" SortExpression="isProcess" meta:resourcekey="TemplateFieldResource66">
                                            <ItemTemplate>
                                                <%# GrdDisplayIsProcess(Eval("isProcess"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>     
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="viwAdd" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                                    ToolTip="Next" ValidationGroup="vgStart" />
                                            </div>
                                        </div>
                                    </StartNavigationTemplate>
                                    <StepNavigationTemplate>
                                        <div class="row">
                                            <div class="col12 center-block">
                                                <asp:ImageButton ID="btnNextStep" runat="server"
                                                    ImageUrl="../images/Wizard_Image/step_next.png"
                                                    meta:resourcekey="btnNextStepResource1" OnClick="btnNextStep_Click"
                                                    ToolTip="Next" ValidationGroup="vgStep1" />

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
                                                    ToolTip="Save" ValidationGroup="vgFinish" />
                                            </div>
                                        </div>
                                    </FinishNavigationTemplate>
                                    <WizardSteps>
                                        <asp:WizardStep ID="WizardStepStart" runat="server" Title="1-Option" meta:resourcekey="WizardStepStartResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="vsStart" runat="server" CssClass="MsgValidation"
                                                        EnableClientScript="False" ValidationGroup="vgStart" meta:resourcekey="vsStartResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span id="spnItmNameAr_WZ" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmNameAr_WZ" runat="server" Text="Name (Ar) :" meta:resourcekey="lblItmNameAr_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtItmNameAr_WZ" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtItmNameAr_WZResource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvItmNameAr_WZ" runat="server"
                                                        ControlToValidate="txtValid_WZ" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        ValidationGroup="vgStart" meta:resourcekey="cvItmNameAr_WZResource1"></asp:CustomValidator>
                                                </div>
                                                <div class="col2">
                                                    <span id="spnItmNameEn_WZ" runat="server" visible="False" class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmNameEn_WZ" runat="server" Text="Name (En) :" meta:resourcekey="lblItmNameEn_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:TextBox ID="txtItmNameEn_WZ" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtItmNameEn_WZResource1"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvItmNameEn_WZ" runat="server"
                                                        ControlToValidate="txtValid_WZ" EnableClientScript="False"
                                                        OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                                        ValidationGroup="vgStart" meta:resourcekey="cvItmNameEn_WZResource1"></asp:CustomValidator>
                                                    <asp:TextBox ID="txtValid_WZ" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValid_WZResource1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmDate_WZ" runat="server" Text="Date :" meta:resourcekey="lblItmDate_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Cal:Calendar2 ID="calItmDate_WZ" runat="server" CalendarType="System"  InitialValue="true" ValidationGroup="vgStart" ValidationRequired="true"  />
                                                </div>
                                                <div class="col2">
                                                    
                                                </div>
                                                <div class="col4">
  
                                                </div> 
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmTimeFrom_WZ" runat="server" Text="From :" meta:resourcekey="lblItmTimeFrom_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Almaalim:TimePicker ID="tpItmTimeFrom_WZ" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                                           TimePickerValidationGroup="vgStart" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='From Time is required' /&gt;" meta:resourcekey="tpItmTimeFrom_WZResource1"/>
                                                </div>
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmTimeTo_WZ" runat="server" Text="To :" meta:resourcekey="lblItmTimeTo_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <Almaalim:TimePicker ID="tpItmTimeTo_WZ" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                                            TimePickerValidationGroup="vgStart" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='To Time is required' /&gt;" meta:resourcekey="tpItmTimeTo_WZResource1"/>
                                                
                                                    <asp:CustomValidator ID="cvTime_WZ" runat="server" 
                                                        ValidationGroup="vgStart" OnServerValidate="Time_ServerValidate" CssClass="CustomValidator"
                                                        EnableClientScript="False" ControlToValidate="txtValid_WZ" meta:resourcekey="cvTime_WZResource1"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class="row" id="DivMac_WZ" runat="server">
                                                <div class="col2">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmMacType_WZ" runat="server" Text="Machine Method :" meta:resourcekey="lblItmMacType_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4">
                                                    <asp:DropDownList ID="ddlItmMacType_WZ" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItmMacType_WZ_SelectedIndexChanged" meta:resourcekey="ddlItmMacType_WZResource1">
                                                        <asp:ListItem Value="1" Text ="Transaction Machine" Selected="True" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text ="Inspection Machine" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col2" id="DivMac1_WZ" runat="server" visible="False">
                                                    <span class="RequiredField">*</span>
                                                    <asp:Label ID="lblItmMacIDs_WZ" runat="server" Text="Machine :" meta:resourcekey="lblItmMacIDs_WZResource1"></asp:Label>
                                                </div>
                                                <div class="col4" id="DivMac2_WZ" runat="server" visible="False">
                                                    <asp:DropDownList ID="ddlItmMacIDs_WZ" runat="server" meta:resourcekey="ddlItmMacIDs_WZResource1">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rvItmMacIDs_WZ" runat="server" ControlToValidate="ddlItmMacIDs_WZ"
                                                        EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Machine is required' /&gt;"
                                                        ValidationGroup="vgStart" CssClass="CustomValidator" Enabled="False" meta:resourcekey="rvItmMacIDs_WZResource1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col2">
                                                    <asp:Label ID="lblItmDescription_WZ" runat="server" Text="Description :" meta:resourcekey="lblItmDescription_WZResource1" ></asp:Label>
                                                </div>
                                                <div class="col4">
                                                      <asp:TextBox ID="txtItmDescription_WZ" runat="server" TextMode="MultiLine" Style="resize: none;" meta:resourcekey="txtItmDescription_WZResource1"></asp:TextBox>
                                                </div>

                                                <div class="col2">
                                
                                                </div>

                                                <div class="col4">
                               
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                        <asp:WizardStep ID="WizardStepFinish" runat="server" Title="2-Select Employees" meta:resourcekey="WizardStepFinishResource1">
                                            <div class="row">
                                                <div class="col12">
                                                    <asp:ValidationSummary ID="vsFinish" runat="server" CssClass="MsgValidation" EnableClientScript="False" ValidationGroup="vgFinish" meta:resourcekey="vsFinishResource1"/>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col12">
                                                    <ucEmp:EmployeeSelected ID="ucEmployeeSelected_WZ" runat="server" ValidationGroupName="vgFinish"  />
                                                </div>
                                            </div>
                                        </asp:WizardStep>
                                    </WizardSteps>
                                    <HeaderTemplate>
                                        <ul id="wizHeader<%=Session["Language"]%>" style="background-color: #C2C2C2;">
                                            <asp:Repeater ID="SideBarList" runat="server">
                                                <ItemTemplate>
                                                    <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>"
                                                        title='<%# Eval("Name") %>'><%# Eval("Name")%></a> </li>
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

