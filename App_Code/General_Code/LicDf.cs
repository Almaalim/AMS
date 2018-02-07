﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Class_AMS_Lic;
using System.Net;

public class LicDf : DataLayerBase
{ 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public enum VerEnum { General, Al_JoufUN, ImamUN, GACA, BorderGuard, Al_Maalim }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public LicDf() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string DetermineCompName(string IP)
    {
        IPAddress myIP = IPAddress.Parse(IP);
        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
        return compName.First();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string getClientPCName()
    {
        string ClientPCName ="";

        try
        {
            IPAddress myIP = IPAddress.Parse(HttpContext.Current.Request.UserHostName);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            ClientPCName = GetIPHost.HostName.ToString();
        } catch {}
        
        return ClientPCName;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FetchLic(string Lic)
    {
        //string ClientPCName = getClientPCName();
        string ClientPCName = "";
        //string ClientPCName = "Ameen.Almaalim.local";
        return AMSLic.FetchAMSLic(Lic, General.ConnString, ClientPCName);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindLicPage()
    {
        //string ClientPCName = getClientPCName();
        string ClientPCName = "";
        //string ClientPCName = "Ameen.Almaalim.local";
        return AMSLic.FindAMSLicPage(General.ConnString, ClientPCName);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindActiveVersion() { return AMSLic.GetAMSActiveVersion(General.ConnString); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public static string FetchVerName(VerEnum name) { return name.ToString(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}