/* File Created: September 24, 2012 */
function OpenChild(url) {
    //            var ParmA = retvalA.value;
    //            var ParmB = retvalB.value;
    //            var ParmC = retvalC.value;
    //            var MyArgs = new Array(ParmA, ParmB, ParmC);
    //var dialogFeatures = 'center:yes; dialogWidth:600px; location:no;dialogHeight:400px; edge:raised; help:no; resizable:no; scroll:no; status:no; statusbar:no; toolbar:no; menubar:no; addressbar:no; titlebar:no;'; 
    var WinSettings = 'center:yes;resizable:no;dialogHeight:400px;dialogwidth:680px;status:no;scroll:no;edge:sunken;titlebar:no;'

    var MyArgs = window.showModalDialog('./' + url, MyArgs, WinSettings);
    //            if (MyArgs == null) {
    //                window.alert('Nothing returned from child. No changes made to input boxes')
    //            }
    //            else {
    //                retvalA.value = MyArgs[0].toString();
    //                retvalB.value = MyArgs[1].toString();
    //                retvalC.value = MyArgs[2].toString();
    //            }
}

function Exit() {
    //            var ParmA = tbParamA.value;
    //            var ParmB = tbParamB.value;
    //            var ParmC = tbParamC.value;
    //            var MyArgs = new Array(ParmA, ParmB, ParmC);
    //            window.returnValue = MyArgs;
    window.close();
}