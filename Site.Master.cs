using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Shipping_Tracker_V1
{
    public partial class SiteMaster : MasterPage
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx"); // Redirect if session is missing
                return;
            }

            if (!IsPostBack)
            {
                LoadMenu();
            }
        }

        private void LoadMenu()
        {
            DataTable dt = GetMenuData();

            if (dt.Rows.Count > 0)
            {
                string menuHtml = "<ul>";
                foreach (DataRow row in dt.Rows)
                {
                    string parent = row["parent"].ToString();
                    string path = row["path"].ToString();
                    menuHtml += $"<li><a href='{path}'>{parent}</a></li>";
                }
                menuHtml += "</ul>";

                NavMenu.Text = menuHtml;
            }
        }

        private DataTable GetMenuData()
        {
            string username = Session["username"].ToString(); // Ensure session is not null
            string query = "SELECT DISTINCT parent, path FROM Pages WHERE Proj='SHT' " +
                           "AND parent IN (SELECT parent FROM user_pages WHERE username=@username)";

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                cmd.Parameters.AddWithValue("@username", username);

                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                sqlConn.Close();
            }
            return dt;
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("login.aspx");
        }
    }
}
