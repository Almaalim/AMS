<%@ Page Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true"
    CodeFile="MenuBuilder.aspx.cs" Inherits="MenuBuilder" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td colspan="3" valign="top" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <table border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                            <tr>
                                <td class="td2Allalign" colspan="3">
                                    <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                                        EnableClientScript="False" ValidationGroup="ShowMsg"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="middle">
                                    <center>
                                        <table>
                                            <tr>
                                                <td valign="bottom">
                                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton" OnClick="btnAdd_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton" OnClick="btnModify_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="GenButton" OnClick="btnDelete_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_delete.png&quot; /&gt; Delete">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" OnClick="btnSave_Click"
                                                        Width="70px" Height="18px" ValidationGroup="Groups" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" OnClick="btnCancel_Click"
                                                        Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel">
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnUserName" runat="server" />
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
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" height="20">
                                    <asp:Label ID="lblMenuItemText" runat="server" Text="Menu Item Text (En) :"></asp:Label>
                                </td>
                                <td valign="middle" align="left" height="20">
                                    <asp:TextBox ID="txtMenuItemText" runat="server" Width="240px" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle" height="20">
                                    <asp:Label ID="lblMenuItemTextArabic" runat="server" Text="Menu Item Text(Ar) :"></asp:Label>
                                </td>
                                <td valign="middle" align="left" height="20">
                                    <asp:TextBox ID="txtMenuItemTextArabic" runat="server" Width="240px" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblMenuItemToolTip" runat="server" Text="Menu Item Tooltip (En) :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtMenuItemToolTip" runat="server" Width="240px" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="Label2" runat="server" Text="Menu Item Tooltip(Ar) :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtMenuItemToolTipArabic" runat="server" Width="240px" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblMenuItemParent" runat="server" Text="Menu Item Parent :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:DropDownList ID="ddlMenuItemParent" runat="server" Width="245px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlMenuItemParent_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="Label1" runat="server" Text="Menu Type :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:DropDownList ID="ddlMnuType" runat="server" Width="245px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblServer" runat="server" Text="Menu Item Server :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtMenuItemServer" runat="server" Width="240px" AutoCompleteType="Disabled">./</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblMenuItemURL" runat="server" Text="Menu Item Webform :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:TextBox ID="txtMenuItemURL" runat="server" Width="240px" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="Label4" runat="server" Text="Group Report :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:DropDownList ID="ddlRgpID" runat="server" Width="245px" 
                                        AutoPostBack="True" onselectedindexchanged="ddlRgpID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="middle">
                                </td>
                                <td valign="middle" align="left">
                                    <asp:CheckBox ID="chkVisible" runat="server" Text="Visible" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                   <asp:Label ID="lblCommands" runat="server" Text="Commands :"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <table>
                                        <tr>
                                           <td align="left">
                                               <asp:Label ID="lblPageCommands" runat="server" Text="Page :"></asp:Label>
                                           </td>
                                           <td align="left">
                                               <asp:Label ID="lblReportCommands" runat="server" Text="Report :"></asp:Label>
                                           </td>
                                        </tr>
                                        <tr>
                                           <td align="left" valign="top">
                                               <asp:CheckBoxList ID="cblPageCommands" runat="server">
                                                    <asp:ListItem Text="Insert-إضافة" Value="Insert"></asp:ListItem>
                                                    <asp:ListItem Text="Update-تعديل" Value="Update"></asp:ListItem>
                                                    <asp:ListItem Text="Delete-حذف" Value="Delete"></asp:ListItem>
                                                    <asp:ListItem Text="Import-استيراد" Value="Import"></asp:ListItem>
                                                    <asp:ListItem Text="Export-تصدير" Value="Export"></asp:ListItem>
                                                    <asp:ListItem Text="Set Acting User-تعيين نائب مستخدم" Value="Set Acting User"></asp:ListItem>
                                                    <asp:ListItem Text="Reset Password-إعادة تعيين كلمة السر" 
                                                        Value="Reset Password"></asp:ListItem>
                                                    <asp:ListItem Text="Set Vacation-تعيين الإجازة" Value="Set Vacation"></asp:ListItem>
                                                </asp:CheckBoxList>
                                           </td>
                                           <td align="left" valign="top">
                                               <asp:CheckBoxList ID="cblReportCommands" runat="server" Enabled="false">
                                                    <asp:ListItem Text="Import-استيراد" Value="Import"></asp:ListItem>
                                                    <asp:ListItem Text="Export-تصدير" Value="Export"></asp:ListItem>
                                                    <asp:ListItem Text="Edit-تعديل" Value="Edit"></asp:ListItem>
                                                    <asp:ListItem Text="Set Default-تعيين كافتراضي" Value="Set Default"></asp:ListItem>
                                                    <asp:ListItem Text="Print-طباعة" Value="Print"></asp:ListItem>
                                                    <asp:ListItem Text="Export To Other Formats-التصدير لصيغ أخرى" 
                                                        Value="Export To Other Formats"></asp:ListItem>
                                               </asp:CheckBoxList>
                                           </td>
                                        </tr>
                                    </table>
                                    
                                    
                                    <asp:HiddenField ID="hdnMenuID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Label ID="Label3" runat="server" Text="MenuItem Type :" Visible="False"></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:DropDownList ID="ddlItemtype" runat="server" Width="240px" Visible="False">
                                        <asp:ListItem Text="Menu" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Command"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td valign="top" align="left">
                <table>
                    <tr>
                        <td height="25px">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-style: solid; background-color: #DCE7E1; color: #6C8695;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:XmlDataSource ID="xmlDataSource1" TransformFile="~/XSL/TransformXSLT.xsl" XPath="MenuItems/MenuItem"
                                        runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                                    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="xmlDataSource1" LineImagesFolder="~/images/TreeLineImages"
                                        ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" OnDataBound="TreeView1_DataBound"
                                        Font-Size="Small">
                                        <DataBindings>
                                            <asp:TreeNodeBinding DataMember="MenuItem" NavigateUrlField="NavigateUrl" TextField="Text"
                                                ToolTipField="ToolTip" ValueField="Value" />
                                        </DataBindings>
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
