$(function () {
    var currentUrl = window.location.href;
    if (currentUrl.indexOf('EXC') == currentUrl.length - 3 || currentUrl.indexOf('COM') == currentUrl.length - 3 || currentUrl.indexOf('JOB') == currentUrl.length - 3 ||
        currentUrl.indexOf('VAC') == currentUrl.length - 3 || currentUrl.indexOf('ESH') == currentUrl.length - 3 || currentUrl.indexOf('SWP') == currentUrl.length - 3) {
        $.noConflict(true);
    }
    $("#LocalDialogModal").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: false,
        draggable: false,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 700,
        height: 260,
        title: "Session Expiry Warning"
    });



    $(document).on("click", "#LoadLocalDialog", function () {
        //debugger;
        $("#LocalDialogModal").dialog("open");
    });

});    //end of main jQuery Ready method