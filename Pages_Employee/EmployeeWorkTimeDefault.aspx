<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeWorkTimeDefault.aspx.cs" Inherits="EmployeeWorkTimeDefault"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <%--script--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div runat="server" id="MainTable">
        <div class="row">

                        <div class="col2">
                                    <asp:Label ID="lblIDFilter" runat="server" Text="Search By:" meta:resourcekey="lblIDFilterResource1"></asp:Label>
                                  </div>
                        <div class="col3">
                                    <asp:DropDownList ID="ddlFilter" runat="server"  meta:resourcekey="ddlFilterResource1">
                                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                                        <asp:ListItem Text="Worktime Name (Ar)" Value="WktNameAr" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                        <asp:ListItem Text="Worktime Name (En)" Value="WktNameEn" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                        <div class="col3">
                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtFilterResource1"></asp:TextBox>
                                    </div>
                        <div class="col3">
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png"
                                        meta:resourcekey="btnFilterResource1" />
                                     </div>
            </div>
            <div class="row">
                <div class="col12">
                                            <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                                                NextRowSelectKey="Add" PrevRowSelectKey="Subtract" />
                                            <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                AutoGenerateColumns="False" AllowPaging="True" 
                                                GridLines="None" DataKeyNames="EwrID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                                                OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                                                OnPreRender="grdData_PreRender"   EnableModelValidation="True" meta:resourcekey="grdDataResource1">
                                                
                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                                    LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                                    NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                                <Columns>

                                                    <asp:BoundField HeaderText="Worktime Name (Ar)" DataField="WktNameAr" SortExpression="WktNameAr"
                                                        InsertVisible="False" ReadOnly="True" meta:resourcekey="BoundFieldResource6">
                                                        <HeaderStyle CssClass="first" />
                                                        <ItemStyle CssClass="first" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WktNameEn" HeaderText="Worktime Name (En)" SortExpression="WktNameEn"
                                                        meta:resourcekey="BoundFieldResource7" />
                                                    <asp:BoundField DataField="EwrID" HeaderText="Ewr ID" SortExpression="EwrID" meta:resourcekey="BoundFieldResource2" />

                                                    <asp:BoundField DataField="EwrWrkDefaultForAll" HeaderText="Default for all" SortExpression="EwrWrkDefaultForAll"
                                                        meta:resourcekey="BoundFieldResource8" />
                                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource3">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" Enabled="False" CommandName="Delete1" CommandArgument='<%# Eval("EwrID") %>'
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
                                    &nbsp;
                                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="txtValid">
                                    </asp:CustomValidator>
                             </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblWorktime" runat="server" Text="Work Time :" meta:resourcekey="lblWorktimeResource1"></asp:Label>
                                 </div>
                        <div class="col4">
                                    <asp:DropDownList ID="ddlWktID" runat="server" Enabled="False" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlWktID_SelectedIndexChanged" meta:resourcekey="ddlWktIDResource1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlWktID" runat="server" ControlToValidate="ddlWktID" CssClass="CustomValidator"
                                        EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;"
                                        ValidationGroup="vgSave" meta:resourcekey="rfvddlWktIDResource1"></asp:RequiredFieldValidator>
                              </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblWorkingdays" runat="server" Text="Working days :" meta:resourcekey="lblWorkingdaysResource1"></asp:Label>
                               </div>
                        <div class="col4">
                            <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrSat" runat="server" Enabled="False" Text="Saturday" meta:resourcekey="chkEwrSatResource1" />
                                   </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrSun" runat="server" Enabled="False" Text="Sunday" meta:resourcekey="chkEwrSunResource1" />
                                   </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrMon" runat="server" Enabled="False" Text="Monday" meta:resourcekey="chkEwrMonResource1" />
                                   </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrTue" runat="server" Enabled="False" Text="Tuesday" meta:resourcekey="chkEwrTueResource1" />
                                    </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrWed" runat="server" Enabled="False" Text="Wednesday" meta:resourcekey="chkEwrWedResource1" />
                                    </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrThu" runat="server" Enabled="False" Text="Thursday" meta:resourcekey="chkEwrThuResource1" />
                                   </span>  <span class="form-inline">
                                    <asp:CheckBox ID="chkEwrFri" runat="server" Enabled="False" Text="Friday" meta:resourcekey="chkEwrFriResource1" />
                                       </span>
                                    <asp:CustomValidator ID="cvSelectWorkDays" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Select Work Days!' /&gt;"
                                        ValidationGroup="vgSave" ErrorMessage="Select Work Days" OnServerValidate="SelectWorkDays_ServerValidate" CssClass="CustomValidator"
                                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvSelectWorkDaysResource1"></asp:CustomValidator>
                                 </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                             </div>
                        <div class="col4">
                                    <asp:CheckBox ID="chkIsForAll" runat="server" Enabled="False" Text="Worktime is Default for All employees"
                                        meta:resourcekey="chkIsForAllResource1" />
                                  
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow5" runat="server" TargetControlID="lnkShow5" />
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose5" runat="server" TargetControlID="lnkClose5" />
                                    <asp:ImageButton ID="lnkShow5" runat="server" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay"
                                        OnClientClick="return false;" />
                                    <div id="pnlInfo5" class="flyOutDiv">
                                        <asp:LinkButton ID="lnkClose5" runat="server" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" OnClientClick="return false;"
                                            Text="X" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint5" runat="server" meta:resourcekey="lblHint5Resource1" Text="By selecting this option will worktime as default for all employees even those who had worktime previously and ended"></asp:Label>
                                        </p>
                                    </div>
                                  </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                                    <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled"
                                        Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                               </div>
                        </div>
                
            </div> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
