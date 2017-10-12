<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ERS_MonthSummary.aspx.cs" Inherits="ERS_MonthSummary" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col12">
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"
                        EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server" meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server" meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click"
                        ImageUrl="../images/Button_Icons/button_magnify.png" meta:resourcekey="btnFilterResource1" />
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


            <div class="h3 collapseToggle">
               

                    <asp:Label ID="lblWorks" runat="server" Text="Works Summary"   meta:resourcekey="lblWorksResource1"></asp:Label>
               
            </div>
            <div class="row" id="collapse">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShiftDuration" runat="server" Text="Shift Duration :" meta:resourcekey="lblMsmShiftDurationResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShiftDuration" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmShiftDurationResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmWorkDuration" runat="server" Text="Work Duration :" meta:resourcekey="lblMsmWorkDurationResource1"></asp:Label>

                    <asp:Label ID="lblVMsmWorkDuration" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmWorkDurationResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmWorkDurWithET" runat="server" Text="Work Duration With Extratime:" meta:resourcekey="lblMsmWorkDurWithETResource1"></asp:Label>

                    <asp:Label ID="lblVMsmWorkDurWithET" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmWorkDurWithETResource1"></asp:Label>
                </div>


                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmBeginEarly" runat="server" Text="Begin Early :" meta:resourcekey="lblMsmBeginEarlyResource1"></asp:Label>

                    <asp:Label ID="lblVMsmBeginEarly" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmBeginEarlyResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmBeginLate" runat="server" Text="Begin Late :" meta:resourcekey="lblMsmBeginLateResource1"></asp:Label>

                    <asp:Label ID="lblVMsmBeginLate" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmBeginLateResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOutEarly" runat="server" Text="Out Early :" meta:resourcekey="lblMsmOutEarlyResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOutEarly" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOutEarlyResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOutLate" runat="server" Text="Out Late :" meta:resourcekey="lblMsmOutLateResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOutLate" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOutLateResource1"></asp:Label>
                </div>
            </div>

             <div class="h3 collapseToggle">
                
                    <asp:Label ID="lblGaps" runat="server" Text="Gaps Summary"  meta:resourcekey="lblGapsResource1"></asp:Label>
                
            </div>

            <div class="row" id="collapse">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_MG" runat="server" Text="Middle Gap :" meta:resourcekey="lblMsmGapDur_MGResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_MG" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_MGResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_WithoutExc" runat="server" Text="Gap Without Excuse :" meta:resourcekey="lblMsmGapDur_WithoutExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_WithoutExc" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_WithoutExcResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_PaidExc" runat="server" Text="Gap with Paid Excuse :" meta:resourcekey="lblMsmGapDur_PaidExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_PaidExc" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_PaidExcResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_UnPaidExc" runat="server" Text="Gap with Unpaid Excuse :" meta:resourcekey="lblMsmGapDur_UnPaidExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_UnPaidExc" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_UnPaidExcResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_Grace" runat="server" Text="Grace Gap :" meta:resourcekey="lblMsmGapDur_GraceResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_Grace" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_GraceResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_WithRule" runat="server" Text="Gap With Rule :" meta:resourcekey="lblMsmGapDur_WithRuleResource1"></asp:Label>

                    <asp:Label ID="lblVMsmGapDur_WithRule" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmGapDur_WithRuleResource1"></asp:Label>
                </div>
            </div>

            <div class="h3 collapseToggle">
                    <asp:Label ID="lblOvertime" runat="server" Text="Overtimes Summary"   meta:resourcekey="lblOvertimeResource1"></asp:Label>
                 
            </div>

            <div class="row" id="collapse">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_BeginEarly" runat="server" Text=" Begin Early Extratime :" meta:resourcekey="lblMsmExtraTimeDur_BeginEarlyResource1"></asp:Label>

                    <asp:Label ID="lblVMsmExtraTimeDur_BeginEarly" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmExtraTimeDur_BeginEarlyResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_BeginEarly" runat="server" Text="Begin Early Overtime :" meta:resourcekey="lblMsmOverTimeDur_BeginEarlyResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOverTimeDur_BeginEarly" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOverTimeDur_BeginEarlyResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_OutLate" runat="server" Text="Out Late Extratime :" meta:resourcekey="lblMsmExtraTimeDur_OutLateResource1"></asp:Label>

                    <asp:Label ID="lblVMsmExtraTimeDur_OutLate" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmExtraTimeDur_OutLateResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_OutLate" runat="server" Text="Out Late Overtime :" meta:resourcekey="lblMsmOverTimeDur_OutLateResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOverTimeDur_OutLate" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOverTimeDur_OutLateResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_OutOfShift" runat="server" Text="Out Of Shift Extratime :" meta:resourcekey="lblMsmExtraTimeDur_OutOfShiftResource1"></asp:Label>

                    <asp:Label ID="lblVMsmExtraTimeDur_OutOfShift" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmExtraTimeDur_OutOfShiftResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_OutOfShift" runat="server" Text="Out Of Shift Overtime :" meta:resourcekey="lblMsmOverTimeDur_OutOfShiftResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOverTimeDur_OutOfShift" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOverTimeDur_OutOfShiftResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_NoShift" runat="server" Text="No Shift Extratime :" meta:resourcekey="lblMsmExtraTimeDur_NoShiftResource1"></asp:Label>

                    <asp:Label ID="lblVMsmExtraTimeDur_NoShift" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmExtraTimeDur_NoShiftResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_NoShift" runat="server" Text="No Shift Overtime :" meta:resourcekey="lblMsmOverTimeDur_NoShiftResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOverTimeDur_NoShift" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOverTimeDur_NoShiftResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_InVac" runat="server" Text="In Vacation Extratime :" meta:resourcekey="lblMsmExtraTimeDur_InVacResource1"></asp:Label>

                    <asp:Label ID="lblVMsmExtraTimeDur_InVac" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmExtraTimeDur_InVacResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_InVac" runat="server" Text="In Vacation Overtime :" meta:resourcekey="lblMsmOverTimeDur_InVacResource1"></asp:Label>

                    <asp:Label ID="lblVMsmOverTimeDur_InVac" runat="server" Text="00:00:00" meta:resourcekey="lblVMsmOverTimeDur_InVacResource1"></asp:Label>
                </div>
            </div>

            <div class="h3 collapseToggle">
                    <asp:Label ID="lblShifts" runat="server" Text="Shifts Summary"   meta:resourcekey="lblShiftsResource1"></asp:Label>
               
            </div>

            <div class="row" id="collapse">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Present" runat="server" Text="Present Shift No. :" meta:resourcekey="lblMsmShifts_PresentResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShifts_Present" runat="server" Text="0" meta:resourcekey="lblVMsmShifts_PresentResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_WithoutExc" runat="server" Text="Absent Shift Without Excuse No. :" meta:resourcekey="lblMsmShifts_Absent_WithoutExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShifts_Absent_WithoutExc" runat="server" Text="0" meta:resourcekey="lblVMsmShifts_Absent_WithoutExcResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_PaidExc" runat="server" Text="Absent Shift With Paid No. :" meta:resourcekey="lblMsmShifts_Absent_PaidExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShifts_Absent_PaidExc" runat="server" Text="0" meta:resourcekey="lblVMsmShifts_Absent_PaidExcResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_UnPaidExc" runat="server" Text="Absent Shift With Unpaid No. :" meta:resourcekey="lblMsmShifts_Absent_UnPaidExcResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShifts_Absent_UnPaidExc" runat="server" Text="0" meta:resourcekey="lblVMsmShifts_Absent_UnPaidExcResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_WithRule" runat="server" Text="Absent Shift With Rule No. :" meta:resourcekey="lblMsmShifts_Absent_WithRuleResource1"></asp:Label>

                    <asp:Label ID="lblVMsmShifts_Absent_WithRule" runat="server" Text="0" meta:resourcekey="lblVMsmShifts_Absent_WithRuleResource1"></asp:Label>
                </div>

            </div>

            <div class="h3 collapseToggle">
                    <asp:Label ID="lblDays" runat="server" Text="Days Summary"  meta:resourcekey="lblDaysResource1"></asp:Label>
                
            </div>

            <div class="row" id="collapse">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Work" runat="server" Text="Work Days No. :" meta:resourcekey="lblMsmDays_WorkResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Work" runat="server" Text="0" meta:resourcekey="lblVMsmDays_WorkResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Present" runat="server" Text="Present Days No. :" meta:resourcekey="lblMsmDays_PresentResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Present" runat="server" Text="0" meta:resourcekey="lblVMsmDays_PresentResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithoutVac" runat="server" Text="Absent Days Without Vacation No. :" meta:resourcekey="lblMsmDays_Absent_WithoutVacResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_WithoutVac" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_WithoutVacResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_PaidVac" runat="server" Text="Absent Days With Paid Vacation No. :" meta:resourcekey="lblMsmDays_Absent_PaidVacResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_PaidVac" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_PaidVacResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_UnPaidVac" runat="server" Text="Absent Days With unpaid Vacation No. :" meta:resourcekey="lblMsmDays_Absent_UnPaidVacResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_UnPaidVac" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_UnPaidVacResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithCom" runat="server" Text="Absent Days With Commission No. :" meta:resourcekey="lblMsmDays_Absent_WithComResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_WithCom" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_WithComResource1"></asp:Label>
                </div>

                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithJob" runat="server" Text="Absent Days With Job Assignment No. :" meta:resourcekey="lblMsmDays_Absent_WithJobResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_WithJob" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_WithJobResource1"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithRule" runat="server" Text="Absent Days With Rule No. :" meta:resourcekey="lblMsmDays_Absent_WithRuleResource1"></asp:Label>

                    <asp:Label ID="lblVMsmDays_Absent_WithRule" runat="server" Text="0" meta:resourcekey="lblVMsmDays_Absent_WithRuleResource1"></asp:Label>
                </div>
            </div>
         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

