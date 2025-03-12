<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPages.aspx.cs" Inherits="Shipping_Tracker_V1.UserPages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="style1">
            <td colspan="2">
                <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Height="184px" PageSize="20" ShowFooter="True" Width="534px">
                    <ItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                    <HeaderStyle BackColor="#007A87" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="frontcheck" Runat="server" Width="40" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="parent" HeaderText="Parent" ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="path" HeaderText="Path" ReadOnly="True"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages" />
                </asp:DataGrid>
            </td>
        </tr>

        <!-- Success Message Label -->
        <tr class="style1">
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Bold="True" Visible="False"></asp:Label>
            </td>
        </tr>

        <tr class="style1">
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Add" Width="81px" OnClick="Button2_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="Button3" runat="server" Text="Clear" Width="81px" />
            </td>
        </tr>
    </table>
</asp:Content>
