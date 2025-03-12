<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEntry.aspx.cs" Inherits="Shipping_Tracker_V1.NewEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 900px;
            margin: 20px auto;
            background: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        label {
            font-weight: bold;
            color: #555;
            display: block;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            height: 30px;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-control:focus {
            border-color: #007bff;
            outline: none;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
        }

        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            color: #fff;
            cursor: pointer;
            transition: background 0.3s ease;
        }

        .btn-primary {
            background-color: #027ca8;
        }

        .btn-primary:hover {
            background-color: #013f82;
        }

        .btn-success {
            background-color: #28a745;
        }

        .btn-success:hover {
            background-color: #1e7e34;
        }

        .tab-container {
            margin-top: 20px;
        }

        .tab-header {
            display: flex;
            justify-content: space-between;
            background-color: #005780;
            color: white;
            padding: 10px;
            border-radius: 5px 5px 0 0;
        }

        .tab-header div {
            flex: 1;
            text-align: center;
            padding: 10px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .tab-header div.active {
            background-color: #01346b;
        }

        .tab-header div:hover {
            background-color: #01346b;
        }

        .tab-content {
            display: none;
            background: #fff;
            border: 1px solid #ccc;
            border-top: none;
            padding: 20px;
            border-radius: 0 0 5px 5px;
        }

        .tab-content.active {
            display: block;
        }

        .row {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .col {
            flex: 1 1 calc(50% - 20px);
            min-width: 300px;
        }
        .readonly-field {
            background-color: #f0f0f0;  
            border: 1px solid #ddd;     
            cursor: not-allowed;        
        }

        .text-end {
            text-align: end;
        }
    </style>

    <asp:Button ID="btnSaveAll" Text="Save All" CssClass="btn btn-success" OnClick="btnSaveAll_Click" runat="server" style="margin-top: 20px; display: block; margin-left: auto; margin-right: auto;" />

    <div class="container">
        <h2>New Order Entry</h2>

        <!-- Message Label -->
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>
        </div>

        <div class="tab-container">
            <!-- Tab Headers -->
            <div class="tab-header">
                <div class="tab-link active" onclick="showTab(0)">Pre-Shipping Info</div>
                <div class="tab-link" onclick="showTab(1)">Loading Info</div>
                <div class="tab-link" onclick="showTab(2)">Post-Shipping Info</div>
                <div class="tab-link" onclick="showTab(3)">Payments / Invoicing</div>
            </div>

            <!-- Pre-Shipping Info Tab -->
            <div class="tab-content active">
                <h3>Pre-Shipping Info</h3>
                <div class="row">
                    <div class="col">
                        <label>Order Number</label>
                        <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>RO Received Date</label>
                        <asp:TextBox ID="txtROReceivedDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Cusdec Done Date</label>
                        <asp:TextBox ID="txtCusdecDoneDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> Cusdec Number</label>
                         <asp:TextBox ID="txtCusdecNumber" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Vessal Name</label>
                         <asp:TextBox ID="txtVessalName" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Customer Name</label>
                         <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> ETA Colombo</label>
                         <asp:TextBox ID="txtETAColombo" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Forwarder</label>
                         <asp:TextBox ID="txtForwarder" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Shipping Line</label>
                         <asp:TextBox ID="txtShippingLine" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> SubOrder No Colombo</label>
                         <asp:TextBox ID="txtSubOrderNoColombo" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Payment Term</label>
                         <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Currency</label>
                         <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Port</label>
                         <asp:TextBox ID="txtPort" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> No of Containers</label>
                         <asp:TextBox ID="txtNoofContainers" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Container Size</label>
                         <asp:TextBox ID="txtContainerSize" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Delivery Term</label>
                         <asp:TextBox ID="txtDeliveryTerm" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Export Approval NOP</label>
                         <asp:TextBox ID="txtExportApprovalNOP" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Fumigation</label>
                         <asp:TextBox ID="txtFumigation" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Currency</label>
                         <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                     <div class="col">
                        <label> Currency</label>
                         <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                     </div>
                    <div class="col">
                        <label>FMG</label>
                        <asp:DropDownList ID="ddlFMG" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <!-- Loading Info Tab -->
            <div class="tab-content">
                <h3>Loading Info</h3>
                <div class="row">
                    <div class="col">
                        <label>Loading Date</label>
                        <asp:TextBox ID="txtLoadingDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>ETD Ship on Board</label>
                        <asp:TextBox ID="txtETDShiponBoard" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Container Number</label>
                        <asp:TextBox ID="txtContainerNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Seal Number</label>
                        <asp:TextBox ID="txtSealNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>BL Draft</label>
                        <asp:TextBox ID="txtBLDraft" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>

            <!-- Post-Shipping Info Tab -->
            <div class="tab-content">
                <h3>Post-Shipping Info</h3>
                <div class="row">
                    <div class="col">
                        <label>Obl Received</label>
                        <asp:TextBox ID="txtOblReceived" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Coa Received Date</label>
                        <asp:TextBox ID="txtCoaReceivedDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Marine Insurance</label>
                        <asp:TextBox ID="txtMarineInsurance" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>ICC  GSP</label>
                        <asp:TextBox ID="txtICCGSP" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Phyto Certificate</label>
                        <asp:TextBox ID="txtPhytoCertificate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Health Certificate</label>
                        <asp:TextBox ID="txtHealthCertificate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>COI Traces Draft</label>
                        <asp:TextBox ID="txtCOITracesDraft" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>ETA Destination</label>
                        <asp:TextBox ID="txtETADestination" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>COI Draft to Shipping</label>
                        <asp:TextBox ID="txtCOIDrafttoShipping" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>COI Draft to Customer</label>
                        <asp:TextBox ID="txtCOIDrafttoCustomer" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> Original COI Sent to Cus </label>
                        <asp:TextBox ID="txtOriginalCOISenttoCus" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Final Docs  Banked  Courier Date</label>
                        <asp:TextBox ID="txtFinalDocsBankedCourierDate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Packing List Send Date</label>
                        <asp:TextBox ID="txtPackingListSendDate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Invoice Send Date</label>
                        <asp:TextBox ID="txtInvoiceSendDate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>

            <!-- Payments / Invoicing Tab -->
            <div class="tab-content">
                <h3>Payments / Invoicing</h3>
                <div class="row">
                    <div class="col">
                        <label>Total Order Value</label>
                        <asp:TextBox ID="txtTotalOrderValue" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> Advance Invoice Amount</label>
                        <asp:TextBox ID="txtAdvanceInvoiceAmount" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> Advance Payment Receipt</label>
                        <asp:TextBox ID="txtAdvancePaymentReceipt" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Pay Due Date</label>
                        <asp:TextBox ID="txtPayDueDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Payment Status</label>
                        <asp:TextBox ID="txtPaymentStatus" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> BL Telex Requested</label>
                        <asp:TextBox ID="txtBLTelexRequested" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>BL Telex Received</label>
                        <asp:TextBox ID="txtBLTelexReceived" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label>Form Status</label>
                        <asp:DropDownList ID="ddlFormStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <label> Bank</label>
                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label> Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showTab(index) {
            const tabs = document.querySelectorAll('.tab-content');
            const links = document.querySelectorAll('.tab-link');
            tabs.forEach((tab, i) => {
                tab.classList.toggle('active', i === index);
                links[i].classList.toggle('active', i === index);
            });
        }
    </script>

</asp:Content>
