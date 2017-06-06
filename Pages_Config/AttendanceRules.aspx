<%@ Page Title="Attendance Rules" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true"
    CodeFile="AttendanceRules.aspx.cs" Inherits="AttendanceRules" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <%--script--%>
    
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col12">
                                <asp:ValidationSummary runat="server" ID="vsMSave" ValidationGroup="vgMSave" EnableClientScript="False"
                                    CssClass="MsgValidation" ShowSummary="False" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col1">
                                <asp:Label ID="lblRuleSetNew" runat="server" Text="RuleSet(Add new):" meta:resourcekey="lblRuleSetNewResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:TextBox ID="txtRuleSet" runat="server" AutoCompleteType="Disabled"
                                    meta:resourcekey="txtRuleSetResource1"></asp:TextBox>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1"></ajaxToolkit:AnimationExtender>
                                <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1"></ajaxToolkit:AnimationExtender>
                                <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay"  />
                                <div id="pnlInfo1" class="flyOutDiv">
                                    <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;" CssClass="GenButton1 glyphicon glyphicon-remove" />
                                     
                                        <asp:Label ID="lblHint1" runat="server" Text="Ruleset can be ttached to employee by adding rules to it" meta:resourcekey="lblHintResource"></asp:Label>
                                   
                                </div>
                                <asp:CustomValidator ID="cvRuleSet" runat="server" Text="&lt;img src='../images/Exclamation.gif' title='Enter Ruleset!' /&gt;"
                                    ValidationGroup="vgMSave" OnServerValidate="RuleSet_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid"
                                    meta:resourcekey="cvRuleSetResource11"></asp:CustomValidator>
                            </div>
                            <div class="col2">
                                <asp:LinkButton ID="btnSaveRuleSet" runat="server" CssClass="GenButton1 glyphicon glyphicon-floppy-disk" OnClick="btnSaveRuleSet_Click" ValidationGroup="vgMSave"
                                    Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Save "
                                    meta:resourcekey="btnSaveRuleSetResource1"></asp:LinkButton>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col1">
                                <asp:Label ID="lblRuleSetSelect" runat="server" Text="Select Rule Set:" meta:resourcekey="lblRuleSetSelectResource1"></asp:Label>
                            </div>
                            <div class="col2">
                                <asp:DropDownList ID="ddlRuleSet" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlRuleSet_SelectedIndexChanged" meta:resourcekey="ddlRuleSetResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rvRuleSet" ControlToValidate="ddlRuleSet" CssClass="CustomValidator"
                                    EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Please select ddlRuleSet!' />"
                                    ValidationGroup="vgSave" meta:resourcekey="rfvRuletypeResource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col2">
                                <asp:LinkButton ID="btnDeleteRuleSet" runat="server" CssClass="GenButton1 glyphicon glyphicon-remove" OnClick="btnDeleteRuleSet_Click"
                                    Text="&lt;img src=&quot;../images/Button_Icons/button_delete.png&quot; /&gt; Delete "
                                    meta:resourcekey="btnDeleteRuleSetResource1"
                                    OnClientClick="return confirm('Are you sure you want to delete');"></asp:LinkButton>
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
                                <div>
                                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                        AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" CellPadding="0"
                                        BorderWidth="0px" GridLines="None" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                        OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound" OnSorting="grdData_Sorting"
                                        OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowCommand="grdData_RowCommand"
                                        OnPreRender="grdData_PreRender" EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                            LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                            NextPageImageUrl="~/images/next.png" PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                        <Columns>
                                            <asp:BoundField DataField="RltType" HeaderText="Rule Type" InsertVisible="False"
                                                ReadOnly="True" meta:resourcekey="BoundFieldResource1">
                                                <HeaderStyle CssClass="first" />
                                                <ItemStyle CssClass="first" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RltRuleMeasureIn" HeaderText="Rule Measure In" meta:resourcekey="BoundFieldResource2" Visible="false" />
                                            <asp:TemplateField HeaderText="Rule Measure In" SortExpression="RuleMeasureIn" meta:resourcekey="BoundFieldResource2">
                                                <ItemTemplate>
                                                    <%# GrdDisplayMeasure(Eval("RltRuleMeasureIn"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="RltUnits" HeaderText="Rule Units" meta:resourcekey="BoundFieldResource3" />
                                            <asp:BoundField DataField="RltFrequency" HeaderText="Rule Frequency" meta:resourcekey="BoundFieldResource4" />
                                            <asp:BoundField DataField="RltActionMeasureIn" HeaderText="Action Measure In" meta:resourcekey="BoundFieldResource5" Visible="false" />
                                            <asp:TemplateField HeaderText="Action Measure In" SortExpression="ActionMeasureIn" meta:resourcekey="BoundFieldResource5">
                                                <ItemTemplate>
                                                    <%# GrdDisplayMeasure(Eval("RltActionMeasureIn"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="RltActionUnits" HeaderText="Action Units" meta:resourcekey="BoundFieldResource6" />
                                            <asp:BoundField DataField="RltID" HeaderText="RltID" Visible="False" meta:resourcekey="BoundFieldResource7" />
                                            <asp:TemplateField HeaderText=" " meta:resourcekey="TemplateFieldResource1">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete1" Enabled="False" CommandArgument='<%# Eval("RltID") %>'
                                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png" meta:resourcekey="imgbtnDeleteResource1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
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
                                    meta:resourcekey="btnAddResource1" Enabled="False"></asp:LinkButton>

                                <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                                      ValidationGroup="vgSave" Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                                    meta:resourcekey="btnSaveResource1" Enabled="False"></asp:LinkButton>
                                
                                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle" OnClick="btnCancel_Click"
                                                                             Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel"
                                                                            meta:resourcekey="btnCancelResource1" Enabled="False"></asp:LinkButton>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>
                              
                                                                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                                                            ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                                            EnableClientScript="False" ControlToValidate="txtValid">
                                                                        </asp:CustomValidator>
                            </div>
                        </div>
                        <div class="GreySetion">
                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblRuletype" runat="server" Text="Ruletype :" meta:resourcekey="lblRuletypeResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlRuletype" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlRuletype_SelectedIndexChanged"
                                        meta:resourcekey="ddlRuletypeResource1" Enabled="False">
                                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">Select Rule type</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">BeginLate</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource3">OutEarly</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource4">Gap</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource5">Absent</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvRuletype" ControlToValidate="ddlRuletype" CssClass="CustomValidator"
                                        InitialValue="Select Rule type" EnableClientScript="False" Text="<img src='../images/Exclamation.gif' title='Please select Rule type!' />"
                                        ValidationGroup="vgSave" meta:resourcekey="rfvRuletypeResource1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblRuleMeasureIn" runat="server" Text="Rule Measure In :" meta:resourcekey="lblRuleMeasureInResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlRuleMeasureIn" runat="server"
                                        meta:resourcekey="ddlRuleMeasureInResource1" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span> &nbsp;<asp:Label ID="lblRuleUnits" runat="server"
                                        Text="Rule Units :" meta:resourcekey="lblRuleUnitsResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtRuleUnits" runat="server" AutoCompleteType="Disabled" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtRuleUnitsResource1" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvRuleUnits" ControlToValidate="txtRuleUnits" CssClass="CustomValidator"
                                        Text="<img src='../images/Exclamation.gif' title='RuleUnits is required!' />" ErrorMessage="RuleUnits is required!"
                                        ValidationGroup="vgSave" EnableClientScript="False" Display="Dynamic" SetFocusOnError="True"
                                        meta:resourcekey="rfvRuleUnitsResource1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblFrequency" runat="server" Text="Frequency :" meta:resourcekey="lblFrequencyResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtFrequency" runat="server" AutoCompleteType="Disabled" onkeypress="return OnlyNumber(event);" meta:resourcekey="txtFrequencyResource1" Enabled="False"></asp:TextBox>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2"></ajaxToolkit:AnimationExtender>
                                    <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2"></ajaxToolkit:AnimationExtender>
                                    <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                                    <div id="pnlInfo2" class="flyOutDiv">
                                        <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;" CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                                        <p>
                                            <br />
                                            <asp:Label ID="lblHint2" runat="server" Text="The count for rule units exceeding which rules are applied" meta:resourcekey="lblHintResource2"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtFrequency" CssClass="CustomValidator"
                                        Text="<img src='../images/Exclamation.gif' title='Frequency is required!' />"
                                        ErrorMessage="Frequency is required!" ValidationGroup="vgSave" EnableClientScript="False"
                                        Display="Dynamic" SetFocusOnError="True" meta:resourcekey="rfvActionUnitsResource11"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblActionMeasureIn" runat="server" Text="Action Measure In :" meta:resourcekey="lblActionMeasureInResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:DropDownList ID="ddlActionMeasureIn" runat="server"
                                        meta:resourcekey="ddlActionMeasureInResource1" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                                <div class="col2">
                                    <span class="RequiredField">*</span>
                                    <asp:Label ID="lblActionUnits" runat="server" Text="Action Units :" meta:resourcekey="lblActionUnitsResource1"></asp:Label>
                                </div>
                                <div class="col4">
                                    <asp:TextBox ID="txtActionUnits" runat="server" AutoCompleteType="Disabled" onkeypress="return OnlyNumber(event);"
                                         meta:resourcekey="txtActionUnitsResource1" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvActionUnits" ControlToValidate="txtActionUnits" CssClass="CustomValidator"
                                        Text="<img src='../images/Exclamation.gif' title='ActionUnits is required!' />"
                                        ErrorMessage="ActionUnits is required!" ValidationGroup="vgSave" EnableClientScript="False"
                                        Display="Dynamic" SetFocusOnError="True" meta:resourcekey="rfvActionUnitsResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col12">
                                    <asp:TextBox ID="txtID" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>
