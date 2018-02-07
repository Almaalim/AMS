<%@ Page Title="Vacation" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="VacationMaster.aspx.cs" Inherits="VacationMaster" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search by:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Vacation Name (Ar)" Value="VtpNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Vacation Name (En)" Value="VtpNameEn" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Vacation Initial (Ar)" Value="VtpInitialAr" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Vacation Initial (En)" Value="VtpInitialEn" meta:resourcekey="ListItemResource5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowSorting="false" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" DataKeyNames="VtpID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True" meta:resourcekey="grdAppuserResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField DataField="VtpNameAr" HeaderText="Name Ar" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VtpID" HeaderText="Vac ID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="VtpNameEn" HeaderText="Name En" InsertVisible="False"
                                ReadOnly="True" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="VtpInitialEn" HeaderText="Initial (En)" meta:resourceKey="BoundFieldResource4" />
                            <asp:BoundField DataField="VtpInitialAr" HeaderText="Initial (Ar)" meta:resourceKey="BoundFieldResource456" />
                            
                             <asp:TemplateField HeaderText="Status" SortExpression="VtpStatus" meta:resourcekey="BoundFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("VtpStatus"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("VtpID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

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

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar) :"
                            meta:resourcekey="lblVtpNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False"
                            meta:resourcekey="txtArVacationNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Vacation Type name (AR) is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="VtpValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvVacNameArResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :"
                            meta:resourcekey="lblVtpNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False"
                            meta:resourcekey="txtEnVacationTypeNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Vacation Type name (En) is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="VtpValidate_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvVacNameEnResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnMaxDays" runat="server" class="RequiredField" visible="False">*</span>
                        <asp:Label ID="Label2" runat="server" Text="Max Days :" meta:resourcekey="Label2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtVacationMaxDays" runat="server" AutoCompleteType="Disabled" Enabled="False" MaxLength="3" meta:resourcekey="txtVacationMaxDaysResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvVacMaxDays" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" meta:resourcekey="cvVacNameEnResource1" OnServerValidate="VtpValidate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;" ValidationGroup="vgSave" CssClass="CustomValidator"></asp:CustomValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtVacationMaxDays"
                            EnableClientScript="False" ErrorMessage="Please Enter Only Numbers!" ValidationExpression="^\d+$"
                            ValidationGroup="vgSave" Text="&lt;img src='../images/message_exclamation.png' title='Please Enter Only Numbers!' /&gt;"
                            meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblVacationDesc" runat="server" Text="Description :" meta:resourcekey="lblVacationDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtVacationTypeDesc" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" Enabled="False" meta:resourcekey="txtVacationTypeDescResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="Label3" runat="server" Text="Initial (Ar) :" meta:resourcekey="Label3Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtVacationInitialAr" runat="server" AutoCompleteType="Disabled" Enabled="False" MaxLength="2" meta:resourcekey="txtVacationInitialArResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblVacationInitial" runat="server" Text="Initial (En) :" meta:resourcekey="lblVacationInitialResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtVacationInitialEn" runat="server" AutoCompleteType="Disabled" Enabled="False" MaxLength="2" meta:resourcekey="txtVacationInitialEnResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkVtpStatus" runat="server" Text="Active" Enabled="False" meta:resourcekey="chkVtpStatusResource1" />
                    </div>
                    <div class="col2"></div>
                    <div class="col4">
                        <asp:CheckBox ID="chkVtpIsReset" runat="server" Text="Reset Vacation" meta:resourcekey="chkResetResource1" />
                        &nbsp;
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1">
                                    </ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1">
                        </ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" meta:resourcekey="lnkButtonResouce"
                            OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="You can initialize vacation here" meta:resourcekey="lblResetResource"></asp:Label>
                            </p>
                        </div>
                        <%--<div id="pnlInfoAr" class="flyOutDiv">
                                        <asp:LinkButton ID="lnkCloseAr" runat="server" Text="X" OnClientClick="return false;"
                                            CssClass= "flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text="تستطيع تصفير الإجازة من هنا"></asp:Label>
                                        </p>
                                    </div>--%>
                                </td>
                                <%--<td>
                                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                        font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                                font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                        </div>
                                        <p>
                                            <asp:Label ID="Label5" runat="server" Text="You can initialize vacation here" meta:resourcekey="lblResetResource"></asp:Label>
                                        </p>
                                        <div id="divEn" runat="server" visible="False">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="you can initialize vacation here"></asp:Label>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divAr" runat="server" visible="False">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="إعادة ضبط الإجازة"></asp:Label>
                                                </td>
                                            </tr>
                                        </div>
                                    </div>
                                </td>--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkIsPaid" runat="server" Text="Paid" meta:resourcekey="chkIsPaidResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <div id="divMedicalReport" runat="server" visible="false">
                            <tr>
                                <td class="td1Allalign"></td>
                                <td class="td2Allalign">
                                    <asp:CheckBox ID="chkVtpIsMedicalReport" runat="server" Text="Needs a medical report" meta:resourcekey="chkVtpIsMedicalReportResource1" />
                                </td>
                            </tr>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col12">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
