<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="MiniLogger.aspx.cs" Inherits="Pages_Trans_MiniLogger" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script type="text/javascript" language="javascript">
         function Connect(ID) {
             try {
                 var FBControl = document.applets('FPObject');
                 FBControl.Start = ID;
             }
             catch (Error) { }
         }         
    </script>

    <div id="pageDiv" runat="Server">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td style="height:15px" align="center"></td> 
            </tr>
            <tr>
                <td align="center">
                    <asp:HiddenField ID="hfdConn" runat="server" />
                    <asp:HiddenField ID="hfdLoginID" runat="server" />
                    <asp:HiddenField ID="hfdLang" runat="server" />
                    <asp:HiddenField ID="hfdEmpID" runat="server" />
                                                       
                   <object id="FPObject" name="FPObject" classid="CLSID:02A89413-192C-4622-A153-F92215FD7E53">
                        <p>The Finger Print activeX can not be loaded, please contact your system administrator to help you</p>
                        <p>لا يمكن تحميل عنصر البصمات، الرجاء الاتصال بمدير النظام لمساعدتك</p>
                    </object>                
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

