<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="ReportDesigner.aspx.cs" Inherits="Pages_Report_ReportDesigner" %>

<%@ Register Assembly="Stimulsoft.Report.WebDesign" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col12">
            <asp:ValidationSummary ID="vsShowMsg" runat="server" ValidationGroup="vgShow"
                EnableClientScript="False" CssClass="MsgValidation"/>
        </div>
    </div>
    <div class="row">
        <div class="col1">
            <asp:LinkButton ID="btnBackToReportsPage" runat="server" CssClass="GenButtonsmall backbtn" meta:resourcekey="btnBackToReportsPageResource1"
                OnClick="btnBackToReportsPage_Click"></asp:LinkButton>

            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" CssClass="CustomValidator"
                        ValidationGroup="vgShow" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="txtValid">
                    </asp:CustomValidator>
            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" Width="10px"></asp:TextBox>     
        </div>
    </div>
    <%--<div class="row"> --%>           
        <cc1:StiWebDesigner ID="StiWebDesigner1" runat="server" OnSaveReport="StiWebDesigner1_SaveReport" Width="100%" Height="1000px" />
    <%--</div>--%>
</asp:Content>
