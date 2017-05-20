<%@ Page Title="Holiday" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Holidays.aspx.cs" Inherits="Holidays" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:"
                        meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server"  
                        meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Holiday Name (Ar)" Value="HolNameAr"
                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Holiday Name (En)" Value="HolNameEn"
                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled"
                        meta:resourcekey="txtFilterResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFilterResource1" />


                </div>
            </div>

            <div class="row">
                <div class="col12">

                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="HolID" ShowFooter="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True"
                        meta:resourcekey="grdAppuserResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="Name (Ar)" DataField="HolNameAr"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="HolID" HeaderText="ID"
                                meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="HolNameEn" HeaderText="Name (En)"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:TemplateField HeaderText="Start Date"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("HolStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("HolEndDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="HolDesc"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" CommandArgument='<%# Eval("HolID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
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

                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" 
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton  glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                        
                        Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                        meta:resourcekey="btnCancelResource1"></asp:LinkButton>

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
                    <div class="col2">
                        <span id="spnNameAr" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameAr" runat="server" Text="Name (Ar) :"
                            meta:resourcekey="lblHolNameArResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtArHolNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameAr" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Name (Ar) is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvArHolNameResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnNameEn" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblNameEn" runat="server" Text="Name (En) :"
                            meta:resourcekey="lblHolNameEnResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" meta:resourcekey="txtEnHolNameResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvNameEn" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                            EnableClientScript="False" OnServerValidate="NameValidate_ServerValidate"
                            Text="&lt;img src='../images/Exclamation.gif' title='Name (En) is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvEnHolNameResource1"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnHolStartDate" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblHolidyStartDate" runat="server" Text="Start Date :" meta:resourcekey="lblDateStartResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate" />
                    </div>
                    <div class="col2">
                        <span id="spnHolEndDate" runat="server" visible="False" class="RequiredField">*</span>
                        <asp:Label ID="lblHolidyEndDate" runat="server" Text="End Date :" meta:resourcekey="lblDateEndResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblHolidyDesc" runat="server" Text="Description :"
                            meta:resourcekey="lblHolidyDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtHolidyDesc" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine"
                            Enabled="False" meta:resourcekey="txtHolidyDescResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkHolIsActive" runat="server" Text="Active" Enabled="False"
                            meta:resourcekey="chkHolIsActiveResource1" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                        </td>
                            </tr>
                                   
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

