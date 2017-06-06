<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ERS_MonthSummary.aspx.cs" Inherits="ERS_MonthSummary" %>

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
                        EnableClientScript="False" ValidationGroup="ShowMsg" />
                </div>
            </div>
            <div class="row">
                <div class="col12">
                    <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px"></asp:TextBox>

                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col2">
                    <asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                </div>
                <div class="col2">
                    <asp:DropDownList ID="ddlYear" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col1">
                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                        ImageUrl="../images/Button_Icons/button_magnify.png" />
                </div>
            </div>
            <div class="row">
                <div class="col12">

                    <asp:Label ID="lblWorks" runat="server" Text="Works Summary" CssClass="h3"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShiftDuration" runat="server" Text="Shift Duration :"></asp:Label>
               
                    <asp:Label ID="lblVMsmShiftDuration" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmWorkDuration" runat="server" Text="Work Duration :"></asp:Label>
              
                    <asp:Label ID="lblVMsmWorkDuration" runat="server" Text="00:00:00"></asp:Label>
                </div>
           
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmWorkDurWithET" runat="server" Text="Work Duration With Extratime:"></asp:Label>
             
                    <asp:Label ID="lblVMsmWorkDurWithET" runat="server" Text="00:00:00"></asp:Label>
                </div>
               
           
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmBeginEarly" runat="server" Text="Begin Early :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmBeginEarly" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmBeginLate" runat="server" Text="Begin Late :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmBeginLate" runat="server" Text="00:00:00"></asp:Label>
                </div>
           
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOutEarly" runat="server" Text="Out Early :"></asp:Label>
                
                    <asp:Label ID="lblVMsmOutEarly" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOutLate" runat="server" Text="Out Late :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmOutLate" runat="server" Text="00:00:00"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblGaps" runat="server" Text="Gaps Summary" CssClass="h3"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_MG" runat="server" Text="Middle Gap :"></asp:Label>
               
                    <asp:Label ID="lblVMsmGapDur_MG" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_WithoutExc" runat="server" Text="Gap Without Excuse :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmGapDur_WithoutExc" runat="server" Text="00:00:00"></asp:Label>
                </div>
            
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_PaidExc" runat="server" Text="Gap with Paid Excuse :"></asp:Label>
               
                    <asp:Label ID="lblVMsmGapDur_PaidExc" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_UnPaidExc" runat="server" Text="Gap with Unpaid Excuse :"></asp:Label>
                
                    <asp:Label ID="lblVMsmGapDur_UnPaidExc" runat="server" Text="00:00:00"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_Grace" runat="server" Text="Grace Gap :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmGapDur_Grace" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmGapDur_WithRule" runat="server" Text="Gap With Rule :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmGapDur_WithRule" runat="server" Text="00:00:00"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblOvertime" runat="server" Text="Overtimes Summary" CssClass="h3"></asp:Label>
                </div>
            </div>
                                 
                                         <div class="row">
                                             <div class="monthSummaryDiv">
                                                 <asp:Label ID="lblMsmExtraTimeDur_BeginEarly" runat="server" Text=" Begin Early Extratime :"></asp:Label>
                                             
                                                 <asp:Label ID="lblVMsmExtraTimeDur_BeginEarly" runat="server" Text="00:00:00"></asp:Label>
                                             </div>
                                             <div class="monthSummaryDiv">
                                                 <asp:Label ID="lblMsmOverTimeDur_BeginEarly" runat="server" Text="Begin Early Overtime :"></asp:Label>
                                              
                                                 <asp:Label ID="lblVMsmOverTimeDur_BeginEarly" runat="server" Text="00:00:00"></asp:Label>
                                             </div>
                                       
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_OutLate" runat="server" Text="Out Late Extratime :"></asp:Label>
                
                    <asp:Label ID="lblVMsmExtraTimeDur_OutLate" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_OutLate" runat="server" Text="Out Late Overtime :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmOverTimeDur_OutLate" runat="server" Text="00:00:00"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_OutOfShift" runat="server" Text="Out Of Shift Extratime :"></asp:Label>
                
                    <asp:Label ID="lblVMsmExtraTimeDur_OutOfShift" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_OutOfShift" runat="server" Text="Out Of Shift Overtime :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmOverTimeDur_OutOfShift" runat="server" Text="00:00:00"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_NoShift" runat="server" Text="No Shift Extratime :"></asp:Label>
                
                    <asp:Label ID="lblVMsmExtraTimeDur_NoShift" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_NoShift" runat="server" Text="No Shift Overtime :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmOverTimeDur_NoShift" runat="server" Text="00:00:00"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmExtraTimeDur_InVac" runat="server" Text="In Vacation Extratime :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmExtraTimeDur_InVac" runat="server" Text="00:00:00"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmOverTimeDur_InVac" runat="server" Text="In Vacation Overtime :"></asp:Label>
               
                    <asp:Label ID="lblVMsmOverTimeDur_InVac" runat="server" Text="00:00:00"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblShifts" runat="server" Text="Shifts Summary" CssClass="h3"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Present" runat="server" Text="Present Shift No. :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmShifts_Present" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_WithoutExc" runat="server" Text="Absent Shift Without Excuse No. :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmShifts_Absent_WithoutExc" runat="server" Text="0"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_PaidExc" runat="server" Text="Absent Shift With Paid No. :"></asp:Label>
               
                    <asp:Label ID="lblVMsmShifts_Absent_PaidExc" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_UnPaidExc" runat="server" Text="Absent Shift With Unpaid No. :"></asp:Label>
               
                    <asp:Label ID="lblVMsmShifts_Absent_UnPaidExc" runat="server" Text="0"></asp:Label>
                </div>
            
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmShifts_Absent_WithRule" runat="server" Text="Absent Shift With Rule No. :"></asp:Label>
               
                    <asp:Label ID="lblVMsmShifts_Absent_WithRule" runat="server" Text="0"></asp:Label>
                </div>
               
            </div>

            <div class="row">
                <div class="col12">
                    <asp:Label ID="lblDays" runat="server" Text="Days Summary" CssClass="h3"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Work" runat="server" Text="Work Days No. :"></asp:Label>
                
                    <asp:Label ID="lblVMsmDays_Work" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Present" runat="server" Text="Present Days No. :"></asp:Label>
                
                    <asp:Label ID="lblVMsmDays_Present" runat="server" Text="0"></asp:Label>
                </div>
            
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithoutVac" runat="server" Text="Absent Days Without Vacation No. :"></asp:Label>
                
                    <asp:Label ID="lblVMsmDays_Absent_WithoutVac" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_PaidVac" runat="server" Text="Absent Days With Paid Vacation No. :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmDays_Absent_PaidVac" runat="server" Text="0"></asp:Label>
                </div>
           
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_UnPaidVac" runat="server" Text="Absent Days With unpaid Vacation No. :"></asp:Label>
               
                    <asp:Label ID="lblVMsmDays_Absent_UnPaidVac" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithCom" runat="server" Text="Absent Days With Commission No. :"></asp:Label>
                
                    <asp:Label ID="lblVMsmDays_Absent_WithCom" runat="server" Text="0"></asp:Label>
                </div>
             
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithJob" runat="server" Text="Absent Days With Job Assignment No. :"></asp:Label>
              
                    <asp:Label ID="lblVMsmDays_Absent_WithJob" runat="server" Text="0"></asp:Label>
                </div>
                <div class="monthSummaryDiv">
                    <asp:Label ID="lblMsmDays_Absent_WithRule" runat="server" Text="Absent Days With Rule No. :"></asp:Label>
                 
                    <asp:Label ID="lblVMsmDays_Absent_WithRule" runat="server" Text="0"></asp:Label>
                </div>
            </div>
            <%-- <div class="row">
                                               <div class="monthSummaryDiv">
                                                <asp:Label ID="lblMsmDaysAbsentDueToGaps" runat="server" Text="Days Work No. :"></asp:Label>
                                            </div>
                                               <div class="monthSummaryDiv">
                                                <asp:Label ID="lblVMsmDaysAbsentDueToGaps" runat="server" Text="0"></asp:Label>
                                            </div>
                                               <div class="monthSummaryDiv">
                                                <asp:Label ID="lblMsmPendingGap" runat="server" Text="Days Present No. :"></asp:Label>
                                            </div>
                                               <div class="monthSummaryDiv">
                                                <asp:Label ID="lblVMsmPendingGap" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

