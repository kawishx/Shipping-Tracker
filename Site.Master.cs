using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shipping_Tracker_V1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
           
                // Optional: Clear session or authentication token if used
                Session.Clear(); // Clears all session data
                Session.Abandon(); // Ends the session

                // Redirect to Login Page
                Response.Redirect("~/Login.aspx");
            

        }
    }
}