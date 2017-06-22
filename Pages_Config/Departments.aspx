<%@ Page Title="Department" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" CodeFile="Departments.aspx.cs" Inherits="Departments"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <%--    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>--%>
    <%--script--%>
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--  <ajaxToolkit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkShow1">
                <Animations>
                    <OnClick Position="absolute">
                        <Sequence>
                            <EnableAction Enabled="false"></EnableAction>
                            <StyleAction AnimationTarget="pnlInfo1" Attribute="display" Value="block"/>                 
                            <Parallel AnimationTarget="pnlInfo1" Duration=".2" Fps="25">
                                <Move Horizontal='10' Vertical='10' />
                                <Resize Height="80%" Width="150%" />
                                <FadeIn />
                            </Parallel>
                            <Parallel AnimationTarget="pnlInfo1" Duration=".5"> 
                                  <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000"/> 
                                  <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" /> 
                            </Parallel> 
                            <EnableAction AnimationTarget="lnkClose1" Enabled="true" />
                        </Sequence>
                    </OnClick>
                </Animations>
            </ajaxToolkit:AnimationExtender>
            <ajaxToolkit:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="lnkClose1">
                <Animations>
                    <OnClick Position="absolute">
                        <Sequence AnimationTarget="pnlInfo1">
                            <EnableAction AnimationTarget="lnkClose1" Enabled="false" />
                            <Parallel AnimationTarget="pnlInfo1" Duration=".3" Fps="25">
                                <Move Horizontal="-10" Vertical="-10" />
                                <Scale ScaleFactor="0.05" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            <EnableAction AnimationTarget="lnkShow1" Enabled="true" />
                        </Sequence>
                    </OnClick>
                </Animations>
            </ajaxToolkit:AnimationExtender>--%>
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

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click" ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="GenButton glyphicon glyphicon-remove"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_delete.png&quot; /&gt; Delete"
                        OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1"></asp:LinkButton>

                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                </div>
                <div class="col4">
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col12">
                        <asp:Panel runat="server" ID="pnlMyDialogBox" Visible="False"
                            BorderStyle="Ridge" meta:resourcekey="pnlMyDialogBoxResource1">
                            <p style="margin-bottom: 5px; font-size: small; font-family: Verdana; color: black; height: 51px; width: 377px;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/QMark.bmp" meta:resourcekey="Image1Resource1" />
                                <strong>Do you want to keep the previous Manager&nbsp;&nbsp;&nbsp; </strong>
                            </p>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button runat="server" ID="btnYes" Text="Yes" Width="70px" Height="23px" OnClick="btnYes_Click"
                                                        meta:resourcekey="btnYesResource1" />
                            &nbsp;<asp:Button runat="server" ID="btnNo" Text="No" Width="70px" Height="23px"
                                OnClick="btnNo_Click" meta:resourcekey="btnNoResource1" />
                        </asp:Panel>
                    </div>
                </div>

                

                <div class="left">
                    <div class="row">
                        <div class="col3">
                            <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                            <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar) :"
                                meta:resourcekey="lblDepArNameResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled"
                                Enabled="False" meta:resourcekey="txtArDepNameResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;" CssClass="CustomValidator"
                                ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvArDepNameResource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col3">
                            <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                            <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :"
                                meta:resourcekey="lblDepEnNameResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                meta:resourcekey="txtEnDepartmentNameResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                                ControlToValidate="txtValid" meta:resourcekey="cvEnDepNameResource1"></asp:CustomValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col3">
                            <asp:Label ID="lblDepParent" runat="server" Text="Parent Name :" meta:resourcekey="lblDepParentResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:DropDownList ID="ddlDepParentName" runat="server" Enabled="False"
                                meta:resourcekey="ddlDepParentNameResource1">
                            </asp:DropDownList>

                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1">
                            </ajaxToolkit:AnimationExtender>
                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1">
                            </ajaxToolkit:AnimationExtender>
                            <asp:ImageButton ID="lnkShow1" runat="server" meta:resourcekey="lnkButtonResouce"
                                OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                            <div id="pnlInfo1" class="flyOutDiv" style="position: absolute">
                                <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                    CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                <p>
                                    <br />
                                    <asp:Label ID="lblHint1" runat="server" Text="You can choose the Parent department from here"
                                        meta:resourcekey="lblParentResource"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col3">
                            <span id="spnManagerName" runat="server" visible="False" class="RequiredField">*</span>
                            <asp:Label ID="lblDepMangerID" runat="server" Text="Manager Name :" meta:resourcekey="lblDepMangerIDResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:DropDownList ID="ddlDepManagerID" runat="server" Enabled="False"
                                meta:resourcekey="ddlDepManagerIDResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfDepMng" runat="server" ControlToValidate="ddlDepManagerID" CssClass="CustomValidator"
                                EnableClientScript="False" meta:resourcekey="rfvWtpIDResource1" Text="&lt;img src='../images/Exclamation.gif' title='Manager Name is required!' /&gt;"
                                ValidationGroup="vgSave"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col3">
                            <asp:Label ID="lblDepartmentDesc" runat="server" Text="Description :" meta:resourcekey="lblDepartmentDescResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:TextBox ID="txtDepartmentDesc" runat="server" AutoCompleteType="Disabled"
                                TextMode="MultiLine" Enabled="False" meta:resourcekey="txtDepartmentDescResource1"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col3">
                        </div>
                        <div class="col9">
                            <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="Active" Visible="false" Checked="true" meta:resourcekey="chkStatusResource1" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col3">
                            <asp:TextBox ID="txtLevel" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                meta:resourcekey="txtArDepNameResource1" Visible="False"></asp:TextBox>

                            <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                        </div>
                        <div class="col9"></div>
                    </div>
                </div>

                <div class="left">
                    <div class="row">
                        <div class="col3">
                            <asp:Label ID="lblBranchtID" runat="server" Text="Branch Name :" meta:resourcekey="lblBranchtIDResource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:DropDownList ID="ddlBrcParentName" runat="server" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlBrcParentName_SelectedIndexChanged"
                                meta:resourcekey="ddlBrcParentNameResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBrcParentName" runat="server" ControlToValidate="ddlBrcParentName" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Branch Name is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rfvBrcParentNameResource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col3">
                            <asp:Label ID="Label1" runat="server" Text="Departments :" meta:resourcekey="Label1Resource1"></asp:Label>
                        </div>
                        <div class="col9">
                            <asp:XmlDataSource ID="xdsDepartment" runat="server" TransformFile="~/XSL/DepTransformXSLT.xsl"
                                CacheExpirationPolicy="Sliding" XPath="MenuItems/MenuItem" EnableCaching="False" />
                            <asp:TreeView ID="trvDept" runat="server" LineImagesFolder="~/images/TreeLineImages" DataSourceID="xdsDepartment"
                                ShowLines="True" OnDataBound="trvDept_DataBound" OnTreeNodeDataBound="trvDept_TreeNodeDataBound"
                                Enabled="False" OnSelectedNodeChanged="trvDept_SelectedNodeChanged"
                                meta:resourcekey="trvDeptResource1">
                                <DataBindings>
                                    <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ValueField="Value" meta:resourcekey="TreeNodeBindingResource1" />
                                </DataBindings>
                                <SelectedNodeStyle ForeColor="Red" />
                            </asp:TreeView>
                        </div>
                    </div>
                </div>
                
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
