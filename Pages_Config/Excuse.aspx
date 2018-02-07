<%@ Page Title="Excuse Type" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" CodeFile="Excuse.aspx.cs" Inherits="Excuse" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

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
                        <asp:ListItem Text="Excuse Name (Ar)" Value="ExcNameAr" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Excuse Name (En)" Value="ExcNameEn" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Excuse Initial (En)" Value="ExcInitialEn" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Excuse Initial (Ar)" Value="ExcInitialAr" meta:resourcekey="ListItemResource5"></asp:ListItem>
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
                        AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" DataKeyNames="ExcID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnSorting="grdData_Sorting" OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender" EnableModelValidation="True"
                        meta:resourcekey="grdAppuserResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="ExcNameAr" SortExpression="ExcNameAr" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ExcID" HeaderText="Excuse ID" SortExpression="ExcID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="ExcNameEn" HeaderText="Name (En)" SortExpression="ExcNameEn" InsertVisible="False"
                                ReadOnly="True" meta:resourcekey="BoundFieldResource3">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Initial (Ar)" DataField="ExcInitialAr" SortExpression="ExcInitialAr" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="ExcInitialEn" HeaderText="Initial (En)" SortExpression="ExcInitialEn" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField HeaderText="Description" DataField="ExcDesc" SortExpression="ExcDesc" meta:resourcekey="BoundFieldResource6" />
                            <asp:TemplateField HeaderText="Status" SortExpression="ExcStatus" meta:resourcekey="BoundFieldResource7">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("ExcStatus"))%>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("ExcID") %>'
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
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
                        <span id="spnNameAr" runat="server" class="RequiredField" visible="False">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar) :"
                            meta:resourcekey="lblExcNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtArExcuseNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" Text="&lt;img src='../images/Exclamation.gif' title='Name (Ar) is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvArExcNameResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" class="RequiredField" visible="False">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :"
                            meta:resourcekey="lblExcNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEnExcuseNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" Text="&lt;img src='../images/Exclamation.gif' title='Name (En) is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvEnExcNameResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="Label1" runat="server" Text="Initial (Ar) :" meta:resourcekey="Label1Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtExcuseInitialAr" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" MaxLength="2" meta:resourcekey="txtExcuseInitialArResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblExcuseInitial" runat="server" Text="Initial (En) :" meta:resourcekey="lblExcuseInitialResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtExcuseInitialEn" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" MaxLength="2" meta:resourcekey="txtExcuseInitialEnResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblMaxMinPerMonth" runat="server" Text="Max Minutes Per Month :"
                            meta:resourcekey="lblMaxMinPerMonthResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TextTime ID="txtMaxHours" runat="server" FormatTime="hhmm" meta:resourcekey="txtMaxHoursResource1"
                            CssClass="TimeCss" />
                        <div class="flyoutWrap">
                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                            <asp:ImageButton ID="lnkShow1" runat="server" meta:resourcekey="lnkButtonResouce"
                                OnClientClick="return false;" ImageUrl="~/../images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                            <div id="pnlInfo1" class="flyOutDiv">
                                <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                    CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                <p>
                                    <br />
                                    <asp:Label ID="lblHint1" runat="server" Text="Max Minutes per month allowable in this Excuse"
                                        meta:resourcekey="lblMaxResource"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblPercentAllowable" runat="server" Text="Percent Allowable Per Shift :"
                            meta:resourcekey="lblPercentAllowableResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtPercentAllowable" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" MaxLength="3" onkeypress="return OnlyNumber(event);"
                            meta:resourcekey="txtPercentAllowableResource1"></asp:TextBox>

                        &nbsp;
                        <asp:Label ID="Label2" runat="server" Text=" % " meta:resourcekey="Label2Resource1" CssClass="LeftOverlay3"></asp:Label>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" meta:resourcekey="lnkShow2Resouce" />
                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHintPercent" runat="server" Text="This option determines, how much the percentage allowed for the employee to Request for Excuse" meta:resourcekey="lblHintPercentResource"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblExcuseDesc" runat="server" Text="Description :" meta:resourcekey="lblExcuseDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtExcuseDesc" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" TextMode="MultiLine" meta:resourcekey="txtExcuseDescResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="ckbIsPaid" runat="server" Enabled="False" Text="Paid" meta:resourcekey="ChkExcuseStatusResource11" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2"></div>
                    <div class="col4">

                        <asp:CheckBox ID="ChkExcuseStatus" runat="server" Enabled="False" Text="Active" meta:resourcekey="ChkExcuseStatusResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
