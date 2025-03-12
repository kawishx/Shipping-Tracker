<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewLists.aspx.cs" Inherits="Shipping_Tracker_V1.ViewLists" %>
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
        <h2>View Order Status</h2>

        <div class="form-group">
            <label for="ddlOrderStatus">Select Order Status:</label>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control">
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

        <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>

        <hr />

        <div class="table-wrapper">
            <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Order_no" HeaderText="Order No" SortExpression="Order_no" />
                    <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                    <asp:BoundField DataField="Del_term" HeaderText="Delivery Term" SortExpression="Del_term" />
                    <asp:BoundField DataField="ship_via" HeaderText="Ship Via" SortExpression="ship_via" />
                    <asp:BoundField DataField="SHLINE" HeaderText="Ship Line" SortExpression="SHLINE" />
                    <asp:BoundField DataField="FORWARD" HeaderText="Forwarder" SortExpression="FORWARD" />
                    <asp:BoundField DataField="ETA" HeaderText="ETA to Colombo" SortExpression="ETA" />
                    <asp:BoundField DataField="del_terms" HeaderText="Delivery Location" SortExpression="del_terms" />
                    <asp:BoundField DataField="RODATE" HeaderText="RODATE" SortExpression="RODATE" />
                    <asp:BoundField DataField="CUSD_DATE" HeaderText="Cusd Date" SortExpression="CUSD_DATE" />
                    <asp:BoundField DataField="CUSDEC_NO" HeaderText="Cusd NO" SortExpression="CUSDEC_NO" />
                    <asp:BoundField DataField="FMG" HeaderText="Fumigation" SortExpression="FMG" />
                    <asp:BoundField DataField="EXNOP" HeaderText="Export Aprvl/NOP" SortExpression="EXNOP" />

                    <asp:TemplateField HeaderText="Click to Update Order">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlOrderNo" runat="server" NavigateUrl='<%# "OrderChecklist.aspx?OrderNo=" + Eval("Order_no") %>' 
                                Text='<%# Eval("Order_no") %>' CssClass="link-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
