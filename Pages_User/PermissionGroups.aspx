<%@ Page Title="Permission Group" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="PermissionGroups.aspx.cs" Inherits="PermissionGroups" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
    <script language="javascript" type="text/javascript">
        //************************** Treeview Parent-Child check behaviour ****************************//  

        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                //            CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any child is not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">

                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" CellPadding="0"
                        BorderWidth="0px" GridLines="None" DataKeyNames="GrpNameEn" ShowFooter="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender" EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1" PageSize="10">

                        <%--<PagerSettings Mode="NextPreviousFirstLast"  />--%>
                        <Columns>
                            <asp:BoundField HeaderText="Group Name (Ar)" DataField="GrpNameAr" SortExpression="GrpNameAr"
                                meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Group ID" DataField="GrpID" SortExpression="GrpID"
                                meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="Group Name (En)" DataField="GrpNameEn" SortExpression="GrpNameEn"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField HeaderText="Group Description" DataField="GrpDescription" SortExpression="GrpDescription"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" Enabled="False" CommandArgument='<%# Eval("GrpID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="row" />
                    </asp:GridView>
                    <%--Custom Grid Pager--%>
                    <%--<table>
                        <tr class="GridPagerStyle">
                            <td colspan="4">
                                <table border="0">
                                    <tbody>
                                        <tr>
                                            <td><a href="#" class="FirstPager"></a></td>
                                            <td><a href="#" class="PreviousPager"></a></td>
                                            <td>
                                                <asp:Label ID="lblPage" runat="server" Text="1"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtPagerNumber" runat="server" CssClass="PagerNumbertxt"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lblPageof" runat="server" Text="Page of 10"></asp:Label></td>
                                            <td><a href="#" class="NextPager"></a></td>
                                            <td><a href="#" class="LastPager"></a></td>


                                        </tr>
                                    </tbody>
                                </table>

                            </td>
                        </tr>
                    </table>--%>
                    <%--Custom Grid Pager--%>
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
                    <asp:ValidationSummary runat="server" ID="vsSave" ValidationGroup="vgSave"
                        EnableClientScript="False" CssClass="MsgValidation"
                        meta:resourcekey="vsumAllResource1" />
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
                        ValidationGroup="vgSave"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>

                <div class="col4">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    &nbsp;
                                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">

                <div class="row">
                    <div class="col2">

                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblGroupType" runat="server" Text="Group Type :"
                            meta:resourcekey="lblGroupTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlGroupType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged" Enabled="False"
                            meta:resourcekey="ddlGroupTypeResource1">
                            <asp:ListItem Value="-Select group type-" Text="-Select group type-"
                                meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Value="Forms" Text="Forms" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Value="Reports" Text="Reports" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="If Forms option is selected,it allows to create permissions for forms,If Reports option is selected it allows to create permissions for Reports" meta:resourcekey="lblHintResource"></asp:Label>
                            </p>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="rfvGroupType" ControlToValidate="ddlGroupType"
                            InitialValue="-Select group type-" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Please select group!' />"
                            ValidationGroup="vgSave" meta:resourcekey="rfvGroupTypeResource1" CssClass="CustomValidator"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblGroupNameArb" runat="server" Text="Group Name(Ar) :"
                            meta:resourcekey="lblGroupNameArbResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtGroupNameArb" runat="server" Enabled="False"
                            AutoCompleteType="Disabled" meta:resourcekey="txtGroupNameArbResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvGroupNameAr" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid"
                            meta:resourcekey="cvArWorkTimeNameResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblGroupName" runat="server" Text="Group Name(En) :"
                            meta:resourcekey="lblGroupNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtGroupNameEn" runat="server" Enabled="False"
                            AutoCompleteType="Disabled" meta:resourcekey="txtGroupNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvGroupNameEn" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="NameValidate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid"
                            meta:resourcekey="cvEnWorkTimeNameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblGroupDescription" runat="server" Text="Group Description :"
                            meta:resourcekey="lblGroupDescriptionResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtGroupDescription" runat="server" Enabled="False"
                            AutoCompleteType="Disabled"
                            meta:resourcekey="txtGroupDescriptionResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="co2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtGrpID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col12">
                        <asp:Label ID="lblPermissions" runat="server" Text="Permissions" CssClass="h3"
                            meta:resourcekey="lblPermissionsResource1"></asp:Label>
                    </div>


                    <div id="divMenu" runat="server" class="col12">
                        <asp:TreeView ID="trvMenu" runat="server" DataSourceID="xmlDataSource1" LineImagesFolder="~/images/TreeLineImages"
                            Enabled="False" ShowLines="True" ShowCheckBoxes="All" OnDataBound="trvMenu_DataBound"
                            OnTreeNodeDataBound="trvMenu_TreeNodeDataBound"
                            meta:resourcekey="trvMenuResource1">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="MenuItem" NavigateUrlField="NavigateUrl" TextField="Text"
                                    ToolTipField="ToolTip" ValueField="Value"
                                    meta:resourcekey="TreeNodeBindingResource1" />
                            </DataBindings>
                        </asp:TreeView>
                    </div>
                    <div id="divReports" runat="server" visible="False" class="col12">
                        <asp:XmlDataSource ID="xmlReportSource" TransformFile="~/XSL/RepTransformXSLT.xsl" XPath="MenuItems/MenuItem"
                            runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                        <asp:TreeView ID="trvReport" runat="server" DataSourceID="xmlReportSource" LineImagesFolder="~/images/TreeLineImages"
                            Enabled="False" ShowLines="True" ShowCheckBoxes="Root" ForeColor="#CAD2D6"
                            OnDataBound="trvReport_DataBound"
                            OnTreeNodeDataBound="trvReport_TreeNodeDataBound"
                            meta:resourcekey="trvReportResource1">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ToolTipField="ToolTip"
                                    ValueField="Value" meta:resourcekey="TreeNodeBindingResource2" />
                            </DataBindings>
                        </asp:TreeView>

                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:HiddenField ID="hdnTreeID" runat="server" Value="ctl00_ContentPlaceHolder1_trvMenun0" />
                        <asp:XmlDataSource ID="xmlDataSource1" TransformFile="~/XSL/TransformXSLT.xsl" XPath="MenuItems/MenuItem"
                            runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                    </div>
                </div>


            </div>

            <div class="clearfix"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
