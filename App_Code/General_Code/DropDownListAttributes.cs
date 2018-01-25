using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDLAttributes
{
    /// <summary>
    /// inheriting the DropDownList to save attributes
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class DropDownListAttributes : DropDownList
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// By using this method save the attributes data in view state
        /// </summary>
        /// <returns></returns>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]        
        protected override object SaveViewState()
        {
            object[] allStates = new object[this.Items.Count + 1];

            object baseState = base.SaveViewState();
            allStates[0] = baseState;

            Int32 i = 1;
 
            foreach (ListItem li in this.Items)
            {
                Int32 j = 0;
                string[][] attributes = new string[li.Attributes.Count][];
                foreach (string attribute in li.Attributes.Keys)
                    attributes[j++] = new string[] { attribute, li.Attributes[attribute] };
                
                allStates[i++] = attributes;
            }
            return allStates;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// /// By using this method Load the attributes data from view state
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] myState = (object[])savedState;

                if (myState[0] != null)
                    base.LoadViewState(myState[0]);

                Int32 i = 1;
                foreach (ListItem li in this.Items)               
                    foreach (string[] attribute in (string[][])myState[i++])                    
                        li.Attributes[attribute[0]] = attribute[1];
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum ShowType { ALL, ActiveOnly }

        public void Show(ShowType SType)
        {
            this.SelectedIndex = -1;

            foreach (ListItem li in this.Items)
            {
                if (SType == ShowType.ALL) { li.Enabled = true; }
                else if (SType == ShowType.ActiveOnly)
                {
                    string Attr = Convert.ToString(li.Attributes["Status"]);
                    if (string.IsNullOrEmpty(Attr) || Attr == "T") { li.Enabled = true; } else { li.Enabled = false; }
                }
            }             
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Populate(DataTable DT, string DataText, string  DataValue, string DataStatus, string InitialMsg)
        {
            try
            {
                if (IsNullOrEmpty(DT)) { return false; }

                //DataRow DR = DT.NewRow();
                //DR[0] = "0"; /**/ DR[1] = "True"; /**/ DR[2] = DR[3] = Msg;
                //DT.Rows.InsertAt(DR, 0);
                //DT.AcceptChanges();

                this.DataSource = null;
                this.Items.Clear();

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(DT.Rows[i][DataText])))
                    {
                        ListItem _li = new ListItem(DT.Rows[i][DataText].ToString(), DT.Rows[i][DataValue].ToString()); 
                        if (!string.IsNullOrEmpty(DataStatus)) { if (Convert.ToBoolean(DT.Rows[i][DataStatus])) { _li.Attributes.Add("Status", "T"); } else { _li.Attributes.Add("Status", "F"); } }
                        this.Items.Add(_li);
                    }
                }

                ListItem lsMsg = new ListItem(InitialMsg, InitialMsg);
                lsMsg.Attributes.Add("Status", "T");
                this.Items.Insert(0, lsMsg);

                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected bool IsNullOrEmpty(DataTable DT)
        {
            if (DT == null)         { return true; }
            if (DT.Rows.Count == 0) { return true; }
            return false;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}