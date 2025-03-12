using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Shipping_Tracker_V1
{
    public partial class OrderSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvOrderDetails.RowDataBound += new GridViewRowEventHandler(gvOrderDetails_RowDataBound);
                PopulateCustomerGroupDropdown();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=OrderSummary.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = Encoding.UTF8;

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvOrderDetails.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        // Required to enable GridView export
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an ASP.NET server control is rendered correctly
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

        private void PopulateCustomerGroupDropdown()
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
                lblMessage.Text = "Please select an order status.";
                lblMessage.CssClass = "alert alert-danger";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;
            string query = $@"SELECT 
                    NULLIF(Order_no, '&nbsp;') AS Order_no,
                    NULLIF(RODATE, '&nbsp;') AS RODATE, 
                    NULLIF(ship_via, '&nbsp;') AS ship_via, 
                    NULLIF(ETA, '&nbsp;') AS ETA, 
                    NULLIF(FORWARD, '&nbsp;') AS FORWARD, 
                    NULLIF(SHLINE, '&nbsp;') AS SHLINE, 
                    NULLIF(dock_code, '&nbsp;') AS dock_code, 
                    NULLIF(name, '&nbsp;') AS name, 
                    NULLIF(pay_term, '&nbsp;') AS pay_term, 
                    NULLIF(del_terms, '&nbsp;') AS del_terms, 
                    NULLIF(Del_term, '&nbsp;') AS Del_term,
                    NULLIF(currency_code, '&nbsp;') AS currency_code, 
                    NULLIF(export_apprvl, '&nbsp;') AS export_apprvl, 
                    NULLIF(fumigation, '&nbsp;') AS fumigation, 
                    NULLIF(picked_date, '&nbsp;') AS picked_date, 
                    NULLIF(ETD, '&nbsp;') AS ETD, 
                    NULLIF(CONTNO, '&nbsp;') AS CONTNO, 
                    NULLIF(CONSN, '&nbsp;') AS CONSN, 
                    NULLIF(customer_no, '&nbsp;') AS customer_no, 
                    NULLIF(Bl_draft, '&nbsp;') AS Bl_draft, 
                    NULLIF(Obl_received, '&nbsp;') AS Obl_received, 
                    NULLIF(Coa_rec_date, '&nbsp;') AS Coa_rec_date, 
                    NULLIF(Marine_insurance, '&nbsp;') AS Marine_insurance, 
                    NULLIF(Icc_gsp, '&nbsp;') AS Icc_gsp, 
                    NULLIF(Phyto_cert, '&nbsp;') AS Phyto_cert, 
                    NULLIF(Health_cert, '&nbsp;') AS Health_cert, 
                    NULLIF(Coi_traces, '&nbsp;') AS Coi_traces, 
                    NULLIF(Coi_draft_ship, '&nbsp;') AS Coi_draft_ship, 
                    NULLIF(Coi_draft_cus, '&nbsp;') AS Coi_draft_cus, 
                    NULLIF(Coi_sent_cus, '&nbsp;') AS Coi_sent_cus, 
                    NULLIF(finaldoc_date, '&nbsp;') AS finaldoc_date, 
                    NULLIF(tot_ordervalue, '&nbsp;') AS tot_ordervalue, 
                    NULLIF(advance, '&nbsp;') AS advance, 
                    NULLIF(adv_paymnt_recpt, '&nbsp;') AS adv_paymnt_recpt, 
                    NULLIF(ETA_destntion, '&nbsp;') AS ETA_destntion, 
                    NULLIF(pay_due_date, '&nbsp;') AS pay_due_date, 
                    NULLIF(paymnt_status, '&nbsp;') AS paymnt_status, 
                    NULLIF(BL_telex_req, '&nbsp;') AS BL_telex_req, 
                    NULLIF(BL_telex_rec, '&nbsp;') AS BL_telex_rec, 
                    NULLIF(Form_status, '&nbsp;') AS Form_status, 
                    NULLIF(account, '&nbsp;') AS account, 
                    NULLIF(remarks, '&nbsp;') AS remarks, 
                    NULLIF(CUSD_DATE, '&nbsp;') AS CUSD_DATE, 
                    NULLIF(FMG, '&nbsp;') AS FMG,
                    NULLIF(CUSDEC_NO, '&nbsp;') AS CUSDEC_NO, 
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
                    btnExport.Visible = true;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No records found for the selected status.";
                    lblMessage.CssClass = "alert alert-warning";
                    btnExport.Visible = false;
                }
            }
        }
    }

}
