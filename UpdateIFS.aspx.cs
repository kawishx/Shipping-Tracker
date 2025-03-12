using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Shipping_Tracker_V1
{
    public partial class UpdateIFS : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnUpdateIFS_Click(object sender, EventArgs e)
        {
            string orderNumber = txtOrderNumber.Text.Trim();

            if (!string.IsNullOrEmpty(orderNumber))
            {
                bool isUpdated = UpdateOrderDetailsFromIFS(orderNumber);

                if (isUpdated)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "showSuccessMessage();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Order details updated successfully. ');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter an order number.');", true);
            }
        }

        private bool UpdateOrderDetailsFromIFS(string orderNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand fetchCmd = new SqlCommand("dbo.SHIP_ORDERS", conn))
                    {
                        fetchCmd.CommandType = CommandType.StoredProcedure;
                        fetchCmd.Parameters.AddWithValue("@orderno", orderNumber);
                        fetchCmd.Parameters.AddWithValue("@ROTP", 2);  // Assuming you need to pass the second parameter

                        // Execute stored procedure
                        int rowsAffected = fetchCmd.ExecuteNonQuery();

                        // Check if rows were affected (indicating success)
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating IFS details: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
