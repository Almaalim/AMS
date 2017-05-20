<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManagersSelected.ascx.cs" Inherits="ManagersSelected" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <div runat="server" id="MainTable" >
           <div class="row">
                            <div class="col12">
                                <asp:Label ID="lblMgrs" runat="server" Text=" Selected Managers :"  CssClass="h4"
                                    meta:resourcekey="lblMgrsResource1"></asp:Label>
                                
                           </div></div>
               <div class="row">
                                <div class="col2">
                                <asp:Label ID="lblMgr" runat="server" Text="Manager :" 
                                    meta:resourcekey="lblMgrResource1"></asp:Label>
                          </div>
                                <div class="col4">
                                <asp:DropDownList ID="ddlMgr" runat="server" 
                                    meta:resourcekey="ddlMgrResource1">
                                </asp:DropDownList>
                            </div>
                                <div class="col2">
                                <asp:Label ID="lblLevel" runat="server" Text="Level :" 
                                    meta:resourcekey="lblLevelResource1"></asp:Label>
                            </div>
                                <div class="col4">
                                <asp:DropDownList ID="ddlLevel" runat="server" 
                                    meta:resourcekey="ddlLevelResource1">
                                </asp:DropDownList>
                           </div></div>
               <div class="row">
                    <div class="col2">
                        </div>
                                <div class="col8">
                                
                                <asp:ImageButton ID="btnAdd" runat="server"  
                                    ImageUrl="~/images/Wizard_Image/add.png" OnClick="btnAdd_Click" ToolTip="Add" 
                                    meta:resourcekey="btnAddResource1" />
                                &nbsp;
                                <asp:ImageButton ID="btnUp" runat="server" 
                                    ImageUrl="~/images/Wizard_Image/arrow_up.png" OnClick="btnUp_Click" 
                                    ToolTip="Up" meta:resourcekey="btnUpResource1" />
                                &nbsp;
                                <asp:ImageButton ID="btnDown" runat="server" 
                                    ImageUrl="~/images/Wizard_Image/arrow_down.png" OnClick="btnDown_Click" 
                                    ToolTip="Down" meta:resourcekey="btnDownResource1" />
                                &nbsp;
                                <asp:ImageButton ID="btnRemoveMgr" runat="server" 
                                    ImageUrl="~/images/Wizard_Image/Delete_User.png" OnClick="btnRemoveMgr_Click"  
                                    ToolTip="Remove Manager" meta:resourcekey="btnRemoveMgrResource1" />
                                &nbsp;
                                <asp:ImageButton ID="btnRemoveLevel" runat="server" 
                                    ImageUrl="~/images/Wizard_Image/delete.png" OnClick="btnRemoveLevel_Click"  
                                    ToolTip="Remove Level" meta:resourcekey="btnRemoveLevelResource1" />
                            </div></div>
               <div class="row">
                                <div class="col2">
                                    </div>
                                <div class="col4">
                                <asp:ListBox ID="lstMgr" runat="server" Enabled="False" Height="200px" meta:resourcekey="lstMgrResource1"></asp:ListBox>
                           
                                <asp:CustomValidator ID="cvlstMgr" runat="server"  CssClass="CustomValidator"
                                    ControlToValidate="txtCustomValid" EnableClientScript="False" OnServerValidate="lstMgr_ServerValidate" 
                                    Text="&lt;img src='images/Exclamation.gif' title='Select Managers!' /&gt;" 
                                    meta:resourcekey="cvlstMgrResource1"></asp:CustomValidator>
                                <asp:TextBox ID="txtCustomValid" runat="server" Text="02120" Visible="False" 
                                    Width="10px" meta:resourcekey="txtCustomValidResource1"></asp:TextBox>
                           </div>
                   </div>
            
        </div>
   </ContentTemplate>
</asp:UpdatePanel>

