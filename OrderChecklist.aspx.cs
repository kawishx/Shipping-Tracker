using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shipping_Tracker_V1
{
    public partial class OrderChecklist : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderNo = Request.QueryString["OrderNo"];
                if (!string.IsNullOrEmpty(orderNo))
                {
                    txtOrderNo.Text = orderNo;
                    FetchOrderDetails(orderNo);

                }
            }
            else
            {
                // Recreate fields dynamically during postback
                RecreateDynamicFields();
            }
        }

        private void RecreateDynamicFields()
        {
            string orderNo = txtOrderNo.Text.Trim();

            if (!string.IsNullOrEmpty(orderNo))
            {
                PopulateFieldsForCategory(phPreShipping, "Pre");
                PopulateFieldsForCategory(phLoading, "Loading");
                PopulateFieldsForCategory(phPostShipping, "Post");
                PopulateFieldsForCategory(phPaymentsInvoicing, "Payment");
            }
        }

        private void PopulateFieldsForCategory(PlaceHolder placeholder, string category)
        {
            foreach (var field in GetCategoryFields(category))
            {
                AddTextField(placeholder, field.Label, field.ControlId, null, field.IsReadOnly);
            }
        }



        protected void btnFetch_Click(object sender, EventArgs e)
        {
            string orderNo = txtOrderNo.Text.Trim();

            if (string.IsNullOrEmpty(orderNo))
            {
                lblMessage.Text = "Please enter an Order Number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            FetchOrderDetails(orderNo);
        }

        private void FetchOrderDetails(string orderNo)
        {
            ClearFields();

            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
                string query = @"
                    SELECT 
                        CASE 
                            WHEN RODATE LIKE '%&nbsp;%' THEN REPLACE(RODATE, '&nbsp;', ' ')
                            ELSE RODATE 
                        END AS RODATE,
                        CASE 
                            WHEN ship_via LIKE '%&nbsp;%' THEN REPLACE(ship_via, '&nbsp;', ' ')
                            ELSE ship_via 
                        END AS ship_via,
                        CASE 
                            WHEN ETA LIKE '%&nbsp;%' THEN REPLACE(ETA, '&nbsp;', ' ')
                            ELSE ETA 
                        END AS ETA,
                        CASE 
                            WHEN FORWARD LIKE '%&nbsp;%' THEN REPLACE(FORWARD, '&nbsp;', ' ')
                            ELSE FORWARD 
                        END AS FORWARD,
                        CASE 
                            WHEN SHLINE LIKE '%&nbsp;%' THEN REPLACE(SHLINE, '&nbsp;', ' ')
                            ELSE SHLINE 
                        END AS SHLINE,
                        CASE 
                            WHEN dock_code LIKE '%&nbsp;%' THEN REPLACE(dock_code, '&nbsp;', ' ')
                            ELSE dock_code 
                        END AS dock_code,
                        CASE 
                            WHEN name LIKE '%&nbsp;%' THEN REPLACE(name, '&nbsp;', ' ')
                            ELSE name 
                        END AS name,
                        CASE 
                            WHEN pay_term LIKE '%&nbsp;%' THEN REPLACE(pay_term, '&nbsp;', ' ')
                            ELSE pay_term 
                        END AS pay_term,
                        CASE 
                            WHEN del_terms LIKE '%&nbsp;%' THEN REPLACE(del_terms, '&nbsp;', ' ')
                            ELSE del_terms 
                        END AS del_terms,
                        CASE 
                            WHEN Del_term LIKE '%&nbsp;%' THEN REPLACE(Del_term, '&nbsp;', ' ')
                            ELSE Del_term 
                        END AS Del_term,
                        CASE 
                            WHEN currency_code LIKE '%&nbsp;%' THEN REPLACE(currency_code, '&nbsp;', ' ')
                            ELSE currency_code 
                        END AS currency_code,
                        CASE 
                            WHEN EXNOP LIKE '%&nbsp;%' THEN REPLACE(EXNOP, '&nbsp;', ' ')
                            ELSE EXNOP 
                        END AS EXNOP,
                        CASE 
                            WHEN fumigation LIKE '%&nbsp;%' THEN REPLACE(fumigation, '&nbsp;', ' ')
                            ELSE fumigation 
                        END AS fumigation,
                        CASE 
                            WHEN picked_date LIKE '%&nbsp;%' THEN REPLACE(picked_date, '&nbsp;', ' ')
                            ELSE picked_date 
                        END AS picked_date,
                        CASE 
                            WHEN ETD LIKE '%&nbsp;%' THEN REPLACE(ETD, '&nbsp;', ' ')
                            ELSE ETD 
                        END AS ETD,
                        CASE 
                            WHEN CONTNO LIKE '%&nbsp;%' THEN REPLACE(CONTNO, '&nbsp;', ' ')
                            ELSE CONTNO 
                        END AS CONTNO,
                        CASE 
                            WHEN CONSN LIKE '%&nbsp;%' THEN REPLACE(CONSN, '&nbsp;', ' ')
                            ELSE CONSN 
                        END AS CONSN,
                        CASE 
                            WHEN customer_no LIKE '%&nbsp;%' THEN REPLACE(customer_no, '&nbsp;', ' ')
                            ELSE customer_no 
                        END AS customer_no,
                        CASE 
                            WHEN Bl_draft LIKE '%&nbsp;%' THEN REPLACE(Bl_draft, '&nbsp;', ' ')
                            ELSE Bl_draft 
                        END AS Bl_draft,
                        CASE 
                            WHEN Obl_received LIKE '%&nbsp;%' THEN REPLACE(Obl_received, '&nbsp;', ' ')
                            ELSE Obl_received 
                        END AS Obl_received,
                        CASE 
                            WHEN Coa_rec_date LIKE '%&nbsp;%' THEN REPLACE(Coa_rec_date, '&nbsp;', ' ')
                            ELSE Coa_rec_date 
                        END AS Coa_rec_date,
                        CASE 
                            WHEN Marine_insurance LIKE '%&nbsp;%' THEN REPLACE(Marine_insurance, '&nbsp;', ' ')
                            ELSE Marine_insurance 
                        END AS Marine_insurance,
                        CASE 
                            WHEN Icc_gsp LIKE '%&nbsp;%' THEN REPLACE(Icc_gsp, '&nbsp;', ' ')
                            ELSE Icc_gsp 
                        END AS Icc_gsp,
                        CASE 
                            WHEN Phyto_cert LIKE '%&nbsp;%' THEN REPLACE(Phyto_cert, '&nbsp;', ' ')
                            ELSE Phyto_cert 
                        END AS Phyto_cert,
                        CASE 
                            WHEN Health_cert LIKE '%&nbsp;%' THEN REPLACE(Health_cert, '&nbsp;', ' ')
                            ELSE Health_cert 
                        END AS Health_cert,
                        CASE 
                            WHEN Coi_traces LIKE '%&nbsp;%' THEN REPLACE(Coi_traces, '&nbsp;', ' ')
                            ELSE Coi_traces 
                        END AS Coi_traces,
                        CASE 
                            WHEN Coi_draft_ship LIKE '%&nbsp;%' THEN REPLACE(Coi_draft_ship, '&nbsp;', ' ')
                            ELSE Coi_draft_ship 
                        END AS Coi_draft_ship,
                        CASE 
                            WHEN Coi_draft_cus LIKE '%&nbsp;%' THEN REPLACE(Coi_draft_cus, '&nbsp;', ' ')
                            ELSE Coi_draft_cus 
                        END AS Coi_draft_cus,
                        CASE 
                            WHEN Coi_sent_cus LIKE '%&nbsp;%' THEN REPLACE(Coi_sent_cus, '&nbsp;', ' ')
                            ELSE Coi_sent_cus 
                        END AS Coi_sent_cus,
                        CASE 
                            WHEN finaldoc_date LIKE '%&nbsp;%' THEN REPLACE(finaldoc_date, '&nbsp;', ' ')
                            ELSE finaldoc_date 
                        END AS finaldoc_date,
                        CASE 
                            WHEN tot_ordervalue LIKE '%&nbsp;%' THEN REPLACE(tot_ordervalue, '&nbsp;', ' ')
                            ELSE tot_ordervalue 
                        END AS tot_ordervalue,
                        CASE 
                            WHEN advance LIKE '%&nbsp;%' THEN REPLACE(advance, '&nbsp;', ' ')
                            ELSE advance 
                        END AS advance,
                        CASE 
                            WHEN adv_paymnt_recpt LIKE '%&nbsp;%' THEN REPLACE(adv_paymnt_recpt, '&nbsp;', ' ')
                            ELSE adv_paymnt_recpt 
                        END AS adv_paymnt_recpt,
                        CASE 
                            WHEN ETA_destntion LIKE '%&nbsp;%' THEN REPLACE(ETA_destntion, '&nbsp;', ' ')
                            ELSE ETA_destntion 
                        END AS ETA_destntion,
                        CASE 
                            WHEN pay_due_date LIKE '%&nbsp;%' THEN REPLACE(pay_due_date, '&nbsp;', ' ')
                            ELSE pay_due_date 
                        END AS pay_due_date,
                        CASE 
                            WHEN paymnt_status LIKE '%&nbsp;%' THEN REPLACE(paymnt_status, '&nbsp;', ' ')
                            ELSE paymnt_status 
                        END AS paymnt_status,
                        CASE 
                            WHEN BL_telex_req LIKE '%&nbsp;%' THEN REPLACE(BL_telex_req, '&nbsp;', ' ')
                            ELSE BL_telex_req 
                        END AS BL_telex_req,
                        CASE 
                            WHEN BL_telex_rec LIKE '%&nbsp;%' THEN REPLACE(BL_telex_rec, '&nbsp;', ' ')
                            ELSE BL_telex_rec 
                        END AS BL_telex_rec,
                        CASE 
                            WHEN Form_status LIKE '%&nbsp;%' THEN REPLACE(Form_status, '&nbsp;', ' ')
                            ELSE Form_status 
                        END AS Form_status,
                        CASE 
                            WHEN account LIKE '%&nbsp;%' THEN REPLACE(account, '&nbsp;', ' ')
                            ELSE account 
                        END AS account,
                        CASE 
                            WHEN remarks LIKE '%&nbsp;%' THEN REPLACE(remarks, '&nbsp;', ' ')
                            ELSE remarks 
                        END AS remarks,
                        CASE 
                            WHEN CUSD_DATE LIKE '%&nbsp;%' THEN REPLACE(CUSD_DATE, '&nbsp;', ' ')
                            ELSE CUSD_DATE 
                        END AS CUSD_DATE,
                        CASE 
                            WHEN CUSDEC_NO LIKE '%&nbsp;%' THEN REPLACE(CUSDEC_NO, '&nbsp;', ' ')
                            ELSE CUSDEC_NO 
                        END AS CUSDEC_NO,
                        CASE 
                            WHEN FMG LIKE '%&nbsp;%' THEN REPLACE(FMG, '&nbsp;', ' ')
                            ELSE FMG 
                        END AS FMG,
                        CASE 
                            WHEN No_of_cont LIKE '%&nbsp;%' THEN REPLACE(FMG, '&nbsp;', ' ')
                            ELSE No_of_cont 
                        END AS No_of_cont,
                        CASE 
                            WHEN consize LIKE '%&nbsp;%' THEN REPLACE(consize, '&nbsp;', ' ')
                            ELSE consize 
                        END AS consize,
                        CASE 
                            WHEN Pack_List_Date LIKE '%&nbsp;%' THEN REPLACE(Pack_List_Date, '&nbsp;', ' ')
                            ELSE Pack_List_Date 
                        END AS Pack_List_Date,
                        CASE 
                            WHEN Inv_List_Date LIKE '%&nbsp;%' THEN REPLACE(Inv_List_Date, '&nbsp;', ' ')
                            ELSE Inv_List_Date 
                        END AS Inv_List_Date
                    FROM order_details WHERE order_no = @OrderNo";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderNo", orderNo);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        // Populate the fields with the updated values
                        PopulateFields(reader, phPreShipping, "Pre");
                        PopulateFields(reader, phLoading, "Loading");
                        PopulateFields(reader, phPostShipping, "Post");
                        PopulateFields(reader, phPaymentsInvoicing, "Payment");

                        lblMessage.Text = "Order details fetched successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "No order found for the entered Order Number.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error fetching order details: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void PopulateFields(SqlDataReader reader, PlaceHolder placeholder, string category)
        {
            foreach (var field in GetCategoryFields(category))
            {
                bool isReadOnly = true; // Mark all fetched fields as read-only
                AddTextField(placeholder, field.Label, field.ControlId, reader[field.Column]?.ToString(), isReadOnly);
            }
        }

        private void AddTextField(PlaceHolder placeholder, string labelText, string controlId, string value, bool isReadOnly = false)
        {
            // Create the outer div for a column container
            var colDiv = new Literal { Text = "<div class='col'>" };

            // Add the label inside the column
            placeholder.Controls.Add(colDiv);
            placeholder.Controls.Add(new Literal { Text = $"<label class='form-label'>{labelText}:</label>" });

            TextBox textBox = new TextBox
            {
                ID = controlId,
                Text = value ?? "",
                CssClass = "form-control"
            };

            if (labelText.Equals("RO Received Date", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }

            if (labelText.Equals("Vessel Name", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }

            if (labelText.Equals("Cusdec Done Date", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Cusdec Number", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Customer Name", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("ETA Colombo", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Forwarder", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Shipping Line", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Suborder No Colombo", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Payment Term", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Port", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Delivery Term", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Loading Date", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("ETD/ Ship on Board", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Container Number", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Seal Number", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Dock Code", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Container Size", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Advance Invoice Amount", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Bank", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Currency", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }
            if (labelText.Equals("Export Approval/ NOP", StringComparison.OrdinalIgnoreCase))
            {
                textBox.ReadOnly = true;
                textBox.CssClass += " readonly-field";
            }

            placeholder.Controls.Add(textBox);

            placeholder.Controls.Add(new Literal { Text = "</div>" });
        }



        protected void btnSavePreShipping_Click(object sender, EventArgs e)
        {
            SaveCategoryData(phPreShipping, "Pre");
        }

        protected void btnSaveLoading_Click(object sender, EventArgs e)
        {
            SaveCategoryData(phLoading, "Loading");
        }

        protected void btnSavePostShipping_Click(object sender, EventArgs e)
        {
            SaveCategoryData(phPostShipping, "Post");
        }

        protected void btnPaymentsInvoicing_Click(object sender, EventArgs e)
        {
            SaveCategoryData(phPaymentsInvoicing, "Payment");
        }

        private void SaveCategoryData(PlaceHolder placeholder, string category)
        {
            string orderNo = txtOrderNo.Text.Trim();

            if (string.IsNullOrEmpty(orderNo))
            {
                lblMessage.Text = "Please enter an Order Number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Fetch existing record to prevent overwriting fields with NULL
                string selectQuery = "SELECT * FROM order_details WHERE order_no = @OrderNo";
                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@OrderNo", orderNo);

                conn.Open();
                SqlDataReader reader = selectCmd.ExecuteReader();

                Dictionary<string, object> existingData = new Dictionary<string, object>();

                if (reader.Read())  
                {
                    foreach (var field in GetCategoryFields(category))
                    {
                        existingData[field.Column] = reader[field.Column] != DBNull.Value ? reader[field.Column] : null;
                    }
                }
                reader.Close();

                // Prepare update statement
                var fields = GetCategoryFields(category);
                string setClause = string.Join(", ", fields.Select(f => $"{f.Column} = @{f.Column}"));

                string updateQuery = $"UPDATE order_details SET {setClause} WHERE order_no = @OrderNo";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);

                foreach (var field in fields)
                {
                    string fieldValue = GetTextBoxValue(placeholder, field.ControlId);

                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        // Use the new value if provided
                        updateCmd.Parameters.AddWithValue($"@{field.Column}", fieldValue);
                    }
                    else
                    {
                        // Keep existing value if new value is empty
                        updateCmd.Parameters.AddWithValue($"@{field.Column}", existingData[field.Column] ?? DBNull.Value);
                    }
                }
                updateCmd.Parameters.AddWithValue("@OrderNo", orderNo);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                lblMessage.Text = rowsAffected > 0
                    ? $"{category} Info saved successfully."
                    : "No rows updated. Please check the Order Number.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }


        private string GetTextBoxValue(PlaceHolder placeholder, string controlId)
        {
            TextBox textBox = placeholder.FindControl(controlId) as TextBox;
            return textBox?.Text.Trim() ?? string.Empty;
        }

        private void ClearFields()
        {
            phPreShipping.Controls.Clear();
            phLoading.Controls.Clear();
            phPostShipping.Controls.Clear();
            phPaymentsInvoicing.Controls.Clear();
            lblMessage.Text = string.Empty;
        }

        private IEnumerable<FieldInfo> GetCategoryFields(string category)
        {
            switch (category)
            {
                case "Pre":
                    return new[]
                    {
                        new FieldInfo("RO Received Date", "txtROReceivedDate", "RODATE", true),
                        new FieldInfo("Cusdec Done Date", "txtCusdecDoneDate", "CUSD_DATE", true),
                        new FieldInfo("Cusdec Number", "txtCusdecNumber", "CUSDEC_NO", true),
                        new FieldInfo("Vessel Name", "txtVesselName","ship_via", true),
                        new FieldInfo("Customer Name", "txtCustomerName","name", true),
                        new FieldInfo("ETA Colombo", "txtETAColombo", "ETA", true),
                        new FieldInfo("Forwarder", "txtForwarder", "FORWARD", true),
                        new FieldInfo("Shipping Line", "txtShippingLine", "SHLINE", true),
                        new FieldInfo("Suborder No Colombo", "txtSuborderNoColombo", "dock_code", true),
                        new FieldInfo("Payment Term", "txtPaymentTerm", "pay_term", true),
                        new FieldInfo("Currency", "txtCurrency", "currency_code"),
                        new FieldInfo("Port", "txtPort", "del_terms", true),
                        new FieldInfo("No of Containers", "txtNoofContainers", "No_of_cont"),
                        new FieldInfo("Container Size", "txtContainerSize", "consize",  true),
                        new FieldInfo("Delivery Term", "txtDeliveryTerm", "Del_term", true),
                        new FieldInfo("Export Approval/ NOP", "txtExportApproval/NOP", "EXNOP"),
                        new FieldInfo("Fumigation", "txtFumigation", "fumigation"),
                        new FieldInfo("Dock Code", "txtDockCode", "dock_code", true),
                        new FieldInfo("FMG", "txtFMG", "FMG"),
            };
                case "Loading":
                    return new[]
                    {
                        new FieldInfo("Loading Date", "txtPickedDate", "picked_date", true),
                        new FieldInfo("ETD/ Ship on Board", "txtETD/ShiponBoard", "ETD", true),
                        new FieldInfo("Container Number", "txtContainerNumber", "CONTNO", true),
                        new FieldInfo("Seal Number", "txtSealNumber", "CONSN", true),
                        new FieldInfo("BL Draft", "txtBLDraft", "Bl_draft"),

            };
                case "Post":
                    return new[]
                    {
                        new FieldInfo("OBL Received", "txtOBLReceived", "Obl_received"),
                        new FieldInfo("COA Recieved Date", "txtCOAReceivedDate", "Coa_rec_date"),
                        new FieldInfo("Marine Insurance", "txtMarineInsurance", "Marine_insurance"),
                        new FieldInfo("ICC/ GSP", "txtICC/GSP", "Icc_gsp"),
                        new FieldInfo("Phyto Certificate", "txtPhytoCertificate", "Phyto_cert"),
                        new FieldInfo("Health Certificate", "txtHealthCertificate", "Health_cert"),
                        new FieldInfo("COI Traces Draft", "txtCOITracesDraft", "Coi_traces"),
                        new FieldInfo("ETA Destination", "txtETADestination", "ETA_destntion"),
                        new FieldInfo("COI Draft to Shipping", "txtCOIDrafttoShipping", "Coi_draft_ship"),
                        new FieldInfo("COI Draft to Customer", "txtCOIDrafttoCustomer", "Coi_draft_cus"),
                        new FieldInfo ("Original COI Sent to Cus.", "txtOriginalCOISenttoCus.", "Coi_sent_cus"),
                        new FieldInfo("Final Docs/ Banked/ Courier Date", "txtFinalDocs/Banked/CourierDate", "finaldoc_date"),
                        new FieldInfo("Packing List Send Date", "txtPackingListSendDate", "Pack_List_Date"),
                        new FieldInfo("Invoice Send Date", "txtInvoiceSendDate", "Inv_List_Date"),
            };
                case "Payment":
                    return new[]
                    {
                        new FieldInfo("Total Order Value", "txtTotalOrderValue", "tot_ordervalue"),
                        new FieldInfo("Advance Invoice Amount", "txtAdvanceInvoiceAmount", "advance", true),
                        new FieldInfo("Advance Paymnet Receipt", "txtAdvancePaymnetReceipt", "adv_paymnt_recpt"),
                        new FieldInfo("Payment Due Date", "txtPaymentDueDate", "pay_due_date"),
                        new FieldInfo("Payment Status", "txtPaymentStatus", "paymnt_status"),
                        new FieldInfo("BL Telex Requested", "txtBLTelexRequested", "BL_telex_req"),
                        new FieldInfo("BL Telex Received", "txtBLTelexReceived", "BL_telex_rec"),
                        new FieldInfo("Form Status", "txtFormStatus", "Form_status"),
                        new FieldInfo("Bank", "txtBank", "account", true),
                        new FieldInfo("Remarks", "txtRemarks", "remarks"),
                    };
                default:
                    return Enumerable.Empty<FieldInfo>();
            }
        }

        private class FieldInfo
        {
            public string Label { get; }
            public string ControlId { get; }
            public string Column { get; }
            public bool IsReadOnly { get; }


            public FieldInfo(string label, string controlId, string column, bool isReadOnly = false)
            {
                Label = label;
                ControlId = controlId;
                Column = column;
                IsReadOnly = isReadOnly;
            }
        }
    }
}
