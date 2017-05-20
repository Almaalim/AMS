<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSelectedGroup.ascx.cs" Inherits="EmployeeSelectedGroup" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table runat="server" id="MainTable" cellpadding="0" cellspacing="0">
            <tr runat="server">
                <td runat="server">

                   <asp:Panel ID="pnlEmloyeeSelected" runat="server"  Width="760px" 
                        meta:resourcekey="pnlEmloyeeSelectedResource1">
                       <table>
                          <tr>
                             <td colspan="5">
                                <table style="width:100%">
                                   <tr >
                                      <td class="td1Allalign" style="width:50%">
                                         <span class="RequiredField">*</span>
                                         <asp:Label ID="lblGroup" runat="server" Text="Group Name :" meta:resourcekey="lblGroupResource1"></asp:Label>
                                      </td>
                                      <td class="td2Allalign" style="width:50%">
                                          <asp:DropDownList ID="ddlGrpName" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlGrpName_SelectedIndexChanged" Width="200px" 
                                                meta:resourcekey="ddlGrpNameResource1">
                                          </asp:DropDownList>
                                      </td>
                                   </tr>
                                </table>
                            </td>
                          </tr>


                          <tr>
                             <td width="10px"></td>
                                        
                             <td width="350px" style=" border-color:#CCCCCC; border-style:Solid; border-width:2px">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="subHeader">
                                        <td>
                                        <center>
                                            <asp:Label ID="lblLeft" runat="server" Text="Select Employees" 
                                                meta:resourcekey="lblLeftResource1"></asp:Label>
                                        </center>
                                        </td>
                                    </tr>
                                    <tr>  
                                        <td valign="middle">
                                        <table>
                                        <tr>
                                            <td class="td1Allalign">
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblDepartment" runat="server" Text="Department :" 
                                                    meta:resourcekey="lblDepartmentResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="173px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDepartment_SelectedIndexChanged" 
                                                    meta:resourcekey="ddlDepartmentResource1"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        </table>
                                        </td>     
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                        <asp:Panel ID="pnlLeftGrid" runat="server" Height="280px" ScrollBars ="Vertical" 
                                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" 
                                                meta:resourcekey="pnlLeftGridResource1">
                                            <asp:GridView ID="grdLeftGrid" runat="server"
                                                SelectedIndex="1" BorderStyle="Solid" CellPadding="2" CellSpacing="2" 
                                                AutoGenerateColumns="False" BorderWidth="2px"
                                                DataKeyNames="EmpID" TabIndex="100" Width="95%" GridLines="Horizontal" 
                                                EnableModelValidation="True" meta:resourcekey="grdLeftGridResource1">
                                                <Columns>
                                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkLeftAll" runat="server" AutoPostBack="True" 
                                                                OnCheckedChanged="chkLeftAll_CheckedChanged" 
                                                                meta:resourcekey="chkLeftAllResource1"/></HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkLeftSelect" runat="server" 
                                                                meta:resourcekey="chkLeftSelectResource1" /></ItemTemplate>
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
                                                <HeaderStyle Height="20px" />
                                                <RowStyle Height="15px" />
                                            </asp:GridView>                 
                                        </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                                        
                             <td width="25px">
                                 <table>
                                     <tr>
                                         <td>
                                             <center>
                                                 <asp:ImageButton ID="btnSelectEmp" runat="server" OnClick="btnSelectEmp_Click" 
                                                     ImageUrl="~/images/Control_Images/next.png" 
                                                     meta:resourcekey="btnSelectEmpResource1" />
                                             </center>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td>
                                             <center>
                                                 <asp:ImageButton ID="btnDeSelectEmp" runat="server" 
                                                     OnClick="btnDeSelectEmp_Click" ImageUrl="~/images/Control_Images/back.png" 
                                                     meta:resourcekey="btnDeSelectEmpResource1" />
                                             </center>
                                         </td>
                                     </tr>
                                 </table>
                             </td>
                                        
                             <td width="350px" style=" border-color:#CCCCCC; border-style:Solid; border-width:2px">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="subHeader">
                                        <td>
                                        <center>
                                            <asp:Label ID="lblRight" runat="server" Text="Selected Employees" 
                                                meta:resourcekey="lblRightResource1"></asp:Label>
                                        </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle">
                                        <table>
                                        <tr>
                                            <td>
                                                <asp:CustomValidator id="cvSelectEmployees" runat="server"
                                                    ErrorMessage="Select Employees"
                                                    Text="&lt;img src='images/message_exclamation.png' title='Select Employees!' /&gt;"
                                                    OnServerValidate="SelectEmployees_ServerValidate"
                                                    EnableClientScript="False" 
                                                    ControlToValidate="txtCustomValidator" 
                                                    meta:resourcekey="cvSelectEmployeesResource1"></asp:CustomValidator>         
                                            </td>
                                            <td style="height:22px">
                                               <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120" Visible="False" 
                                                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>  
                                            </td>
                                        </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                        <asp:Panel ID="pnlRightGrid" runat="server" Height="280px" ScrollBars ="Vertical"
                                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" 
                                                meta:resourcekey="pnlRightGridResource1">
                                            <asp:GridView ID="grdRightGrid" runat="server"
                                                SelectedIndex="1" BorderStyle="Solid" CellPadding="2" CellSpacing="2" 
                                                AutoGenerateColumns="False" BorderWidth="2px"
                                                DataKeyNames="EmpID" TabIndex="100" Width="95%" GridLines="Horizontal" 
                                                EnableModelValidation="True" meta:resourcekey="grdRightGridResource1">
                                                <Columns>
                                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource2" >
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkRightAll" runat="server" AutoPostBack="True" 
                                                                OnCheckedChanged="chkRightAll_CheckedChanged" 
                                                                meta:resourcekey="chkRightAllResource1"/></HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRightSelect" runat="server" 
                                                                meta:resourcekey="chkRightSelectResource1" /></ItemTemplate>
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
                                                <HeaderStyle Height="20px" Width="100%" />
                                                <RowStyle Height="15px" Width="100%" />
                                            </asp:GridView>                 
                                        </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                                        
                             <td width="10px"></td>
                          </tr>
                       </table>
                   </asp:Panel>

                </td>
            </tr>
        </table>
   </ContentTemplate>
</asp:UpdatePanel>