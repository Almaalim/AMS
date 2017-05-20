using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

/// <summary>
/// Summary description for ZipFiles
/// </summary>

public class ZipFiles
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool Zip(string zipName,string SettingPath, string RepArPath, string RepEnPath, string RepArDefPath, string RepEnDefPath)
    {
        try
        {
            using (ZipFile zip = new ZipFile())
            {
                //if (File.Exists(zipName))    { File.Delete(zipName); }

                if (File.Exists(SettingPath))  { zip.AddFile(SettingPath,""); }

                zip.AddDirectoryByName("Ar");
                if (File.Exists(RepArPath))    { zip.AddFile(RepArPath,"Ar"); } 
                    
                zip.AddDirectoryByName("En");
                if (File.Exists(RepEnPath))    { zip.AddFile(RepEnPath,"En"); } 
                    
                zip.AddDirectoryByName("DefAr");
                if (File.Exists(RepArDefPath)) { zip.AddFile(RepArDefPath,"DefAr"); } 

                zip.AddDirectoryByName("DefEn");
                if (File.Exists(RepEnDefPath)) { zip.AddFile(RepEnDefPath,"DefEn"); } 
                    
                zip.Save(zipName); //+ ".ams"


                   

                //string sourceFile      = source      + fileName.Replace("." + fileExt, zipExt);
                //string destinationFile = destination + fileName.Replace("." + fileExt, zipExt);

                //System.IO.File.Move(sourceFile, destinationFile);
            }
            return true;
        }
        catch (Exception ex) { return false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool unZip(string Path)
    {
        try
        {
            using (ZipFile zip = Ionic.Zip.ZipFile.Read(Path))
            {
                //zip.ExtractAll(General.TempPath(),ExtractExistingFileAction.OverwriteSilently);
            } 

            return true;
        }
        catch (Exception ex) { return false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}