<%@ Page Title="Application Users" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" CodeFile="ApplicationUsers.aspx.cs" Inherits="ApplicationUsers"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--=============================================================================--%>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup1.js"></script>
    <script type="text/javascript">
        function OnCheckBoxCheckChanged(evt) {
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
                // CheckUncheckParents(src, src.checked);
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
                        return; //do not need to check parent if any(one or more) child not checked 
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
    <script type="text/javascript">
        var TREEVIEW_ID = "ctl00_ContentPlaceHolder1_TreeView1n0"; //the ID of the TreeView control
        //the constants used by GetNodeIndex()
        var LINK = 0;
        var CHECKBOX = 1;

        //this function is executed whenever user clicks on the node text
        function ToggleCheckBox(senderId) {


            var nodeIndex = GetNodeIndex(senderId, LINK);
            var checkBoxId = TREEVIEW_ID + "n" + nodeIndex + "CheckBox";
            var checkBox = document.getElementById(checkBoxId);
            checkBox.checked = !checkBox.checked;

            ToggleChildCheckBoxes(checkBox);
            ToggleParentCheckBox(checkBox);
        }

        //checkbox click event handler
        function checkBox_Click(eventElement) {
            ToggleChildCheckBoxes(eventElement.target);
            ToggleParentCheckBox(eventElement.target);
        }

        //returns the index of the clicked link or the checkbox
        function GetNodeIndex(elementId, elementType) {
            var nodeIndex;
            if (elementType == LINK) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "t").length);
            }
            else if (elementType == CHECKBOX) {
                nodeIndex = elementId.substring((TREEVIEW_ID + "n").length, elementId.indexOf("CheckBox"));
            }
            return nodeIndex;
        }

        //checks or unchecks the nested checkboxes
        function ToggleChildCheckBoxes(checkBox) {
            var postfix = "n";
            var childContainerId = TREEVIEW_ID + postfix + GetNodeIndex(checkBox.id, CHECKBOX) + "Nodes";
            var childContainer = document.getElementById(childContainerId);
            if (childContainer) {
                var childCheckBoxes = childContainer.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++) {
                    childCheckBoxes[i].checked = checkBox.checked;
                }
            }
        }

        //unchecks the parent checkboxes if the current one is unchecked
        function ToggleParentCheckBox(checkBox) {
            //            if(checkBox.checked == false)
            //            {
            //                var parentContainer = GetParentNodeById(checkBox, TREEVIEW_ID);
            //                if(parentContainer) 
            //                {
            //                    var parentCheckBoxId = parentContainer.id.substring(0, parentContainer.id.search("Nodes")) + "CheckBox";
            //                    if($get(parentCheckBoxId) && $get(parentCheckBoxId).type == "checkbox") 
            //                    {
            //                        $get(parentCheckBoxId).checked = false;
            //                        ToggleParentCheckBox($get(parentCheckBoxId));
            //                    }
            //                }
            //            }
        }

        //returns the ID of the parent container if the current checkbox is unchecked
        function GetParentNodeById(element, id) {
            var parent = element.parentNode;
            if (parent == null) {
                return false;
            }
            if (parent.id.search(id) == -1) {
                return GetParentNodeById(parent, id);
            }
            else {
                return parent;
            }
        }
    </script>
    <script type="text/javascript">
        var links = document.getElementsByTagName("a");
        for (var i = 0; i < links.length; i++) {
            if (links[i].className == TREEVIEW_ID + "_0") {
                links[i].href = "javascript:ToggleCheckBox(\"" + links[i].id + "\");";
            }
        }

        var checkBoxes = document.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox") {
                $addHandler(checkBoxes[i], "click", checkBox_Click);
            }
        }
    </script>   
    <script type="text/javascript">
        function showPopup(devName) { 
            document.getElementById(devName).style.display = 'block';
            document.getElementById(devName).style.visibility = 'visible';
        }

        function hidePopup(devName) {
            //document.getElementById(devName).style.visibility = 'hidden';
            //document.getElementById(devName).style.display = 'none';
            document.getElementById('<%=btnActCancel.ClientID %>').click();
        }
     </script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" Font-Names="Tahoma"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="User Name" Value="UsrName" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="User Full Name" Value="UsrFullName" meta:resourcekey="ListItemResource3"></asp:ListItem>
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
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                        PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp"
                        PreviousPageKey="PageDown" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="UsrName" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                        OnPreRender="grdData_PreRender" AllowSorting="True" EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="User Name" DataField="UsrName" InsertVisible="False"
                                ReadOnly="True" SortExpression="UsrName" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="User Full Name" DataField="UsrFullName" SortExpression="UsrFullName"
                                meta:resourcekey="BoundFieldResource2" />
                            <asp:TemplateField HeaderText="Start Date" SortExpression="UsrStartDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("UsrStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expiry Date" SortExpression="UsrExpireDate" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("UsrExpireDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="UsrStatus" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("UsrStatus"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("UsrName") %>'
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

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton  glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnActingUser" runat="server" CssClass="GenButton glyphicon glyphicon-ok-sign"
                        OnClick="btnActingUser_Click" Enabled="False" Text="&lt;img src=&quot;../images/Button_Icons/button_reset_Password.png&quot; /&gt; Set Acting User"
                        meta:resourcekey="btnActingUserResource1"></asp:LinkButton>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    &nbsp;
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"></asp:CustomValidator>
                </div>
            </div>
            <div class="GreySetion">
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblUsername" runat="server" Text="User Name :" Font-Names="Tahoma"
                            Font-Size="10pt" meta:resourcekey="lblUsernameResource1"></asp:Label>
                    </div>
                    <div class="col4">

                        <asp:TextBox ID="txtUsername" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtUsernameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqUsername" ControlToValidate="txtUsername" CssClass="CustomValidator"
                            Text="<img src='../images/Exclamation.gif' title='Username is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" meta:resourcekey="reqUsernameResource1"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvUsrName" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='User Name already exists!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="UsrName_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvUsrNameResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblFullname" runat="server" Text="User Full Name :" meta:resourcekey="lblFullnameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtFullname" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtFullnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="Fullname" ControlToValidate="txtFullname"
                            ValidationGroup="vgSave" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Fullname is required!' />"
                            meta:resourcekey="FullnameResource2" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :" meta:resourcekey="lblEmpIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEmpID" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='EmpID do not exist!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="EmpID_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvArWorkTimeNameResource11" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblLanguage" runat="server" Text="Language :" meta:resourcekey="lblLanguageResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlLanguage" runat="server" ValidationGroup="vgSave"
                            Enabled="False" meta:resourcekey="ddlLanguageResource1">
                            <asp:ListItem Selected="True" meta:resourcekey="ListItemResource4">Select Language</asp:ListItem>
                            <asp:ListItem Value="EN" meta:resourcekey="ListItemResource5">English</asp:ListItem>
                            <asp:ListItem Value="AR" meta:resourcekey="ListItemResource6">Arabic</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rfvLanguage" ControlToValidate="ddlLanguage"
                            InitialValue="Select Language" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Language is required!' />"
                            ValidationGroup="vgSave" meta:resourcekey="rfvLanguageResource1" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">

                        <asp:CheckBox ID="chkActiveUser" runat="server" Enabled="False" Text="Active" meta:resourcekey="chkActiveUserResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkActUser" runat="server" Enabled="False" Text="Acting User Active"
                            meta:resourcekey="chkActUserResource1" />

                        &nbsp;
                                     <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />

                        <%--<asp:Button ID="lnkShow1" runat="server" Text="?" meta:resourcekey="lnkButtonResouce" OnClientClick="return false;" Height="21px" Font-Bold="True" />--%>
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="If checked makes current user In active and Acting user Active" meta:resourcekey="lblHintResource"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>


                <div id="divML" runat="server" visible="false" class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkAdminMiniLogger" runat="server" Enabled="False" Text="Admin Mini Logger" meta:resourcekey="chkAdminMiniLoggerResource1" />

                    </div>
                    <div class="col2">
                        <asp:Label ID="lblDescription" runat="server" Text="Description :" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblPermissionGroup" runat="server" Text="Page Permission :" meta:resourcekey="lblPermissionGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlPermissionGroup" runat="server" Enabled="False"
                            meta:resourcekey="ddlPermissionGroupResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlPermissionGroup_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblRepPermissionGroup" runat="server" Text="Report permission :" meta:resourcekey="lblRepPermissionGroupResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlRepPermissionGroup" runat="server" Enabled="False"
                            meta:resourcekey="ddlRepPermissionGroupResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlRepPermissionGroup_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" meta:resourcekey="lblStartDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationGroup="vgSave" CalTo="calEndDate" />
                    </div>

                    <div class="col2">
                        <asp:Label ID="lblDateEnd" runat="server" Text="Expiry Date :" meta:resourcekey="lblDateEndResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblPassword" runat="server" Text="Password :" meta:resourcekey="lblPasswordResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" TextMode="Password"
                            Enabled="False" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="Password" ControlToValidate="txtPassword"
                            Text="<img src='../images/Exclamation.gif' title='Password is required!' />" ValidationGroup="vgSave"
                            EnableClientScript="False" meta:resourcekey="PasswordResource2" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col2">
                        <asp:Label ID="lblEmail" runat="server" Text="*" Visible="false" CssClass="RequiredField"></asp:Label>
                        <asp:Label ID="lblEmailID" runat="server" Text="Email ID :" meta:resourcekey="lblEmailIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmailID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEmailIDResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvEmail" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Email ID is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="Email_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvArWorkTimeNameResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblMob" runat="server" Text="*" Visible="false" CssClass="RequiredField"></asp:Label>
                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No. :" meta:resourcekey="lblMobileNoResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMobileNo" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cdvMobileNo" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Mobile No is required!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="MobileNo_ServerValidate" EnableClientScript="False"
                            ControlToValidate="txtValid" meta:resourcekey="cvMobileNoResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>

                    <div class="col2">
                        <asp:Label ID="lblUsrADUser" runat="server" Text="Active Directory User :" meta:resourcekey="lblUsrADUserResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtUsrADUser" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtUsrADUserResource1"></asp:TextBox>
                        <asp:ImageButton ID="btnGetADUsr" runat="server" OnClick="btnGetADUsr_Click" Enabled="false" ImageUrl="../images/Button_Icons/button_magnify.png"
                            meta:resourcekey="btnGetADUsrResource1" CssClass="LeftOverlay" />
                        <asp:CustomValidator ID="cvADUser" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Active Directory User already exists!' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="ADUser_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                            ControlToValidate="txtValid" meta:resourcekey="cvADUserResource11"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                    </div>

                    <div class="col4">
                        <asp:CheckBox ID="chkEmailAlert" runat="server" Enabled="False" Text="Email Alert"
                            meta:resourcekey="chkEmailAlertResource1" />
                    </div>
                </div>




                <div id="Div1" runat="server" visible="false" class="row">
                    <div class="col2">
                        <asp:Label ID="lblDomainName" runat="server" Text="Domain Name :" meta:resourcekey="lblDomainNameResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDomainName" runat="server" AutoCompleteType="Disabled" Width="168px"
                            Enabled="False" meta:resourcekey="txtDomainNameResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkMobileAlert" runat="server" Enabled="False" Text="Mobile Alert"
                            meta:resourcekey="chkMobileAlertResource1" />
                    </div>
                </div>


                <div class="row">
                    <div class="col12">
                        <asp:Label ID="lblMenuPermissions" runat="server" Text="Permission" meta:resourcekey="lblMenuPermissionsResource1" CssClass="h4"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col6">
                        <asp:XmlDataSource ID="xmlDataSource1" TransformFile="~/XSL/TransformXSLT.xsl" XPath="MenuItems/MenuItem"
                            runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="xmlDataSource1" LineImagesFolder="~/images/TreeLineImages"
                            ShowLines="True" OnDataBound="TreeView1_DataBound" ShowCheckBoxes="All" OnTreeNodeDataBound="TreeView1_TreeNodeDataBound"
                            Font-Names="Tahoma" Font-Size="10pt" ForeColor="#CAD2D6" meta:resourcekey="TreeView1Resource1"
                            Visible="False">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ToolTipField="ToolTip"
                                    ValueField="Value" meta:resourcekey="TreeNodeBindingResource1" />
                            </DataBindings>
                        </asp:TreeView>
                        </td>
                    </div>
                    <div class="col6">
                        <asp:XmlDataSource ID="xmlReportSource" TransformFile="~/XSL/RepTransformXSLT.xsl" XPath="MenuItems/MenuItem"
                            runat="server" CacheExpirationPolicy="Sliding" EnableCaching="False" />
                        <asp:TreeView ID="trvReport" runat="server" DataSourceID="xmlReportSource" LineImagesFolder="~/images/TreeLineImages"
                            ForeColor="#CAD2D6" ShowLines="True" ShowCheckBoxes="Root" OnDataBound="trvReport_DataBound"
                            OnTreeNodeDataBound="trvReport_TreeNodeDataBound" meta:resourcekey="trvReportResource1"
                            Visible="False">
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ToolTipField="ToolTip"
                                    ValueField="Value" meta:resourcekey="TreeNodeBindingResource2" />
                            </DataBindings>
                        </asp:TreeView>
                    </div>
                </div>
            </div>


            <%--Popup--%>
            <div id="DivPopup" class="popup" data-popup="popup-1" runat="server" visible="false">  
                <div class="popup-inner">
                    <a class="popup-close" data-popup-close="popup-1" href="#" onclick="hidePopup('ctl00_ContentPlaceHolder1_DivPopup')">x</a>
                    <div class="popup-wrap">
                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsShowMsg2" runat="server" CssClass="MsgSuccess"
                                EnableClientScript="False" ValidationGroup="vgShowMsg2" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsSave2" runat="server" ValidationGroup="vgSave2"
                                EnableClientScript="False" CssClass="MsgValidation"/>
                        </div>
                    </div>
                         
                    <div class="GreySetion">
                        <div class="row">
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblActLoginName" runat="server" Text="Login Name :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActLoginName" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rvActLoginName" ControlToValidate="txtActLoginName"
                                    Text="<img src='../images/Exclamation.gif' title='Username is required' />" ValidationGroup="vgSave2"
                                    EnableClientScript="False" Display="Dynamic" SetFocusOnError="True" CssClass="CustomValidator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col2">
                                <span class="RequiredField">*</span>
                                <asp:Label ID="lblActPassword" runat="server" Text="Password :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActPassword" runat="server" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rvActPassword" ControlToValidate="txtActPassword"
                                    Text="<img src='../images/Exclamation.gif' title='Password is required!' />" ValidationGroup="vgSave2"
                                    EnableClientScript="False"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblActEmpID" runat="server" Text="Emp ID :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActEmpID" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CustomValidator ID="cvActEmpID" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='EmpID do not exist' /&gt;"
                                    ValidationGroup="vgSave2" OnServerValidate="ActEmpID_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid" CssClass="CustomValidator"></asp:CustomValidator>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblActEmailID" runat="server" Text="Email ID :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActEmailID" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CustomValidator ID="cvActEmailID" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Email ID is required' /&gt;"
                                    ValidationGroup="vgSave2" OnServerValidate="ActEmail_ServerValidate" EnableClientScript="False"
                                    ControlToValidate="txtValid" CssClass="CustomValidator"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblActADUser" runat="server" Text="Active Directory User :"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActADUser" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:ImageButton ID="btnActADUser" runat="server" OnClick="btnActADUser_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay" />
                                <asp:CustomValidator ID="cvActADUser" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Active Directory User already exists!' /&gt;"
                                    ValidationGroup="vgSave2" OnServerValidate="ActADUser_ServerValidate" EnableClientScript="False"
                                    ControlToValidate="txtValid" CssClass="CustomValidator"></asp:CustomValidator>
                            </div>
                            <div class="col2">
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtActID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col8">
                                <asp:LinkButton ID="btnActSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnActSave_Click"
                                    Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                    ValidationGroup="vgSave2"></asp:LinkButton>

                                <asp:LinkButton ID="btnActCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnActCancel_Click"
                                    Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>
                            </div>
                            <div class="col2">
                                <asp:CustomValidator ID="cvShowMsg2" runat="server" Display="None"
                                    ValidationGroup="vgShowMsg2" OnServerValidate="ShowMsg2_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid">
                                </asp:CustomValidator>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>               
            </div>
            <%--Popup--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
