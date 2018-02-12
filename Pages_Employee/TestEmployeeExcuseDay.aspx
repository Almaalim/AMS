<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="TestEmployeeExcuseDay.aspx.cs" Inherits="Pages_Employee_TestEmployeeExcuseDay" %>



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
                   <%-- <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <asp:GridView ID="grdData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BorderWidth="0px" CellPadding="0" CssClass="datatable" DataKeyNames="ExrID" ShowFooter="True"
                        GridLines="None" OnPageIndexChanging="grdData_PageIndexChanging" OnRowCommand="grdData_RowCommand"
                        OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnSorting="grdData_Sorting"  OnRowCreated="grdData_RowCreated"
                        OnPreRender="grdData_PreRender" meta:resourcekey="grdDataResource1">

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
                            <asp:TemplateField HeaderText="Date" SortExpression="ExrStartDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ExrStartDate"))%>
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
                            <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ExrID") %>'
                                        CommandName="Delete1" Enabled="False" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>        --%>       
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender ID="gridviewextender" runat="server" TargetControlID="grdData"/>
                    <AM:GridView  ID="grdData" runat="server" BorderWidth="0px" CellPadding="0" CssClass="datatable" GridLines="None" 
                        AutoGenerateColumns="False" AllowSorting="True"  AllowPaging="True"  DataKeyNames="ExrID" ShowFooter="True"
                        
                        DataSourceID="odsGrdData"
                        OnDataBound="grdData_DataBound"  
                        OnRowCreated="grdData_RowCreated" 
                        OnRowCommand="grdData_RowCommand"
                        OnRowDataBound="grdData_RowDataBound" 
                        OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnSorting="grdData_Sorting" 
                        OnPreRender="grdData_PreRender"> 
                    
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
                            <asp:TemplateField HeaderText="Date" SortExpression="ExrStartDate" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ExrStartDate"))%>
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
                            <asp:Parameter Name="CacheKey" Direction="Input" DefaultValue="ExcuseDay" />
                            <asp:Parameter Name="DataID" Direction="Input" DefaultValue="EmployeeExcuseDayRelInfoView" />
                            <asp:Parameter Name="sortID" Direction="Input" DefaultValue="ExrID" />
                            <asp:Parameter Name="DT" Direction="Output" DefaultValue="" Type="Object" />
                        </SelectParameters>                                            
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSearchCriteria" runat="server" />
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
                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="GenButton  glyphicon glyphicon-plus-sign"  OnClick="btnAdd_Click"
                        Text="&lt;img src=&quot;../images/Button_Icons/button_add.png&quot; /&gt; Add"
                        meta:resourcekey="btnAddResource1"></asp:LinkButton>

                    <asp:LinkButton ID="btnModify" runat="server" CssClass="GenButton glyphicon glyphicon-edit" Visible="False"
                         OnClick="btnModify_Click" Text="&lt;img src=&quot;../images/Button_Icons/button_edit.png&quot; /&gt; Modify"
                        meta:resourcekey="btnModifyResource1"></asp:LinkButton>

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
                        <asp:TextBox ID="txtEmpID" runat="server" autocomplete="off" Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        <asp:Panel runat="server" ID="pnlauID" meta:resourcekey="pnlauIDResource1" />
                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="auID" TargetControlID="txtEmpID"
                            ServicePath="~/Service/AutoComplete.asmx" ServiceMethod="GetEmployeeIDList" MinimumPrefixLength="1"
                            OnClientItemSelected="AutoCompleteID_txtEmpID_ItemSelected" CompletionListElementID="pnlauID"
                            CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionSetCount="12"
                            DelimiterCharacters="" Enabled="True" />
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
                        <asp:RequiredFieldValidator ID="rvExcType" runat="server" ControlToValidate="ddlExcType"
                            EnableClientScript="False" Text="&lt;img src='../images/Exclamation.gif' title='Excuse Type is required' /&gt;"
                            ValidationGroup="vgSave" meta:resourcekey="rfvddlExcTypeResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartDate" runat="server" Text="Date:" meta:resourcekey="lblStartDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" InitialValue="true" />
                    </div>
                    <div class="col2"></div>
                    <div class="col4"></div>
                </div>
                <div class="row">
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblStartTime" runat="server" Text="Start Time:" meta:resourcekey="lblStartTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpStartTime" runat="server" FormatTime="HHmm" TimePickerValidationGroup="vgSave"/>
                    </div>
                    <div class="col2">
                        <span class="RequiredField">*</span>
                        <asp:Label ID="lblEndTime" runat="server" Text="End Time:" meta:resourcekey="lblEndTimeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Almaalim:TimePicker ID="tpEndTime" runat="server" FormatTime="HHmm" TimePickerValidationGroup="vgSave"/>
                        
                        <asp:CustomValidator ID="cvExcuseTime" runat="server" Text="&lt;img src='../images/message_exclamation.png' title='Enter correct Excuse Time' /&gt;"
                            ErrorMessage="Enter correct Excuse Time" ValidationGroup="vgSave" OnServerValidate="ExcuseTimeValidate_ServerValidate" CssClass="CustomValidator"
                            EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvExcuseTimeResource1"></asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col2">
                        <asp:Label ID="lblDesc" runat="server" Text="Description:" meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="60px" Width="380px"
                            meta:resourcekey="txtDescResource1"></asp:TextBox>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled" Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
