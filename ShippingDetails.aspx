<%@ Page Title="Shipping Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShippingDetails.aspx.cs" Inherits="Shipping_Tracker_V1.ShippingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 1400px;
            margin: 30px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            color: #005780;
            margin-bottom: 20px;
            font-size: 1.5rem;
        }
        .fetch-btn {
            background-color: #027ca8; 
            color: #ffffff;
            border: none;
            padding: 10px 20px;
            font-size: 1rem;
            cursor: pointer;
            border-radius: 6px;
            transition: background-color 0.3s ease;
        }

        .fetch-btn:hover {
            background-color: #013f82; 
        }

        .fetch-btn:active {
            background-color: #1e7e34;
            box-shadow: inset 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .fetch-btn:focus {
            outline: none;
            border: 2px solid #28a745;
        }


        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            font-weight: bold;
            color: #333;
            display: block;
            margin-bottom: 8px;
        }

        .form-group input {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 6px;
            font-size: 1rem;
        }

        .form-group button,
        .form-group asp:button {
            background-color: #005780;
            color: #ffffff;
            border: none;
            padding: 10px 20px;
            font-size: 1rem;
            cursor: pointer;
            border-radius: 6px;
            transition: background-color 0.3s ease;
        }

        .form-group button:hover,
        .form-group asp:button:hover {
            background-color: #003e54;
        }
        .form-group button:active,
        .form-group asp:button:active {
            background-color: #003e54;
            box-shadow: inset 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .form-group button:focus,
        .form-group asp:button:focus {
            outline: none;
            border: 2px solid #005780;
        }

        .table-container {
            margin-top: 30px;
            overflow-x: auto;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
            font-size: 0.9rem;
        }

        table th {
            background-color: #005780;
            color: #ffffff;
            padding: 10px;
            text-align: center;
        }

        table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        table tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        table tr:hover {
            background-color: #f1faff;
        }

        .logout-btn {
            background-color: #ffffff;
            color: #005780;
            border: 2px solid #005780;
            padding: 8px 15px;
            border-radius: 6px;
            font-size: 0.9rem;
            cursor: pointer;
            float: right;
        }

        .logout-btn:hover {
            background-color: #005780;
            color: #ffffff;
        }
    </style>

    <div class="container">
        <h2>Shipping Details</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <div class="form-group">
            <label for="txtOrderNumbers">Enter Order Number(s):</label>
            <asp:TextBox ID="txtOrderNumbers" runat="server" placeholder="Enter single or multiple order numbers separated by commas"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnFetchDetails" runat="server" Text="Fetch Details" CssClass="fetch-btn" OnClick="btnFetchDetails_Click" />
        </div>
        <div class="table-container">
            <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="false" CssClass="order-table" GridLines="None" OnRowCommand="gvOrderDetails_RowCommand">
                <Columns>
                    <asp:BoundField DataField="order_no" HeaderText="Order No" />
                    <asp:BoundField DataField="name" HeaderText="Name">
                     <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Del_term" HeaderText="Delivery Term" />
                    <asp:BoundField DataField="Ship_via" HeaderText="Vessal Name" />
                    <asp:BoundField DataField="del_terms" HeaderText="Delivery Location" />
                    <asp:BoundField DataField="ETD" HeaderText="ETD" />
                    <asp:BoundField DataField="RODATE" HeaderText="RO DATE" />
                    <asp:BoundField DataField="CONTNO" HeaderText="Container No" />
                    <asp:BoundField DataField="SHLINE" HeaderText="Ship Line" />
                    <asp:BoundField DataField="CONSN" HeaderText="Seal No" />
                    <asp:BoundField DataField="customer_no" HeaderText="Customer No" />
                    <asp:BoundField DataField="Advance" HeaderText="Advance Amount" />
                    <asp:BoundField DataField="account" HeaderText="Bank Account" />
                    <asp:BoundField DataField="ETA" HeaderText="ETA" />
                    <asp:BoundField DataField="CUSD_DATE" HeaderText="Cusd Date" />
                    <asp:BoundField DataField="FORWARD" HeaderText="Forwarder" />
                    <asp:BoundField DataField="dock_code" HeaderText="Dock Code" />
                    <asp:BoundField DataField="pay_term" HeaderText="Pay Term" />
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSaveRow" runat="server" Text="Save Row" CssClass="save-row-btn" CommandName="SaveOrder" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
