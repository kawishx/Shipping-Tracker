using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Shipping_Tracker_V1
{
    public partial class ViewLists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvOrderDetails.RowDataBound += new GridViewRowEventHandler(gvOrderDetails_RowDataBound);
                PopulateCustomerGroupChecklist();
            }
        }

        protected void gvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text.Trim() == "&nbsp;")
                    {
                        cell.Text = "";
                    }
                }
            }
        }

        private void PopulateCustomerGroupChecklist()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;
            string query = "SELECT DISTINCT stgroup FROM order_details WHERE stgroup IS NOT NULL AND stgroup <> '' ORDER BY stgroup";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    cblCustomerGroup.Items.Clear();

                    while (reader.Read())
                    {
                        cblCustomerGroup.Items.Add(new ListItem(reader["stgroup"].ToString(), reader["stgroup"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error loading customer groups: " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string selectedStatus = ddlOrderStatus.SelectedValue;
            List<string> selectedGroups = new List<string>();

            foreach (ListItem item in cblCustomerGroup.Items)
            {
                if (item.Selected)
                {
                    selectedGroups.Add(item.Value);
                }
            }

            string groupFilter = selectedGroups.Count > 0 ? "(" + string.Join(",", selectedGroups.ConvertAll(x => "'" + x + "'")) + ")" : "''";

            if (string.IsNullOrEmpty(selectedStatus) && selectedGroups.Count == 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select at least one filter.";
                lblMessage.CssClass = "alert alert-danger";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;
            string query = $@"SELECT 
                    NULLIF(Order_no, '&nbsp;') AS Order_no, 
                    NULLIF(name, '&nbsp;') AS name, 
                    NULLIF(Del_term, '&nbsp;') AS Del_term, 
                    NULLIF(ship_via, '&nbsp;') AS ship_via, 
                    NULLIF(SHLINE, '&nbsp;') AS SHLINE, 
                    NULLIF(FORWARD, '&nbsp;') AS FORWARD, 
                    NULLIF(ETA, '&nbsp;') AS ETA, 
                    NULLIF(del_terms, '&nbsp;') AS del_terms, 
                    NULLIF(RODATE, '&nbsp;') AS RODATE, 
                    NULLIF(CUSD_DATE, '&nbsp;') AS CUSD_DATE, 
                    NULLIF(CUSDEC_NO, '&nbsp;') AS CUSDEC_NO, 
                    NULLIF(FMG, '&nbsp;') AS FMG, 
                    NULLIF(EXNOP, '&nbsp;') AS EXNOP
                FROM order_details 
                WHERE (@status = '' OR Form_status = @status) 
                AND (stgroup IN {groupFilter})";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@status", selectedStatus);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gvOrderDetails.DataSource = dt;
                    gvOrderDetails.DataBind();
                    lblMessage.Visible = false;
                }
                else
                {
                    gvOrderDetails.DataSource = null;
                    gvOrderDetails.DataBind();
                    lblMessage.Visible = true;
                    lblMessage.Text = "No records found.";
                    lblMessage.CssClass = "alert alert-warning";
                }
            }
        }
    }
}
