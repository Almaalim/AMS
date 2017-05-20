<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="OrderMenu.aspx.cs" Inherits="OrderMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td colspan="3" valign="top" align="center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <table border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td height="10"></td>
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
                                        <%--<table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="buttonBG" OnClick="btnModify_Click" Width="60px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonBG" OnClick="btnSave_Click" Width="60px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonBG" OnClick="btnCancel_Click" Width="60px" />
                                                </td>
                                            </tr>
                                        </table>--%>

                                           <table>
                         <tr>
                         
                           <td>
                           &nbsp;
                              <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton" OnClick="btnModify_Click" Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify">
                              </asp:LinkButton> 
                           </td>
                          
                           <td>
                              &nbsp;
                              <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton" OnClick="btnSave_Click" Width="70px" Height="18px" ValidationGroup="Groups" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save">
                              </asp:LinkButton>
                           </td>
                           <td>
                           &nbsp;
                              <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton" OnClick="btnCancel_Click" Width="70px" Height="18px" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel">
                              </asp:LinkButton>
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
                                <td align="right" valign="middle">
                                    <asp:Label ID="lblMenu" runat="server" Text="Menu :" ></asp:Label>
                                </td>
                                <td valign="middle" align="left">
                                    <asp:DropDownList ID="ddlMenu" runat="server" Width="180px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlMenu_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>


                            <tr>
                               
                                <td align="right" valign="middle">
                                </td>
                                <td valign="middle" align="left">
                                    <asp:Button ID="btnUP" runat="server" CssClass="buttonBG" onclick="btnUP_Click" Text="UP" Width="60px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDOWN" runat="server" CssClass="buttonBG" onclick="btnDOWN_Click" Text="DOWN" Width="60px" />
                                </td>
                            </tr>  


                            <tr>
                              
                                <td align="right" valign="middle">
                                </td>
                                <td valign="middle" align="left">
                                    <asp:ListBox ID="lstOrderMenu" runat="server" Height="200px" Width="180px"></asp:ListBox>
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
            <td style="border-style: solid;background-color: #DCE7E1;color:#6C8695;">
            
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:XmlDataSource ID="xmlDataSource1" TransformFile="~/XSL/TransformXSLT.xsl" XPath="MenuItems/MenuItem"
                            runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                        
                        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="xmlDataSource1" LineImagesFolder="~/images/TreeLineImages"
                            ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" 
                            OnDataBound="TreeView1_DataBound" Font-Size="Small">
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

