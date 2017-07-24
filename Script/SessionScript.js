$(function () {
    $.noConflict(true);
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