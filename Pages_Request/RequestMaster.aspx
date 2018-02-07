<%@ Page Title="Requests Page" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="RequestMaster.aspx.cs" Inherits="RequestMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <%--script--%>
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




