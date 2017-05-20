/* File Created: June 20, 2012 */
function OnlyNumber(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) { return false; }
    return true;
}