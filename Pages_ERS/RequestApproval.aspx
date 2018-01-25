<%@ Page Title="Request Approval" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RequestApproval.aspx.cs" Inherits="RequestApproval" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="TimePickerServerControl" Namespace="TimePickerServerControl" TagPrefix="Almaalim" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagName="Calendar2" TagPrefix="Cal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>
    <link href="../CSS/validationStyle.css" rel="stylesheet" type="text/css" />

    <%--<asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="row">
                <div class="col12">
                    <iframe id="ifrmProgress" runat="server" src="../Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="500px" width="100%"></iframe>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReqFile" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="MainTable">
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgValidation"
                            EnableClientScript="False" ValidationGroup="vgShowMsg" />
                    </div>
                </div>
                <div class="row">
                    <div class="col1">
                        <asp:Label ID="lblFilter" runat="server" Text="Employee ID:"
                            meta:resourcekey="lblIDFilterResource1"></asp:Label>
                    </div>
                    <div class="col2">
                        <asp:DropDownList ID="ddlFilter" runat="server"
                            meta:resourcekey="ddlFilterResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="col1">
                        <asp:Label ID="lblMonth" runat="server" Text="Month:"
                            meta:resourcekey="lblMonthResource1"></asp:Label>
                    </div>
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
                        <asp:ImageButton ID="btnFilter" runat="server"
                            OnClick="btnFilter_Click" CssClass="LeftOverlay"
                            ImageUrl="../images/Button_Icons/button_magnify.png"
                            meta:resourcekey="btnMonthFilterResource1" />
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col12">
                        <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender"
                            TargetControlID="grdData" />


                        <asp:GridView ID="grdData" runat="server" CssClass="datatable"
                            AutoGenerateColumns="False" AllowPaging="True"
                            CellPadding="0" BorderWidth="0px" GridLines="None" DataKeyNames="ErqID" ShowFooter="True"
                            OnPageIndexChanging="grdData_PageIndexChanging" OnRowCreated="grdData_RowCreated"
                            OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanged="grdData_SelectedIndexChanged"
                            OnRowCommand="grdData_RowCommand" OnPreRender="grdData_PreRender"
                            EnableModelValidation="True"
                            meta:resourcekey="grdDataResource1">

                            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                FirstPageImageUrl="~/images/first.png" LastPageText="Last" LastPageImageUrl="~/images/last.png"
                                NextPageText="Next" NextPageImageUrl="~/images/next.png" PreviousPageText="Prev"
                                PreviousPageImageUrl="~/images/prev.png" />
                            <Columns>


                                <asp:BoundField HeaderText="ID" DataField="EmpID" SortExpression="EmpID"
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="ErqID" HeaderText="ErqID" SortExpression="ErqID"
                                    meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="GapOvtID" HeaderText="GapOvtID"
                                    SortExpression="GapOvtID" meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="ErqTypeID" HeaderText="ErqTypeID"
                                    SortExpression="ErqTypeID" meta:resourcekey="BoundFieldResource4" />
                                <asp:BoundField DataField="EmpNameAr" HeaderText="Emp. Name (Ar)"
                                    SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource5" />
                                <asp:BoundField DataField="EmpNameEn" HeaderText="Emp. Name (En)"
                                    SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource6" />

                                <asp:BoundField DataField="RetID" HeaderText="RetID" SortExpression="RetID"
                                    meta:resourcekey="BoundFieldResource7" />

                                <asp:BoundField DataField="ErqStartDate" HeaderText="ErqStartDate"
                                    SortExpression="ErqStartDate" meta:resourcekey="BoundFieldResource8" />
                                <asp:BoundField DataField="ShiftID" HeaderText="ShiftID"
                                    SortExpression="ShiftID" meta:resourcekey="BoundFieldResource9" />
                                <asp:BoundField DataField="ErqStatusTime" HeaderText="ErqStatusTime"
                                    SortExpression="ErqStatusTime" meta:resourcekey="BoundFieldResource10" />

                                <asp:TemplateField HeaderText="Type" SortExpression="RetID2"
                                    meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <%# GrdDisplayType(Eval("RetID"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="ErqReason" HeaderText="Reason"
                                    SortExpression="ErqReason" meta:resourcekey="BoundFieldReasonResource10" />

                                <asp:TemplateField HeaderText="           "
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnOkPaid" CommandName="OkPaid" CommandArgument='<%# Eval("RetID") + "," + Eval("ErqID") + "," + Eval("EmpID") + "," + Eval("GapOvtID") + "," + Eval("ErqTypeID") + "," + Eval("ErqStartDate") + "," + Eval("ShiftID") + "," + Eval("ErqStatusTime") %>'
                                            runat="server" ImageUrl="~/images/ERS_Images/approve.png"
                                            meta:resourcekey="imgbtnOkPaidResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="           "
                                    meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnCancel" CommandName="Rejected" CommandArgument='<%# Eval("RetID") + "," + Eval("ErqID") + "," + Eval("EmpID")  %>'
                                            runat="server" ImageUrl="~/images/ERS_Images/reject.png"
                                            meta:resourcekey="imgbtnCancelResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--+ "," + Eval("GapOvtID") + "," + Eval("ErqTypeID") + "," + Eval("ErqStartDate") + "," + Eval("EmpID") + "," + Eval("ShiftID") + "," + Eval("ErqStatusTime")--%>

                                <asp:BoundField DataField="ErqReqFilePath" HeaderText="ErqReqFilePath"
                                    SortExpression="ErqReqFilePath" meta:resourcekey="BoundFieldResource11" />


                            </Columns>

                        </asp:GridView>

                    </div>
                </div>
                
                <div class="row">
                    <div class="col12">
                        <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                            EnableClientScript="False" CssClass="MsgSuccess"
                            meta:resourcekey="vsumAllResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col12">
                         <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                    Width="10px" meta:resourcekey="txtCustomValidatorResource1"></asp:TextBox>

                                <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                                    ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid">
                                </asp:CustomValidator>
                    </div>
                </div>
                <div class="row" runat="server" id="divVacType">
                    <div class="col2">


                        <asp:Label ID="lblVacType" runat="server" Text="Vacation Type :"
                            meta:resourcekey="lblVacTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlVacType" runat="server"
                            meta:resourcekey="ddlVacTypeResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                        <asp:Label ID="lblVacHospitalType" runat="server" Text="Hospital Type :" Visible="false" meta:resourcekey="lblVacHospitalTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlVacHospitalType" runat="server" Visible="false" meta:resourcekey="ddlVacHospitalTypeResource1">
                            <asp:ListItem Text="-Select Hospital Type-" Value="N" meta:resourcekey="liSelectHospitalTypeResource"></asp:ListItem>
                            <asp:ListItem Text="Government Hospital" Value="GH" meta:resourcekey="liGHResource"></asp:ListItem>
                            <asp:ListItem Text="Private Hospital" Value="PH" meta:resourcekey="liPHResource"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </div>
                </div>

                <div class="row" runat="server" id="divExcType">
                    <div class="col2">
                        <asp:Label ID="lblExcType" runat="server" Text="Excuse Type :"
                            meta:resourcekey="lblExcTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:DropDownList ID="ddlExcType" runat="server"
                            meta:resourcekey="ddlExcTypeResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="col2">
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtGapOrOVTID" runat="server" AutoCompleteType="Disabled"
                            Enabled="False" Visible="False"
                            meta:resourcekey="txtGapOrOVTIDResource1"></asp:TextBox>
                    </div>
                </div>

                <div runat="server" id="divType" class="row">
                    <div class="col2">
                        <asp:Label ID="lblType" runat="server" Text="Type :"
                            meta:resourcekey="lblTypeResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <asp:TextBox ID="txtType" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtTypeResource1"></asp:TextBox>
                    </div>
                </div>

                <div runat="server" id="divDate" class="row">
                    <div class="col2">
                        <asp:Label ID="lblStartDate" runat="server"
                            meta:resourcekey="lblStartDateResource1">Start Date :</asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                    </div>
                </div>

                <div runat="server" id="divEndDate" class="row">
                    <div class="col2">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :"
                            meta:resourcekey="lblEndDateResource1"></asp:Label>
                    </div>
                    <div class="col4">
                        <Cal:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                    </div>
                </div>



                <div runat="server" id="divEmp2">
                    <div class="row">
                        <div class="col2">
                            <asp:Label ID="lblEmpID2" runat="server" Text="Swap With Employee ID:"
                                meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpID2Resource1"></asp:TextBox>
                        </div>
                        <div class="col2">
                            <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :"
                                meta:resourcekey="lblEmpNameResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled"
                                Enabled="False"
                                meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <asp:Label ID="lblStartDate2" runat="server" Text="Start Date :"
                                meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" />
                        </div>
                        <div class="col2"></div>
                        <div class="col4"></div>
                    </div>
                </div>
                    <div runat="server" id="divWorkTime">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblWorkTimeName" runat="server" Text="Worktime Name :"
                                    meta:resourcekey="lblWorkTimeNameResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtWorkTimeName" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False"  meta:resourcekey="txtWorkTimeNameResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblWorkTimeID" runat="server" Text="Worktime ID :"
                                    meta:resourcekey="lblWorkTimeIDResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtWorkTimeID" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False"  meta:resourcekey="txtWorkTimeIDResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblShiftName" runat="server" Text="Shift Name :"
                                    meta:resourcekey="lblShiftNameResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtShiftName" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False"  meta:resourcekey="txtShiftNameResource1"></asp:TextBox>
                            </div>
                            <div class="col2">
                                <asp:Label ID="lblShiftID" runat="server" Text="Shift ID :"
                                    meta:resourcekey="lblShiftIDResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtShiftID" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False"  meta:resourcekey="txtShiftIDResource1"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divTimeFrom">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblTimeFrom" runat="server" Text="Time from :"
                                    meta:resourcekey="lblTimeFromResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpFrom" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                    meta:resourcekey="tpFromResource1" />
                            </div>
                            <div class="col2" id="divbtnSplit" runat="server">
                                &nbsp;
                                                    <asp:Button ID="btnSplit" runat="server" CssClass="buttonBG"
                                                        OnClick="btnSplit_Click" Text="Split" Width="60px"
                                                        meta:resourcekey="btnSplitResource1" />
                            </div>
                        </div>

                    </div>

                    <div runat="server" id="divSplit">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblTimeSplit" runat="server" Text="Time Split :"
                                    meta:resourcekey="lblTimeSplitResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpTimeSplit" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                    meta:resourcekey="tpTimeSplitResource1" />
                                <asp:CustomValidator ID="cvTimeSplit" runat="server" CssClass="CustomValidator"
                                    Text="&lt;img src='../images/message_exclamation.png' title='Enter correct Split Time' /&gt;"
                                    ErrorMessage="Enter correct Split Time"
                                    ValidationGroup="vgSave"
                                    OnServerValidate="TimeSplit_ServerValidate"
                                    EnableClientScript="False"
                                    ControlToValidate="txtValid"
                                    meta:resourcekey="cvTimeSplitResource1"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblPart" runat="server" Text="this Request is :"
                                    meta:resourcekey="lblPartResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:RadioButtonList ID="rdlSplit" runat="server" RepeatDirection="Horizontal"
                                    meta:resourcekey="rdlSplitResource1">
                                    <asp:ListItem Text="Part 1" Selected="True"
                                        meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Part 2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                            <div class="col4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonBG"
                                    ValidationGroup="vgSave" OnClick="btnSave_Click" Width="60px"
                                    meta:resourcekey="btnSaveResource1" />
                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonBG"
                                                    OnClick="btnCancel_Click" Width="60px" meta:resourcekey="btnCancelResource2" />
                            </div>
                            </tr>
                        </div>
                    </div>

                    <div runat="server" id="divTimeTo">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblTimeTo" runat="server" Text="Time To :"
                                    meta:resourcekey="lblTimeToResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <Almaalim:TimePicker ID="tpTo" runat="server" FormatTime="HHmmss" CssClass="TimeCss"
                                    meta:resourcekey="tpToResource1" />
                            </div>
                            <div class="col2">
                                <asp:TextBox ID="txtID" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False" Visible="false" Width="15px"></asp:TextBox>
                            </div>
                            <div class="col4">
                                
                            </div>

                        </div>
                    </div>

                    <div runat="server" id="divFileReq">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblReqFile" runat="server" Text="Request File :"
                                    meta:resourcekey="lblReqFileResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:Button ID="btnReqFile" runat="server" Text="Download" CssClass="buttonBG"
                                    OnClick="btnReqFile_Click" Width="80px"
                                    meta:resourcekey="btnReqFileResource1" />
                            </div>
                            <div class="col2">
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtReqFile" runat="server" AutoCompleteType="Disabled"
                                    Enabled="False"  Visible="False"
                                    meta:resourcekey="txtReqFileResource1"></asp:TextBox></td>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divReason">
                        <div class="row">
                            <div class="col2">
                                <asp:Label ID="lblDesc" runat="server" Text="Request Reason :"
                                    meta:resourcekey="lblDescResource1"></asp:Label>
                            </div>
                            <div class="col4">
                                <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                                    TextMode="MultiLine" Width="600px" Enabled="False"
                                    meta:resourcekey="txtDescResource1"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

