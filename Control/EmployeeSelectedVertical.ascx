<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSelectedVertical.ascx.cs" Inherits="EmployeeSelectedVertical"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always"> <%--UpdateMode="Conditional"--%>
    <ContentTemplate>
        <div runat="server" id="MainTable">

            <asp:Panel ID="pnlEmloyeeSelected" runat="server" Width="100%" meta:resourcekey="pnlEmloyeeSelectedResource1">
                <div class="GreySetion">
                    <div class="row">
                        <div class="col12">
                            <asp:Label ID="lblEmployees" runat="server" Text="Employees" CssClass="h3" meta:resourcekey="lblEmployeesResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col3">
                            <asp:Label ID="lblEmpTypeID" runat="server" Text="Employee Type:" meta:resourcekey="lblEmpTypeIDResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlEmpTypeID" runat="server" AutoPostBack="true"
                                meta:resourcekey="ddlEmpTypeIDResource1"
                                OnSelectedIndexChanged="ddlEmpTypeID_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                        </div>
                        <div class="col3">
                        </div>
                    </div>

                    <div class="row" id="divAllEmp" runat="server" visible="false">
                        <div class="col12">
                            <asp:RadioButton ID="rdoSelectAll" runat="server" Text="Select All Employess" GroupName="SelectOption" AutoPostBack="True"
                                OnCheckedChanged="rdoSelectAll_CheckedChanged" meta:resourcekey="rdoSelectAllResource1"  />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col3">
                            <asp:RadioButton ID="rdoSelectByID" runat="server" Text="Select Employee :"
                                GroupName="SelectOption" AutoPostBack="True"
                                OnCheckedChanged="rdoSelectByID_CheckedChanged" meta:resourcekey="rdoSelectByIDResource1" />
                        </div>
                        <div class="col4">

                            <asp:TextBox ID="txtSearchByID" runat="server" Enabled="False"></asp:TextBox>
                            <asp:CustomValidator ID="cvSearchByID" runat="server"
                                ErrorMessage="" CssClass="CustomValidator"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                OnServerValidate="SelectEmployees_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtCustomValidator">
                            </asp:CustomValidator>

                            <asp:Panel runat="server" ID="pnlauID" Height="200px" ScrollBars="Vertical" />
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"
                                ID="auID"
                                TargetControlID="txtSearchByID"
                                ServicePath="~/Service/AutoComplete.asmx"
                                ServiceMethod="GetEmployeeIDListWithCon"
                                MinimumPrefixLength="1"
                                CompletionInterval="1000"
                                EnableCaching="true"
                                OnClientItemSelected="AutoCompleteDepNameItemSelected"
                                CompletionListElementID="pnlauID"
                                CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                CompletionSetCount="12" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col3">
                            <asp:RadioButton ID="rdoSelectDep" runat="server" Text="Select Department :"
                                GroupName="SelectOption" AutoPostBack="True"
                                OnCheckedChanged="rdoSelectDep_CheckedChanged" meta:resourcekey="rdoSelectDepResource1" />
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtSearchByDep" runat="server" Enabled="False"></asp:TextBox>
                            <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay" />

                            <asp:Panel runat="server" ID="pnlauDepName" Height="200px" ScrollBars="Vertical" />
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"
                                ID="auDepName"
                                TargetControlID="txtSearchByDep"
                                ServicePath="~/Service/AutoComplete.asmx"
                                ServiceMethod="GetDepNameList"
                                MinimumPrefixLength="1"
                                CompletionInterval="1000"
                                EnableCaching="true"
                                OnClientItemSelected="AutoCompleteDepNameItemSelected"
                                CompletionListElementID="pnlauDepName"
                                CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                CompletionSetCount="12" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col3"></div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlDepartment" runat="server" 
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                meta:resourcekey="ddlDepartmentResource1" Enabled="False">
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>
                <div class="GreySetion">
                    <div class="row">
                        <div class="col5"  >
                            <div class="row">
                                <div class="col12 center-block">
                                    <asp:Label ID="lblLeft" CssClass="h4" runat="server" Text="Select Employees"
                                        meta:resourcekey="lblLeftResource1"></asp:Label>
                                </div>
                            </div>


                            <asp:Panel ID="pnlLeftGrid" runat="server" Height="200px" ScrollBars="Vertical"
                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px"
                                meta:resourcekey="pnlLeftGridResource1" Enabled="False">
                                <div class="row">
                                    <div class="col12 center" style="border-bottom: #CCCCCC; border-bottom-style: Solid; border-bottom-width: 2px">
                                        <asp:CheckBox ID="chkAllEmpSelect" runat="server" AutoPostBack="True" Text="Select All"
                                            OnCheckedChanged="chkAllEmpSelect_CheckedChanged"
                                            meta:resourcekey="chkAllEmpSelectResource1"  />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col12">
                                        <asp:CheckBoxList ID="cblEmpSelect" runat="server" Width="95%" Font-Size="11" AutoPostBack="True" OnSelectedIndexChanged="cblEmpSelect_SelectedIndexChanged"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>

                        <div class="col2">
                            <div class="row">
                                <div class="col12 center-block" style="padding-top:25%"></div>
                            </div>  
                        </div>

                        <div class="col5"  >
                            <div class="row">
                                <div class="col12 center-block">
                                    <asp:Label ID="lblRight" runat="server" CssClass="h4" Text="Selected Employees" meta:resourcekey="lblRightResource1"></asp:Label>

                                    <asp:CustomValidator ID="cvSelectEmployees" runat="server"
                                        ErrorMessage="Select Employees" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/message_exclamation.png' title='Select Employees!' /&gt;"
                                        OnServerValidate="SelectEmployees_ServerValidate"
                                        EnableClientScript="False"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvSelectEmployeesResource1">
                                    </asp:CustomValidator>
                                </div>
                            </div>

                            <asp:Panel ID="pnlRightGrid" runat="server" Height="200px" ScrollBars="Vertical"
                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px"
                                meta:resourcekey="pnlRightGridResource1" Enabled="False">

                                <div class="row">
                                    <div class="col12 center" style="border-bottom: #CCCCCC; border-bottom-style: Solid; border-bottom-width: 2px">
                                        <asp:CheckBox ID="chkAllEmpSelected" runat="server" AutoPostBack="True" Text="Clear All"
                                            OnCheckedChanged="chkAllEmpSelected_CheckedChanged" meta:resourcekey="chkAllEmpSelectedResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col12">
                                        <asp:CheckBoxList ID="cblEmpSelected" runat="server" Width="95%" Font-Size="11" AutoPostBack="True" OnSelectedIndexChanged="cblEmpSelected_SelectedIndexChanged"></asp:CheckBoxList>
                                    </div>
                                </div>



                            </asp:Panel>

                        </div>

                    </div>

                </div>
            </asp:Panel>


        </div>
    </ContentTemplate>
</asp:UpdatePanel>
