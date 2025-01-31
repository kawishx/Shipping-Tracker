using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Shipping_Tracker_V1
{
    public partial class ViewLists : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string selectedStatus = ddlOrderStatus.SelectedValue;

            if (string.IsNullOrEmpty(selectedStatus))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select an order status.";
                lblMessage.CssClass = "alert alert-danger";  
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;
            string query = "SELECT Order_no, RODATE, ship_via, name, ETA, currency_code FROM order_details WHERE Form_status = @status";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@status", selectedStatus);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // If records are found, bind the data to the GridView
                    gvOrderDetails.DataSource = dt;
                    gvOrderDetails.DataBind();

                    // Hide the message label
                    lblMessage.Visible = false;
                }
                else
                {
                    // Show message if no records are found
                    lblMessage.Visible = true;
                    lblMessage.Text = "No records found for the selected status.";
                    lblMessage.CssClass = "alert alert-warning";  // For warning message
                }
            }
        }
    }
    
}