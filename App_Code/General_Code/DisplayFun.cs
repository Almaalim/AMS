using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;


public class DisplayFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static PageFun pgCs = new PageFun();
    static DTFun   DTCs = new DTFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDate(object gre, object pDateType, object pDateFormat)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        try
        {
            if (gre == DBNull.Value) { return ""; }
            if (pDateType.ToString() == "Gregorian")
            {
                DateTime greg = (DateTime)gre;
                string DH = greg.ToString(pDateFormat.ToString());
                return DH;
            }
            else
            {
                DateTime greg = (DateTime)gre;
                string DH = greg.ToString("dd/MM/yyyy");
                return DTCs.GregToHijri(DH, pDateFormat.ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public static string GrdDisplayDate(object gre)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        pgCs.FillDTSession();

        return GrdDisplayDate(gre, pgCs.DateType, pgCs.DateFormat);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayTime(object pTime)
    {
        try
        {
            if (pTime == DBNull.Value) { return string.Empty; }
            return String.Format("{0:HH:mm:ss}", pTime);
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayTime2(object Time)
    {
        try
        {
            if (Time == DBNull.Value) { return string.Empty; }
            else if (string.IsNullOrEmpty(Time.ToString())) { return string.Empty; }
            else
            {
                string[] Times = Time.ToString().Split(' ');
                string[] Parts = Times[1].Split(':');
                
                
                return Convert.ToInt32(Parts[0]).ToString("00") + ":" + Convert.ToInt32(Parts[1]).ToString("00") + ":" + Convert.ToInt32(Parts[2]).ToString("00");
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDuration(object pDuration)
    {
        try
        {
            if (pDuration == DBNull.Value) { return "00:00:00"; }
            if (string.IsNullOrEmpty(pDuration.ToString())) { return "00:00:00"; }

            TimeSpan tsGen = TimeSpan.FromSeconds(Convert.ToDouble(pDuration));
            int Hours = tsGen.Hours;
            if (tsGen.Days > 0) { Hours = (tsGen.Days * 24) + tsGen.Hours; }
            return Hours.ToString("00") + ":" + tsGen.Minutes.ToString("00") + ":" + tsGen.Seconds.ToString("00");
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "00:00:00";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDuration(object ShiftID, object Duration)
    {
        try
        {
            if (ShiftID == DBNull.Value) { return ""; }
            else if (string.IsNullOrEmpty(ShiftID.ToString())) { return ""; }
            else
            {
                if (Duration == DBNull.Value) { return "00:00:00"; }
                if (string.IsNullOrEmpty(Duration.ToString())) { return "00:00:00"; }

                TimeSpan tsGen = TimeSpan.FromSeconds(Convert.ToDouble(Duration));
                int Hours = tsGen.Hours;
                if (tsGen.Days > 0) { Hours = (tsGen.Days * 24) + tsGen.Hours; }
                return Hours.ToString("00") + ":" + tsGen.Minutes.ToString("00") + ":" + tsGen.Seconds.ToString("00");
            }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return "00:00:00";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayOverTimeType(object ID)
    {
        try
        {
            if      (ID.ToString() == "1") { return General.Msg("Begin Early", "حضور مبكر"); }
            else if (ID.ToString() == "2") { return General.Msg("Out Late" ,"خروج متأخر"); }
            else if (ID.ToString() == "3") { return General.Msg("Out of Shift" ,"خارج الوردية"); }
            else if (ID.ToString() == "4") { return General.Msg("No Worktime" ,"بدون وقت عمل"); }
            else if (ID.ToString() == "5") { return General.Msg("In Vacation" ,"في الإجازة"); }
            else { return string.Empty; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
        }
    }

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayStatus(object pStatus)
    {
        try
        {
            if      (pStatus.ToString() == "True")  { return General.Msg("Active", "فعال"); } 
            else if (pStatus.ToString() == "False") { return General.Msg("Inactive", "غير فعال"); }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    public static string GrdDisplayTypeTrans(object pType)
    {
        try
        {
            if      (pType.ToString() == "1") { return General.Msg("IN", "دخول"); }
            else if (pType.ToString() == "0") { return General.Msg("OUT", "خروج"); }
            else { return ""; }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayShiftStatus(object pStatus)
    {
        try
        {
            if      (pStatus.ToString() == "A")  { return General.Msg("Absent", "غائب"); }
            else if (pStatus.ToString() == "P")  { return General.Msg("Present" ,"حاضر"); }
            else if (pStatus.ToString() == "IN") { return General.Msg("IN Only" ,"دخول فقط"); }
            else if (pStatus.ToString() == "OU") { return General.Msg("OUT Only" ,"خروج فقط"); }
            else if (pStatus.ToString() == "OI") { return General.Msg("OUT-IN" ,"خروج-دخول"); }
            else { return string.Empty; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDayStatus(object pStatus, string pActiveVersion)
    {
        try
        {
            if      (pStatus.ToString().Trim() == "A")  { return General.Msg("Absent", "غائب"); }
            else if (pStatus.ToString().Trim() == "P")  { return General.Msg("Present" ,"حاضر"); }
            else if (pStatus.ToString().Trim() == "N")  { return General.Msg("No work" ,"لا يوجد عمل"); }
            else if (pStatus.ToString().Trim() == "H")  { return General.Msg("Holiday" ,"عطلة"); }
            else if (pStatus.ToString().Trim() == "WE") { return General.Msg("Week end" ,"نهاية الأسبوع"); }
            else if (pStatus.ToString().Trim() == "PE") { if (pActiveVersion == "BorderGuard") { return General.Msg("Vacation", "إجازة"); } else { return General.Msg("Paid Vacation", "إجازة مدفوعة"); } }
            else if (pStatus.ToString().Trim() == "UE") { return General.Msg("UnPaid Vacation" ,"إجازة غير مدفوعة"); }
            else if (pStatus.ToString().Trim() == "CM") { return General.Msg("Commission" ,"إنتداب"); }
            else if (pStatus.ToString().Trim() == "JB") { return General.Msg("Job Assignment" ,"مهمة عمل"); }
            else if (pStatus.ToString().Trim() == "T")  { return General.Msg("In Process" ,"تحت الإجراء"); }
            else if (pStatus.ToString().Trim() == "NP") { return General.Msg("Not Processed" ,"لم تتم المعالجة"); }

            else if (pStatus.ToString().Trim() == "IN") { return General.Msg("IN Only" ,"دخول فقط"); }
            else if (pStatus.ToString().Trim() == "OU") { return General.Msg("OUT Only" ,"خروج فقط"); }
            else if (pStatus.ToString().Trim() == "OI") { return General.Msg("OUT-IN" ,"خروج-دخول"); }
            else { return string.Empty; }
        }
        catch (Exception e1)
        {
            ErrorSignal.FromCurrentContext().Raise(e1);
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayIS(object pIS)
    {
        try
        {
            if (pIS.ToString() == "True") { return General.Msg("Yes", "نعم"); }
            else if (pIS.ToString() == "False") { return General.Msg("No", "لا"); }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Empty;
        }
    }


    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}