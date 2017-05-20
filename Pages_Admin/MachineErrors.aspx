<%@ Page Title="" Language="C#" MasterPageFile="~/AMSMasterPage.master" AutoEventWireup="true" CodeFile="MachineErrors.aspx.cs" Inherits="MachineErrors" %>

<%@ Register Assembly="AjaxSamples" Namespace="AjaxSamples" TagPrefix="as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row">
                <div class="col1">
                                    <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
                               </div>
            <div class="col3">
                                    <asp:DropDownList ID="ddlMonth" runat="server"  ></asp:DropDownList>
                               </div>   <div class="col1">    
                                    <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                                   </div>
            <div class="col3">
                                     <asp:DropDownList ID="ddlYear" runat="server" >
                                    </asp:DropDownList>
                                </div>   <div class="col1"> 
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="LeftOverlay" ImageUrl="../images/Button_Icons/button_magnify.png"/>
                               </div>
                </div>
            <div class="row">
                  <div class="col12">
                                                <asp:Literal ID="Literal1" runat="server" Text="Machine Errors"></asp:Literal>
                                           </div>
                </div>
            <div class="row">
                  <div class="col12">
                                                <as:GridViewKeyBoardPagerExtender runat="server" ID="gridviewextender" TargetControlID="grdData"
                                                    PrevRowSelectKey="Subtract" NextRowSelectKey="Add" NextPageKey="PageUp" 
                                                    PreviousPageKey="PageDown" />
                                                
                                                <asp:GridView ID="grdData" runat="server" CssClass="datatable" SelectedIndex="0"
                                                    AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" BorderWidth="0px"
                                                    GridLines="None" DataKeyNames="MacID" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                                    OnRowCreated="grdData_RowCreated" OnRowDataBound="grdData_RowDataBound"
                                                    OnPreRender="grdData_PreRender"  
                                                    EnableModelValidation="True" meta:resourcekey="grdDataResource1">

                                                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" FirstPageImageUrl="~/images/first.png"
                                                        LastPageText="Last" LastPageImageUrl="~/images/last.png" NextPageText="Next"
                                                        NextPageImageUrl="~/images/next.png"   PreviousPageText="Prev" PreviousPageImageUrl="~/images/prev.png" />
                                                    
                                                    <Columns>
                                                        <asp:BoundField HeaderText="ID"            DataField="MacID"         SortExpression="MacID" />
                                                        <asp:BoundField HeaderText="IP Address"    DataField="MacIP"         SortExpression="MacIP" />
                                                        <asp:BoundField HeaderText="Type"          DataField="MtpName"       SortExpression="MtpName" />
                                                        <asp:BoundField HeaderText="Location (En)" DataField="MacLocationEn" SortExpression="MacLocationEn" />
                                                        <asp:BoundField HeaderText="Location (Ar)" DataField="MacLocationAr" SortExpression="MacLocationAr" />
                                                        <asp:BoundField HeaderText="Number"        DataField="ErrNo"         SortExpression="ErrNo" />
                                                        <asp:BoundField HeaderText="Message"       DataField="ErrMsg"        SortExpression="ErrMsg" /> 
                                                        <asp:TemplateField HeaderText="Time" SortExpression="ErrTime">
                                                            <ItemTemplate>
                                                                <%# DisplayFun.GrdDisplayDate(Eval("ErrTime")) + " " + DisplayFun.GrdDisplayTime(Eval("ErrTime"))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                   
                                                </asp:GridView>
                                            </div>
                                        </div>
                                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

