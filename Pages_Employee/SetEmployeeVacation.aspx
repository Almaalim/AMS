<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetEmployeeVacation.aspx.cs"
    Inherits="SetEmployeeVacation" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Set Emplyee Vacation</title>

    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
    <script type="text/javascript" src="../Script/DivPopup.js"></script>
     <script type="text/javascript" src="../Script/jquery-1.7.1.min.js"></script>
      <script type="text/javascript">
        $(document).ready(function () {
            $("div[class*='col']").each(function () {
                if ($(this).children(".RequiredField").length > 0) {
                    $(this).addClass("RequiredFieldDiv");
                }
                var $this = $(this);

                $this.html($this.html().replace(/&nbsp;/g, ''));

            });
        });
    </script>
    <%--script--%>
    <%--stylesheet--%>
    <link href="../CSS/Metro/Metro.css" rel="stylesheet" />
    <%--stylesheet--%>
</head>
<body  >
    <form id="form1" runat="server"  >
        <div>
            <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                     <div class="row">
                        <div class="col12">
                            <asp:Label ID="lblName" runat="server" Text="" CssClass="h4"></asp:Label>
                        </div>
                    </div>
                    
                    
                    <div class="row">
                        <div class="col12">
                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                AutoGenerateColumns="False" PageSize="5" AllowPaging="false"
                                GridLines="None" DataKeyNames="EvsID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                                OnRowCommand="grdData_RowCommand" OnRowCreated="grdData_RowCreated"
                                meta:resourcekey="grdDataResource1">

                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                    LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                    NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                <Columns>
                                    <asp:BoundField HeaderText="Employee ID" DataField="EmpID" SortExpression="EmpID"
                                        meta:resourcekey="BoundFieldResource1" />
                                    <asp:BoundField HeaderText="ID" DataField="EvsID" SortExpression="EvsID" meta:resourcekey="BoundFieldResource2" />
                                     <asp:BoundField HeaderText="Vacation Name (Ar)" DataField="VtpNameAr" SortExpression="VtpNameAr"
                                        meta:resourcekey="BoundFieldResource3" />
                                    <asp:BoundField HeaderText="Vacation Name (En)" DataField="VtpNameEn" SortExpression="VtpNameEn"
                                        meta:resourcekey="BoundFieldResource4" />
                                    <asp:BoundField HeaderText="Max Days" DataField="EvsMaxDays" SortExpression="EvsMaxDayes"
                                        meta:resourcekey="BoundFieldResource5" />
                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("EvsID") %>'
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
                                EnableClientScript="False" ValidationGroup="vgShowMsg" />
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
                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                                meta:resourcekey="btnAddResource1"></asp:LinkButton>

                            <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit"
                                OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                                meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"   OnClick="btnSave_Click"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                ValidationGroup="vgSave"   meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                                OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid">
                            </asp:CustomValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblVtpName" runat="server" Text="Vacation Type :" meta:resourcekey="lblVtpNameResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlVtpType" runat="server" Enabled="False" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlVtpType_SelectedIndexChanged" meta:resourcekey="ddlVtpTypeResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvVtpType" runat="server" ControlToValidate="ddlVtpType" CssClass="CustomValidator"
                                EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Vacation Type is required!' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rfvVtpTypeResource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled"
                                Enabled="False" Visible="false" Width="15px"></asp:TextBox>

                            <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled"
                                Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblMaxDays" runat="server" Text="Max Days:" meta:resourcekey="lblMaxDaysResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtMaxDayes" runat="server" Enabled="False" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtMaxDayesResource1"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtMaxDayes"
                                EnableClientScript="False" ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator"
                                ValidationGroup="vgSave" meta:resourcekey="RegularExpressionValidator7Resource1"><img src="../images/Exclamation.gif" 
                                                   title="Enter Only Numbers!" /></asp:RegularExpressionValidator>
                             <asp:CustomValidator ID="cvMaxDays" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Max Days is required!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="MaxDaysValidate_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvMaxDaysResource1"></asp:CustomValidator>
                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1" />
                            <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1" />

                            <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay"
                                meta:resourcekey="lnkShow1Resource1" />
                            <div id="pnlInfo1" class="flyOutDiv">
                                <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                    CssClass="flyOutDivCloseX glyphicon glyphicon-remove" meta:resourcekey="lnkClose1Resource1" />
                                <p>
                                    <br />
                                    <asp:Label ID="lblHint1" runat="server" meta:resourcekey="lblHint1Resource1" Text="enter number of maximum days for vacation"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="col1">
                           
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
