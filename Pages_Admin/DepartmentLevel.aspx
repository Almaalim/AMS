<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="DepartmentLevel.aspx.cs" Inherits="DepartmentLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
        function CreateName() {
            document.getElementById('<%=hdnNameEn.ClientID%>').value = '';
            document.getElementById('<%=hdnNameAr.ClientID%>').value = '';

            var itemCount = document.getElementById('<%=ddlCount.ClientID%>').value;

            var nameEn = '';
            var nameAr = '';
            for (var x = 0; x < itemCount; x++) {
                var TXTEN_ID = "ctl00_ContentPlaceHolder1_txtLevelEn_" + x;
                var TXTAR_ID = "ctl00_ContentPlaceHolder1_txtLevelAr_" + x;

                if (nameEn == '') { nameEn = document.getElementById(TXTEN_ID).value } else { nameEn = nameEn + ',' + document.getElementById(TXTEN_ID).value.toString().trim() }
                if (nameAr == '') { nameAr = document.getElementById(TXTAR_ID).value } else { nameAr = nameAr + ',' + document.getElementById(TXTAR_ID).value.toString().trim() }

                // alert(nameEn);
            }
            document.getElementById('<%=hdnNameEn.ClientID%>').value = nameEn;
            document.getElementById('<%=hdnNameAr.ClientID%>').value = nameAr;
        }    
   </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                    </div>
                </div>
            <div class="row">
                <div class="col8">
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                                        meta:resourcekey="btnModifyResource1">
                                    </asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"  
                                        OnClick="btnSave_Click" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                        ValidationGroup="Groups"  >
                                    </asp:LinkButton>
                                 
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                        meta:resourcekey="btnCancelResource1">
                                    </asp:LinkButton>
                                 </div>
                              <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                    
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                               
                                    <asp:HiddenField ID="hdnDepCount" runat="server" />
                                    <asp:HiddenField ID="hdnNameEn" runat="server" />
                                    <asp:HiddenField ID="hdnNameAr" runat="server" />
                             </div>
                </div>
            <div class="row">
                  <div class="col12">
                        <asp:Table ID="tblLevel" runat="server" Width="100%" CssClass ="TableLevel" >
                            <asp:TableRow>
                                <asp:TableCell>
                                    
                                    <asp:Label ID="lblCount" runat="server" Text="Level Count:"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="ddlCount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCount_SelectedIndexChanged"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>  
                        </asp:Table> 
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

