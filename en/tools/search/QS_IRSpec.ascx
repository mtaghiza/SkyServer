﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QS_IRSpec.ascx.cs" Inherits="SkyServer.Tools.Search.QS_IRSpec" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace=" SkyServer.Tools.Search" %>

<table cellspacing='3' cellpadding='3' class='frame' width='640'>
<tr><td align=middle class="qtitle">Infrared Spectroscopy Constraints</td>
</tr>
     <table border=0 cellpadding=4 cellspacing=4 width=100%>
	    <tr>
		    <td class='q' rowspan=2><a href=<%=userguide%>#Magnitudes onmouseover="return escape('Apply constraints based on 2MASS infrared magnitudes');">IR Magnitudes</a></td>
		    <td class='q'>min</td>
		    <td class='q'><table width=100%><tr><td>J</td><td align=right><input class='mag' type=text name=jMin size=2></td></tr></table></td>
		    <td class='q'><table width=100%><tr><td>H</td><td align=right><input class='mag' type=text name=hMin size=2></td></tr></table></td>
		    <td class='q'><table width=100%><tr><td>K<sub>s</sub></td><td align=right><input class='mag' type=text name=kMin size=2></td></tr></table></td>
	    </tr>
	    <tr>
		    <td class='q'>max</td>
		    <td class='q'><table width=100%><tr><td>J</td><td align=right><input class='mag' type=text name=jMax size=2></td></tr></table></td>
		    <td class='q'><table width=100%><tr><td>H</td><td align=right><input class='mag' type=text name=hMax size=2></td></tr></table></td>
		    <td class='q'><table width=100%><tr><td>K<sub>s</sub></td><td align=right><input class='mag' type=text name=kMax size=2></td></tr></table></td>
	    </tr>

        <tr>
		        <td class='q' rowspan=2><a href=<%=userguide%>#Colors onmouseover="return escape('Apply constraints based on 2MASS infrared colors');">IR Colors</a></td>
		        <td class='q'>min</td>
		        <td class='q'><table width=100%><tr><td>J-H</td><td align=right><input class='mag' type=text name=jhMin size=2></td></tr></table></td>
		        <td class='q'><table width=100%><tr><td>H-K<sub>s</sub></td><td align=right><input class='mag' type=text name=hkMin size=2></td></tr></table></td>
		        <td class='q'>&nbsp;</td>
	        </tr>
	    <tr>
		    <td class='q'>max</td>
		        <td class='q'><table width=100%><tr><td>J-H</td><td align=right><input class='mag' type=text name=jhMax size=2></td></tr></table></td>
		        <td class='q'><table width=100%><tr><td>H-K<sub>s</sub></td><td align=right><input class='mag' type=text name=hkMax size=2></td></tr></table></td>
		        <td class='q'>&nbsp;</td>
	    </tr>
    </table>

    <!-- nested table -->
      <table border=0 cellpadding=4 cellspacing=4 width=100%>
          <tr>
	        <td class='q' rowspan=2 align=center width=75>
		        Observational Parameters
	        </td>
		        <td class='q' width=25>min</td>
                <td class='q' width=95><table width=100%><tr><td>SNR</sub></td><td align=right><input class='params' type=text name=snrMin size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>v<sub>Helio</sub></td><td align=right><input class='params' type=text name=vhelioMin size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>Scatter in v</td><td align=right><input class='params' type=text name=scatterMin size=2></td></tr></table></td>
	        </tr>
            <tr>
		        <td class='q' width=25>max</td>
                <td class='q' width=95><table width=100%><tr><td>SNR</sub></td><td align=right><input class='params' type=text name=snrMax size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>v<sub>Helio</sub></td><td align=right><input class='params' type=text name=vhelioMax size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>Scatter in v</td><td align=right><input class='params' type=text name=scatterMax size=2></td></tr></table></td>
	        </tr>
      </table>


      <table border=0 cellpadding=4 cellspacing=4 width=100%>
          <tr>
	        <td class='q' rowspan=2 align=center width=75>
		        Stellar Parameters
	        </td>
		        <td class='q' width=25>min</td>
                <td class='q' width=95><table width=100%><tr><td>Temp</td><td align=right><input class='params' type=text name=tempMin size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>log(g)</td><td align=right><input class='params' type=text name=loggMin size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>[M/H]</td><td align=right><input class='params' type=text name=fehMin size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>[&alpha;/Fe]</td><td align=right><input class='abunds' type=text name=afeMin size=2></td></tr></table></td>
	        </tr>
            <tr>
		        <td class='q' width=25>max</td>
                <td class='q' width=95><table width=100%><tr><td>Temp</td><td align=right><input class='params' type=text name=tempMax size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>log(g)</td><td align=right><input class='params' type=text name=loggMax size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>[M/H]</td><td align=right><input class='params' type=text name=fehMax size=2></td></tr></table></td>
                <td class='q' width=95><table width=100%><tr><td>[&alpha;/Fe]</td><td align=right><input class='abunds' type=text name=afeMax size=2></td></tr></table></td>
	        </tr>
      </table>

<!-------------------------------------------------------------------------------->
<tr class='q'><td colspan=8 align=center class=smallbodytext>
   (<b>Shift-mouse</b> to select multiple <b>contiguous</b> entries, <b>Ctrl-mouse</b>
    to select <b>non-contiguous</b> entries)</td></tr>

   <table border=0 cellpadding=4 cellspacing=4 width=100%>
	<tr>
		<td class='q'>Targeting Flags #1<br />(APOGEE_TARGET1)</td>
		<td class='q'>
		  <table>
			<tr>
<%
        ResponseREST rs = new ResponseREST();
        
        string cmd = "SELECT [name] FROM DataConstants\n";
        cmd += " WHERE field='ApogeeTarget1' AND [name] != ''\n";
        cmd += " AND [name] NOT IN ('APOGEE_FAINT', 'APOGEE_MEDIUM', 'APOGEE_BRIGHT', 'APOGEE_CHECKED')\n";
        cmd += " ORDER BY field,value";
        
        DataSet ds = rs.RunCasjobs(cmd);
        using(DataTableReader reader = ds.CreateDataReader(ds.Tables[0]))
        {
                if (!reader.HasRows)
                {
    %>
                   <td><b>No APOGEE Targeting Flags (APOGEE_TARGET1) found in DataConstants table</b></td>
    <%            }
                else
                {
     %>              <td align='middle' onmouseover="return escape('The bit-wise OR of these flags must be non-zero');">
                        <strong>At least one of these flags ON</strong></td>
                    <td width=20>&nbsp;</td>
                    <td align='middle' onmouseover="return escape('The bit-wise AND of these flags must be zero');">
                        <strong>All of these flags OFF</strong><br>
                    </td></tr>
                    <tr><td><SELECT name="irTargetFlagsOnList" multiple="multiple+" size="5">
                        <OPTION value="ignore" selected>ignore</OPTION>
      <%            List<string> values = new List<string>();
                    while (reader.Read())
                    {
                        
                        values.Add(reader.GetValue(0).ToString());
                    }

                    foreach (string v in values)
                    {
        %>
                        <OPTION value="<%=v %>"><%=v %> 
       <%           }
       %>           </OPTION></SELECT></td><td>&nbsp;</td>

               <td><SELECT name="irTargetFlagsOffList" multiple="multiple+" size="5">
                    <OPTION value="ignore" selected>ignore</OPTION>
        <%            foreach (string v in values)
                      {
        %>              
                            <OPTION value="<%=v%>"><%=v %>
        <%            }
        %>            </OPTION></SELECT></td>
        <%      }
            } // using DataReader   
%>
			</tr>
		  </table>
        </td>
    </tr>
	<tr>
		<td class='q'>Targeting Flags #2<br />(APOGEE_TARGET2)</td>
		<td class='q'>
		  <table>
			<tr>
<%
        cmd = "SELECT [name] FROM DataConstants\n";
        cmd += " WHERE field='ApogeeTarget2' AND [name] != ''\n";
        cmd += " AND [name] NOT IN ('APOGEE_EMBEDDEDCLUSTER_STAR', 'APOGEE_LONGBAR', 'APOGEE_EMISSION_STAR', 'APOGEE_KEPLER_COOLDWARF', 'APOGEE_MIRCLUSTER_STAR', 'APOGEE_CHECKED')\n";
        cmd += " ORDER BY field,value";
        DataSet data =  rs.RunCasjobs(cmd);
        using (DataTableReader reader = data.CreateDataReader(ds.Tables[0]))
        {
                if (!reader.HasRows)
                {
 %>                  
                 <td><b>No APOGEE Targeting Flags (APOGEE_TARGET2) found in DataConstants table</b></td>
 <%             }
                else
                {
%>                  <td align='middle' onmouseover="return escape('The bit-wise OR of these flags must be non-zero');">
                        <strong>At least one of these flags ON</strong></td>\n");
                    <td width=20>&nbsp;</td>
                    <td align='middle' onmouseover="return escape('The bit-wise AND of these flags must be zero');">
                        <strong>All of these flags OFF</strong><br></td></tr>
                    <tr><td><SELECT name="irTargetFlags2OnList" multiple="multiple+" size="5">
                    <OPTION value="ignore" selected>ignore</OPTION>
 <%                 List<string> values = new List<string>();
                    while (reader.Read())
                    {
                        
                        values.Add(reader.GetValue(0).ToString());
                    }

                    foreach (string v in values)
                    {
%>                        <OPTION value="<%=v%>"><%=v %>
<%                  }
%>                    
                    </OPTION></SELECT></td><td>&nbsp;</td>
                    <td><SELECT name="irTargetFlags2OffList" multiple="multiple+" size="5">
                    <OPTION value="ignore" selected>ignore</OPTION>
<%                  foreach (string v in values)
                    {
%>                        <OPTION value="<%=v%>"><%=v%>
<%                  }
%>                  </OPTION></SELECT></td>
<%                }
            } // using DataReader  
%>
			</tr>
		  </table>
<!-------------------------------------------------------------------------------->
</td></tr>

</table>
 
<table width="100%">
		<tr>
			<td align="left"><input id=submit type="submit" value="Submit Request"></td>
			<td colspan="2">&nbsp;</td>
			<td align="right"><input id=reset  type="reset" value="Reset Form"></td>
		</tr>
</table>
