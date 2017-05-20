function showPopup(devName) { 
    document.getElementById('divBackground').style.visibility='visible';
    document.getElementById('divBackground').style.display='block';

    if (devName == null || devName == '') { devName = 'divPopup'; }
    document.getElementById(devName).style.display = 'block';
    document.getElementById(devName).style.visibility = 'visible';
}

function hidePopup(devName) {
    document.getElementById('divBackground').style.visibility='hidden';
    document.getElementById('divBackground').style.display = 'none';

    if (devName == null || devName == '') { devName = 'divPopup'; }
    document.getElementById(devName).style.visibility = 'hidden';
    document.getElementById(devName).style.display = 'none';
}

function hideparentPopup(devName) {
    
    try
    {
        window.parent.document.close();
        window.parent.document.getElementById('divBackground').style.display = 'none';
        window.parent.document.getElementById('divBackground').style.visibility = 'hidden';

        if (devName == null || devName == '') { devName = 'divPopup'; }
        window.parent.document.getElementById(devName).style.display = 'none';
        window.parent.document.getElementById(devName).style.visibility = 'hidden';
    }
    catch (e) {}
}

function hideparentPopupSave(devName, parentName) {

    try {
        window.parent.document.close();
        window.parent.document.getElementById('divBackground').style.display = 'none';
        window.parent.document.getElementById('divBackground').style.visibility = 'hidden';

        if (devName == null || devName == '') { devName = 'divPopup'; }
        window.parent.document.getElementById(devName).style.display = 'none';
        window.parent.document.getElementById(devName).style.visibility = 'hidden';

        window.parent.location.href = parentName;

        //        window.opener.location.href = 'AttendanceList.aspx';
        //        window.opener.location.reload(true);
        //        window.opener.history.go(0); 

    }
    catch (e) { }
}