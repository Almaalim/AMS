<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeWorktimeTable.aspx.cs" Inherits="Pages_Employee_EmployeeWorktimeTable" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="../CSS/PopupStyle.css" rel="stylesheet" type="text/css" />
    
    <%--script--%>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>

    <%--<script type="text/javascript">
        $(document).ready(function() {
            $("#<%=grdData.ClientID%> tr:has(td)").hover(function(e) {
                $(this).css("cursor", "pointer");
            });    
        })
    </script>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="MainTable">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblMonth" runat="server" Text="Month:" meta:resourcekey="lblMonthResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1"></asp:DropDownList>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1"></asp:DropDownList>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblDep" runat="server" Text=" Department:" meta:resourcekey="lblDepResource1"></asp:Label>
                    </div>
                    <div class="col3">
                        <asp:DropDownList ID="ddlDep" runat="server" meta:resourcekey="ddlDepResource1"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvDep" runat="server" ControlToValidate="ddlDep" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Department is required' /&gt;"
                                ValidationGroup="vgFilter" meta:resourcekey="rvDepResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col1">
                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ValidationGroup="vgFilter" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay" meta:resourcekey="btnFilterResource1"/>
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
                            CssClass="MsgValidation" ShowSummary="False" meta:resourcekey="vsSaveResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col8">
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

                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>

                        <asp:CustomValidator ID="cvValid" runat="server" Display="None" CssClass="CustomValidator"
                            ValidationGroup="vgSave" OnServerValidate="Valid_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvValidResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col12">
                        <asp:Label ID="lblTitel" runat="server" meta:resourcekey="lblTitelResource1" CssClass="h4"></asp:Label>
                        <asp:HiddenField ID="hdnRowID"  runat="server" />
                        <asp:HiddenField ID="hdnColID"  runat="server" />
                        <asp:HiddenField ID="hdnEmpid"  runat="server" />
                        <asp:HiddenField ID="hdnChangeEmpID" runat="server" />
                        <asp:HiddenField ID="hdnChangeDayNo" runat="server" />
                        <asp:HiddenField ID="hdnChangeWktID" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            OnRowDataBound="grdData_RowDataBound" BorderWidth="0"
                            OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated" meta:resourcekey="grdDataResource1"> 
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div id='divBackground'></div>
            <div id='divPopup' class="divPopup" >
                <div id='divPopupHead' class="divPopupHead">
                    <asp:Label ID="lblNamePopup" runat="server" CssClass="lblNamePopup" meta:resourcekey="lblNamePopupResource1"></asp:Label>
                </div>
                <div id='divClosePopup' class="divClosePopup" onclick="hidePopup('divPopup')"><a href='#'>X</a></div>
                <div id='divPopupContent' class="divPopupContent">
                    <div class="row">
                       <div class="col6">
                           </div> 
                        </div>
                    <div class="row">
                        <div class="col3"></div>
                    <div class="col6">
                        <asp:DropDownList ID="ddlWktID" runat="server" meta:resourcekey="ddlWktIDResource1"></asp:DropDownList>
                        </div>
                        </div>
                        <div class="row"> <div class="col3"></div>
                        <div class="col6 center-block">
                        <a id="Button1" onclick="changeValue()" Class="GenButton glyphicon glyphicon-edit" >Button</a>
                            
                  </div>
                        </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function DoOpen(rowid,colid,empid)
        {
            document.getElementById('<%= hdnRowID.ClientID %>').value = rowid;
            document.getElementById('<%= hdnColID.ClientID %>').value = colid;
            document.getElementById('<%= hdnEmpid.ClientID %>').value = empid;

            showPopup('');
        }

        function changeValue()
        {
            var wkt = document.getElementById('<%= ddlWktID.ClientID %>');
            var val = wkt.options[wkt.selectedIndex].value;
            var txt = wkt.options[wkt.selectedIndex].innerText;
            if (val == '0') { txt = '-'; }

            var rowid = document.getElementById('<%= hdnRowID.ClientID %>').value; 
            var colid = document.getElementById('<%= hdnColID.ClientID %>').value; 
            var empid = document.getElementById('<%= hdnEmpid.ClientID %>').value; 
            var grd   = document.getElementById('<%= grdData.ClientID %>');

            var ChangeEmpID = document.getElementById('<%= hdnChangeEmpID.ClientID %>').value; 
            var ChangeDayNo = document.getElementById('<%= hdnChangeDayNo.ClientID %>').value; 
            var ChangeWktID = document.getElementById('<%= hdnChangeWktID.ClientID %>').value; 

            var Split = '';
            if (ChangeEmpID != '') { Split = ','; }
            
            ChangeEmpID = ChangeEmpID + Split + empid;
            ChangeDayNo = ChangeDayNo + Split + (colid - 1);
            ChangeWktID = ChangeWktID + Split + val;
                
            document.getElementById('<%= hdnChangeEmpID.ClientID %>').value = ChangeEmpID;
            document.getElementById('<%= hdnChangeDayNo.ClientID %>').value = ChangeDayNo;
            document.getElementById('<%= hdnChangeWktID.ClientID %>').value = ChangeWktID;

            //alert(Change);
            grd.rows[rowid].cells[colid].childNodes[0].innerText = txt;
            grd.rows[rowid].cells[colid].style.backgroundColor = 'LightCoral';

            //document.getElementById('<%= btnSave.ClientID %>').disabled = false;
            //$('#<%= btnSave.ClientID %>').prop('disabled', false);
            //$('#<%= btnCancel.ClientID %>').prop('disabled', false);
            //$('#<%= btnSave.ClientID %>').attr('disabled', true);

            hidePopup('divPopup')
        }
    </script>
</asp:Content>

