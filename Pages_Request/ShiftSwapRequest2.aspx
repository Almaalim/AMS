﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftSwapRequest2.aspx.cs" Inherits="ShiftSwapRequest2" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript">
        function showWait() {
            //if ($get('fudReqFile').value.length > 0) {
            $get('upWaiting').style.display = 'block';
            //}
        }
    </script>
    <script type="text/javascript">
        function PostbackFunction() {
            (function () {
                // Load the script
                var script = document.createElement("SCRIPT");
                script.src = '../Script/jquery-ui-1.8.17.custom.min.js';
                script.type = 'text/javascript';
                script.onload = function () {
                    var $ = window.jQuery;
                    // Use $ here...
                };
                document.getElementsByTagName("head")[0].appendChild(script);
            })();
            $(document).click(function (event) {
                if ($('.flyOutDiv').css('opacity') == '1') {
                    $('.flyOutDiv').css('display', 'none');
                    //tooltipPopup.parent('.flyOutDiv').addClass("hide");
                }
            });
            $("[id*='iFrame']").load(function () {

                $(this).height($(this).contents().find("html").height());

            });
            $("[id*='ifrm']").load(function () {

                $(this).height(($(this).contents().find("html").height()) - 30);

            });

            MobileListResize();
        }
        function MobileListResize() {
            //$("div[class*='col']>span[id*='lbl']").each(function (i, obj) {
            //    labelh = $(this).height();

            //    if ((labelh > 32) && (!$(window).width() < 1012)) {
            //        $(this).css("white-space", "nowrap");
            //        var parentDiv = $(this).parent().attr('class');
            //        parentDiv = ("." + parentDiv);
            //        lblW = $(this).width();
            //        $(parentDiv).css("width", lblW);
            //        //$(this).parent("div[class*='col']").css("width", "auto");
            //    }
            //    else
            //    {
            //        $(this).css("white-space", "");
            //        $(this).parent("div[class*='col']").css("width", "");
            //    }

            //});
            var Notifi = $(".NotificationsPopup");
            if ($(window).width() < 874) {
                $(".NotificationsPopup").addClass("NotificationsMobile");

                $(".NotificationsWrapper").after(Notifi);
            }
            else {
                $(".NotificationsPopup").removeClass("NotificationsMobile");
                $("#aspnetForm").prepend(Notifi);
            }
            $("[id*='iFrame']").load(function () {
                $(this).height($(this).contents().find("html").height());
            });
            $('input').bind('blur', function (e) {

                e.preventDefault();
                $(this).focus();

            });
            $("a.level2.dynamic").each(function (index) {
                var str1 = $(this).attr('title');

                str1 = str1.replace(/\s/g, '').replace(/[_\W]+/g, "");

                $(this).addClass(str1);
            });
            $('a.static').click(function (e) {
                return false;
            });
            $("li>a.folder>span").each(function (index) {
                var linkwidth = $(this).width();
                if (linkwidth > 74) {
                    $(this).addClass("longlinkfolder")
                }
            });
            $('img').each(function () {
                if ($(this).attr("src") == '../images/Exclamation.gif') {
                    $(this).parents("span:first").addClass('CustomValidator');
                }
            });

            //$(".flyOutDiv").each(function () {
            //    var peraH = $(this).find("p").height();
            //    $(this).height(peraH);
            //});
            $("a.nextStep").each(function () {
                if ($(this).attr('title') == "  ") {
                    $(this).hide();
                }
            });
            if ($(".GridTable").width() > $(".row").width()) {
                //                $(".GridTable").prepend("<thead></thead>");
                //                var GridHead = $(".GridHeaderStyle");
                //                $(".GridTable thead").append(GridHead);
                $(".GridTable").parent().addClass("GridDiv");

                $('.sub-list>li>a').each(function () {
                    var widthMenu = $(this).width();
                    //alert(widthMenu);
                    $(this).height(widthMenu);

                });

                $("a").each(function (index) {
                    if ($(this).is('[disabled=disabled]')) {
                        $(this).addClass("disabled");

                    }

                });


            }

            $("div[class*='col']").each(function () {
                if ($(this).children(".RequiredField").length > 0) {
                    $(this).addClass("RequiredFieldDiv");
                }
                var $this = $(this);

                $this.html($this.html().replace(/&nbsp;/g, ''));

            });
            var colors = ["009ad7", "ffaa31", "68af27", "c22439", "005683", "622695", "d13f2a"];

            $('.rp_servlnk2').each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $('.monthSummaryDiv').each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $('.GapSummeryDiv').each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $("a.SideMenuItem").each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $(".apps li a").each(function (i) {
                $(this).addClass('color' + colors[i % colors.length]);
            });
            $(".ReportBtns").each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $("ul.level2.dynamic").each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });

            $("ul.level1.static>li").each(function (i) {
                $(this).css('background-color', '#' + colors[i % colors.length]);
            });
            $('[data-popup-close]').on('click', function (e) {
                var targeted_popup_class = jQuery(this).attr('data-popup-close');
                $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);

                e.preventDefault();
            });
            $(".NotificationsIcon").on('click', function (e) {
                $(".NotificationsPopup").addClass("NotificationsActive");

            });
            $(".NotificationsPopupClose").on('click', function (e) {
                $(".NotificationsPopup").removeClass("NotificationsActive");

            });
        }
        $(document).click(function (e) {
            var container = $(".taptap-menu-active, .taptap-main-wrapper-active, .NotificationsPopup");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                $(".taptap-menu-active").removeClass("taptap-menu-active");
                $(".NotificationsPopup").removeClass("NotificationsActive");
                $(".taptap-main-wrapper").removeClass("taptap-main-wrapper-active");
                $("body").css("overflow", "auto");
            }
        });
        $(window).ready(function () {
            // Animate loader off screen
            $(".LoadingWrapper").delay(1500).fadeOut("slow", 0);

        });
        $(document).ready(function () {

            //$("a.level2.dynamic").each(function (index) {
            //    var str1 = $(this).attr('title');

            //    str1 = str1.replace(/\s/g, '').replace(/[_\W]+/g, "");

            //    $(this).addClass(str1);


            //});
            //$("a.SideMenuItem").each(function (index) {
            //    var str1 = $(this).attr('title');

            //    str1 = str1.replace(/\s/g, '').replace(/[_\W]+/g, "");

            //    $(this).addClass(str1);


            //});

            //$(".headerExpand").click(function () {

            //    $header = $(this);
            //    //getting the next element
            //    $content = $header.next();
            //    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            //    $content.slideToggle(200);

            //});
            $(function ($) {
                var num_cols = 15,
                    container = $(".taptap-contents-wrapper>ul>li>ul"),
                    listItem = "li",
                    listClass = "sub-list";
                container.each(function () {
                    var items_per_col = new Array(),
                        items = $(this).find(listItem),
                        min_items_per_col = Math.floor(items.length / num_cols),
                        difference = items.length - min_items_per_col * num_cols;
                    for (var i = 0; i < num_cols; i++) {
                        if (i < difference) {
                            items_per_col[i] = min_items_per_col + 1;
                        } else {
                            items_per_col[i] = min_items_per_col;
                        }
                    }
                    for (var i = 0; i < num_cols; i++) {
                        $(this).append($("<div ></div>").addClass(listClass));
                        for (var j = 0; j < items_per_col[i]; j++) {
                            var pointer = 0;
                            for (var k = 0; k < i; k++) {
                                pointer += items_per_col[k];
                            }
                            $(this).find("." + listClass).last().append(items[j + pointer]);

                        }
                    }
                });

            });



            //var menuDoc = $("#ctl00_Menu1").html();
            //$(".taptap-contents-wrapper").append(menuDoc);

            $("a.level1.static").each(function (index) {
                var str = $(this).attr("title");
                str = str.replace('/', '');

                $(this).addClass(str);
                $(this).parent().addClass(str + "list")
                //debugger;
            });
            $("a").each(function (index) {
                if ($(this).is('[disabled=disabled]')) {
                    $(this).addClass("disabled");
                }
            });


            jQuery(".taptap-menu-button-wrapper").on('click', function (e) {


                if ($(this).hasClass("taptap-menu-active")) {
                    $(this).removeClass("taptap-menu-active");
                    $(".taptap-main-wrapper").removeClass("taptap-main-wrapper-active");
                    $("body").css("overflow", "auto");

                }

                else {
                    $(this).addClass("taptap-menu-active");
                    $(".taptap-main-wrapper").addClass("taptap-main-wrapper-active");
                    $("body").css("overflow", "hidden");




                }

            })

            //$(function () {

            //    $(".taptap-main-inner").mousewheel(function (event, delta) {

            //        this.scrollLeft -= (delta * 100);

            //        event.preventDefault();

            //    });

            //});



        });
        $(document).ready(MobileListResize);
        var w = 0;

        $(window).load(function () {

            w = $(window).width();

        });
        $(window).resize(function () {
            if (w != $(window).width()) {
                MobileListResize();
            }
        });
        // $(window).resize(MobileListResize);
    </script>

    <title>Shift Swap Request </title>

    <script type="text/javascript" src="../Script/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Script/AutoComplete.js"></script>
    <link href="~/CSS/Metro/Metro.css" rel="stylesheet" runat="server" id="LanguageSwitch" />

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
    <style>
        .col2, .col4 {
            max-width: 50% !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

            <asp:UpdateProgress ID="upWaiting" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="row">
                        <div class="col12">
                            <iframe id="ifrmProgress" runat="server" src="../Pages_Mix/Progress.aspx" scrolling="no" frameborder="0" height="600px" width="100%"></iframe>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div id="divName" runat="server" class="row">
                        <div class="col12">
                            <asp:Label ID="lblName" runat="server" meta:resourcekey="lblNameResource1"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False" ValidationGroup="ShowMsg" meta:resourcekey="vsShowMsgResource1" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col12">
                            <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="vgSave"
                                EnableClientScript="False" CssClass="MsgValidation" meta:resourcekey="vsSaveResource1"/>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblType" runat="server" Text="Type :" meta:resourcekey="lblTypeResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:DropDownList ID="ddlType" runat="server" meta:resourcekey="ddlTypeResource1">
                                <asp:ListItem Text="-Select Type-" Value="0" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Working Day with Working Day" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="Working Day with Off Day" Value="2" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Text="Off Day with Working Day" Value="3" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddlType" runat="server" CssClass="CustomValidator"
                                ControlToValidate="ddlType" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='Type is required' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rvddlTypeResource1"></asp:RequiredFieldValidator>
                        </div>
                        <div id="divCat1" runat="server" visible ="False" class="col2">
                            <asp:Label ID="lblCatID" runat="server" Text="Category :" meta:resourcekey="lblCatIDResource1"></asp:Label>
                        </div>
                        <div id="divCat2" runat="server" visible ="False" class="col4">
                            <asp:DropDownList ID="ddlCatID" runat="server" Enabled="False" meta:resourcekey="ddlCatIDResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblEmpID1" runat="server" Text="Employee ID :" meta:resourcekey="lblEmpID1Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtEmpID1" runat="server" AutoCompleteType="Disabled" Enabled="False" meta:resourcekey="txtEmpID1Resource1"></asp:TextBox>
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblStartDate1" runat="server" Text="Date 1 :" meta:resourcekey="lblStartDate1Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate1" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" CalTo="calStartDate2" />

                            <asp:CustomValidator ID="cvDays1" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave" CssClass="CustomValidator"
                                OnServerValidate="Days1_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvDays1Resource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblEmpID2" runat="server" Text="swap With Employee ID:" meta:resourcekey="lblEmpID2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtEmpID2" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpID2Resource1"></asp:TextBox>
                            <asp:Panel runat="server" ID="pnlauID2" meta:resourcekey="pnlauID2Resource1" />
                            <ajaxToolkit:AutoCompleteExtender
                                ID="auID2" runat="server"
                                TargetControlID="txtEmpID2"
                                ServicePath="~/Service/AutoComplete.asmx"
                                ServiceMethod="GetEmployeeIDListWithCon2"
                                MinimumPrefixLength="1"
                                OnClientItemSelected="AutoCompleteID_txtEmpID2_WithoutMaster_ItemSelected"
                                CompletionListElementID="pnlauID2"
                                CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                CompletionSetCount="12" DelimiterCharacters="" Enabled="True" />

                            <asp:CustomValidator ID="cvEmployee2" runat="server" ValidationGroup="vgSave"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                CssClass="CustomValidator"
                                OnServerValidate="Employee_ServerValidate"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvEmployee2Resource1"></asp:CustomValidator>
                        </div>
                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblStartDate2" runat="server" Text="Date 2 :" meta:resourcekey="lblStartDate2Resource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <Cal:Calendar2 ID="calStartDate2" runat="server" CalendarType="System" ValidationRequired="true" ValidationGroup="vgSave" />

                            <asp:CustomValidator ID="cvDays2" runat="server"
                                Text="&lt;img src='../images/message_exclamation.png' title='' /&gt;"
                                ValidationGroup="vgSave"
                                OnServerValidate="Days2_ServerValidate" CssClass="CustomValidator"
                                EnableClientScript="False"
                                ControlToValidate="txtValid" meta:resourcekey="cvDays2Resource1"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col2">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblDesc" runat="server" Text="Reason :" meta:resourcekey="lblDescResource1"></asp:Label>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtDesc" runat="server" AutoCompleteType="Disabled"
                                TextMode="MultiLine" Enabled="False" meta:resourcekey="txtDescResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvtxtDesc" runat="server"
                                ControlToValidate="txtDesc" EnableClientScript="False"
                                Text="&lt;img src='../images/Exclamation.gif' title='Reason is required' /&gt;"
                                ValidationGroup="vgSave" CssClass="CustomValidator" meta:resourcekey="rvtxtDescResource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col2"></div>
                        <div class="col4"></div>
                    </div>
                    <div class="row">
                        <div class="col2"></div>
                        <div class="col4">
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="GenButton glyphicon glyphicon-floppy-disk"
                                OnClick="btnSave_Click" ValidationGroup="vgSave"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_storage.png&quot; /&gt; Save" meta:resourcekey="btnSaveResource1"></asp:LinkButton>

                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="GenButton glyphicon glyphicon-remove-circle"
                                OnClick="btnCancel_Click"
                                Text="&lt;img src=&quot;../images/Button_Icons/button_Cancel.png&quot; /&gt; Cancel" meta:resourcekey="btnCancelResource1"></asp:LinkButton>
                        </div>
                        <div class="col4">
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
