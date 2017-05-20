<%@ Page Title="Branch" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Branches.aspx.cs" Inherits="Branches" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
                <div class="col1">
                                    <asp:Label ID="lblFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                                     </div>
                <div class="col2">
                                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                                        <asp:ListItem Text="Branch Name (Ar)" Value="BrcNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Value="BrcNameEn" meta:resourcekey="ListItemResource3">Branch Name (En)</asp:ListItem>
                                        <asp:ListItem Text="Branch City" Value="BrcCity" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    </asp:DropDownList>
                                  </div>
                <div class="col2">
                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>
                                    &nbsp;
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                                        meta:resourcekey="btnFilterResource1" />
                                </div>
            </div>

            <div class="row">
                <div class="col12">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" AllowSorting="True"
                                                BorderWidth="0px" GridLines="None" DataKeyNames="BrcID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                                OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"  
                                                EnableModelValidation="True" meta:resourcekey="grdAppuserResource1">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True"   />
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                                    LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                                    NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Name (Ar)" DataField="BrcNameAr"
                                                        InsertVisible="False" ReadOnly="True" 
                                                        meta:resourcekey="BoundFieldResource1">
                                                        <HeaderStyle CssClass="first" />
                                                        <ItemStyle CssClass="first" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BrcID" HeaderText="Branch ID" 
                                                        meta:resourcekey="BoundFieldResource2" />
                                                    <asp:BoundField DataField="BrcNameEn" HeaderText="Name (En)"
                                                        meta:resourcekey="BoundFieldResource3" />
                                                    <asp:BoundField HeaderText="Branch City" DataField="BrcCity"
                                                        meta:resourcekey="BoundFieldResource4" />
                                                    <asp:BoundField HeaderText="Branch Tell No" DataField="BrcTelNo"
                                                        Visible="False" meta:resourcekey="BoundFieldResource5" />
                                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("BrcID") %>'
                                                                runat="server" ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1" />
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
                        <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                            EnableClientScript="False" CssClass="MsgValidation" 
                            meta:resourcekey="vsumAllResource1" />
                 </div>
            </div>
            <div class="row">
                <div class="col8">
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                                        meta:resourcekey="btnAddResource1"></asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>
                               
                                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" CssClass="GenButton  glyphicon glyphicon-floppy-disk"
                                        OnClick="btnSave_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>
                                
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton  glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                                 </div>
                <div class="col4">
                                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                                   
                                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator" 
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                               </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                                    <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                                    <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar) :" Font-Size="10pt"
                                        meta:resourcekey="lblBranchArNameResource1"></asp:Label>
                               </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtArBranchNameResource1"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                        ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtValid" meta:resourcekey="cvArBranchNameResource1"></asp:CustomValidator>
                               </div>
                    <div class="col2">
                                    <asp:Label ID="lblBranchAddress1" runat="server" Text="Address :" Font-Size="10pt"
                                        meta:resourcekey="lblBranchAddress1Resource1"></asp:Label>
                                </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchAddress" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchAddressResource1"></asp:TextBox>
                                </div>
                </div>

                <div class="row">
                    <div class="col2">
                                    <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                                    <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :" Font-Size="10pt"
                                        meta:resourcekey="lblBranchEnNameResource1"></asp:Label>
                               </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False" Style="margin-bottom: 0px" 
                                        meta:resourcekey="txtEnBranchNameResource1"></asp:TextBox>
                                    <asp:CustomValidator ID="cvNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                        ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtValid" meta:resourcekey="cvEnBranchNameResource1"></asp:CustomValidator>
                                </div>
                    <div class="col2">
                                    <asp:Label ID="lblBranchTell" runat="server" Text="Telephone :" Font-Size="10pt" meta:resourcekey="lblBranchTellResource1"></asp:Label>
                              </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchTell" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchTellResource1"></asp:TextBox>
                                 </div>
                </div>

                <div class="row" id="divBranchParent" runat="server" visible="False">
                    <div class="col2">
                            
                               
                                        <asp:Label ID="lblBranchParentName" runat="server" Font-Size="10pt" meta:resourcekey="lblBranchParentNameResource1">Parent Name :</asp:Label>
                                   </div>
                    <div class="col4">
                                        <asp:DropDownList ID="ddlBranchParentName" runat="server" Enabled="False"
                                            meta:resourcekey="ddlBranchParentNameResource1">
                                        </asp:DropDownList>
                                     </div>
                    
                            </div>
                           <div class="row">
                    <div class="col2">
                                    &nbsp;<asp:Label ID="lblDepMangerID0" runat="server" Text="Manager Name :" Font-Size="10pt"
                                        meta:resourcekey="lblDepMangerID0Resource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <asp:DropDownList ID="ddlBranchManagerID" runat="server" Enabled="False"
                                        meta:resourcekey="ddlBranchManagerIDResource1">
                                    </asp:DropDownList>
                               </div>

                    <div class="col2">
                                    <asp:Label ID="lblBranchEmail1" runat="server" Text="Email :" Font-Size="10pt" meta:resourcekey="lblBranchEmail1Resource1"></asp:Label>
                               </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchEmail" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchEmailResource1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBranchEmail"
                                        EnableClientScript="False" ErrorMessage="Enter correct Email" Text="&lt;img src='../images/message_exclamation.png' title='Enter correct Email!' /&gt;"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgSave"
                                        meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                               </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblBranchCountry1" runat="server" Text="Country :" Font-Size="10pt"
                                        meta:resourcekey="lblBranchCountry1Resource1"></asp:Label>
                                  </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchCountry" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchCountryResource1"></asp:TextBox>
                             </div>

                    <div class="col2">
                                    <asp:Label ID="lblBranchCity1" runat="server" Text="City :" Font-Size="10pt" meta:resourcekey="lblBranchCity1Resource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchCity" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchCityResource1"></asp:TextBox>
                                </div>
                </div>
                <div class="row">
                    <div class="col2">
                                    <asp:Label ID="lblBranchPoBox1" runat="server" Text="P.O.Box :" Font-Size="10pt"
                                        meta:resourcekey="lblBranchPoBox1Resource1"></asp:Label>
                                 </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtBranchPoBox" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtBranchPoBoxResource1"></asp:TextBox>
                                   </div>

                    <div class="col2">
                        </div>
                    <div class="col4">
                                    <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" 
                                        Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                               </div>
                </div>
                <div class="row">
                    <div class="col2">
                               
                                   
                                </div>
                    <div class="col4">
                         <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="Active" 
                                        meta:resourcekey="chkStatusResource1" Checked="True" Visible="False" />
                        </div>
                    </div>
                </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>