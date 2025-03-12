using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Routing;
using System.Xml.Linq;

namespace Shipping_Tracker_V1
{
    public partial class ShippingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvOrderDetails.DataSource = null;
                gvOrderDetails.DataBind();
                               
            }
        }

        protected void btnFetchDetails_Click(object sender, EventArgs e)
        {
            string orderNumbers = txtOrderNumbers.Text.Trim();
            if (string.IsNullOrWhiteSpace(orderNumbers))
            {
                lblMessage.Text = "Please enter at least one order number.";
                return;
            }

            DataTable dt = FetchOrderDetails(orderNumbers);
            if (dt.Rows.Count > 0)
            {
                ViewState["FullOrderData"] = dt;
                gvOrderDetails.DataSource = dt;
                gvOrderDetails.DataBind();
                lblMessage.Text = string.Empty;
            }
            else
            {
                gvOrderDetails.DataSource = null;
                gvOrderDetails.DataBind();
                lblMessage.Text = "No records found.";
            }
        }

        private DataTable FetchOrderDetails(string orderNumbers)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.SHIP_ORDERS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderno", orderNumbers);
                    cmd.Parameters.AddWithValue("@ROTP", 0);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        protected void gvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the index of the 'Export Aprvl/NOP' column
                int exnopIndex = -1;
                foreach (DataControlField field in gvOrderDetails.Columns)
                {
                    if (field.HeaderText == "Export Aprvl/NOP")
                    {
                        exnopIndex = gvOrderDetails.Columns.IndexOf(field);
                        break;
                    }
                }

                if (exnopIndex != -1)
                {
                    TableCell exnopCell = e.Row.Cells[exnopIndex];
                    if (string.IsNullOrWhiteSpace(exnopCell.Text) || exnopCell.Text == "&nbsp;")
                    {
                        exnopCell.BackColor = System.Drawing.Color.Red;
                        exnopCell.ForeColor = System.Drawing.Color.White; 
                    }
                }
            }
        }


        protected void gvOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveOrder")
            {
                if (ViewState["FullOrderData"] != null)
                {
                    gvOrderDetails.DataSource = ViewState["FullOrderData"];
                    gvOrderDetails.DataBind();
                }
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOrderDetails.Rows[rowIndex];

                string order_no = row.Cells[0].Text.Trim();
                string name = row.Cells[1].Text.Trim();
                string Del_term = row.Cells[2].Text.Trim();
                string Ship_via = row.Cells[3].Text.Trim();
                string SHLINE = row.Cells[4].Text.Trim();
                string FORWARD = row.Cells[5].Text.Trim();
                string ETA = row.Cells[6].Text.Trim();
                string del_terms = row.Cells[7].Text.Trim();
                string RODATE = row.Cells[8].Text.Trim();
                string CUSD_DATE = row.Cells[9].Text.Trim();
                string CUSDEC_NO = row.Cells[10].Text.Trim();
                string FMG = row.Cells[11].Text.Trim();
                string EXNOP = row.Cells[12].Text.Trim();

                string CONTNO = ((HiddenField)row.FindControl("hfCONTNO")).Value;
                string ETD = ((HiddenField)row.FindControl("hfETD"))?.Value ?? "";
                string currency_code = ((HiddenField)row.FindControl("hfcurrencycode")).Value;
                string CONSN = ((HiddenField)row.FindControl("hfCONSN"))?.Value ?? "";
                string customer_no = ((HiddenField)row.FindControl("hfcustomerno"))?.Value ?? "";
                string Advance = ((HiddenField)row.FindControl("hfAdvance"))?.Value ?? "";
                string account = ((HiddenField)row.FindControl("hfAccount"))?.Value ?? "";
                string dock_code = ((HiddenField)row.FindControl("hfDockCode"))?.Value ?? "";
                string pay_term = ((HiddenField)row.FindControl("hfPayTerm"))?.Value ?? "";
                string stgroup = ((HiddenField)row.FindControl("hfstgroup"))?.Value ?? "";
                string consize = ((HiddenField)row.FindControl("hfconsize"))?.Value ?? "";

                lblMessage.Text = $"Hidden Fields: {CONTNO}, {ETD}, {currency_code}, {CONSN}, {customer_no}";



                SaveOrderDetails(order_no, name, Del_term, Ship_via, ETD, ETA, del_terms, currency_code, RODATE, CONTNO, SHLINE, CONSN, customer_no, Advance, account, CUSD_DATE, CUSDEC_NO, FORWARD, dock_code, pay_term, FMG, EXNOP, stgroup, consize);
            }
        }



        private void SaveOrderDetails(string order_no, string name, string Del_term, string Ship_via, string ETD, string ETA,
            string del_terms, string currency_code, string RODATE, string CONTNO, string SHLINE, string CONSN, string customer_no,
            string Advance, string account, string CUSD_DATE, string CUSDEC_NO, string FORWARD, string dock_code, string pay_term, string FMG, string EXNOP, string stgroup, string consize)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM order_details WHERE Order_no = @order_no", conn))
                {
                    checkCmd.Parameters.AddWithValue("@order_no", order_no);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        lblMessage.Text = $"Order {order_no} already exists in the database.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                using (SqlCommand cmd = new SqlCommand("INSERT INTO order_details (Order_no, name, Del_term, ship_via, del_terms, currency_code, ETD, RODATE, CONTNO, SHLINE, CONSN, customer_no, advance, account, ETA, CUSD_DATE, CUSDEC_NO, FORWARD, dock_code, pay_term, FMG, EXNOP, stgroup, consize) VALUES (@order_no, @name, @Del_term, @Ship_via, @del_terms, @currency_code, @ETD, @RODATE, @CONTNO, @SHLINE, @CONSN, @customer_no, @Advance, @account, @ETA, @CUSD_DATE, @CUSDEC_NO, @FORWARD, @dock_code, @pay_term, @FMG, @EXNOP, @stgroup, @consize)", conn))
                {


                    cmd.Parameters.AddWithValue("@order_no", order_no);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@Del_term", Del_term);
                    cmd.Parameters.AddWithValue("@Ship_via", Ship_via);
                    cmd.Parameters.AddWithValue("@ETD", ETD);
                    cmd.Parameters.AddWithValue("@ETA", ETA);
                    cmd.Parameters.AddWithValue("@del_terms", del_terms);
                    cmd.Parameters.AddWithValue("@currency_code", currency_code);
                    cmd.Parameters.AddWithValue("@RODATE", RODATE);
                    cmd.Parameters.AddWithValue("@CONTNO", CONTNO);
                    cmd.Parameters.AddWithValue("@SHLINE", SHLINE);
                    cmd.Parameters.AddWithValue("@CONSN", CONSN);
                    cmd.Parameters.AddWithValue("@customer_no", customer_no);
                    cmd.Parameters.AddWithValue("@Advance", Advance);
                    cmd.Parameters.AddWithValue("@account", account);
                    cmd.Parameters.AddWithValue("@CUSD_DATE", CUSD_DATE);
                    cmd.Parameters.AddWithValue("@CUSDEC_NO", CUSDEC_NO);
                    cmd.Parameters.AddWithValue("@FORWARD", FORWARD);
                    cmd.Parameters.AddWithValue("@dock_code", dock_code);
                    cmd.Parameters.AddWithValue("@pay_term", pay_term);
                    cmd.Parameters.AddWithValue("@FMG", FMG);
                    cmd.Parameters.AddWithValue("@EXNOP", EXNOP);
                    cmd.Parameters.AddWithValue("@stgroup", stgroup);
                    cmd.Parameters.AddWithValue("@consize", consize);


                    cmd.ExecuteNonQuery();
                }
            }
            lblMessage.Text = $"Order {order_no} has been successfully saved.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }
    }
}
