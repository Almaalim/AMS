<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ADSetting.aspx.cs" Inherits="ADSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
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
                                    &nbsp;
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                                </div>
                </div>
            <div class="row">
                  <div class="col12">
                                    <asp:CheckBox ID="chkADEnabled" runat="server" Text="Enabled Active Directory" />
                              </div>
                </div>
            <div class="row">
                  <div class="col2">
                                    <asp:Label ID="lblADDomainName" runat="server" Text="Active Directory Domain :"></asp:Label>
                                    </div>
                              <div class="col4">
                                    <asp:TextBox ID="txtADDomainName" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
                                   </div>
                </div>
            <div class="row">
                              <div class="col2">
                                    <asp:Label ID="lblADJoinMethod" runat="server" Text="Active Directory Join Method :"></asp:Label>
                                </div>
                              <div class="col4">
                                     <asp:DropDownList ID="ddlADJoinMethod" runat="server"
                                          >
                                        <asp:ListItem Value="Email" Text="Email" Selected="True"></asp:ListItem>
                                        <%--<asp:ListItem Value="ID"    Text="ID Number"></asp:ListItem>--%>
                                        <asp:ListItem Value="EmpID" Text="Employee ID"></asp:ListItem>
                                     </asp:DropDownList>
                               </div>
                </div>
            <div class="row">
                              <div class="col2">
                                    <asp:Label ID="lblADColName" runat="server" Text="Active Directory Column Name :"></asp:Label>
                                 </div>
                              <div class="col4">
                                    <asp:TextBox ID="txtADColName" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
                               </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

