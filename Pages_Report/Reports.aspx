<%@ Page Title="Report" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" Culture="auto" meta:resourcekey="PageResource2"
    UICulture="auto" %>

<%@ Register Assembly="Stimulsoft.Report.WebDesign" Namespace="Stimulsoft.Report.Web" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Control/EmployeeSelectedVertical.ascx" TagName="EmployeeSelectedVertical" TagPrefix="ucEmp" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/TabContainer.js"></script>
    <%-- <link href="../CSS/buttonStyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="../CSS/WizardStyle.css" rel="stylesheet" type="text/css" />
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
    
    <script type="text/javascript">
        function showPopup(dev1, dev2, dev3, devShow) {
            hidePopup('', dev2, dev3);
            document.getElementById(devShow).style.visibility = 'visible'; document.getElementById(devShow).style.display = 'block';
            document.getElementById(dev1).style.visibility = 'visible'; document.getElementById(dev1).style.display = 'block';
        }

        function hidePopup(dev1, dev2, dev3) {
            if (dev1 != '') { document.getElementById(dev1).style.visibility = 'hidden'; document.getElementById(dev1).style.display = 'none'; }
            if (dev2 != '') { document.getElementById(dev2).style.visibility = 'hidden'; document.getElementById(dev2).style.display = 'none'; }
            if (dev3 != '') { document.getElementById(dev3).style.visibility = 'hidden'; document.getElementById(dev3).style.display = 'none'; }
        }

    </script>
    
    
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEditReport" />
            <asp:PostBackTrigger ControlID="btnExportRecord" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="rp_Search" runat="server">
                            <asp:DataList ID="dltReport" runat="server" CaptionAlign="Top" RepeatColumns="6"
                                BorderStyle="None" OnItemCommand="dltReport_ItemCommand"
                                OnItemDataBound="dltReport_ItemDataBound" meta:resourcekey="DataList1Resource1"
                                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <SelectedItemStyle BackColor="Red" Font-Bold="True" />

                                <ItemTemplate>

                                    <asp:LinkButton ID="lnkBtnEn" CommandArgument='<%# DataBinder.Eval(Container,"dataitem.RepID") %>' CssClass="ReportBtns glyphicon glyphicon-align-left reportsicon"
                                        runat="server" Text='<%# DataBinder.Eval(Container,getRepName()) %>'
                                        meta:resourcekey="lnkBtnEnResource1">
                                        <asp:ImageButton ID="Image1" runat="server" Height="30px" ImageUrl="~/images/reports-icon.png"
                                            Width="30px" meta:resourcekey="Image1Resource1" CommandArgument='<%# DataBinder.Eval(Container,"dataitem.RepID") %>' />
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblTitleReport" runat="server" Font-Size="13pt" meta:resourcekey="lblTitleReportResource1" CssClass="h4"></asp:Label>
                </div>
            </div>
            <%--Popup--%>
            <div id="DivPopup" class="popup" data-popup="popup-1" runat="server">  
                <div class="popup-inner">
                    <a class="popup-close" data-popup-close="popup-1" href="#" onclick="hidePopup('ctl00_ContentPlaceHolder1_DivPopup','','')">x</a>
                    <div class="popup-wrap">
                    <div class="row">
                        <div class="col12">
                            <asp:TextBox ID="txtDescReport" runat="server" Height="60px" TextMode="MultiLine"
                                Width="100%" meta:resourcekey="txtDescReportResource1" BackColor="#DCE7E1"
                                Enabled="False"></asp:TextBox>
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
                            <asp:ValidationSummary ID="vsShow" runat="server" ValidationGroup="vgShow"
                                EnableClientScript="False" CssClass="MsgValidation"
                                meta:resourcekey="vsumAllResource1" />
                        </div>
                    </div>
                    <div class="GreySetion">
                        <asp:Panel ID="pnlDate" runat="server" meta:resourcekey="panelDateResource1">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblDate" runat="server" Font-Size="Small" meta:resourcekey="lblDateResource1"
                                        Text="Date :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgShow" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlMonth" runat="server" meta:resourcekey="pnlMonthResource1" Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblMonth" runat="server" Font-Size="Small" meta:resourcekey="lblMonthResource1"
                                        Text="Month :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvMonthList" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvMonthListResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Must Select Month!' /&gt;"
                                        ValidationGroup="vgShow"></asp:CustomValidator>
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblYear" runat="server" Font-Size="Small" meta:resourcekey="lblYearResource1"
                                        Text="Year :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDateFromTo" runat="server" meta:resourcekey="pnlDateFromToResource1" Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblDateFrom" runat="server" Font-Size="Small" meta:resourcekey="lblDateFromResource1"
                                        Text="Start Date :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgShow" CalTo="calEndDate" />
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblDateTo" runat="server" Font-Size="Small" meta:resourcekey="lblDateToResource1"
                                        Text="End Date :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgShow" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDaysCount" runat="server" meta:resourcekey="pnlDaysCountResource1">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblDaysCount" runat="server" Font-Size="Small" meta:resourcekey="lblDaysCountResource1"
                                        Text="Days Count :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtDaysCount" runat="server" Text="20" AutoCompleteType="Disabled"
                                        meta:resourcekey="txtDaysCountResource1" onkeypress="return OnlyNumber(event);"></asp:TextBox>
                                    <asp:CustomValidator ID="cvDaysCount" runat="server" ControlToValidate="txtValid" EnableClientScript="False"
                                        meta:resourcekey="cvDaysCountResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='You must enter a value that is greater than 0' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWorkTime" runat="server" meta:resourcekey="pnlWorkTimeResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col12">
                                    <asp:CustomValidator ID="cvWorkTime" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvWorkTimeResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one field in WorkTime filters!' /&gt;"
                                        ValidationGroup="vgShow"></asp:CustomValidator>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblArWorkTimeName" runat="server" Font-Size="Small" meta:resourcekey="lblArWorkTimeNameResource1"
                                        Text="Work Time (Ar):"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtArWorkTimeName" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtArWorkTimeNameResource1"></asp:TextBox>
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblEnWorkTimeName" runat="server" Font-Size="Small" meta:resourcekey="lblEnWorkTimeNameResource1"
                                        Text="Work Time (En) :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtEnWorkTimeName" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEnWorkTimeNameResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="Label19" runat="server" Font-Size="Small" meta:resourcekey="Label19Resource1"
                                        Text="Work Type Name :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlWtpID" runat="server" meta:resourcekey="ddlWtpIDResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlMachine" runat="server" meta:resourcekey="pnlMachineResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblMachineLocation1" runat="server" Font-Size="Small" meta:resourcekey="lblMachineLocation1Resource1"
                                        Text="Machine Location :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlLocation" runat="server" meta:resourcekey="ddlLocationResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvMachine" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvMachineResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one feild in Machine filters!' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlCategory" runat="server" meta:resourcekey="pnlCategoryResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblCatName" runat="server" Font-Size="Small" meta:resourcekey="lblCatArNameResource1"
                                        Text="Category Name :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlCatName" runat="server" meta:resourcekey="ddlArCatNameResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvCategory" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvCategoryResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one feild in Category filters!' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlUsers" runat="server" meta:resourcekey="pnlUsersResource1">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblUsername" runat="server" Font-Size="Small" meta:resourcekey="lblUsernameResource1"
                                        Text="User Name :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlUserName" runat="server" meta:resourcekey="ddlUserNameResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvUser" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvUserResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one feild in User filters!' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlVacType" runat="server" meta:resourcekey="pnlVacTypeResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblVacType" runat="server" Font-Size="Small" meta:resourcekey="lblVacTypeResource1"
                                        Text="Vacation type :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlVacType" runat="server" meta:resourcekey="ddlVacTypeResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvVacType" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvVacTypeResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one feild in Vacation Type filters!' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlExcType" runat="server" meta:resourcekey="pnlExcTypeResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblExcType" runat="server" Font-Size="Small" meta:resourcekey="lblExcTypeResource1"
                                        Text="Excuse Type :"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlExcType" runat="server" meta:resourcekey="ddlExcTypeResource1">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="cvExcType" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvExcTypeResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Fill at least one feild in Excuse Type filters!' /&gt;"
                                        ValidationGroup="vgShow">
                                    </asp:CustomValidator>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDepartmnets" runat="server" meta:resourcekey="pnlDepartmnetsResource1"
                            Width="100%">
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblBranchID" runat="server" Text="Branch Name :" meta:resourcekey="lblBranchIDResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlBranchID" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBranchID_SelectedIndexChanged" meta:resourcekey="ddlBranchIDResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col2">
                                    <asp:Label ID="lblSearchByDep" runat="server" Text="Department Name :" meta:resourcekey="lblSearchByDepResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtSearchByDep" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="btnSearchByDep" runat="server" OnClick="btnSearchDep_Click" CssClass="LeftOverlay"
                                        ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnSearchByDepResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12 center-block">
                                    <asp:Label ID="lblDepName" runat="server" Text="Departments :" meta:resourcekey="lblDepNameResource1" CssClass="h3"></asp:Label>
                                    <asp:CustomValidator ID="cvDepartment" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" meta:resourcekey="cvDepartmentResource1" OnServerValidate="Validate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='check at least one list of Departments !' /&gt;"
                                        ValidationGroup="vgShow"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col12">
                                    <asp:XmlDataSource ID="xmlDataSource2" runat="server" TransformFile="~/XSL/DepTransformXSLT.xsl"
                                        CacheExpirationPolicy="Sliding" XPath="MenuItems/MenuItem" EnableCaching="False" />
                                    <asp:TreeView ID="trvDept" runat="server" ExpandDepth="0" LineImagesFolder="~/images/TreeLineImages" DataSourceID="xmlDataSource2"
                                        ShowLines="True" ShowCheckBoxes="All" OnDataBound="trvDept_DataBound" OnTreeNodeDataBound="trvDept_TreeNodeDataBound"
                                        meta:resourcekey="trvDeptResource1">
                                        <DataBindings>
                                            <asp:TreeNodeBinding DataMember="MenuItem" TextField="Text" ValueField="Value" meta:resourcekey="TreeNodeBindingResource1"
                                                ImageToolTipField="Check" />
                                        </DataBindings>
                                        <SelectedNodeStyle ForeColor="Red" />
                                    </asp:TreeView>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlEmployee" runat="server" meta:resourcekey="pnlUsersResource1">
                            <div class="row">
                                <div class="col12">
                                    <ucEmp:EmployeeSelectedVertical ID="ucEmployeeSelected" runat="server" ValidationGroupName="vgShow"
                                        ClientIDMode="Inherit" />
                                </div>
                            </div>
                        </asp:Panel>

                        <%--</asp:Panel>--%>
                        <div class="row">
                            <div class="col8">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="GenButton glyphicon glyphicon-eye-open" OnClick="btnShow_Click"
                                    Text="&lt;img src=&quot;../images/Button_Icons/page_show.png&quot; /&gt; Show"
                                    ValidationGroup="vgShow" meta:resourcekey="btnFilterResource1"></asp:LinkButton>

                                <asp:LinkButton ID="btnEditReport" runat="server" CssClass="GenButton glyphicon glyphicon-edit"
                                    OnClick="btnEditReport_Click" Text="&lt;img src=&quot;../images/Button_Icons/page_edit.png&quot; /&gt; Edit"
                                    meta:resourcekey="btnEditReportResource1"></asp:LinkButton>

                                <asp:LinkButton ID="btnSetAsDefault" runat="server" CssClass="GenButton glyphicon glyphicon-refresh"
                                    OnClick="btnSetAsDefault_Click" OnClientClick="return confirm('if you click 'ok' you will lost all did changes on report');"
                                    Text="&lt;img src=&quot;../images/Button_Icons/page_refresh.png&quot; /&gt; Return Default"
                                    meta:resourcekey="btnSetAsDefaultResource1"></asp:LinkButton>
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

                        <div class="row">
                          
                            <div class="col2">
                                <span id="spnFavNameEn" runat="server" class="RequiredField">*</span>
                                <asp:Label ID="lblFavNameEn" runat="server" Text="Name (En) :"></asp:Label>
                                  </div>
                            <div class="col2">
                                <asp:TextBox ID="txtFavNameEn" runat="server" AutoCompleteType="Disabled" Enabled="False" ></asp:TextBox>
                                  </div>
                            <div class="col2">
                                <span id="spnFavNameAr" runat="server" class="RequiredField">*</span>
                                <asp:Label ID="lblFavNameAr" runat="server" Text="Name (Ar) :"></asp:Label>
                                 </div>
                            <div class="col2">
                                <asp:TextBox ID="txtFavNameAr" runat="server" AutoCompleteType="Disabled" Enabled="False" ></asp:TextBox>
                                 </div>
                            </div>

                        <div class="row">
                            <div class="col8">
                                <asp:LinkButton ID="btnAddFavorite" runat="server" CssClass="GenButton glyphicon glyphicon-star"   OnClick="btnAddFavorite_Click"
                                    Text="&lt;img src=&quot;../images/Button_Icons/page_show.png&quot; /&gt; Add Favorite Report"  
                                    ValidationGroup="vgFav" meta:resourcekey="btnAddFavoriteResource1"></asp:LinkButton>
                                <asp:LinkButton ID="btnFavSave" runat="server" ValidationGroup="vgShow" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnFavSave_Click" 
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"></asp:LinkButton>
                           
                                
                                <asp:LinkButton ID="btnFavCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnFavCancel_Click"
                                     Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"></asp:LinkButton>
                            </div>
                        </div>


                    </div>
                    </div>
                   
                </div>
            </div>
            <%--Popup--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col6">
            <asp:LinkButton ID="btnExportRecord" runat="server" CssClass="GenButton glyphicon glyphicon-export"
                OnClick="btnExportRecord_Click" Text="&lt;img src=&quot;../images/Button_Icons/page_export.png&quot; /&gt; Export"
                meta:resourcekey="btnExportRecordResource1"></asp:LinkButton>

            <asp:FileUpload ID="fileUpload" runat="server" meta:resourcekey="fileUploadResource1" />
        </div>
        <div class="col6">
            <asp:LinkButton ID="btnUploadReport" runat="server" CssClass="GenButton glyphicon glyphicon-upload"
                OnClick="btnUploadReport_Click" Text="&lt;img src=&quot;../images/Button_Icons/page_upload.png&quot; /&gt; import"
                meta:resourcekey="btnUploadReportResource1"></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col12">
            <cc2:StiWebDesigner ID="StiWebDesigner1" runat="server" OnSaveReport="StiWebDesigner1_SaveReport"
                OnPreInit="StiWebDesigner1_PreInit" />
        </div>
    </div>
    <div id="Div1" runat="server" class="row">
        <div class="col12">
        </div>
    </div>
    <div id="Div2" runat="server" class="row">
        <div class="col12">
            <asp:LinkButton ID="btnImportRecord" runat="server" CssClass="GenButton glyphicon glyphicon-import"
                OnClick="btnImportRecord_Click" Text="&lt;img src=&quot;../images/Button_Icons/page_import.png&quot; /&gt; Import Technical"
                meta:resourcekey="btnImportRecordResource1" Visible="false"></asp:LinkButton>
        </div>
    </div>

</asp:Content>
