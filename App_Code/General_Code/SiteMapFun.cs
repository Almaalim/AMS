using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

public class SiteMapFun : StaticSiteMapProvider
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DBFun DBCs = new DBFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private SiteMapNode parentNode { get; set; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string Lang { get; set; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ExcludedFolders { get { return "(App_Data)|(App_Code)|(App_LocalResources)|(App_Themes)|(CDUP)|(Control)|(CSS)|(FusionCharts)|(HTML)|(images)|(RequestsFiles)|(Script)|(Service)|(Stimulsoft)|(TempFiles)|(tinymce)|(win10Menu)|(XSL)|(bin)"; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string ExcludedFiles { get { return "(AMSMasterPage.master)|(Default.aspx)|(Global.asax)|(Login.aspx)|(LoginMasterPage.master)|(web.config)|(Web.sitemap)|(Home.aspx)"; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override SiteMapNode BuildSiteMap()
    {
        lock (this)
        {
            DataTable DT = DBCs.FetchData(new SqlCommand("SELECT * FROM MENU"));

            Lang = HttpContext.Current.Cache["SiteMapLang"] as string;
            parentNode = HttpContext.Current.Cache["SiteMapParentNode1"] as SiteMapNode;
            if (parentNode == null || Lang != General.Msg("EN", "AR"))
            {
                base.Clear();
                Lang = General.Msg("EN", "AR");

                DataRow[] DRs = DT.Select("MnuURL ='Home.aspx'");
                string DisName = (DRs.Length == 0) ? "Home" : DRs[0][General.Msg("MnuTextEn", "MnuTextAr")].ToString();

                string AppDomain = HttpRuntime.AppDomainAppVirtualPath;
                if (AppDomain == "/") { AppDomain = "~"; }
                parentNode = new SiteMapNode(this, AppDomain, AppDomain + "/Pages_Mix/Home.aspx", DisName);

                AddNode(parentNode);
                AddFiles(parentNode, DT);
                AddFolders(parentNode, DT);

                HttpContext.Current.Cache.Insert("SiteMapLang", Lang);
                HttpContext.Current.Cache.Insert("SiteMapParentNode1", parentNode);
            }
            return parentNode;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void AddFolders(SiteMapNode parentNode, DataTable DT)
    {
        var folders = from o in Directory.GetDirectories(HttpContext.Current.Server.MapPath(parentNode.Key))
                      let dir = new DirectoryInfo(o)
                      where !Regex.Match(dir.Name, ExcludedFolders).Success
                      select new
                      {
                          DirectoryName = dir.Name
                      };

        foreach (var item in folders)
        {
            string folderUrl = parentNode.Key + "/" + item.DirectoryName;
            DataRow[] DRs = DT.Select("MnuParentID = 0 AND MnuServer ='~/" + item.DirectoryName + "/'");
            string DisName = (DRs.Length == 0) ? item.DirectoryName : DRs[0][General.Msg("MnuTextEn", "MnuTextAr")].ToString();

            SiteMapNode folderNode = new SiteMapNode(this, folderUrl, null, DisName, DisName);

            AddNode(folderNode, parentNode);
            AddFiles(folderNode, DT);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void AddFiles(SiteMapNode folderNode, DataTable DT)
    {
        var files = from o in Directory.GetFiles(HttpContext.Current.Server.MapPath(folderNode.Key))
                    let fileName = new FileInfo(o)
                    where !Regex.Match(fileName.Name, ExcludedFiles).Success
                    select new
                    {
                        FileName = fileName.Name
                    };

        foreach (var item in files)
        {
            if (item.FileName == "RequestMaster.aspx" || item.FileName == "Reports.aspx")
            {
                DataRow[] DRs = DT.Select("MnuURL LIKE '%" + item.FileName + "?%'");
                foreach (DataRow DR in DRs)
                {
                    SiteMapNode fileNode = new SiteMapNode(this, DR["MnuURL"].ToString(), folderNode.Key + "/" + DR["MnuURL"].ToString(), DR[General.Msg("MnuTextEn", "MnuTextAr")].ToString());
                    AddNode(fileNode, folderNode);
                }
            }
            else
            {
                DataRow[] DRs = DT.Select("MnuURL ='" + item.FileName + "'");
                string DisName = (DRs.Length == 0) ? item.FileName : DRs[0][General.Msg("MnuTextEn", "MnuTextAr")].ToString();

                string ss = folderNode.Key;

                SiteMapNode fileNode = new SiteMapNode(this, item.FileName, folderNode.Key + "/" + item.FileName, DisName);
                AddNode(fileNode, folderNode);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected override SiteMapNode GetRootNodeCore() { return BuildSiteMap(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}