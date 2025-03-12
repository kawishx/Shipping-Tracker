using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Shipping_Tracker_V1
{
    public partial class NewEntry : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["stdconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            string orderNumber = txtOrderNumber.Text.Trim();
            if (string.IsNullOrEmpty(orderNumber))
            {
                lblMessage.Text = "Order number is required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        string checkOrderQuery = "SELECT COUNT(*) FROM order_details WHERE Order_no = @Order_no";
                        using (SqlCommand checkCmd = new SqlCommand(checkOrderQuery, con, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@Order_no", orderNumber);
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                lblMessage.Text = "Order number already exists!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }

                        string insertQuery = @"INSERT INTO order_details 
                        (Order_no, RODATE, CUSD_DATE, CUSDEC_NO, ship_via, name, ETA, FORWARD, SHLINE, dock_code, pay_term, currency_code, 
                        del_terms, No_of_cont, consize, Del_term, export_apprvl, fumigation, FMG, 
                        picked_date, ETD, CONTNO, CONSN, Bl_draft, 
                        Obl_received, Coa_rec_date, Marine_insurance, Icc_gsp, Phyto_cert, Health_cert, Coi_traces, 
                        ETA_destntion, Coi_draft_ship, Coi_draft_cus, Coi_sent_cus, finaldoc_date, Pack_List_Date, Inv_List_Date, 
                        tot_ordervalue, advance, adv_paymnt_recpt, pay_due_date, paymnt_status, BL_telex_req, 
                        BL_telex_rec, Form_status, account, remarks) 
                        VALUES (@Order_no, @RODATE, @CUSD_DATE, @CUSDEC_NO, @ship_via, @name, @ETA, @FORWARD, @SHLINE, @dock_code, @pay_term, @currency_code, 
                        @del_terms, @No_of_cont, @consize, @Del_term, @export_apprvl, @fumigation, @FMG, 
                        @picked_date, @ETD, @CONTNO, @CONSN, @Bl_draft, 
                        @Obl_received, @Coa_rec_date, @Marine_insurance, @Icc_gsp, @Phyto_cert, @Health_cert, @Coi_traces, 
                        @ETA_destntion, @Coi_draft_ship, @Coi_draft_cus, @Coi_sent_cus, @finaldoc_date, @Pack_List_Date, @Inv_List_Date,
                        @tot_ordervalue, @advance, @adv_paymnt_recpt, @pay_due_date, @paymnt_status, @BL_telex_req, 
                        @BL_telex_rec, @Form_status, @account, @remarks)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Order_no", orderNumber);
                            cmd.Parameters.AddWithValue("@RODATE", txtROReceivedDate.Text);
                            cmd.Parameters.AddWithValue("@CUSD_DATE", txtCusdecDoneDate.Text);
                            cmd.Parameters.AddWithValue("@CUSDEC_NO", txtCusdecNumber.Text);
                            cmd.Parameters.AddWithValue("@ship_via", txtVessalName.Text);
                            cmd.Parameters.AddWithValue("@name", txtCustomerName.Text);
                            cmd.Parameters.AddWithValue("@ETA", txtETAColombo.Text);
                            cmd.Parameters.AddWithValue("@FORWARD", txtForwarder.Text);
                            cmd.Parameters.AddWithValue("@SHLINE", txtShippingLine.Text);
                            cmd.Parameters.AddWithValue("@dock_code", txtSubOrderNoColombo.Text);
                            cmd.Parameters.AddWithValue("@pay_term", txtPaymentTerm.Text);
                            cmd.Parameters.AddWithValue("@currency_code", txtCurrency.Text);
                            cmd.Parameters.AddWithValue("@del_terms", txtPort.Text);
                            cmd.Parameters.AddWithValue("@No_of_cont", txtNoofContainers.Text);
                            cmd.Parameters.AddWithValue("@consize", txtContainerSize.Text);
                            cmd.Parameters.AddWithValue("@Del_term", txtDeliveryTerm.Text);
                            cmd.Parameters.AddWithValue("@export_apprvl", txtExportApprovalNOP.Text);
                            cmd.Parameters.AddWithValue("@fumigation", txtFumigation.Text);
                            cmd.Parameters.AddWithValue("@FMG", ddlFMG.SelectedValue);
                            cmd.Parameters.AddWithValue("@picked_date", txtLoadingDate.Text);
                            cmd.Parameters.AddWithValue("@ETD", txtETDShiponBoard.Text);
                            cmd.Parameters.AddWithValue("@CONTNO", txtContainerNumber.Text);
                            cmd.Parameters.AddWithValue("@CONSN", txtSealNumber.Text);
                            cmd.Parameters.AddWithValue("@Bl_draft", txtBLDraft.Text);
                            cmd.Parameters.AddWithValue("@Obl_received", txtOblReceived.Text);
                            cmd.Parameters.AddWithValue("@Coa_rec_date", txtCoaReceivedDate.Text);
                            cmd.Parameters.AddWithValue("@Marine_insurance", txtMarineInsurance.Text);
                            cmd.Parameters.AddWithValue("@Icc_gsp", txtICCGSP.Text);
                            cmd.Parameters.AddWithValue("@Phyto_cert", txtPhytoCertificate.Text);
                            cmd.Parameters.AddWithValue("@Health_cert", txtHealthCertificate.Text);
                            cmd.Parameters.AddWithValue("@Coi_traces", txtCOITracesDraft.Text);
                            cmd.Parameters.AddWithValue("@ETA_destntion", txtETADestination.Text);
                            cmd.Parameters.AddWithValue("@Coi_draft_ship", txtCOIDrafttoShipping.Text);
                            cmd.Parameters.AddWithValue("@Coi_draft_cus", txtCOIDrafttoCustomer.Text);
                            cmd.Parameters.AddWithValue("@Coi_sent_cus", txtOriginalCOISenttoCus.Text);
                            cmd.Parameters.AddWithValue("@finaldoc_date", txtFinalDocsBankedCourierDate.Text);
                            cmd.Parameters.AddWithValue("@Pack_List_Date", txtPackingListSendDate.Text);
                            cmd.Parameters.AddWithValue("@Inv_List_Date", txtInvoiceSendDate.Text);
                            cmd.Parameters.AddWithValue("@tot_ordervalue", txtTotalOrderValue.Text);
                            cmd.Parameters.AddWithValue("@advance", txtAdvanceInvoiceAmount.Text);
                            cmd.Parameters.AddWithValue("@adv_paymnt_recpt", txtAdvancePaymentReceipt.Text);
                            cmd.Parameters.AddWithValue("@pay_due_date", txtPayDueDate.Text);
                            cmd.Parameters.AddWithValue("@paymnt_status", txtPaymentStatus.Text);
                            cmd.Parameters.AddWithValue("@BL_telex_req", txtBLTelexRequested.Text);
                            cmd.Parameters.AddWithValue("@BL_telex_rec", txtBLTelexReceived.Text);
                            cmd.Parameters.AddWithValue("@Form_status", ddlFormStatus.SelectedValue);
                            cmd.Parameters.AddWithValue("@account", txtBank.Text);
                            cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        lblMessage.Text = "All information saved successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}
