using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkyServer.en.tools.explore
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
   {
        public bool loggedin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void logUserIn(object sender, EventArgs e)
        {
            loggedin = true;

        }

        protected void logUserOut(object sender, EventArgs e)
        {
            loggedin = false;

        }
    }

}