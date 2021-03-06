﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSelectedGroup.ascx.cs" Inherits="EmployeeSelectedGroup" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div runat="server" id="MainTable">
            <asp:Panel ID="pnlEmloyeeSelected" runat="server" meta:resourcekey="pnlEmloyeeSelectedResource1">
                <div class="row">
                    <div class="col6">
                        <div class="GreySetion">
                            <div class="row">
                                <div class="col12">
                                    <asp:Label ID="lblLeft" runat="server" Text="Select Employees" CssClass="h4" meta:resourcekey="lblLeftResource1"></asp:Label>                           
                                </div>
                            </div>
                            <div class="row">
                                <div class="col3">
                                    <asp:Label ID="lblDepartment" runat="server" Text="Department :" meta:resourcekey="lblDepartmentResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="200%"
                                        AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                        meta:resourcekey="ddlDepartmentResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <asp:Panel ID="pnlLeftGrid" runat="server" Height="280px"
                                        meta:resourcekey="pnlLeftGridResource1">
                                        <asp:GridView ID="grdLeftGrid" runat="server"
                                            SelectedIndex="1"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="EmpID" TabIndex="100" Width="100%" GridLines="Horizontal"
                                            EnableModelValidation="True" meta:resourcekey="grdLeftGridResource1">
                                            <Columns>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkLeftAll" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkLeftAll_CheckedChanged"
                                                            meta:resourcekey="chkLeftAllResource1" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkLeftSelect" runat="server"
                                                            meta:resourcekey="chkLeftSelectResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                                    ReadOnly="True" meta:resourcekey="BoundFieldResource1">
                                                    <HeaderStyle
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Name" DataField="EmpName" SortExpression="EmpName"
                                                    ReadOnly="True" meta:resourcekey="BoundFieldResource2">
                                                    <HeaderStyle
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:BoundField>
                                            </Columns>

                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col1 tableDiv">
                        <div class="tableCell"> 
                            <asp:ImageButton ID="btnSelectEmp" runat="server" OnClick="btnSelectEmp_Click" 
                                ImageUrl="~/images/Control_Images/next.png" 
                                meta:resourcekey="btnSelectEmpResource1" />
                            <br />
                            <asp:ImageButton ID="btnDeSelectEmp" runat="server" 
                                OnClick="btnDeSelectEmp_Click" ImageUrl="~/images/Control_Images/back.png" 
                                meta:resourcekey="btnDeSelectEmpResource1" />
                        </div>
                    </div>
                    <div class="col5">
                        <div class="GreySetion">
                           <div class="row">
                                <div class="col12">
                                    <asp:Label ID="lblRight" runat="server" Text="Selected Employees"  CssClass="h4" meta:resourcekey="lblRightResource1"></asp:Label>
                                       
                                    <asp:CustomValidator ID="cvSelectEmployees" runat="server"
                                        ErrorMessage="Select Employees"
                                        Text="&lt;img src='images/message_exclamation.png' title='Select Employees!' /&gt;"
                                        OnServerValidate="SelectEmployees_ServerValidate"
                                        EnableClientScript="False"
                                        ControlToValidate="txtCustomValidator"
                                        meta:resourcekey="cvSelectEmployeesResource1"></asp:CustomValidator>
                                 
                                    <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col3">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblGroup" runat="server" Text="Group Name :" meta:resourcekey="lblGroupResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlGrpName" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlGrpName_SelectedIndexChanged" Width="200px"
                                        meta:resourcekey="ddlGrpNameResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                           <div class="row">
                                <div class="col12">
                                    <asp:Panel ID="pnlRightGrid" runat="server" Height="280px" 
                                        meta:resourcekey="pnlRightGridResource1">
                                        <asp:GridView ID="grdRightGrid" runat="server"
                                            SelectedIndex="1" 
                                            AutoGenerateColumns="False"  
                                            DataKeyNames="EmpID" TabIndex="100" Width="100%" GridLines="Horizontal"
                                            EnableModelValidation="True" meta:resourcekey="grdRightGridResource1">
                                            <Columns>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkRightAll" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkRightAll_CheckedChanged"
                                                            meta:resourcekey="chkRightAllResource1" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRightSelect" runat="server"
                                                            meta:resourcekey="chkRightSelectResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                                    ReadOnly="True" meta:resourcekey="BoundFieldResource3">
                                                    <HeaderStyle
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Name" DataField="EmpName" SortExpression="EmpName"
                                                    ReadOnly="True" meta:resourcekey="BoundFieldResource4">
                                                    <HeaderStyle
                                                        VerticalAlign="Middle" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:BoundField>
                                            </Columns>
                                     
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>   
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
