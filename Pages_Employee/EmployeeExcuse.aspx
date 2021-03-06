﻿<%@ Page Title="Employee Excuse" Language="C#" MasterPageFile="~/AMSMasterPage.master"
    AutoEventWireup="true" CodeFile="EmployeeExcuse.aspx.cs" Inherits="EmployeeExcuse" 
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col1">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by:" meta:resourcekey="lblSearchByResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlSearchByResource1">
                        <asp:ListItem Selected="True" meta:resourcekey="ListItemResource1">[None]</asp:ListItem>
                        <asp:ListItem Text="Employee ID" Value="EmpID" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (AR)" Value="EmpNameAr" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Text="Excuse Type (AR)" Value="ExcNameAr" meta:resourcekey="ListItemResource5"></asp:ListItem>
                        <asp:ListItem Text="Excuse Type (En)" Value="ExcNameEn" meta:resourcekey="ListItemResource6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtFilter" runat="server" meta:resourcekey="txtSearchResource1"></asp:TextBox>

                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ImageUrl="../images/Button_Icons/button_magnify.png" CssClass="LeftOverlay"
                        meta:resourcekey="btnFetchDataResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <AM:GridView  ID="grdData" runat="server" BorderWidth="0px" CellPadding="0" CssClass="datatable" GridLines="None" 
                        AutoGenerateColumns="False" AllowSorting="True"  AllowPaging="True"  DataKeyNames="ExrID" ShowFooter="True"
                        EnableModelValidation="True"

                        DataSourceID="odsGrdData"
                        OnDataBound="grdData_DataBound"  
                        OnRowCreated="grdData_RowCreated" 
                        OnRowCommand="grdData_RowCommand"
                        OnRowDataBound="grdData_RowDataBound" 
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnSorting="grdData_Sorting" 
                        OnPreRender="grdData_PreRender"  

                        meta:resourcekey="grdDataResource1"> 
                        
                        <PagerStyle HorizontalAlign="Left" />
                        <PagerSettings FirstPageImageUrl="~/images/first.png" FirstPageText="First" LastPageImageUrl="~/images/last.png"
                            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/next.png"
                            NextPageText="Next" PreviousPageImageUrl="~/images/prev.png" PreviousPageText="Prev" />
                        <Columns>
                            <asp:BoundField DataField="EmpID" HeaderText="ID" SortExpression="EmpID" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="ExrID" HeaderText="ExrID" SortExpression="ExrID" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="EmpNameAr" HeaderText="Name (Ar)" SortExpression="EmpNameAr"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="EmpNameEn" HeaderText="Name (En)" SortExpression="EmpNameEn"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="ExcNameAr" HeaderText="Excuse Type (Ar)" SortExpression="ExcNameAr"
                                meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField DataField="ExcNameEn" HeaderText="Excuse Type (En)" SortExpression="ExcNameEn"
                                meta:resourcekey="BoundFieldResource6" />
                            <asp:BoundField DataField="WktNameAr" HeaderText="Work Time (Ar)" SortExpression="WktNameAr"
                                meta:resourcekey="BoundFieldResource7" />
                            <asp:BoundField DataField="WktNameEn" HeaderText="Work Time (En)" SortExpression="WktNameEn"
                                meta:resourcekey="BoundFieldResource8" />
                            <asp:TemplateField HeaderText="Start Date" SortExpression="ExrStartDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ExrStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="ExrEndDate" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ExrEndDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time" SortExpression="ExrStartTime" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ExrStartTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time" SortExpression="ExrEndTime" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ExrEndTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ExrISOvernight" HeaderText="Is Overnight" SortExpression="ExrISOvernight"
                                                        meta:resourcekey="BoundFieldResource9" />
                                                    <asp:BoundField DataField="ExrIsOverTime" HeaderText="Is OverTime" SortExpression="ExrIsOverTime"
                                                        meta:resourcekey="BoundFieldResource10" />--%>
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ExrID") %>'
                                        CommandName="Delete1" Enabled="False" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </AM:GridView>

                    <asp:ObjectDataSource ID="odsGrdData" runat="server" 
                        TypeName="LargDataGridView.DAL.LargDataGrid" 
                        SelectMethod="GetDataSortedPage" 
                        SelectCountMethod="GetDataCount"  
                        EnablePaging="True" 
                        SortParameterName="sortExpression" OnSelected="odsGrdData_Selected">
                        <SelectParameters>
                            <asp:ControlParameter  ControlID="hfSearchCriteria"  Name="searchCriteria" Direction="Input"  />
                            <asp:ControlParameter ControlID="HfRefresh" Name="Refresh" Direction="Input"  />
                            <asp:Parameter Name="CacheKey" Direction="Input" DefaultValue="ExcusePeriodDay" />
                            <asp:Parameter Name="DataID" Direction="Input" DefaultValue="EmployeeExcusePeriodRelInfoView" />
                            <asp:Parameter Name="sortID" Direction="Input" DefaultValue="ExrID DESC" />
                            <asp:Parameter Name="DT" Direction="Output" DefaultValue="" Type="Object" />
                        </SelectParameters>                                            
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSearchCriteria" runat="server" />
                    <asp:HiddenField ID="HfRefresh" runat="server" />
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

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" Visible="false" OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify" meta:resourcekey="btnModifyResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk" OnClick="btnSave_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save"
                        ValidationGroup="vgSave" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                        OnClick="btnCancel_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
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
                        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :" meta:resourcekey="lblEmpIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtEmpID" runat="server" autocomplete="off" Enabled="False"
                            meta:resourcekey="txtEmpIDResource1" AutoPostBack="True" OnTextChanged="txtEmpID_TextChanged"></asp:TextBox>

                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="auID" TargetControlID="txtEmpID"
                            ServicePath="~/Service/AutoComplete.asmx" ServiceMethod="GetEmployeeIDList" MinimumPrefixLength="1"
                            CompletionInterval="1000" EnableCaching="true" OnClientItemSelected="AutoCompleteID_txtEmpID_ItemSelected"
                            CompletionListElementID="pnlauID" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="12" />
                        
                        <asp:CustomValidator ID="cvEmpID" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                            ValidationGroup="vgSave" OnServerValidate="EmpID_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid"></asp:CustomValidator>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblExcuseType" runat="server" Text="Excuse Type:" meta:resourcekey="lblExcuseTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownListAttributes ID="ddlExcType" runat="server" Enabled="False"></asp:DropDownListAttributes>
                       <%-- <asp:DropDownList ID="ddlExcType" runat="server" meta:resourcekey="ddlExcTypeResource1"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlExcType_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        <asp:RequiredFieldValidator ID="rvExcType" runat="server" ControlToValidate="ddlExcType" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Excuse Type is required' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvddlExcTypeResource1"></asp:RequiredFieldValidator>
                        <%--<asp:CustomValidator ID="cvMaxTimeExc" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='MaxTime!' /&gt;"
                            ValidationGroup="vgSave" ErrorMessage="MaxTimeExcuse!" OnServerValidate="MaxTimeExcuse_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" 
                            meta:resourcekey="cvMaxTimeExcResource1"></asp:CustomValidator>--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblWktID" runat="server" Text="Work Time:" meta:resourcekey="lbllblWktIDResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlWktID" runat="server" meta:resourcekey="ddlWktIDResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlWktID" runat="server" ControlToValidate="ddlWktID" CssClass="CustomValidator"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Work Time is required!' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvddlWktIDResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" meta:resourcekey="lblStartDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calEndDate" />
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" meta:resourcekey="lblEndDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartTime" runat="server" Text="Start Time:" meta:resourcekey="lblStartTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" TimePickerValidationGroup="Users" 
                            CssClass="TimeCss" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='Start Time Excuse is required!' /&gt;"
                            meta:resourcekey="tpStartTimeResource1" />
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEndTime" runat="server" Text="End Time:" meta:resourcekey="lblEndTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpEndTime" runat="server" FormatTime="HHmm" TimePickerValidationGroup="Users"
                            CssClass="TimeCss" TimePickerValidationText="&lt;img src='../images/Exclamation.gif' title='End time Excuse is required!' /&gt;"
                            meta:resourcekey="tpEndTimeResource1" />
                        <asp:CustomValidator ID="cvExcuseTime" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Enter correct Excuse Time' /&gt;"
                            ErrorMessage="Enter correct Excuse Time!" ValidationGroup="vgSave" OnServerValidate="ExcuseTimeValidate_ServerValidate"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShift1TimeResource1" CssClass="CustomValidator"></asp:CustomValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkExrIsOvernight" runat="server" Enabled="False" Text="Is Overnight"
                            meta:resourcekey="chkExrIsOvernightResource1" />
                        <div class="flyoutWrap">
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow1" runat="server" TargetControlID="lnkShow1">
                        </ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose1" runat="server" TargetControlID="lnkClose1">
                        </ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow1" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo1" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose1" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint1" runat="server" Text="This option means that the excuse is located in Midnight period "
                                    meta:resourcekey="lblHint1Resource"></asp:Label>
                            </p>
                        </div>
                            </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:CheckBox ID="chkExrIsOT" runat="server" Enabled="False" Text="Is Overtime" meta:resourcekey="chkExrIsOTResource1" />
                        <div class="flyoutWrap">
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderShow2" runat="server" TargetControlID="lnkShow2">
                        </ajaxToolkit:AnimationExtender>
                        <ajaxToolkit:AnimationExtender ID="AnimationExtenderClose2" runat="server" TargetControlID="lnkClose2">
                        </ajaxToolkit:AnimationExtender>
                        <asp:ImageButton ID="lnkShow2" runat="server" OnClientClick="return false;" ImageUrl="~/images/Hint_Image/HintEN.png" CssClass="LeftOverlay" />
                        <div id="pnlInfo2" class="flyOutDiv">
                            <asp:LinkButton ID="lnkClose2" runat="server" Text="X" OnClientClick="return false;"
                                CssClass="flyOutDivCloseX glyphicon glyphicon-remove" />
                            <p>
                                <br />
                                <asp:Label ID="lblHint2" runat="server" Text="When selected Is Overtime The system will allow the employee to send request an Overtime When he still in shift"
                                    meta:resourcekey="lblHint1Resource"></asp:Label>
                            </p>
                        </div>
                            </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblDesc" runat="server" Text="Description:" meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" meta:resourcekey="txtDescResource1"
                            Height="60px"></asp:TextBox>
                        </div><div class="col2">
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
