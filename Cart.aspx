<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MWM_Assignment_New.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="fw-bold mb-4">Your Shopping Cart</h2>

        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False"
            CssClass="table table-hover align-middle border shadow-sm" OnRowDeleting="gvCart_RowDeleting" DataKeyNames="ProductID">
            <Columns>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <div class="d-flex align-items-center">
                            <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' style="width: 50px; height: 50px; object-fit: contain;" class="me-3" />
                            <span class="fw-bold"><%# Eval("ProductName") %></span>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="RM {0:N2}" />
                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="RM {0:N2}" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" ControlStyle-CssClass="btn btn-sm btn-danger" />
            </Columns>
        </asp:GridView>

        <div class="text-end mt-4">
            <h4>Grand Total:
                <asp:Label ID="lblGrandTotal" runat="server" CssClass="text-primary" /></h4>
            <hr />
            <a href="Products.aspx" class="btn btn-outline-secondary">Continue Shopping</a>
            <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-success px-5" OnClick="btnCheckout_Click" />
        </div>
    </div>
</asp:Content>
