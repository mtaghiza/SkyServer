﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using SkyServer;
using SkyServer.Tools.Explore;
using System.Collections.Specialized;
using System.Data;

namespace SkyServer.Tools.Explore
{
    public partial class ObjectExplorer : System.Web.UI.MasterPage
    {
        protected const string ZERO_ID = "0x0000000000000000";

        protected HRefs hrefs = new HRefs();

        protected long? id = null;
        public string apid;
        protected long? specId = null;

        protected int tabwidth = 128;
        
        protected string url;
        protected string enUrl;

        double? ra;
        double? dec;
        public string objId;
        string fieldId;
        string specObjId;
        string plateId;
        int? mjdNum;
        short? fiberId;
        int? run;
        short? rerun;
        short? camcol;
        short? field;
        protected int? plate = null;

        protected Globals globals;
        public ExplorerQueries exploreQuery;
        public RunQuery runQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            globals = (Globals)Application[Globals.PROPERTY_NAME];
            url = getURL();
            enUrl = getEnURL();

            string qId = HttpUtility.UrlEncode(Request.QueryString["id"]);
            string qSpecId = Request.QueryString["spec"];
            string qApogeeId = HttpUtility.UrlEncode(Request.QueryString["apid"]);

            id = Utilities.ParseId(qId);
            specId = Utilities.ParseId(qSpecId);
            apid = ("".Equals(qApogeeId))?null:qApogeeId;

            objId = qId;

            using (SqlConnection oConn = new SqlConnection(globals.ConnectionString))
            {
                    oConn.Open();
                    if (id.HasValue) pmtsFromPhoto(oConn, id, specId);
            }

            
            exploreQuery = new ExplorerQueries(id.ToString(), specId.ToString(), apid, fiberId.ToString(), plateId);
            runQuery = new RunQuery();
            
            // id is the decimal representation; objId is the hex representation.
            hrefs.Summary = "summary.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;

            // common query to explorer
            string explore = "Explorer.aspx?cmd=";
            
            hrefs.PhotoObj = explore + exploreQuery.PhotoObjQuery + "&name=PhotoObj&id=" + id;
            hrefs.PhotoTag = explore + exploreQuery.PhotoTagQuery + "&name=PhotoTag&id=" + id;
            hrefs.Field    = explore + exploreQuery.FieldQuery + "&name=Field&id=" + id ;
            hrefs.Frame    = explore + exploreQuery.FrameQuery + "&name=Frame&id=" + id ;
            

            if (globals.ReleaseNumber >= 8)
                    hrefs.Galaxyzoo = "galaxyzoo.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;

            if (globals.ReleaseNumber > 4)
            {
                hrefs.PhotoZ =  explore+exploreQuery.PhotoZ+"&name=photoZ&id=" + id ;
                hrefs.PhotozRF = explore + exploreQuery.PhotozRF + "&name=photozRF&id=" + id;
            }

            hrefs.Matches = "matches.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;
            hrefs.Neighbors = "neighbors.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;
            hrefs.Chart = "javascript:gotochart(" + ra + "," + dec + ");";
            hrefs.Navigate = "javascript:gotonavi(" + ra + "," + dec + ");";
            hrefs.SaveBook = "javascript:saveBook(\"" + objId + "\");";
            hrefs.ShowBook = "javascript:showNotes();";
            
            if (globals.Database.StartsWith("STRIPE"))
            {
                    if (run == 106)  run = 100006;
                    if (run == 206)  run = 200006;
            }

            hrefs.FITS = "fitsimg.aspx?id=" + id + "&fieldId=" + fieldId + "&spec=" + specId + "&apid=" + apid;
            
            hrefs.NED = "http://nedwww.ipac.caltech.edu/cgi-bin/nph-objsearch?search_type=Near+Position+Search"
                                + "&in_csys=Equatorial&in_equinox=J2000.0&obj_sort=Distance+to+search+center"
                                + "&lon=" + (ra.HasValue?(Math.Round((double)ra, 7).ToString()+"d"):"") + "&lat=" + (dec.HasValue?(Math.Round((double)dec, 7).ToString()+"d"):"") + "&radius=1.0";
            string hmsRA;
                hmsRA = Functions.hmsPad(ra ?? 0).Replace(" ", "+");

            string dmsDec;
                if (dec >= 0)
                    dmsDec = Functions.dmsPad(dec ?? 0).Replace("+", "%2B");
                else
                    dmsDec = Functions.dmsPad(dec ?? 0);

                dmsDec = dmsDec.Replace(" ", "+");

            hrefs.SIMBAD = "http://simbad.u-strasbg.fr/sim-id.pl?protocol=html&Ident=" + hmsRA + "+" + dmsDec + "&NbIdent=1"
                                    + "&Radius=1.0&Radius.unit=arcmin&CooFrame=FK5&CooEpoch=2000&CooEqui=2000"
                                    + "&output.max=all&o.catall=on&output.mesdisp=N&Bibyear1=1983&Bibyear2=2005"
                                    + "&Frame1=FK5&Frame2=FK4&Frame3=G&Equi1=2000.0&Equi2=1950.0&Equi3=2000.0"
                                    + "&Epoch1=2000.0&Epoch2=1950.0&Epoch3=2000.0";
            hrefs.ADS = "http://adsabs.harvard.edu/cgi-bin/nph-abs_connect?db_key=AST&sim_query=YES&aut_xct=NO&aut_logic=OR"
                                    + "&obj_logic=OR&author=&object=" + hmsRA + "+" + dmsDec + "&start_mon=&start_year=&end_mon="
                                    + "&end_year=&ttl_logic=OR&title=&txt_logic=OR&text=&nr_to_return=100&start_nr=1"
                                    + "&start_entry_day=&start_entry_mon=&start_entry_year=&min_score=&jou_pick=ALL"
                                    + "&ref_stems=&data_and=ALL&group_and=ALL&sort=SCORE&aut_syn=YES&ttl_syn=YES"
                                    + "&txt_syn=YES&aut_wt=1.0&obj_wt=1.0&ttl_wt=0.3&txt_wt=3.0&aut_wgt=YES&obj_wgt=YES"
                                    + "&ttl_wgt=YES&txt_wgt=YES&ttl_sco=YES&txt_sco=YES&version=1";

            hrefs.Print = "framePrint();";
            hrefs.AllSpec = "allSpec.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;

            if (specId != null)
            {
                hrefs.SpecObj     = explore + exploreQuery.SpecObjQuery      + "&name=SpecObj&spec="     + specId;
                hrefs.sppLines    = explore + exploreQuery.sppLinesQuery     + "&name=sppLines&spec="    + specId;
                hrefs.sppParams   = explore + exploreQuery.sppParamsQuery    + "&name=sppParams&spec="   + specId;
                hrefs.galSpecLine = explore + exploreQuery.galSpecLineQuery  + "&name=galSpecLine&spec=" + specId;
                hrefs.galSpecIndx = explore + exploreQuery.galSpecIndexQuery + "&name=galSpecIndx&spec=" + specId;
                hrefs.galSpecInfo = explore + exploreQuery.galSpecInfoQuery  + "&name=galSpecInfo&spec=" + specId;
                hrefs.Plate       = explore + exploreQuery.Plate + "&name=Plate&plateId=" + plateId;

                hrefs.Spectrum = "../../get/SpecById.ashx?ID=" + specId;

                hrefs.SpecFITS = "fitsspec.aspx?&sid=" + specObjId + "&id=" + id + "&spec=" + specId + "&apid=" + apid;
                    
                if (globals.ReleaseNumber >= 8)
                {
                    hrefs.theParameters = "parameters.aspx?id=" + id + "&spec=" + specId + "&apid=" + apid;
                    hrefs.stellarMassStarformingPort = explore + exploreQuery.stellarMassStarformingPortQuery+ "&name=stellarMassStarFormingPort&spec=" + specId;
                    hrefs.stellarMassPassivePort     = explore + exploreQuery.stellarMassPassivePortQuery+ "&name=stellarMassPassivePort&spec=" + specId;
                    hrefs.emissionLinesPort          = explore + exploreQuery.emissionLinesPortQuery + "&name=emissionlinesPort&spec=" + specId;
                    hrefs.stellarMassPCAWiscBC03     = explore + exploreQuery.stellarMassPCAWiscBC03Query+"&name=stellarMassPCAWiscBC03&spec=" + specId ;
                    hrefs.stellarMassPCAWiscM11      = explore + exploreQuery.stellarMassPCAWiscM11Query+ "&name=stellarMassPCAWiscM11&spec=" + specId ;
                }
                if (globals.ReleaseNumber >= 10)
                {
                    hrefs.stellarMassFSPSGranEarlyDust   = explore + exploreQuery.stellarMassFSPSGranEarlyDust + "&name=stellarMassFSPSGranEarlyDust&spec=" + specId;
                    hrefs.stellarMassFSPSGranEarlyNoDust = explore + exploreQuery.stellarMassFSPSGranEarlyNoDust+"&name=stellarMassFSPSGranEarlyNoDust&spec=" + specId;
                    hrefs.stellarMassFSPSGranEarlyDust   = explore + exploreQuery.stellarMassFSPSGranWideDust + "&name=stellarMassFSPSGranWideDust&spec=" + specId;
                    hrefs.stellarMassFSPSGranWideNoDust  = explore + exploreQuery.stellarMassFSPSGranWideNoDust + "&name=stellarMassFSPSGranWideNoDust&spec=" + specId;
                 }
             }            
             if (!String.IsNullOrEmpty(apid))
             {
                 hrefs.apogeeStar = explore + exploreQuery.apogeeStar + "&name=apogeeStar&apid=" + apid;
                 hrefs.aspcapStar = explore + exploreQuery.aspcapStar + "&name=aspcapStar&apid=" + apid;
             }
        }

        public void pmtsFromPhoto(SqlConnection oConn, long? id, long? specId) 
        {
            using (SqlCommand oCmd = oConn.CreateCommand())
            {
                oCmd.CommandText =
                    " select p.ra, p.dec, p.run, p.rerun, p.camcol, p.field," +
                    " cast(p.fieldId as binary(8)) as fieldId," +
                    " cast(s.specobjid as binary(8)) as specObjId," +
                    " cast(p.objId as binary(8)) as objId " +
                    " from PhotoTag p " +
                    " left outer join SpecObjAll s ON s.bestobjid=p.objid " +
                    " where p.objId=dbo.fObjId(@id) ";

                oCmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ra = reader.GetDouble(0);
                        dec = reader.GetDouble(1);
                        run = reader.GetInt16(2);
                        rerun = reader.GetInt16(3);
                        camcol = reader.GetByte(4);
                        field = reader.GetInt16(5);
                        fieldId = Functions.BytesToHex(reader.GetSqlBytes(6).Buffer);
                        specObjId = Functions.BytesToHex(reader.GetSqlBytes(7).Buffer);
                        objId = Functions.BytesToHex(reader.GetSqlBytes(8).Buffer);
                    }
                }
           
                if (specId == null)
                {
                    specId = Utilities.ParseId(specObjId);
                } 

                if (specId != null)
                {
                    oCmd.Parameters.Clear();
                    oCmd.CommandText = 
                        " select cast(s.plateId as binary(8)) as plateId, s.mjd, s.fiberId, q.plate" +
                        " from SpecObjAll s JOIN PlateX q ON s.plateId=q.plateId where specObjId=@specId"; //specObjId";
                    oCmd.Parameters.AddWithValue("@specId", specId);
                    using (SqlDataReader reader = oCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                                plateId = Functions.BytesToHex(reader.GetSqlBytes(0).Buffer);
                                mjdNum = reader.GetInt32(1);
                                fiberId = reader.GetInt16(2);
                                plate = reader.GetInt16(3);
                        }
                    }
                }
            }
        }

        public string getURL()
        {
            string host = Request.ServerVariables["SERVER_NAME"];
            string path = Request.ServerVariables["SCRIPT_NAME"];

            string root = "http://" + host;
            string s = path;
            string[] q = s.Split('/');

            string lang = "";
            for (int i = 0; i < q.Length; i++)
            {
                if (i > 0) root += "/";
                root += q[i];
                lang = q[i];
                if (lang == "en" || lang == "de" || lang == "jp"
                  || lang == "hu" || lang == "sp" || lang == "ce" || lang == "pt" || lang == "zh" || lang == "uk" || lang == "ru")
                {
                    //depth = q.length - i - 2;
                    return root;
                }
            }
            return root;
        }

        public string getEnURL()
        {
            string host = Request.ServerVariables["SERVER_NAME"];
            string path = Request.ServerVariables["SCRIPT_NAME"];

            string root = "http://" + host;
            string s = path;
            string[] q = s.Split('/');

            string lang = "";
            for (int i = 0; i < q.Length; i++)
            {
                if (i > 0) root += "/";
                lang = q[i];
                if (lang == "en" || lang == "de" || lang == "jp"
                  || lang == "hu" || lang == "sp" || lang == "ce" || lang == "pt" || lang == "zh" || lang == "uk" || lang == "ru")
                {
                    //depth = q.Length - i - 2;
                    root += "en";
                    return root;
                }
                else
                {
                    root += q[i];
                }
            }
            return root;
        }

        // ***** Functions *****

        public void showNTable(SqlConnection oConn, string cmd) {
            using (SqlCommand oCmd = oConn.CreateCommand())
            {
                oCmd.CommandText = cmd;
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    string u = "<a class='content' target='_top' href='obj.aspx?id=";
                    string id, v;
                    char c;

                    Response.Write("<table cellpadding=2 cellspacing=2 border=0>");

                    if (reader.HasRows)
                    {
                        Response.Write("<tr>");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Response.Write("<td align='middle' class='h'>" + reader.GetName(i) + "</td>");
                        Response.Write("</tr>\n");
                    }

                    c = 't';
                    while (reader.Read())
                    {
                        Response.Write("<tr>");
                        id = reader.GetValue(0).ToString();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            v = reader.GetValue(i).ToString();
                            v = (v == "" ? "&nbsp;" : v);
                            Response.Write("<td nowrap align='middle' class='" + c + "'>");
                            if (i == 0) Response.Write(u + id + "'>" + id + "</a></td>");
                            else Response.Write(v + "</td>");
                        }
                        Response.Write("</tr>\n");
                        c = (c == 't' ? 'b' : 't');
                    }
                    Response.Write("</table>\n");
                } // using SqlDataReader
            } // using SqlCommand
        }

        //public void showSTable(SqlConnection oConn, string cmd) 
        //{
        //    using (SqlCommand oCmd = oConn.CreateCommand())
        //    {
        //        oCmd.CommandText = cmd;

        //        string u = "<a class='content' target='_top' href='obj.aspx?sid=";
        //        string id, v;
        //        char c;

        //        Response.Write("<table cellpadding=2 cellspacing=2 border=0>");

        //        Response.Write("<tr>");
        //        using (SqlDataReader reader = oCmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                    Response.Write("<td align='middle' class='h'>" + reader.GetName(i) + "</td>");
        //            }
        //            Response.Write("</tr>\n");

        //            c = 't';

        //            while (reader.Read())
        //            {
        //                Response.Write("<tr>");
        //                id = reader.GetValue(0).ToString();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    v = reader.GetValue(i).ToString();
        //                    v = (v == "" ? "&nbsp;" : v);
        //                    Response.Write("<td nowrap align='middle' class='" + c + "'>");
        //                    if (i == 0) Response.Write(u + id + "'>" + id + "</a></td>");
        //                    else Response.Write(v + "</td>");
        //                }
        //                Response.Write("</tr>\n");
        //                c = (c == 't' ? 'b' : 't');
        //            }
        //            Response.Write("</table>\n");
        //        } //using SqlDataReader
        //    } // using SqlCommand
        //}

        //public void showFTable(SqlConnection oConn, long? plateId) {
        //    using (SqlCommand oCmd = oConn.CreateCommand()) {
        //        string cmd = " select cast(specObjID as binary(8)) as specObjId," +
        //            " fiberId, class as name, str(z,5,3) as z" +
        //            " from SpecObjAll where plateID=@plateId order by fiberId";

        //        oCmd.CommandText = cmd;
        //        oCmd.Parameters.AddWithValue("@plateId", plateId);

        //        using (SqlDataReader reader = oCmd.ExecuteReader()) {

        //            string u = "<a class='content' target='_top' href='obj.aspx?sid=";
        //            string sid;
	
        //            int col=0;
        //            int row=0;
        //            string c = "st";
        //            Response.Write("<table>\n");
        //            Response.Write("<tr>");
        //            while(reader.Read()) {
        //                sid   = u+Functions.BytesToHex(reader.GetSqlBytes(0).Buffer)+"'>";
        //                string v = "["+reader.GetValue(1).ToString()+"]&nbsp;";
        //                v += reader.GetValue(2).ToString()+" z="+reader.GetValue(3).ToString();
        //                Response.Write("<td nowrap class='"+c+"'>"+sid+v+"</a></td>\n");
        //                if (++col>3) {
        //                    col = 0;
        //                    row++;
        //                    Response.Write("</tr>\n<tr>\n");
        //                    c = ("st".Equals(c)?"sb":"st");
        //                }
        //            }
        //            Response.Write("<td></td></tr>\n</table>\n");
        //        } // using SqlDataReader
        //    } // using SqlCommand
        //}

        public string getUnit(SqlConnection oConn, string tableName, string name)
        {
            using (SqlCommand oCmd = oConn.CreateCommand())
            {
                oCmd.CommandText = "select unit from DBcolumns where tablename=@tablename and [name]=@name";
                oCmd.Parameters.AddWithValue("@tablename", tableName);
                oCmd.Parameters.AddWithValue("@name", name);

                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            return "";
        }

       
        /// <summary>
        /// Added new HTable with namevalue pair options
        /// </summary>
        /// <param name="namevalues"></param>
        /// <param name="w"></param>
        public void showHTable(NameValueCollection namevalues, int w)
        {
            char c = 't'; string unit = "test";
            
            Response.Write("<table cellpadding=2 cellspacing=2 border=0");
            
            if (w > 0)
                Response.Write(" width=" + w);
            Response.Write(">\n");
            
                Response.Write("<tr>");

                foreach (String k in namevalues.AllKeys)
                {
                    Response.Write("<td align='middle' class='h'>");
                    Response.Write("<span ");
                    if (unit != "")
                        Response.Write("ONMOUSEOVER=\"this.T_ABOVE=true;this.T_WIDTH='100';return escape('<i>unit</i>=" + unit + "')\" ");
                    Response.Write("></span>");
                    Response.Write(k+"</td>");                                      
                }
                Response.Write("</tr>");
                
                Response.Write("<tr>");

                foreach (String k in namevalues.AllKeys)
                {
                    Response.Write("<td nowrap align='middle' class='" + c + "'>");
                    Response.Write(namevalues[k]);
                    Response.Write("</td>");
                }
                Response.Write("</tr>");
            
            Response.Write("</table>");
        }
        
        /// <summary>
        /// Vertical aligned table  With name value pair
        /// </summary>
        /// <param name="namevalues"></param>
        /// <param name="w"></param>
        public void showVTable(NameValueCollection namevalues, int w)
        {
            char c = 't'; string unit = "test";
            Response.Write("<table cellpadding=2 cellspacing=2 border=0");
            if (w > 0)
                Response.Write(" width=" + w);
            Response.Write(">\n");
            foreach (String k in namevalues.AllKeys)
            {
                Response.Write("<tr align='left' >");
                Response.Write("<td  valign='top' class='h'>");
                Response.Write("<span ");
                if (unit != "")
                    Response.Write("ONMOUSEOVER=\"this.T_ABOVE=true;this.T_WIDTH='100';return escape('<i>unit</i>=" + unit + "')\" ");
                Response.Write("></span>");
                Response.Write(k + "</td>");
            
                Response.Write("<td valign='top' class='" + c + "'>");
                Response.Write(namevalues[k]);
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
        }


        /// <summary>
        /// Vertical aligned table  With DataSet
        /// </summary>
        /// <param name="namevalues"></param>
        /// <param name="w"></param>
        public void showVTable(DataSet ds, int w)
        {
            using (DataTableReader reader = ds.Tables[0].CreateDataReader())
            {
                char c = 't'; string unit = "test";
                Response.Write("<table cellpadding=2 cellspacing=2 border=0");
                if (w > 0)
                    Response.Write(" width=" + w);
                Response.Write(">\n");
                if (reader.Read())
                {
                    for(int k=0; k <reader.FieldCount; k++)
                    {
                        Response.Write("<tr align='left' >");
                        Response.Write("<td  valign='top' class='h'>");
                        Response.Write("<span ");
                        if (unit != "")
                            Response.Write("ONMOUSEOVER=\"this.T_ABOVE=true;this.T_WIDTH='100';return escape('<i>unit</i>=" + unit + "')\" ");
                        Response.Write("></span>");
                        Response.Write(reader.GetName(k) + "</td>");

                        Response.Write("<td valign='top' class='" + c + "'>");
                        Response.Write(reader.GetValue(k));
                        Response.Write("</td>");
                        Response.Write("</tr>");
                    }
                }
                Response.Write("</table>");
            }
        }

        /// <summary>
        /// Added new HTable with namevalue pair options
        /// </summary>
        /// <param name="namevalues"></param>
        /// <param name="w"></param>
        public void showHTable(DataSet ds, int w, string target)
        {
            using (DataTableReader reader = ds.Tables[0].CreateDataReader())
            {
                char c = 't'; string unit = "test";

                Response.Write("<table cellpadding=2 cellspacing=2 border=0");

                if (w > 0)
                    Response.Write(" width=" + w);
                Response.Write(">\n");

                Response.Write("<tr>");

                if (reader.Read())
                {
                    //foreach (String k in namevalues.AllKeys)
                    for (int k = 0; k < reader.FieldCount;k++ )
                    {
                        Response.Write("<td align='middle' class='h'>");
                        Response.Write("<span ");
                        if (unit != "")
                            Response.Write("ONMOUSEOVER=\"this.T_ABOVE=true;this.T_WIDTH='100';return escape('<i>unit</i>=" + unit + "')\" ");
                        Response.Write("></span>");                        
                        Response.Write(reader.GetName(k) + "</td>");
                    }
                    Response.Write("</tr>");

                    Response.Write("<tr>");

                    for (int k = 0; k < reader.FieldCount; k++)
                    {
                        Response.Write("<td nowrap align='middle' class='" + c + "'>");

                        // think something else if possible for this
                        if (target.Equals("AllSpectra") && k == 0)
                        {
                            string u = "<a class='content' target='_top' href='obj.aspx?sid=";

                            Response.Write(u + reader.GetValue(k) + "'>" + reader.GetValue(k) + "</a></td>");
                            
                        }
                        else
                        {
                            Response.Write(reader.GetValue(k));
                        }
                        Response.Write("</td>");
                    }
                }

                Response.Write("</tr>");

                Response.Write("</table>");
            }
        }
        

    }
}