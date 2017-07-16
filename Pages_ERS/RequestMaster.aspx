<%@ Page Title="Vacation Request" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RequestMaster.aspx.cs" Inherits="RequestMaster" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <%--script--%>
    <script type="text/javascript" src="../Script/GridEvent.js"></script>
    <script type="text/javascript" src="../Script/CheckKey.js"></script>
    <script type="text/javascript" src="../Script/ModalPopup.js"></script>
     <script type="text/javascript" src="../Script/jquery-1.7.1.min.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            $("div[class*='col']").each(function () {
                if ($(this).children(".RequiredField").length > 0) {
                    $(this).addClass("RequiredFieldDiv");
                }
                var $this = $(this);

                $this.html($this.html().replace(/&nbsp;/g, ''));

            });
        });
    </script>
    <%--script--%>
    <div class="GreySetion" style="padding:0">
    <iframe src="" id="iFrame" runat="server" width="100%" height="800px" scrolling="no" frameborder="0" ></iframe> 
        </div>
</asp:Content>




