<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderSummary.aspx.cs" Inherits="Shipping_Tracker_V1.OrderSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 90%;
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: white;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            font-weight: bold;
        }

        .btn {
            padding: 10px 15px;
            font-size: 16px;
            cursor: pointer;
            border: none;
            border-radius: 5px;
        }

        .btn-primary {
            background-color: #027ca8;
            color: white;
            transition: 0.3s;
        }

        .btn-primary:hover {
            background-color: #013f82;
        }

        .table-wrapper {
            overflow-x: auto;
        }

        .table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
            table-layout: auto;
        }

        .table th, .table td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
            white-space: nowrap;
        }

        .table th {
            background-color: #027ca8;
            color: white;
        }

        .table-striped tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .link-primary {
            color: #027ca8;
            text-decoration: none;
            font-weight: bold;
        }

        .link-primary:hover {
            text-decoration: underline;
        }
    </style>

    <div class="container">
        <h2>View Order Summary</h2>

        <div class="form-group">
            <label for="ddlOrderStatus">Select Order Status:</label>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control" AutoPostBack="false">
                <asp:ListItem Text="Select Status" Value="" />
                <asp:ListItem Text="Ongoing" Value="Ongoing" />
                <asp:ListItem Text="Completed" Value="Completed" />
            </asp:DropDownList>
        </div>
           
        <div class="form-group">
            <label for="cblCustomerGroup">Select Customer Groups:</label>
            <asp:CheckBoxList ID="cblCustomerGroup" runat="server" CssClass="form-control" RepeatColumns="3" />
        </div>


        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" OnClick="btnView_Click" />
        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-primary" 
                    OnClick="btnExport_Click" Visible="false"/>
        <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>

        <hr />

        <div class="table-wrapper">
            <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>

                    <asp:BoundField DataField="Order_no" HeaderText="Order No" SortExpression="Order_no"/>
                    <asp:BoundField DataField="RODATE" HeaderText="RO Received Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="RODATE" />
                    <asp:BoundField DataField="ship_via" HeaderText="Vessel Name" SortExpression="ship_via"/>
                    <asp:BoundField DataField="ETA" HeaderText="ETA" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ETA"/>
                    <asp:BoundField DataField="FORWARD" HeaderText="Forwarder" SortExpression="FORWARD" />
                    <asp:BoundField DataField="SHLINE" HeaderText="Shipping Line" SortExpression="SHLINE" />
                    <asp:BoundField DataField="dock_code" HeaderText="Dock Code" SortExpression="dock_code" />
                    <asp:BoundField DataField="name" HeaderText="Customer Name" SortExpression="name" />
                    <asp:BoundField DataField="pay_term" HeaderText="Payment Term" SortExpression="pay_term"/>
                    <asp:BoundField DataField="del_terms" HeaderText="Port" SortExpression="del_terms"/>
                    <asp:BoundField DataField="Del_term" HeaderText="Delivery Terms" SortExpression="Del_term"/>
                    <asp:BoundField DataField="currency_code" HeaderText="Currency Code" SortExpression="currency_code"/>
                    <asp:BoundField DataField="export_apprvl" HeaderText="Export Approval/ NOP" SortExpression="export_apprvl"/>
                    <asp:BoundField DataField="fumigation" HeaderText="Fumigation" SortExpression="fumigation"/>
                    <asp:BoundField DataField="picked_date" HeaderText="Picked Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="picked_date"/>
                    <asp:BoundField DataField="ETD" HeaderText="ETD" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ETD"/>
                    <asp:BoundField DataField="CONTNO" HeaderText="Container No" SortExpression="CONTNO"/>
                    <asp:BoundField DataField="CONSN" HeaderText="Seal Number" SortExpression="CONSN" />
                    <asp:BoundField DataField="customer_no" HeaderText="Customer No" SortExpression="customer_no"/>
                    <asp:BoundField DataField="Bl_draft" HeaderText="CBL Draft" SortExpression="Bl_draft"/>
                    <asp:BoundField DataField="Obl_received" HeaderText="OBL Received" DataFormatString="{0:yyyy-MM-dd}" SortExpression="Obl_received"/>
                    <asp:BoundField DataField="Coa_rec_date" HeaderText="COA Recieved Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="Coa_rec_date"/>
                    <asp:BoundField DataField="Marine_insurance" HeaderText="Marine Insurance" SortExpression="Marine_insurance"/>
                    <asp:BoundField DataField="Icc_gsp" HeaderText="ICC/ GS" SortExpression="Icc_gsp"/>
                    <asp:BoundField DataField="Phyto_cert" HeaderText="Phyto Certificate" SortExpression="Phyto_cert"/>
                    <asp:BoundField DataField="Health_cert" HeaderText="Health Certificate" SortExpression="Health_cert"/>
                    <asp:BoundField DataField="Coi_traces" HeaderText="COI Traces Draft" SortExpression="Coi_traces"/>
                    <asp:BoundField DataField="Coi_draft_ship" HeaderText="COI Draft to Shipping" SortExpression="Coi_draft_ship"/>
                    <asp:BoundField DataField="Coi_draft_cus" HeaderText="COI Draft to Customer" SortExpression="Coi_draft_cus"/>
                    <asp:BoundField DataField="Coi_sent_cus" HeaderText="Original COI Sent to Cus" SortExpression="Coi_sent_cus"/>
                    <asp:BoundField DataField="finaldoc_date" HeaderText="Final Docs/ Banked/ Courier Dat" DataFormatString="{0:yyyy-MM-dd}" SortExpression="finaldoc_date"/>
                    <asp:BoundField DataField="tot_ordervalue" HeaderText="Total Order Value" SortExpression="tot_ordervalue"/>
                    <asp:BoundField DataField="advance" HeaderText="Advance Invoice Amount" SortExpression="advance"/>
                    <asp:BoundField DataField="adv_paymnt_recpt" HeaderText="Advance Paymnet Receipt" SortExpression="adv_paymnt_recpt"/>
                    <asp:BoundField DataField="ETA_destntion" HeaderText="ETA Destination" SortExpression="ETA_destntion"/>
                    <asp:BoundField DataField="pay_due_date" HeaderText="Payment Due Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="pay_due_date"/>
                    <asp:BoundField DataField="paymnt_status" HeaderText="Payment Status" SortExpression="paymnt_status"/>
                    <asp:BoundField DataField="BL_telex_req" HeaderText="BL Telex Requested" SortExpression="BL_telex_req"/>
                    <asp:BoundField DataField="BL_telex_rec" HeaderText="BL Telex Received" SortExpression="BL_telex_rec"/>
                    <asp:BoundField DataField="Form_status" HeaderText="Form Status" SortExpression="Form_status"/>
                    <asp:BoundField DataField="account" HeaderText="Bank" SortExpression="account"/>
                    <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks"/>
                    <asp:BoundField DataField="CUSD_DATE" HeaderText="Cusdec Done Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CUSD_DATE"/>
                    <asp:BoundField DataField="FMG" HeaderText="FMG" SortExpression="FMG"/>
                    <asp:BoundField DataField="CUSDEC_NO" HeaderText="Cusdec Number" SortExpression="CUSDEC_NO"/>
                    <asp:BoundField DataField="EXNOP" HeaderText="EXNOP" SortExpression="EXNOP"/>
            
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

