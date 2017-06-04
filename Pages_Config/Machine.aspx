<%@ Page Title="Machine" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Machine.aspx.cs" Inherits="Machine" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Machine Location (Ar)" Value="MacLocationAr" meta:resourcekey="ListItemResource22"></asp:ListItem>
                        <asp:ListItem Text="Machine Location (En)" Value="MacLocationEn" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Machine Number" Value="MacNo" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>
                    &nbsp;
                                    <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                                        meta:resourcekey="btnFilterResource1" OnClick="btnFilter_Click" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                        GridLines="None" DataKeyNames="MacID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                        OnSorting="grdData_Sorting" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True" meta:resourcekey="grdAppuserResource1">

                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                        <Columns>
                            <asp:BoundField HeaderText="Machine Type" InsertVisible="False" ReadOnly="True" DataField="MtpName"
                                meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MacID" HeaderText="Machine ID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField HeaderText="Location Ar" DataField="MacLocationAr" meta:resourcekey="BoundFieldResourceAr" />
                            <asp:BoundField DataField="MacLocationEn" HeaderText="Location En" meta:resourcekey="BoundFieldResourceEn" />
                            <asp:BoundField DataField="MacInstallDate" HeaderText="" Visible="false" />
                            <asp:TemplateField HeaderText="Install Date" meta:resourcekey="BoundFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("MacInstallDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText=" Install Date" DataField="MacInstallDate" meta:resourcekey="BoundFieldResource4" />--%>
                            <asp:BoundField DataField="MacNo" HeaderText="Number" meta:resourcekey="BoundFieldResource5"
                                Visible="False" />
                            
                            
                            <asp:TemplateField HeaderText="Status" SortExpression="MacStatus" meta:resourcekey="BoundFieldResource6">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayStatus(Eval("MacStatus"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("MacID") %>'
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign" OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" OnClick="btnModify_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="vgSave" CssClass="GenButton  glyphicon glyphicon-floppy-disk"
                        OnClick="btnSave_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
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
                        <span id="spnMacType" runat="server" class="RequiredField">*</span>
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1">Machine Type :</asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlMacType" runat="server" Enabled="False" meta:resourcekey="ddlMacTypeResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvMacType" runat="server" ControlToValidate="ddlMacType"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Machine Type is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvMacTypeResource1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col2">
                        <span id="Span2" runat="server" class="RequiredField">*</span>
                        <asp:Label ID="lblMacTrnType" runat="server" meta:resourcekey="MacTrnTypeResource">Machine Transaction Type :</asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlMacTrnType" runat="server" Enabled="False">
                            <asp:ListItem Text="Select Type" Value="2" meta:resourcekey="ddlMacTypeResource1"></asp:ListItem>
                            <asp:ListItem Value="1">IN</asp:ListItem>
                            <asp:ListItem Value="0">OUT</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="rfvMacTrnType" runat="server" ControlToValidate="ddlMacTrnType"
                                        EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Machine Transaction Type is required!' /&gt;"
                                        ValidationGroup="vgSave" meta:resourcekey="rfvMacTrnTypeResource1"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="Active" meta:resourcekey="chkStatusResource1" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkIsRound" runat="server" Enabled="False" Text="Round Patrol Device"
                            AutoPostBack="True" OnCheckedChanged="chkIsRound_CheckedChanged" meta:resourcekey="chkIsRoundResource1" />
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
                                <asp:Label ID="lblHint1" runat="server" Text="Enable Portable attendance device option"
                                    meta:resourcekey="lblRoundResource"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span id="spnMacLocAr" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="Label1" meta:resourcekey="lblMacLocArResource" runat="server" Text=" Location (Ar) :"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMacLocAr" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"></asp:TextBox>
                        <asp:CustomValidator ID="cvMacLocAr" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="MachineValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvMacLocationResource1"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span id="spnMacLocEn" runat="server" visible="false" class="RequiredField">*</span>
                        <asp:Label ID="lblMachineLocation1" meta:resourcekey="lblMacLocEnResource" runat="server"
                            Text=" Location (En) :"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtMacLocEn" runat="server" AutoCompleteType="Disabled"  
                            Enabled="False"></asp:TextBox>
                        <asp:CustomValidator ID="cvMacLocEn" runat="server" ControlToValidate="txtValid"
                            EnableClientScript="False" OnServerValidate="MachineValidate_ServerValidate" CssClass="CustomValidator"
                            Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="cvMacLocationResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span id="spnMacIp" runat="server" class="RequiredField">*</span>
                        <asp:Label ID="lblMachineIP6" runat="server" Text=" IP :" meta:resourcekey="lblMachineIP6Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                        
                                    <asp:TextBox ID="txtMachineIP" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtMachineIPResource1"></asp:TextBox>
                                 
                                    <asp:CustomValidator ID="cvMacIP" runat="server" ControlToValidate="txtValid" CssClass="CustomValidator"
                                        EnableClientScript="False" OnServerValidate="MachineValidate_ServerValidate"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                                        ValidationGroup="vgSave" meta:resourcekey="cvMacIPResource1"></asp:CustomValidator>
                                 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMachineIP"
                                        EnableClientScript="False" ErrorMessage=" Enter correct IP" ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                        Text="&lt;img src='../images/message_exclamation.png' title='Enter correct IP' /&gt;" CssClass="CustomValidator"
                                        ValidationGroup="vgSave" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                 
                    </div>

                    <div class="col2">
                        <span id="spnMacPort" runat="server" class="RequiredField">*</span>
                        <asp:Label ID="lblMachinePort3" runat="server" Text="Port :" meta:resourcekey="lblMachinePort3Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                         
                                    <asp:TextBox ID="txtMachinePort" runat="server" AutoCompleteType="Disabled" Enabled="False" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtMachinePortResource1"></asp:TextBox>
                                
                                    <asp:CustomValidator ID="cvMacPort" runat="server" ControlToValidate="txtValid"
                                        EnableClientScript="False" OnServerValidate="MachineValidate_ServerValidate" CssClass="CustomValidator"
                                        Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                                        ValidationGroup="vgSave" meta:resourcekey="cvMacPortResource1"></asp:CustomValidator>
                                
                                    <span class="requiredStar">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtMachineNo"
                                            EnableClientScript="False" ErrorMessage=" Enter Only Numbers for port" ValidationExpression="^\d+$" CssClass="CustomValidator"
                                            ValidationGroup="vgSave" meta:resourcekey="RegularExpressionValidator8Resource1"><img src="../images/Exclamation.gif" 
                                                    title="Enter Only Numbers for port!" /></asp:RegularExpressionValidator>
                                    </span>
                                
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblMachineNo2" runat="server" Text="No :" meta:resourcekey="lblMachineNo2Resource1"></asp:Label>
                    </div>
                    <div class="col4">
                         
                                    <asp:TextBox ID="txtMachineNo" runat="server" AutoCompleteType="Disabled" Enabled="False"
                                        MaxLength="2" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtMachineNoResource1"></asp:TextBox>
                               
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtMachineNo"
                                        EnableClientScript="False" ErrorMessage=" Enter Only Numbers" ValidationExpression="^\d+$" CssClass="CustomValidator" 
                                        ValidationGroup="vgSave" meta:resourcekey="RegularExpressionValidator7Resource1"><img src="../images/Exclamation.gif" 
                                                    title="Enter Only Numbers!" /></asp:RegularExpressionValidator>
                               
                                    <asp:CustomValidator ID="cvMachineNo" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Work Type is required!' /&gt;"
                                        ValidationGroup="vgSave" OnServerValidate="MachineValidate_ServerValidate" EnableClientScript="False" CssClass="CustomValidator"
                                        ControlToValidate="txtValid" meta:resourcekey="cvMachineNoResource1"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span id="Span1" runat="server" class="RequiredField" visible="true">*</span>
                        <asp:Label ID="lblMachineInsDate3" runat="server" meta:resourcekey="lblDateStartResource1"
                            Text="Installation Date :"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calMachineInsDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                    </div>
                </div>
                </div>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
