﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyServer;
using System.Data;

namespace SkyServer.Tools.Explore
{
    public partial class MetaDataControl : System.Web.UI.UserControl
    {
        protected Globals globals;

        protected double ra ;
        protected double dec ;

        protected long? specObjId = null;
        protected int? clean = null;
        protected int? mode = null;
        protected string otype = null;
        protected string survey;
        protected int? imageMJD = null;

        protected ObjectExplorer master;
        protected RunQuery runQuery;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            globals = (Globals)Application[Globals.PROPERTY_NAME];
            master = (ObjectExplorer)Page.Master;
            string token = "";
            HttpCookie cookie = Request.Cookies["Keystone"];
            if (cookie != null)
                if (cookie["token"] != null || !cookie["token"].Equals(""))
                    token = cookie["token"];
            runQuery = new RunQuery(token);

            if (master.objId != null && !master.objId.Equals(""))
            executeQuery();      
        }
     
        private void executeQuery()
        {
            string cmd = ExplorerQueries.getObjParamaters.Replace("@objId", master.objId);
            DataSet ds = runQuery.RunCasjobs(cmd,"Explore: Metadata");
            using (DataTableReader reader = ds.Tables[0].CreateDataReader())
            {
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        ra = (double)reader["ra"];
                        dec = (double)reader["dec"];
                        specObjId = reader["specObjId"] is DBNull ? -999999 : (long)(reader["specObjId"]);                       
                        clean = (int)reader["clean"];
                        survey = reader["survey"] is DBNull ? null:(string)reader["survey"];
                        mode = (int)reader["mode"];
                        otype = reader["otype"] is DBNull ? null : (string)reader["otype"];
                        imageMJD = (int)reader["mjd"];
                    }
                }
            }
        }
    }
}