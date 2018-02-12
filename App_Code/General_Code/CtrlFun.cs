using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public class CtrlFun : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    General GenCs = new General();
    DBFun DBCs = new DBFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public CtrlFun() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Clean(Control PCtrl)
    {
        foreach (Control ctrl in PCtrl.Controls)
        {
            if (ctrl is TextBox)
            {
                if (((TextBox)ctrl).TextMode == TextBoxMode.Password) { ((TextBox)ctrl).Attributes["value"] = ""; } else { ((TextBox)ctrl).Text = String.Empty; }
            }
            if (ctrl is DropDownList) { ((DropDownList)ctrl).SelectedIndex = -1; }
            if (ctrl is RadioButtonList) { ((RadioButtonList)ctrl).SelectedIndex = -1; }
            if (ctrl is CheckBox) { ((CheckBox)ctrl).Checked = false; }
            //if (ctrl is Image)           
            //{ 
            //    ((Image)ctrl).ImageUrl  = "~/Images/Logo/noImage.jpg"; 
            //}
            //if (ctrl is GridView) { 
            //    ((GridView)ctrl).DataSource = new DataTable(); 
            //    ((GridView)ctrl).DataBind();
            //}

            if (ctrl is UserControl) { } else { if (ctrl.Controls.Count > 0) { Clean(ctrl); } }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsNullOrEmpty(DataTable dt)
    {
        if (dt == null) { return true; }
        if (dt.Rows.Count == 0) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region List

    public bool PopulateDDL(DropDownList ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text].ToString()))
                {
                    ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            ListItem lsMsg = new ListItem(Msg, Msg);
            ddl.Items.Insert(0, lsMsg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool PopulateDDL2(DropDownList ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text].ToString()))
                {
                    ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            ListItem lsMsg = new ListItem(Msg, "0");
            ddl.Items.Insert(0, lsMsg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool PopulateDDL(DropDownList ddl, DataTable dt, string Text1, string Text2, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text1].ToString()) || !string.IsNullOrEmpty(dt.Rows[i][Text2].ToString()))
                {
                    ListItem ls = new ListItem(dt.Rows[i][Text1].ToString() + "-" + dt.Rows[i][Text2].ToString(), dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            ListItem lsMsg = new ListItem(Msg, Msg);
            ddl.Items.Insert(0, lsMsg);
            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool PopulateLBX(ListBox ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }
            ddl.DataSource = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text].ToString()))
                {
                    ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            return true;
        }
        catch (Exception e1)
        {
            throw e1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool PopulateCBL(CheckBoxList ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }
            ddl.DataSource = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text].ToString()))
                {
                    ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            return true;
        }
        catch (Exception e1)
        {
            throw e1;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool PopulateChosenDDL(DropDownList ddl, DataTable dt, string Text1, string Text2, string Value, string Msg)
    {
        try
        {
            if (DBCs.IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][Text1].ToString()))
                {
                    string txt = (string.IsNullOrEmpty(dt.Rows[i][Text2].ToString())) ? dt.Rows[i][Text1].ToString() : dt.Rows[i][Text1].ToString() + "-" + dt.Rows[i][Text2].ToString();

                    ListItem ls = new ListItem(txt, dt.Rows[i][Value].ToString());
                    ddl.Items.Add(ls);
                }
            }

            ListItem lsMsg = new ListItem("", "-1");
            ddl.Items.Insert(0, lsMsg);

            ddl.Attributes.Add("data-placeholder", Msg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected DataTable PopulateDepartment(string DepList, string Version)
    {
        string DQ = "";
        if (Version == "BorderGuard")
        {
            DQ = " SELECT DISTINCT DepID, DepNameEn AS FullNameEn, DepNameAr AS FullNameAr FROM Department WHERE DepID IN (" + DepList + ") ";
        }
        else // (ActiveVersion == "General")
        {
            DQ = " SELECT DISTINCT DepID, FullNameEn, FullNameAr FROM DepartmentLevelView WHERE DepID IN (" + DepList + ") ";
        }

        return DBCs.FetchData(new SqlCommand(DQ));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public void PopulateDepartmentList(ref DropDownList _ddl, string DepList, string Version, string label)
    {
        DataTable DT = PopulateDepartment(DepList, Version);
        if (!DBCs.IsNullOrEmpty(DT)) { PopulateDDL(_ddl, DT, General.Msg("FullNameEn", "FullNameAr"), "DepID", label); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public void PopulateDepartmentList(ref DropDownList _ddl, string DepList, string Version)
    {
        PopulateDepartmentList(ref _ddl, DepList, Version, General.Msg("-Select Department-", "-اختر القسم-"));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void PopulateDepartmenCheckList(ref CheckBoxList _cbl, string DepList, string Version)
    {
        DataTable DT = PopulateDepartment(DepList, Version);
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateCBL(_cbl, DT, General.Msg("FullNameEn", "FullNameAr"), "DepID", General.Msg("-Select Department-", "-اختر القسم-"));
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Fill Data List 

    //CtrlCs.FillExcuseTypeList(ref ddlExcType, rfvddlExcType, false, true);
    //CtrlCs.FillVacationTypeList(ref ddlVacType, rfvddlVacType, false, true, "VAC");
    //CtrlCs.FillWorkingTimeList(ref ddlWktID, rfvddlWktID, false, true);

    public void FillExcuseTypeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll)
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT ExcID, ExcStatus, ExcNameAr, ExcNameEn FROM ExcuseType WHERE ISNULL(ExcDeleted,0) = 0 AND ExcStatus = (CASE WHEN @P1 = 'A' THEN ExcStatus ELSE 'True' END) AND ExcCategory IS NULL ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("ExcNameEn", "ExcNameAr"), "ExcID", General.Msg("-Select Excuse Type-", "-اختر نوع الإستئذان-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }

            //tempServicesItem.Attributes.Add("disabled", "disabled");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillExcuseTypeList(DDLAttributes.DropDownListAttributes _ddl, RequiredFieldValidator _rv, bool isAll)
    {
        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT ExcID, ExcStatus, ExcNameAr, ExcNameEn FROM ExcuseType WHERE ISNULL(ExcDeleted,0) = 0 AND ExcStatus = (CASE WHEN @P1 = 'A' THEN ExcStatus ELSE 'True' END) AND ExcCategory IS NULL ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            _ddl.Populate(DT, General.Msg("ExcNameEn", "ExcNameAr"), "ExcID", "ExcStatus", General.Msg("-Select Excuse Type-", "-اختر نوع الإستئذان-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillVacationTypeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll, string Category) //Category = "ALL","VAC","COM","LIC","JOB"
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT VtpID, VtpStatus, VtpNameAr, VtpNameEn FROM VacationType WHERE ISNULL(VtpDeleted,0) = 0 AND VtpCategory = (CASE WHEN @P1 = 'ALL' THEN VtpCategory ELSE @P1 END) AND VtpStatus = (CASE WHEN @P2 = 'A' THEN VtpStatus ELSE 'True' END) ", new string[] { Category, All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("VtpNameEn", "VtpNameAr"), "VtpID", General.Msg("-Select Vacation type-", "-اختر نوع الإجازة-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillVacationTypeList(DDLAttributes.DropDownListAttributes _ddl, RequiredFieldValidator _rv, bool isAll, string Category)
    {
        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT VtpID, VtpStatus, VtpNameAr, VtpNameEn FROM VacationType WHERE ISNULL(VtpDeleted,0) = 0 AND VtpCategory = (CASE WHEN @P1 = 'ALL' THEN VtpCategory ELSE @P1 END) AND VtpStatus = (CASE WHEN @P2 = 'A' THEN VtpStatus ELSE 'True' END) ", new string[] { Category, All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            _ddl.Populate(DT, General.Msg("VtpNameEn", "VtpNameAr"), "VtpID", "VtpStatus", General.Msg("-Select Vacation type-", "-اختر نوع الإجازة-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillWorkingTimeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll)
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";

        DataTable DT = DBCs.FetchData(" SELECT WktID, WktNameAr, WktNameEn FROM WorkingTime WHERE ISNULL(WktDeleted,0) = 0 AND WtpID IN (SELECT WtpID FROM WorkType WHERE WtpInitial !='RO') AND WktIsActive = (CASE WHEN @P1 = 'A' THEN WktIsActive ELSE 'True' END) ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("WktNameEn", "WktNameAr"), "WktID", General.Msg("-Select Worktime-", "-اختر جدول العمل-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillWorkingTimeList(DDLAttributes.DropDownListAttributes _ddl, RequiredFieldValidator _rv, bool isAll)
    {
        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT WktID, WktIsActive, WktNameAr, WktNameEn FROM WorkingTime WHERE ISNULL(WktDeleted,0) = 0 AND WtpID IN (SELECT WtpID FROM WorkType WHERE WtpInitial !='RO') AND WktIsActive = (CASE WHEN @P1 = 'A' THEN WktIsActive ELSE 'True' END) ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            _ddl.Populate(DT, General.Msg("WktNameEn", "WktNameAr"), "WktID", "WktIsActive", General.Msg("-Select Worktime-", "-اختر جدول العمل-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }  
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillWorkingTimeList1Sift(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll)
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";

        DataTable DT = DBCs.FetchData(" SELECT WktID, WktNameAr, WktNameEn FROM WorkingTime WHERE ISNULL(WktDeleted,0) = 0 AND WktShiftCount = 1 AND WtpID IN (SELECT WtpID FROM WorkType WHERE WtpInitial !='RO') AND WktIsActive = (CASE WHEN @P1 = 'A' THEN WktIsActive ELSE 'True' END) ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("WktNameEn", "WktNameAr"), "WktID", General.Msg("- Select Worktime -", "- اختر جدول العمل-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillCategoryList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT CatID, CatNameAr, CatNameEn FROM Category WHERE ISNULL(CatDeleted,0) = 0 "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("CatNameEn", "CatNameAr"), "CatID", General.Msg("-Select Category-", "-أختر التصنيف-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillNationalityList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT NatID, NatNameAr, NatNameEn FROM Nationality WHERE ISNULL(NatDeleted,0) = 0 "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("NatNameEn", "NatNameAr"), "NatID", General.Msg("-Select Nationnality-", "-اختر الجنسية-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillEmploymentTypeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT EtpID, EtpNameAr, EtpNameEn FROM EmploymentType WHERE ISNULL(EtpDeleted,0) = 0 "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("EtpNameEn", "EtpNameAr"), "EtpID", General.Msg("-Select Employee type-", "-اختر نوع الموظف-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillRuleSetList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT RlsID, RlsNameAr, RlsNameEn FROM RuleSet WHERE ISNULL(RlsDeleted,0) = 0  "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("RlsNameEn", "RlsNameAr"), "RlsID", General.Msg("-Select Attendance Rules Set-", "-اختر مجموعة قواعد الحضور-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillRanksList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT rnk_code, rnk_name FROM Ranks "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, "rnk_name", "rnk_code", General.Msg("-Select Rank-", "-اختر الرتبة-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillMachineList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll, string InOu, string Type) // InOu = "A" , "I", "O" - Type = "A" (All), "AT", "RP", "IT"
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT MacID, MacLocationEn, MacLocationAr FROM Machine WHERE ISNULL(MacDeleted,0) = 0 AND MacTransactionType = (CASE WHEN @P3 = 'A' THEN MacTransactionType ELSE @P3 END) AND MacStatus = (CASE WHEN @P1 = 'A' THEN MacStatus ELSE 'True' END) AND (MacInOutType = (CASE WHEN @P2 = 'I' THEN 'True' WHEN @P1 = 'O' THEN 'False' ELSE MacInOutType END) OR MacInOutType IS NULL) ", new string[] { All, InOu, Type });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("MacLocationEn", "MacLocationAr"), "MacID", General.Msg("-Select Location-", "-اختر الموقع-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillVirtualMachineList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, string Type) 
    {
        if (isClear) { _ddl.Items.Clear(); }

        string MacVirtual = "";
        if (Type == "IN") { MacVirtual = "AddTransIN"; } else if (Type == "OUT") { MacVirtual = "AddTransOUT"; }

        DataTable DT = DBCs.FetchData(" SELECT MacID, MacLocationEn, MacLocationAr FROM Machine WHERE ISNULL(MacDeleted,0) = 0 AND MacVirtualType = @P1 ", new string[] { MacVirtual });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("MacLocationEn", "MacLocationAr"), "MacID", General.Msg("-Select Location-", "-اختر الموقع-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillMachineTypeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, bool isAll) 
    {
        if (isClear) { _ddl.Items.Clear(); }

        string All = (isAll) ? "A" : "N";
        DataTable DT = DBCs.FetchData(" SELECT MtpID, MtpName FROM MachineType WHERE MtpStatus = (CASE WHEN @P1 = 'A' THEN MtpStatus ELSE 'True' END) ", new string[] { All });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, "MtpName", "MtpID", General.Msg("-Select Machine Type-", "-اختر نوع الماكينة-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillBranchList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Branch WHERE ISNULL(BrcDeleted,0) = 0 "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("BrcNameEn", "BrcNameAr"), "BrcID", General.Msg("-Select Branch-", "-اختر الفرع-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillWorkTypeList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM WorkType WHERE WtpInitial != 'RO' "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("WtpNameEn", "WtpNameAr"), "WtpID", General.Msg("-Select Work Type-", "-اختر نوع العمل-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPermissionGroupList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear, string Type) // type = "Forms" , "Reports"
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(" SELECT * FROM PermissionGroup WHERE GrpType = @P1 AND ISNULL(GrpDeleted,0) = 0 ", new string[] { Type });
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("GrpNameEn", "GrpNameAr"), "GrpID", General.Msg("-Select Group-", "-اختر مجموعة-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillMgrsList2(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT UsrName FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 AND UsrStatus = 'True' "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, "UsrName", "UsrName", General.Msg("-Select Manager Name-", "-اختر مدير القسم-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillMgrsChosenList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT UsrName,EmpID FROM AppUser WHERE ISNULL(UsrDeleted,0) = 0 AND UsrStatus = 'True' "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateChosenDDL(_ddl, DT, "UsrName", "EmpID", "UsrName", General.Msg("-Select Manager Name-", "-اختر مدير القسم-"));
            if (_rv != null) { _rv.InitialValue = "-1"; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillDepartmentList(ref DropDownList _ddl, RequiredFieldValidator _rv, bool isClear)
    {
        if (isClear) { _ddl.Items.Clear(); }

        DataTable DT = DBCs.FetchData(new SqlCommand(" SELECT * FROM Department WHERE ISNULL(DepDeleted,0) = 0 "));
        if (!DBCs.IsNullOrEmpty(DT))
        {
            PopulateDDL(_ddl, DT, General.Msg("DepNameEn", "DepNameAr"), "DepID", General.Msg("-Select Department-", "-اختر القسم-"));
            if (_rv != null) { _rv.InitialValue = _ddl.Items[0].Text; }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Grid

    public void FillGridEmpty(ref GridView grd, int HRow, string MsgEn, string MsgAr)
    {
        try
        {
            DataTable dt = new DataTable();
            DataColumn column;
            //to get column in gridview
            String columnName = string.Empty;
            for (int i = 0; i < grd.Columns.Count; i++)
            {
                DataControlField field = grd.Columns[i];
                BoundField bfield = field as BoundField;
                if (bfield != null) { columnName = bfield.DataField; /*Get DataFiled column*/ } else { columnName = field.SortExpression;/*Get TemplateFiled column*/ }
                column = new DataColumn(columnName);
                column.AllowDBNull = true;
                dt.Columns.Add(column);
            }
            dt.Rows.Add(dt.NewRow());
            grd.DataSource = dt;
            grd.DataBind();
            int totalcolums = grd.Rows[0].Cells.Count;
            grd.Rows[0].Cells.Clear();
            grd.Rows[0].Cells.Add(new TableCell());
            grd.Rows[0].Cells[0].ColumnSpan = totalcolums;

            GridViewRow pagerRow = grd.BottomPagerRow;
            //TableCell tcc = pagerRow.Cells[0];
            if (pagerRow != null) { pagerRow.Cells.Clear(); }
            //pagerRow.Cells.Add(tcc);
            //pagerRow.Cells[0].ColumnSpan = totalcolums;

            grd.Rows[0].Cells[0].Text = General.Msg(MsgEn, MsgAr);
            grd.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grd.Rows[0].Cells[0].VerticalAlign = VerticalAlign.Middle;
            grd.Rows[0].Cells[0].Height = HRow;
            grd.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            //grd.Rows[0].Cells[0].Font.Size = 12;
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGridEmpty(ref GridView grd, int HRow) { FillGridEmpty(ref grd, HRow, "No Data Found", "لا توجد بيانات"); }
    public void FillGridEmpty(ref GridView grd) { FillGridEmpty(ref grd, 50, "No Data Found", "لا توجد بيانات"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getTextRowEmpty() { return General.Msg("No Data Found", "لا توجد بيانات"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool isGridEmpty(string pRowText)
    {
        if (pRowText == "No Data Found" || pRowText == "لا توجد بيانات" || pRowText == "&nbsp;" || pRowText == "") { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool isGridEmpty(string pRowText, string pMsg)
    {
        if (pRowText == pMsg || pRowText == "&nbsp;" || pRowText == "") { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RefreshGridEmpty(ref GridView gv, int pHeight)
    {
        if (gv.Rows.Count > 0) { if (gv.Rows[0].Cells[0].Text == General.Msg("No Data Found", "لا توجد بيانات")) { FillGridEmpty(ref gv, pHeight); } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RefreshGridEmpty(ref GridView gv)
    {
        if (gv.Rows.Count > 0) { if (gv.Rows[0].Cells[0].Text == General.Msg("No Data Found", "لا توجد بيانات")) { FillGridEmpty(ref gv); } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void GridRender(GridView gv)
    {
        if (gv != null)
        {
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;
            if (pagerRow != null) { pagerRow.Visible = true; }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DropDownList PagerList(GridView pGrid)
    {
        DropDownList _ddlPagerList = new DropDownList();
        _ddlPagerList.ID = "ddlPagesize";
        _ddlPagerList.Items.Add("10");
        _ddlPagerList.Items.Add("50");
        _ddlPagerList.Items.Add("100");
        _ddlPagerList.Items.Add("200");
        _ddlPagerList.AutoPostBack = true;
        ListItem li = _ddlPagerList.Items.FindByText(pGrid.PageSize.ToString());
        if (li != null) { _ddlPagerList.SelectedIndex = _ddlPagerList.Items.IndexOf(li); }
        return _ddlPagerList;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public TableCell PagerCell(DropDownList pList)
    {
        TableCell _PagerCell = new TableCell();
        _PagerCell.Style["padding-left"] = "15px";
        _PagerCell.Controls.Add(pList);
        _PagerCell.Controls.Add(new LiteralControl(General.Msg(" Record\\Records", " سجل/سجلات")));
        return _PagerCell;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Button ExportBtnPager(GridView pGrid)
    {
        Button _ExportBtn = new Button();
        _ExportBtn.ID = "ExportBtn";
        _ExportBtn.Text = General.Msg("Export", "تصدير");

        return _ExportBtn;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public TableCell ExportBtnPagerCell(Button pBtn)
    {
        TableCell _PagerCell = new TableCell();
        _PagerCell.Style["padding-left"] = "15px";
        _PagerCell.Controls.Add(pBtn);
        return _PagerCell;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region GridViewControl

    public void FillGridEmpty(ref AlmaalimControl.GridViewControl.GridView grd, int HRow, string MsgEn, string MsgAr)
    {
        try
        {
            DataTable dt = new DataTable();
            DataColumn column;
            //to get column in gridview
            String columnName = string.Empty;
            for (int i = 0; i < grd.Columns.Count; i++)
            {
                DataControlField field = grd.Columns[i];
                BoundField bfield = field as BoundField;
                if (bfield != null) { columnName = bfield.DataField; /*Get DataFiled column*/ } else { columnName = field.SortExpression;/*Get TemplateFiled column*/ }
                column = new DataColumn(columnName);
                column.AllowDBNull = true;
                dt.Columns.Add(column);
            }
            dt.Rows.Add(dt.NewRow());
            grd.DataSource = dt;
            grd.DataBind();
            int totalcolums = grd.Rows[0].Cells.Count;
            grd.Rows[0].Cells.Clear();
            grd.Rows[0].Cells.Add(new TableCell());
            grd.Rows[0].Cells[0].ColumnSpan = totalcolums;

            GridViewRow pagerRow = grd.BottomPagerRow;
            //TableCell tcc = pagerRow.Cells[0];
            if (pagerRow != null) { pagerRow.Cells.Clear(); }
            //pagerRow.Cells.Add(tcc);
            //pagerRow.Cells[0].ColumnSpan = totalcolums;

            grd.Rows[0].Cells[0].Text = General.Msg(MsgEn, MsgAr);
            grd.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grd.Rows[0].Cells[0].VerticalAlign = VerticalAlign.Middle;
            grd.Rows[0].Cells[0].Height = HRow;
            grd.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            //grd.Rows[0].Cells[0].Font.Size = 12;
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGridEmpty(ref AlmaalimControl.GridViewControl.GridView grd, int HRow) { FillGridEmpty(ref grd, HRow, "No Data Found", "لا توجد بيانات"); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RefreshGridEmpty(ref AlmaalimControl.GridViewControl.GridView gv)
    {
        if (gv.Rows.Count > 0) { if (gv.Rows[0].Cells[0].Text == General.Msg("No Data Found", "لا توجد بيانات")) { FillGridEmpty(ref gv, 50); } }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ExportGridToExcel

    public string Export(string pFileName, GridView gv)
    {
        try
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gv.RenderControl(hw);

            string filePath = ServerMapPath("download/" + pFileName);

            if (File.Exists(filePath)) { File.Delete(filePath); }

            System.IO.StreamWriter vw = new System.IO.StreamWriter(filePath, true);
            sw.ToString().Normalize();
            vw.Write(sw.ToString());
            vw.Flush();
            vw.Close();
            WriteAttachment(pFileName, "application/vnd.ms-excel", sw.ToString());
            //WriteAttachment(pFileName, "application/octet-stream", sw.ToString());
            //WriteAttachment(pFileName, "application/vnd.ms-excel", filePath); //sw.ToString()

            return "Success";
        }
        catch (Exception ex)
        {
            return "Faild";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ServerMapPath(string path)
    {
        string m = HttpContext.Current.Server.MapPath(path);
        string P = m.Replace(@"\Pages", "");
        return P;

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        //Response.AppendHeader("Content-Length", content.Length.ToString());
        //Response.AppendHeader("Content-Length", new System.IO.FileInfo(content).Length.ToString());
        Response.ContentType = FileType;

        string c = Response.Charset;

        Response.Write(content);  // to Write Stream Writer string
                                  //Response.WriteFile(content); // to Write File by path

        Response.End();

        //Response.TransmitFile(content);
        //Response.Charset = "";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region ValidMsg

    public enum TypeMsg { Info, Success, Warning, Error, Validation };
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ValidMsg(Page pg, ref CustomValidator cv, bool isShow, string pMsg)
    {
        if (isShow)
        {
            cv.ErrorMessage = pMsg;
            cv.Text = pg.Server.HtmlDecode("&lt;img src='../images/Exclamation.gif' title='" + pMsg + "' /&gt;");
        }
        else
        {
            cv.ErrorMessage = "";
            cv.Text = pg.Server.HtmlDecode("&lt;img src='../images/Exclamation.gif' title='" + pMsg + "' /&gt;");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ValidMsg(Page pg, ref RequiredFieldValidator rv, string pMsg)
    {
        rv.ErrorMessage = "";
        rv.Text = pg.Server.HtmlDecode("&lt;img src='../images/Exclamation.gif' title='" + pMsg + "' /&gt;");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FalseValidMsg(Page pg, ref CustomValidator cv, ref ServerValidateEventArgs e, bool isShow, string pMsg)
    {
        ValidMsg(pg, ref cv, isShow, pMsg);
        e.IsValid = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowMsg(Page pg, ValidationSummary vs, CustomValidator cv, TypeMsg Type, string VG, string pMsg)
    {
        vs.ValidationGroup = VG;
        cv.ErrorMessage = pMsg;
        cv.ValidationGroup = VG;

        if (Type == TypeMsg.Info) { vs.CssClass = "MsgInfo";       /****/ vs.ForeColor = ColorTranslator.FromHtml("#00529B"); }
        else if (Type == TypeMsg.Success) { vs.CssClass = "MsgSuccess ";   /****/ vs.ForeColor = ColorTranslator.FromHtml("#4F8A10"); }
        else if (Type == TypeMsg.Warning) { vs.CssClass = "MsgWarning";    /****/ vs.ForeColor = ColorTranslator.FromHtml("#9F6000"); }
        else if (Type == TypeMsg.Error) { vs.CssClass = "MsgError";      /****/ vs.ForeColor = ColorTranslator.FromHtml("#D8000C"); }
        else if (Type == TypeMsg.Validation) { vs.CssClass = "MsgValidation"; /****/ vs.ForeColor = ColorTranslator.FromHtml("#D63301"); }

        pg.Validate(VG);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowMsg(Page pg, TypeMsg Type, string pMsg)
    {
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg") as CustomValidator;

        ShowMsg(pg, vs, cv, Type, VG, pMsg);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowSaveMsg(Page pg)
    {
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg") as CustomValidator;

        string msg = General.Msg("Saved data successfully", "تم الحفظ البيانات بنجاح");

        ShowMsg(pg, vs, cv, TypeMsg.Success, VG, msg);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowSaveMsg2(Page pg)
    {
        string VG = "vgShowMsg2";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg2") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg2") as CustomValidator;

        string msg = General.Msg("Saved data successfully", "تم الحفظ البيانات بنجاح");

        ShowMsg(pg, vs, cv, TypeMsg.Success, VG, msg);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowDelMsg(Page pg, bool isDel)
    {
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg") as CustomValidator;

        if (isDel)
        {
            ShowMsg(pg, vs, cv, TypeMsg.Success, VG, General.Msg("detail deleted successfully", "تم حذف البيانات بنجاح"));
        }
        else
        {
            ShowMsg(pg, vs, cv, TypeMsg.Validation, VG, General.Msg("Unable to delete because of relationship with other data", "لا يمكنك حذف هذا السجل لانه مرتبط ببيانات أخرى"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowAdminMsg(Page pg, ValidationSummary vs, CustomValidator cv, string VG, string pMsg, string pEx)
    {
        string errMsg = Regex.Replace(pEx, @"[&\/\\#,+()$~%.':*?<>{ }\r\n]", " "); 
        string DMsg = General.Msg("<a style=\"color:Blue\" href='#' onclick=\"alert('" + errMsg + "');\">To find out the error details Click here </a> ", "<a style=\"color:Blue\" href='#' onclick=\"alert('" + errMsg + "')\">لمعرفة تفاصيل الخطأ اضغط هنا </a> ");

        vs.ValidationGroup = VG;       
        cv.ErrorMessage = pMsg + DMsg;
        cv.ValidationGroup = VG;
        vs.CssClass = "MsgError";
        vs.ForeColor = ColorTranslator.FromHtml("#D8000C");
        pg.Validate(VG);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowAdminMsg(Page pg, ValidationSummary vs, CustomValidator cv, string VG, string pEx)
    {
        string MMsg = General.Msg("Transaction failed to commit please contact your administrator. ", "النظام غير قادر على حفظ البيانات, الرجاء الاتصال بمدير النظام. ");
        ShowAdminMsg(pg, vs, cv, VG, MMsg, pEx);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowAdminMsg(Page pg, string pEx)
    {
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg") as CustomValidator;

        ShowAdminMsg(pg, vs, cv, VG, pEx);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ShowAdminMsg2(Page pg, string pEx)
    {
        string VG = "vgShowMsg2";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder1").FindControl("vsShowMsg2") as ValidationSummary;
        CustomValidator cv = pg.Master.FindControl("ContentPlaceHolder1").FindControl("cvShowMsg2") as CustomValidator;

        //string ss = vs.ValidationGroup;

        ShowAdminMsg(pg, vs, cv, VG, pEx);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ConfirmDeleteMsg()
    {
        return General.Msg("return confirm('Are you sure you want to delete');", "return confirm('هل أنت متأكد أنك تريد حذف');");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public bool PageIsValid(Page pg, ValidationSummary vs)
    {
        if (!pg.IsValid)
        {
            ValidatorCollection ValidatorColl = pg.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vs.ShowSummary = true; return false; }
                vs.ShowSummary = false;
            }
            return false;
        }

        return true;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Animation

    public void Animation(ref AjaxControlToolkit.AnimationExtender aniExtShow, ref AjaxControlToolkit.AnimationExtender aniExtClose, ref ImageButton plnkShow, string pID)
    {
        string lang = HttpContext.Current.Session["Language"].ToString();

        aniExtShow.Animations = AnimationShow(lang, pID);
        aniExtClose.Animations = AnimationClose(lang, pID);
        plnkShow.ImageUrl = "~/images/Hint_Image/Hint" + lang + ".png";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string AnimationShow(string pLang, string pID)
    {
        string Horizontal = "10";
        string Vertical = "10";

        if (pLang == "AR") { Horizontal = "-200"; Vertical = "20"; }

        return "<OnClick Position='absolute'>"
                     + "<Sequence>"
                       + "<EnableAction Enabled='true'></EnableAction>"
                            + "<StyleAction AnimationTarget='pnlInfo" + pID + "' Attribute='display' Value='block'/>"
                            + "<Parallel AnimationTarget='pnlInfo" + pID + "' Duration='.2' Fps='25'>"
                                + "<Move Horizontal='" + Horizontal + "' Vertical='" + Vertical + "' />"
                                + "<Resize Height='150%' Width='250%' />"
                                //+"<Move Horizontal='150' Vertical='-50' />"
                                //+"<Resize Height='260' Width='280' />"

                                + "<FadeIn />"
                            + "</Parallel>"
                            + "<Parallel AnimationTarget='pnlInfo" + pID + "' Duration='.5'>"
                                  + "<Color PropertyKey='color' StartValue='#666666' EndValue='#FF0000'/>"
                                  + "<Color PropertyKey='borderColor' StartValue='#666666' EndValue='#FF0000' />"
                            + "</Parallel>"
                            + "<EnableAction AnimationTarget='lnkClose" + pID + "' Enabled='true' />"
                        + "</Sequence>"
                    + "</OnClick>";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string AnimationClose(string pLang, string pID)
    {
        string Horizontal = "-10";
        string Vertical = "-10";

        if (pLang == "AR") { Horizontal = "200"; Vertical = "-20"; }

        return "<OnClick Position='absolute'>"
                        + "<Sequence AnimationTarget='pnlInfo" + pID + "' >"
                            + "<EnableAction AnimationTarget='lnkClose" + pID + "' Enabled='false' />"
                            + "<Parallel AnimationTarget='pnlInfo" + pID + "' Duration='.3' Fps='25'>"
                                + "<Move Horizontal='" + Horizontal + "' Vertical='" + Vertical + "' />"
                                + "<Scale ScaleFactor='0.05' FontUnit='px' />"
                                + "<FadeOut />"
                            + "</Parallel>"
                            + "<EnableAction AnimationTarget='lnkShow" + pID + "' Enabled='true' />"
                        + "</Sequence>"
                     + "</OnClick>";
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Tree

    public DataSet FetchMenuDataset(string query)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, MainConnection);

        da.Fill(ds);
        da.Dispose();

        ds.DataSetName = "Menus";
        ds.Tables[0].TableName = "Menu";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Menu"].Columns["MnuID"], ds.Tables["Menu"].Columns["MnuParentID"], false);
        relation.Nested = true;
        ds.Relations.Add(relation);
        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchDepartmentDataSet(string query)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, MainConnection);

        da.Fill(ds);
        da.Dispose();
        ds.DataSetName = "Departments";
        ds.Tables[0].TableName = "Department";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Department"].Columns["DepID"], ds.Tables["Department"].Columns["DepParentID"], false);
        relation.Nested = true;
        ds.Relations.Add(relation);
        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchReportDataSet(string query)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, MainConnection);

        da.Fill(ds);
        da.Dispose();
        ds.DataSetName = "Menus";
        ds.Tables[0].TableName = "Menu";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Menu"].Columns["RepID"], ds.Tables["Menu"].Columns["RgpID"], false);
        relation.Nested = true;
        ds.Relations.Add(relation);
        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FetchBranchDataSet(string query)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, MainConnection);

        da.Fill(ds);
        da.Dispose();

        ds.DataSetName = "Departments";
        ds.Tables[0].TableName = "Department";
        DataRelation relation = new DataRelation("ParentChild", ds.Tables["Department"].Columns["BrcID"], ds.Tables["Department"].Columns["BrcParentID"], false);
        relation.Nested = true;
        ds.Relations.Add(relation);
        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillDepTreeDS(string pBrcID, string ActiveVersion)
    {
        string sql;

        string DQ = "";
        if (ActiveVersion == "BorderGuard")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,DepName" + General.Msg("En", "Ar") + " AS DepNameEn FROM Department WHERE BrcID= " + pBrcID + "";
        }
        else // (ActiveVersion == "General")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,FullName" + General.Msg("En", "Ar") + " AS DepNameEn FROM DepartmentLevelView WHERE BrcID= " + pBrcID + "";
        }

        DataSet ds = new DataSet();
        ds = (DataSet)FetchDepartmentDataSet(sql = DQ);

        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public DataSet FillDepTreeDS_ByBrcID(string pBrcID, string ActiveVersion, string DepList)
    //{
    //    string sql;

    //    string DQ = "";
    //    if (ActiveVersion == "BorderGuard")
    //    {
    //        DQ = " SELECT DISTINCT DepID,DepParentID,DepName" + General.Msg("En", "Ar") + " AS DepNameEn,dbo.GetDepPerm(DepID,'" + DepList + "') AS DepCheck FROM Department WHERE BrcID= " + pBrcID + "";
    //    }
    //    else // (ActiveVersion == "General")
    //    {
    //        DQ = " SELECT DISTINCT DepID,DepParentID,FullName" + General.Msg("En", "Ar") + " AS DepNameEn,dbo.GetDepPerm(DepID,'" + DepList + "') AS DepCheck FROM DepartmentLevelView WHERE BrcID= " + pBrcID + "";
    //    }

    //    DataSet ds = new DataSet();
    //    ds = (DataSet)FetchDepartmentDataSet(sql = DQ);

    //    return ds;
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillDepTreeDS(string pBrcID, string ActiveVersion, string loginUserName, string DepList)
    {
        string sql;

        string DQ = "";
        if (ActiveVersion == "BorderGuard")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,DepName" + General.Msg("En", "Ar") + " AS DepNameEn,dbo.GetDepPerm(DepID,'" + DepList + "') AS DepCheck FROM Department WHERE BrcID = " + pBrcID + " AND DepID IN (" + DepList + ") ";
        }
        else // (ActiveVersion == "General")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,FullName" + General.Msg("En", "Ar") + " AS DepNameEn,dbo.GetDepPerm(DepID,'" + DepList + "') AS DepCheck FROM DepartmentLevelView WHERE BrcID = " + pBrcID + " AND DepID IN (" + DepList + ") ";
        }

        DataSet ds = new DataSet();
        ds = (DataSet)FetchDepartmentDataSet(sql = DQ);

        return ds;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DataSet FillBrcDepTreeDS(string ActiveVersion)
    {
        string sql;

        string DQ = "";
        if (ActiveVersion == "BorderGuard")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,DepName" + General.Msg("En", "Ar") + " AS DepNameEn FROM Department ";
        }
        else // (ActiveVersion == "General")
        {
            DQ = " SELECT DISTINCT DepID,DepParentID,FullName" + General.Msg("En", "Ar") + " AS DepNameEn FROM DepartmentWithBranchLevelView ";
        }

        DataSet ds = new DataSet();
        ds = (DataSet)FetchDepartmentDataSet(sql = DQ);

        return ds;
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}