<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeFingerPrint.aspx.cs" Inherits="EmployeeFingerPrint" meta:resourcekey="PageResource1" %>

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
                                                       
                    <object id="FPObject" name="FPObject" classid="CLSID:27BF3D51-2B12-4593-8455-49226FB8D5E1">
                        <p>The Finger Print activeX can not be loaded, please contact your system administrator to help you</p>
                        <p>لا يمكن تحميل عنصر البصمات، الرجاء الاتصال بمدير النظام لمساعدتك</p>
                    </object>                
                </td>
            </tr>
        </table>
    </div>

</asp:Content>