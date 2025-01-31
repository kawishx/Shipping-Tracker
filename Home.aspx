<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Shipping_Tracker_V1.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .home-container {
            max-width: 900px;
            margin: 50px auto;
            padding: 30px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
            animation: fadeIn 1s ease-in-out;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .home-title {
            color: #005780;
            font-size: 2rem;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .home-subtitle {
            color: #333;
            font-size: 1.2rem;
            margin-bottom: 20px;
        }

        .action-btn {
            display: inline-block;
            background-color: #005780;
            color: #ffffff;
            padding: 12px 25px;
            font-size: 1rem;
            font-weight: bold;
            text-decoration: none;
            border-radius: 6px;
            transition: all 0.3s ease;
        }

        .action-btn:hover {
            background-color: #003e54;
            transform: translateY(-2px);
        }
    </style>

    <div class="home-container">
        <h1 class="home-title">Welcome to Renuka Group</h1>
        <p class="home-subtitle">Shipment Tracking Web Application</p>
        <p>Use the menu buttons above to navigate through the system and start managing your shipments efficiently.</p>
        <a href="ShippingDetails.aspx" class="action-btn">Get Started</a>
    </div>

</asp:Content>
