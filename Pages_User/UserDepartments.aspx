<%@ Page Title="User Department" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="UserDepartments.aspx.cs" Inherits="UserDepartments" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //************************** Treeview Parent-Child check behaviour ****************************//  

        function OnTreeClick(evt) {
            //            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            //            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            //            if (isChkBoxClick) {
            //                var parentTable = GetParentByTagName("table", src);
            //                var nxtSibling = parentTable.nextSibling;
            //                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            //                {
            //                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
            //                    {
            //                        //check or uncheck children at all levels
            //                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
            //                    }
            //                }
            //                //check or uncheck parents at all levels
            //                //            CheckUncheckParents(src, src.checked);
            //            }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblSearchBy" runat="server" Text="Search By:"
                        meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"
                        meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="User Name" Value="UsrName"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Department Name (Ar)" Value="DepNameAr"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Department Name (En)" Value="DepNameEn"
                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled"
                        meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay"
                        ImageUrl="../images/Button_Icons/button_magnify.png"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                        AutoGenerateColumns="False"
                        AllowPaging="True" CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="UsrName"
                        ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" meta:resourcekey="grdDataResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.gif" LastPageText="Last" LastPageImageUrl="~/images/last.gif"
                            NextPageText="Next" NextPageImageUrl="~/images/next.gif" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.gif" />
                        <Columns>
                            <asp:BoundField DataField="UsrName" HeaderText="User Name"
                                SortExpression="UsrName" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="UsrFullName" HeaderText="Full Name" SortExpression="UsrFullName"
                                meta:resourcekey="BoundFieldResource422" />
                        </Columns>


                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
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
                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit"
                        OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" ValidationGroup="Groups"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                </div>
                <div class="col8">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>

                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <span class="h3">
                        <asp:Label ID="lblDepartment" runat="server" Text="Department"
                            meta:resourcekey="lblDepartmentResource1"></asp:Label></span>
                    <asp:CustomValidator ID="cvDepartment" runat="server"
                        ValidationGroup="vgSave"
                        OnServerValidate="tree_ServerValidate"
                        EnableClientScript="False"
                        ControlToValidate="txtValid"
                        meta:resourcekey="cvDepartmentResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:XmlDataSource ID="xmlDataSource2" runat="server" TransformFile="~/XSL/DepTransformXSLT.xsl"
                        CacheExpirationPolicy="Sliding" XPath="MenuItems/MenuItem" EnableCaching="False" />
                    <asp:TreeView ID="trvDept" runat="server" LineImagesFolder="~/images/TreeLineImages" DataSourceID="xmlDataSource2"
                        ShowLines="True" ShowCheckBoxes="All" OnDataBound="trvDept_DataBound"
                        OnTreeNodeDataBound="trvDept_TreeNodeDataBound" ForeColor="#CAD2D6"
                        meta:resourcekey="trvDeptResource1">
                        <DataBindings>
                            <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ValueField="Value"
                                meta:resourcekey="TreeNodeBindingResource1" ImageUrlField="Check" />
                        </DataBindings>
                    </asp:TreeView>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="False" Width="15px" meta:resourcekey="txtIDResource1"></asp:TextBox>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
