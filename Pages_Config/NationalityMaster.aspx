<%@ Page Title="Nationality" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="NationalityMaster.aspx.cs" Inherits="NationalityMaster" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                                                CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="NatID" ShowFooter="True"
                                                OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                                                OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender" 
                                                 EnableModelValidation="True" 
                                                meta:resourcekey="grdAppuserResource1">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                                    FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                                    NextPageText="Next" NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev"
                                                    PreviousPageImageUrl="~/images/prev.png" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Name (Ar)" DataField="NatNameAr" 
                                                        meta:resourcekey="BoundFieldResource1" />
                                                    <asp:BoundField DataField="NatID" HeaderText="Nationality ID" 
                                                        meta:resourcekey="BoundFieldResource2" />
                                                    <asp:BoundField DataField="NatNameEn" HeaderText="Name (En)" 
                                                        meta:resourcekey="BoundFieldResource3" />
                                                    <asp:BoundField HeaderText="Created By"
                                                        DataField="NatCreatedBy" Visible="False" 
                                                        meta:resourcekey="BoundFieldResource4">
                                                        <HeaderStyle CssClass="first" />
                                                        <ItemStyle CssClass="first" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Modified By" DataField="NatModifiedBy"
                                                        Visible="False" meta:resourcekey="BoundFieldResource5" />
                                                    <asp:TemplateField HeaderText="           " 
                                                        meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("NatID") %>'
                                                                runat="server" ImageUrl="../images/Button_Icons/button_delete.png" 
                                                                meta:resourcekey="imgbtnDeleteResource1"  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                             
                                            </asp:GridView>
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
                            CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsumAllResource1" />
                   </div>
            </div>

            <div class="row">
                <div class="col8">
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" 
                                       OnClick="btnAdd_Click"  
                                       Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add" 
                                       meta:resourcekey="btnAddResource1"></asp:LinkButton>
                             
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" 
                                       OnClick="btnModify_Click"   
                                       Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" 
                                       meta:resourcekey="btnModifyResource1"></asp:LinkButton> 
                               
                                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" 
                                       CssClass="GenButton  glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"  
                                       Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" 
                                       meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                             
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" 
                                        OnClick="btnCancel_Click" 
                                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" 
                                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                               </div>
                <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                    
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                             </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar)  :" 
                            meta:resourcekey="lblNatNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtNatArNameResource1"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid"
                                        EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Name (Ar) is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="cvArNatNameResource1"></asp:CustomValidator>
                               </div>
                    <div class="col2">
                                    <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                                    <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :" 
                                        meta:resourcekey="lblNatNameEnResource1"></asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtNatEnNameResource1"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid"
                                        EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Name (En) is required!' /&gt;" 
                                        ValidationGroup="vgSave" meta:resourcekey="cvEnNatNameResource1"></asp:CustomValidator>
                                </div>
                </div>

                <div class="row">
                    <div class="col2">
                                    <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                               </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
