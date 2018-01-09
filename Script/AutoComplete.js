function AutoCompleteIDItemSelected() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID').value;
    var FullValue = txtValue.split("-");
    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID').value = FullValue[0];

    if (document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName') != null) {
        document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[1];
    }
}

function AutoCompleteNameItemSelected() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value;
    var FullValue = txtValue.split("-");
    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[0];
    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID').value = FullValue[1];
}

function AutoCompleteIDSearchItemSelected() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpIDSearch').value;
    //var FullValue = txtValue.split("-");
    //document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[0];
    //document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID').value = FullValue[1];
}

function AutoCompleteNameSearchItemSelected() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpNameSearch').value;
    //var FullValue = txtValue.split("-");
    //document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[0];
    //document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID').value = FullValue[1];
}

function AutoCompleteIDItemSelectedWithoutMaster() {
    var txtValue = document.getElementById('txtEmpID2').value;
    var FullValue = txtValue.split("-");
    document.getElementById('txtEmpID2').value = FullValue[0];

    if (document.getElementById('txtEmpName2') != null) {
        document.getElementById('txtEmpName2').value = FullValue[1];
    }
}

function AutoCompleteItemSelected_EmpID1() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID1').value;
    var FullValue = txtValue.split("-");
    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID1').value = FullValue[0];

    //if (document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName') != null) {
    //    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[1];
    //}
}


function AutoCompleteItemSelected_EmpID2() {
    var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID2').value;
    var FullValue = txtValue.split("-");
    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpID2').value = FullValue[0];

    //if (document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName') != null) {
    //    document.getElementById('ctl00_ContentPlaceHolder1_txtEmpName').value = FullValue[1];
    //}
}


function AutoCompleteDepNameItemSelected() {

    //var x = document.getElementById('ctl00_ContentPlaceHolder1_ucEmployeeSelected');

    //alert(PageMethods.MyMethod());



    //var x = document.getElementById('<%=pnlEmployee.ClientID%>');
    //document.getElementById('<%=btnHidden.ClientID%>');
    //var s = document.getElementById('<% =ucEmployeeSelected.userIdClientId %>').value;
    //var txtName = document.getElementById('<%=ucName.FindControl("txtName").ClientID %>');


   // alert(x)
    
    
    //var txtValue = document.getElementById('ctl00_ContentPlaceHolder1_pnlEmployee_ucEmployeeSelected').value;
    //var FullValue = txtValue.split("-");
    //document.getElementById('ctl00_ContentPlaceHolder1_ddlDepartment').value = FullValue[1];
}