<%@ Page Title="Employee Request" Language="C#" Culture="auto" UICulture="auto"
    MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeRequest.aspx.cs" Inherits="EmployeeRequest" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReqFile" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="vgShowMsg" />
                </div>
            </div>

            <div class="row">
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server"
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:"
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server"
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png"  
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <span class="h3">
                        <asp:Literal ID="LitEmpName" runat="server" Text="Employee name"
                            meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </span>
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender"
                        TargetControlID="grdData" />
                    <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                        AutoGenerateColumns="False" AllowPaging="True"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="ErqID" ShowFooter="True"
                        OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                        OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                        OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                        EnableModelValidation="True"
                        meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Type" SortExpression="RetID2"
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%# GrdDisplayType(Eval("RetID"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ErqID" HeaderText="ErqID" SortExpression="ErqID"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="RetID" HeaderText="RetID" SortExpression="RetID"
                                Visible="False" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="ErqTypeID" HeaderText="ErqTypeID"
                                SortExpression="ErqTypeID" Visible="False"
                                meta:resourcekey="BoundFieldResource3" />

                            <asp:TemplateField HeaderText="Name (Ar)" SortExpression="NameAr"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <%# GrdDisplayReqType(Eval("RetID"), Eval("ErqTypeID"), "AR")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name (En)" SortExpression="NameEn"
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <%# GrdDisplayReqType(Eval("RetID"), Eval("ErqTypeID"), "EN")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Date" SortExpression="ErqStartDate"
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ErqStartDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="ErqEndDate"
                                meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayDate(Eval("ErqEndDate"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time" SortExpression="ErqStartTime"
                                meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ErqStartTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time" SortExpression="ErqEndTime"
                                meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <%# DisplayFun.GrdDisplayTime(Eval("ErqEndTime"))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ErqReason" HeaderText="Reason"
                                SortExpression="ErqReason" meta:resourcekey="BoundFieldResource4" />

                            <asp:BoundField DataField="ErqReqStatus" HeaderText="Status"
                                SortExpression="ErqReqStatus" meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField HeaderText="Status"
                                meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnStatus" CommandName="Status_Command" CommandArgument='<%# Eval("ErqReqStatus") %>'
                                        runat="server" ImageUrl="~/images/ERS_Images/approve.png" Enabled="False"
                                        meta:resourcekey="imgbtnStatusResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="           "
                                meta:resourcekey="TemplateFieldResource9">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete_Command" CommandArgument='<%# Eval("ErqID") %>'
                                        runat="server" ImageUrl="../images/Button_Icons/button_delete.png"
                                        meta:resourcekey="imgbtnDeleteResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                            NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                            PreviousPageImageUrl="~/images/prev.png" />

                    </asp:GridView>
                </div>
            </div>
            <div class="row" runat="server" id="divVacType">
                <div class="col1">
                    <asp:Label ID="lblVacType" runat="server" Text="Vacation Type :"
                        meta:resourcekey="lblVacTypeResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlVacType" runat="server"
                        meta:resourcekey="ddlVacTypeResource1" Enabled="False">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblVacHospitalType" runat="server" Text="Hospital Type :" Visible="false" meta:resourcekey="lblVacHospitalTypeResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlVacHospitalType" runat="server"
                        Visible="false" meta:resourcekey="ddlVacHospitalTypeResource1" Enabled="False">
                        <asp:ListItem Text="-Select Hospital Type-" Value="N" meta:resourcekey="liSelectHospitalTypeResource"></asp:ListItem>
                        <asp:ListItem Text="Government Hospital" Value="GH" meta:resourcekey="liGHResource"></asp:ListItem>
                        <asp:ListItem Text="Private Hospital" Value="PH" meta:resourcekey="liPHResource"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" runat="server" id="divExcType">
                <div class="col1">
                    <asp:Label ID="lblExcType" runat="server" Text="Excuse Type :"
                        meta:resourcekey="lblExcTypeResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlExcType" runat="server"
                        meta:resourcekey="ddlExcTypeResource1" Enabled="False">
                    </asp:DropDownList>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtGapOrOVTID" runat="server" AutoCompleteType="Disabled"
                        Enabled="False" Visible="False"
                        meta:resourcekey="txtGapOrOVTIDResource1"></asp:TextBox>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                        Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="row" runat="server" id="divType">
                <div class="col1">
                    <asp:Label ID="lblType" runat="server" Text="Type :"
                        meta:resourcekey="lblTypeResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:TextBox ID="txtType" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtTypeResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row" runat="server" id="divDate">
                <div class="col1">
                    <asp:Label ID="lblStartDate" runat="server"
                        meta:resourcekey="lblStartDateResource1">Start Date :</asp:Label>
                </div>
                <div class="col2">
                    <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                </div>
                </div>
                <div runat="server" id="divEndDate" class="row">
                    <div class="col1">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :"
                            meta:resourcekey="lblEndDateResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                    </div>
                </div>
            


            <div runat="server" id="divEmp2">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblEmpID2" runat="server" Text="Swap With Employee ID:"
                            meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpID2Resource1" Enabled="False"></asp:TextBox>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :"
                            Visible="False" meta:resourcekey="lblEmpNameResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" Visible="False"
                            meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblStartDate2" runat="server" Text="Start Date :"
                            meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" />
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblEndDate2" runat="server" Text="End Date :"
                            meta:resourcekey="lblEndDate2Resource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <Cal:Calendar2 ID="calEndDate2" runat="server" CalendarType="System" />
                    </div>
                </div>
            </div>

            <div runat="server" id="divWorkTime">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblWorkTimeName" runat="server" Text="Worktime Name :"
                            meta:resourcekey="lblWorkTimeNameResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtWorkTimeName" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"   meta:resourcekey="txtWorkTimeNameResource1"></asp:TextBox>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblWorkTimeID" runat="server" Text="Worktime ID :"
                            meta:resourcekey="lblWorkTimeIDResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtWorkTimeID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"   meta:resourcekey="txtWorkTimeIDResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblShiftName" runat="server" Text="Shift Name :"
                            meta:resourcekey="lblShiftNameResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtShiftName" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"   meta:resourcekey="txtShiftNameResource1"></asp:TextBox>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblShiftID" runat="server" Text="Shift ID :"
                            meta:resourcekey="lblShiftIDResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtShiftID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"   meta:resourcekey="txtShiftIDResource1"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div runat="server" id="divTimeFrom">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblTimeFrom" runat="server" Text="Time from :" meta:resourcekey="lblTimeFromResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <Almaalim:TimePicker ID="tpFrom" runat="server" FormatTime="HHmmss"
                            CssClass="TimeCss" meta:resourcekey="tpFromResource1" Enabled="False" />
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblTimeTo" runat="server" Text="Time To :" meta:resourcekey="lblTimeToResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <Almaalim:TimePicker ID="tpTo" runat="server" FormatTime="HHmmss"
                            CssClass="TimeCss" meta:resourcekey="tpToResource1" Enabled="False" />
                    </div>
                </div>
            </div>


            <div runat="server" id="divFileReq">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblReqFile" runat="server" Text="Request File :"
                            meta:resourcekey="lblReqFileResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:Button ID="btnReqFile" runat="server" Text="Download" CssClass="buttonBG"
                             
                            meta:resourcekey="btnReqFileResource1" OnClick="btnReqFile_Click" />
                    </div>
                    <div class="col1">
                    </div>
                    <div class="col2">
                        <asp:TextBox ID="txtReqFile" runat="server" AutoCompleteType="Disabled"
                            Enabled="False"   Visible="False"
                            meta:resourcekey="txtReqFileResource1"></asp:TextBox></td>
                    </div>
                </div>
            </div>

            <div runat="server" id="divReason">
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblDesc" runat="server" Text="Request Reason :"
                            meta:resourcekey="lblDescResource1"></asp:Label>
                    </div>
                    <div class="col5">
                        <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                            TextMode="MultiLine"   Enabled="False"
                            meta:resourcekey="txtDescResource1"></asp:TextBox>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





