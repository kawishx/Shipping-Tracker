<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderChecklist.aspx.cs" Inherits="Shipping_Tracker_V1.OrderChecklist" %>
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
            background-color: #f0f0f0;  /* Light grey background */
            border: 1px solid #ddd;     /* Lighter border */
            cursor: not-allowed;        /* Show a non-editable cursor */
        }

        .text-end {
            text-align: end;
        }
    </style>

   <div class="container">
        <h2>Order Checklist</h2>

        <!-- Message Label -->
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>
        </div>

        <div>
            <label for="txtOrderNo">Order No:</label>
            <asp:TextBox ID="txtOrderNo" CssClass="form-control" runat="server" placeholder="Enter Order Number"></asp:TextBox>
            <div class="text-end">
                <asp:Button ID="btnFetchOrder" Text="Fetch Details" CssClass="btn btn-primary" OnClick="btnFetch_Click" runat="server" />
            </div>
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
                    <asp:PlaceHolder ID="phPreShipping" runat="server"></asp:PlaceHolder>
                 </div>
                <div class="text-end">
                    <asp:Button ID="btnSavePreShipping" Text="Save" CssClass="btn btn-success" OnClick="btnSavePreShipping_Click" runat="server" />
                </div>
            </div>

            <!-- Loading Info Tab -->
            <div class="tab-content">
                <h3>Loading Info</h3>
                <div class="row">
                <asp:PlaceHolder ID="phLoading" runat="server"></asp:PlaceHolder>
                </div>
                <div class="text-end">
                    <asp:Button ID="btnSaveLoading" Text="Save" CssClass="btn btn-success" OnClick="btnSaveLoading_Click" runat="server" />
                </div>
            </div>

            <!-- Post-Shipping Info Tab -->
            <div class="tab-content">
                <h3>Post-Shipping Info</h3>
                <div class="row">
                <asp:PlaceHolder ID="phPostShipping" runat="server"></asp:PlaceHolder>
                </div>
                <div class="text-end">
                    <asp:Button ID="btnSavePostShipping" Text="Save" CssClass="btn btn-success" OnClick="btnSavePostShipping_Click" runat="server" />
                </div>
            </div>

             <!-- Payments / Invoicing Tab -->
            <div class="tab-content">
                <h3>Payments / Invoicing</h3>
                <div class="row">
                <asp:PlaceHolder ID="phPaymentsInvoicing" runat="server"></asp:PlaceHolder>
                </div>
                <div class="text-end">
                    <asp:Button ID="btnSavePaymentsInvoicing" Text="Save" CssClass="btn btn-success" OnClick="btnPaymentsInvoicing_Click" runat="server" />
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
