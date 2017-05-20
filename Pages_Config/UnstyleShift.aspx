<%@ Page Title="Unstyle Shift" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="UnstyleShift.aspx.cs" Inherits="UnstyleShift" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="MainTable">
                <div class="row">
                    <div class="col1">
                                <asp:Label ID="lblMonth" runat="server" Text="Month:"  
                                    meta:resourcekey="lblMonthResource1"></asp:Label>
                            </div>
                    <div class="col2">
                                <asp:DropDownList ID="ddlMonth" runat="server"   
                                    meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                              </div>
                    <div class="col1">
                                <asp:Label ID="lblYear" runat="server" Text="Year:"  
                                    meta:resourcekey="lblYearResource1"></asp:Label>
                                </div>
                    <div class="col2">
                                <asp:DropDownList ID="ddlYear" runat="server"   
                                    meta:resourcekey="ddlYearResource1"></asp:DropDownList>
                             </div>
                </div>
               
            <div class="row">
                <div class="col12">
                                    
                        <asp:ValidationSummary ID="vsShowMsg" runat="server"  CssClass="MsgSuccess" 
                            EnableClientScript="False" ValidationGroup="ShowMsg"/>
                     </div>
                </div>
               
            <div class="row">
                <div class="col12">
                                    
                        <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave" EnableClientScript="False"
                            CssClass="MsgValidation" ShowSummary="False"/>
                   </div>
                 </div>
            <div class="row">
                <div class="col8">
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                                          Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                                        meta:resourcekey="btnAddResource1"></asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                                          ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                                          Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                                 </div>
                <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                  
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                                    
                                    <asp:CustomValidator id="cvValid" runat="server" Display="None"  CssClass="CustomValidator"
                                        ValidationGroup="vgSave" OnServerValidate="Valid_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                               </div>
            </div>

            <div class="row">
                <div class="col12">
                                    <asp:Panel ID="pnlGrdAddData" runat="server" CssClass="GridDiv" >
                                        <asp:GridView ID="grdAddData" runat="server" AutoGenerateColumns = "false" 
                                            onrowdatabound="grdAddData_RowDataBound" 
                                            onrowcommand="grdAddData_RowCommand" ShowFooter="True">
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

