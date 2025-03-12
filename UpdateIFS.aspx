<%@ Page Title="Update IFS Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateIFS.aspx.cs" Inherits="Shipping_Tracker_V1.UpdateIFS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 700px;
            margin: 30px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        h2 {
            color: #005780;
            font-size: 1.5rem;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
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

        .form-group button {
            background-color: #027ca8;
            color: #ffffff;
            border: none;
            padding: 12px 20px;
            font-size: 1rem;
            cursor: pointer;
            border-radius: 6px;
            transition: background-color 0.3s ease;
            width: 100%;
        }

        .form-group button:hover {
            background-color: #013f82;
        }
    </style>

    <div class="container">
        <h2>Update IFS Details</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false" />
        
        <div class="form-group">
            <label for="txtOrderNumber">Enter Order Number:</label>
            <asp:TextBox ID="txtOrderNumber" runat="server" placeholder="Enter Order Number" CssClass="form-control" />
        </div>
        
        <div class="form-group">
            <asp:Button ID="btnUpdateIFS" runat="server" Text="Update IFS Details" CssClass="fetch-btn" OnClick="btnUpdateIFS_Click" />
        </div>
    </div>
</asp:Content>
