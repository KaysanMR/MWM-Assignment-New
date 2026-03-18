<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MWM_Assignment_New.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4 text-center">Admin Dashboard</h2>

        <div class="row text-center mt-4">
            <div class="col-md-6 mb-4">
                <div class="card border-primary h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-primary">User Management</h5>
                        <h2 class="display-4 my-3">
                            <asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label></h2>
                        <p class="card-text text-muted">Registered members in the system.</p>
                        <a href="ManageUsers.aspx" class="btn btn-primary w-100">Go to Users</a>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-success h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-success">Order Management</h5>
                        <h2 class="display-4 my-3">
                            <asp:Label ID="lblPendingOrders" runat="server" Text="0"></asp:Label></h2>
                        <p class="card-text text-muted">Orders currently marked as "Pending".</p>
                        <a href="ManageOrders.aspx" class="btn btn-success w-100">Process Orders</a>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-info h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-info">Store Catalog</h5>
                        <h2 class="display-4 my-3">
                            <asp:Label ID="lblTotalProducts" runat="server" Text="0"></asp:Label></h2>
                        <p class="card-text text-muted">Total items in inventory.</p>
                        <div class="d-grid gap-2">
                            <a href="ManageProducts.aspx" class="btn btn-info text-white">Manage Products</a>
                            <a href="ManageCategories.aspx" class="btn btn-outline-info">Manage Categories</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-warning h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-warning">Customer Feedback</h5>
                        <h2 class="display-4 my-3">
                            <asp:Label ID="lblTotalFeedback" runat="server" Text="0"></asp:Label></h2>
                        <p class="card-text text-muted">Unread messages or product reviews.</p>
                        <a href="ManageFeedback.aspx" class="btn btn-warning text-white w-100">View Feedback</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
