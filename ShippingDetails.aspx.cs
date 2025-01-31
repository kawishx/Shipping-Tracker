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

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

        protected void gvOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveOrder") 
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument); 
                GridViewRow row = gvOrderDetails.Rows[rowIndex];

                string order_no = row.Cells[0].Text;
                string name = row.Cells[1].Text;
                string Del_term = row.Cells[2].Text;
                string Ship_via = row.Cells[3].Text;
                string del_terms = row.Cells[4].Text;
                string currency_code = row.Cells[5].Text;
                string ETD = row.Cells[6].Text;
                string RODATE = row.Cells[7].Text;
                string CONTNO = row.Cells[8].Text;
                string SHLINE = row.Cells[9].Text;
                string CONSN = row.Cells[10].Text;
                string customer_no = row.Cells[11].Text;
                string Advance = row.Cells[12].Text;
                string account = row.Cells[13].Text;
                string ETA = row.Cells[14].Text;
                string CUSD_DATE = row.Cells[15].Text;
                string FORWARD = row.Cells[16].Text;
                string dock_code = row.Cells[17].Text;
                string pay_term = row.Cells[18].Text;

                // Save data to the database
                SaveOrderDetails(order_no, name, Del_term, Ship_via, del_terms, currency_code,
                    ETD, RODATE, CONTNO, SHLINE, CONSN, customer_no, Advance, account, ETA,
                    CUSD_DATE, FORWARD, dock_code, pay_term);
               // lblMessage.Text = $"Order {order_no} has been saved          .";
            }
        }    

        private void SaveOrderDetails(string order_no, string name , string Del_term, string Ship_via, string del_terms, string currency_code,
                string ETD, string RODATE, string CONTNO, string SHLINE, string CONSN, string customer_no, string Advance, string account, string ETA,
                string CUSD_DATE, string FORWARD, string dock_code, string pay_term)
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

                
                using (SqlCommand cmd = new SqlCommand("INSERT INTO order_details (Order_no, name, Del_term, ship_via, del_terms, currency_code, ETD, RODATE, CONTNO, SHLINE, CONSN, customer_no, advance, account, ETA, CUSD_DATE, FORWARD, dock_code, pay_term) VALUES (@order_no, @name, @Del_term, @Ship_via, @del_terms, @currency_code, @ETD, @RODATE, @CONTNO, @SHLINE, @CONSN, @customer_no, @Advance, @account, @ETA, @CUSD_DATE, @FORWARD, @dock_code, @pay_term)", conn))
                {
                    cmd.Parameters.AddWithValue("@order_no", order_no);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@Del_term", Del_term);
                    cmd.Parameters.AddWithValue("@ship_via", Ship_via);
                    cmd.Parameters.AddWithValue("@del_terms", del_terms);
                    cmd.Parameters.AddWithValue("@currency_code", currency_code);
                    cmd.Parameters.AddWithValue("@ETD", ETD);
                    cmd.Parameters.AddWithValue("@RODATE", RODATE);
                    cmd.Parameters.AddWithValue("@CONTNO", CONTNO);
                    cmd.Parameters.AddWithValue("@SHLINE", SHLINE);
                    cmd.Parameters.AddWithValue("@CONSN", CONSN);
                    cmd.Parameters.AddWithValue("@customer_no", customer_no);
                    cmd.Parameters.AddWithValue("@advance", Advance);
                    cmd.Parameters.AddWithValue("@account", account);
                    cmd.Parameters.AddWithValue("@ETA", ETA);
                    cmd.Parameters.AddWithValue("@CUSD_DATE", CUSD_DATE);
                    cmd.Parameters.AddWithValue("@FORWARD", FORWARD);
                    cmd.Parameters.AddWithValue("@dock_code", dock_code);
                    cmd.Parameters.AddWithValue("@pay_term", pay_term);
                    

                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = $"Order {order_no} has been successfully saved.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }


    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              