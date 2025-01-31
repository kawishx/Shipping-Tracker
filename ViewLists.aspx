<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewLists.aspx.cs" Inherits="Shipping_Tracker_V1.ViewLists" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
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
        }

        .btn-primary:hover {
            background-color: #013f82;
        }

        .table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }

        .table th, .table td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .table th {
            background-color: #f1f1f1;
        }

        .table-bordered {
            border: 1px solid #ddd;
        }

        .table-striped tr:nth-child(even) {
            background-color: #f9f9f9;
        }

    </style>
    

    <h2>View Order Status</h2>

    <div class="form-group">
        <label for="ddlOrderStatus">Select Order Status:</label>
        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control" AutoPostBack="false">
            <asp:ListItem Text="Select Status" Value="" />
            <asp:ListItem Text="Ongoing" Value="Ongoing" />
            <asp:ListItem Text="Completed" Value="Completed" />
        </asp:DropDownList>
    </div>

    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" OnClick="btnView_Click" />

        <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>


    <hr />

    <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Order_no" HeaderText="Order No" SortExpression="Order_no" />
            <asp:BoundField DataField="RODATE" HeaderText="RODATE" SortExpression="RODATE" />
            <asp:BoundField DataField="ship_via" HeaderText="Ship Via" SortExpression="ship_via" />
            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
            <asp:BoundField DataField="ETA" HeaderText="ETA" SortExpression="ETA" />
            <asp:BoundField DataField="currency_code" HeaderText="Currency Code" SortExpression="currency_code" />
       
            <asp:TemplateField HeaderText="Click to Update Order">
                <ItemTemplate>
                    <asp:HyperLink ID="hlOrderNo" runat="server" NavigateUrl='<%# "OrderChecklist.aspx?OrderNo=" + Eval("Order_no") %>' 
                        Text='<%# Eval("Order_no") %>' CssClass="link-primary" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>

</asp:Content>