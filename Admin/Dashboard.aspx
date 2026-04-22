<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MWM_Assignment_New.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <h2 class="mb-4 text-center">Admin Dashboard</h2>

        <div class="row g-4 align-items-start">
            <div class="col-lg-4 col-xl-3">
                <aside class="admin-dashboard-sidebar">
                    <h3 class="fw-bold mb-3">Management</h3>

                    <div class="admin-sidebar-card border-primary">
                        <div>
                            <h5 class="text-primary">Users</h5>
                            <p class="text-muted mb-0">Registered members</p>
                        </div>
                        <strong><asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label></strong>
                        <a href="ManageUsers.aspx" class="btn btn-primary w-100">Go to Users</a>
                    </div>

                    <div class="admin-sidebar-card border-success">
                        <div>
                            <h5 class="text-success">Orders</h5>
                            <p class="text-muted mb-0">Pending fulfillment</p>
                        </div>
                        <strong><asp:Label ID="lblPendingOrders" runat="server" Text="0"></asp:Label></strong>
                        <a href="ManageOrders.aspx" class="btn btn-success w-100">Process Orders</a>
                    </div>

                    <div class="admin-sidebar-card border-info">
                        <div>
                            <h5 class="text-info">Catalog</h5>
                            <p class="text-muted mb-0">Products in inventory</p>
                        </div>
                        <strong><asp:Label ID="lblTotalProducts" runat="server" Text="0"></asp:Label></strong>
                        <div class="d-grid gap-2">
                            <a href="ManageProducts.aspx" class="btn btn-info text-white">Manage Products</a>
                            <a href="ManageCategories.aspx" class="btn btn-outline-info">Manage Categories</a>
                        </div>
                    </div>

                    <div class="admin-sidebar-card border-warning">
                        <div>
                            <h5 class="text-warning">Feedback</h5>
                            <p class="text-muted mb-0">Customer reviews</p>
                        </div>
                        <strong><asp:Label ID="lblTotalFeedback" runat="server" Text="0"></asp:Label></strong>
                        <a href="ManageFeedback.aspx" class="btn btn-warning text-white w-100">View Feedback</a>
                    </div>
                </aside>
            </div>

            <div class="col-lg-8 col-xl-9">
                <asp:HiddenField ID="hfOrdersChartData" runat="server" />
                <asp:HiddenField ID="hfTopProductsChartData" runat="server" />
                <asp:HiddenField ID="hfInventoryChartData" runat="server" />

                <div class="admin-reporting-section">
                    <div class="d-flex justify-content-between align-items-end flex-wrap gap-3 mb-3">
                        <div>
                            <h3 class="fw-bold mb-0">Sales Snapshot</h3>
                        </div>
                        <a href="ManageOrders.aspx" class="btn btn-outline-dark">Review Orders</a>
                    </div>

                    <div class="row text-center g-4 mb-4">
                        <div class="col-md-6">
                            <div class="card border-success h-100 shadow-sm admin-kpi-card">
                                <div class="card-body">
                                    <h5 class="card-title text-success">Revenue</h5>
                                    <h2 class="display-6 my-3">RM <asp:Label ID="lblRevenue" runat="server" Text="0.00"></asp:Label></h2>
                                    <p class="card-text text-muted">Total completed and pending order value.</p>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="card border-info h-100 shadow-sm admin-kpi-card">
                                <div class="card-body">
                                    <h5 class="card-title text-info">Average Rating</h5>
                                    <h2 class="display-6 my-3"><asp:Label ID="lblAverageRating" runat="server" Text="N/A"></asp:Label></h2>
                                    <p class="card-text text-muted">Average feedback score from customers.</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row g-4">
                        <div class="col-lg-8">
                            <div class="card admin-chart-card h-100">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between align-items-start gap-3 mb-3">
                                        <div>
                                            <h5 class="fw-bold mb-1">Orders Over Time</h5>
                                            <p class="text-muted mb-0 small">Daily order count and revenue for the latest activity window.</p>
                                        </div>
                                    </div>
                                    <div class="admin-chart-wrap">
                                        <canvas id="ordersOverTimeChart"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="card admin-chart-card h-100">
                                <div class="card-body">
                                    <h5 class="fw-bold mb-1">Inventory Health</h5>
                                    <p class="text-muted mb-3 small">Current stock distribution across the catalog.</p>
                                    <div class="admin-chart-wrap admin-chart-wrap-sm">
                                        <canvas id="inventoryHealthChart"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-7">
                            <div class="card admin-chart-card h-100">
                                <div class="card-body">
                                    <h5 class="fw-bold mb-1">Top Products</h5>
                                    <p class="text-muted mb-3 small">Best moving tins by quantity sold.</p>
                                    <div class="admin-chart-wrap">
                                        <canvas id="topProductsChart"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-5">
                            <div class="card admin-chart-card h-100">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between align-items-center mb-3">
                                        <div>
                                            <h5 class="fw-bold mb-1">Low-Stock Watch</h5>
                                            <p class="text-muted mb-0 small">Products with five or fewer units left.</p>
                                        </div>
                                        <a href="ManageProducts.aspx" class="small fw-bold">Manage</a>
                                    </div>
                                    <asp:Repeater ID="rptLowStockProducts" runat="server">
                                        <ItemTemplate>
                                            <div class="admin-low-stock-row">
                                                <span><%# Eval("ProductName") %></span>
                                                <strong><%# Eval("StockQuantity") %></strong>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="pnlNoLowStockProducts" runat="server" CssClass="profile-empty-note" Visible="false">
                                        Inventory is healthy. No low-stock products right now.
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-5">
                            <div class="card admin-chart-card h-100">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between align-items-center mb-3">
                                        <div>
                                            <h5 class="fw-bold mb-1">Recent Order Activity</h5>
                                            <p class="text-muted mb-0 small">Latest customer checkout movement.</p>
                                        </div>
                                        <a href="ManageOrders.aspx" class="small fw-bold">Open</a>
                                    </div>
                                    <asp:Repeater ID="rptRecentOrders" runat="server">
                                        <ItemTemplate>
                                            <div class="admin-activity-row">
                                                <div>
                                                    <strong>Order #<%# Eval("OrderID") %></strong>
                                                    <span><%# Eval("CustomerName") %> · <%# Eval("OrderDate", "{0:dd MMM yyyy}") %></span>
                                                </div>
                                                <div class="text-end">
                                                    <span class="admin-status-pill"><%# Eval("Status") %></span>
                                                    <strong>RM <%# Eval("TotalAmount", "{0:N2}") %></strong>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="pnlNoRecentOrders" runat="server" CssClass="profile-empty-note" Visible="false">
                                        No recent orders yet.
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        (function () {
            function readJson(id, fallback) {
                var field = document.getElementById(id);
                if (!field || !field.value) return fallback;
                try {
                    return JSON.parse(field.value);
                } catch (error) {
                    return fallback;
                }
            }

            function makeChart(id, config) {
                var canvas = document.getElementById(id);
                if (!canvas || typeof Chart === 'undefined') return;
                new Chart(canvas, config);
            }

            var orders = readJson('<%= hfOrdersChartData.ClientID %>', { labels: [], counts: [], revenue: [] });
            var topProducts = readJson('<%= hfTopProductsChartData.ClientID %>', { labels: [], quantities: [] });
            var inventory = readJson('<%= hfInventoryChartData.ClientID %>', { labels: [], counts: [] });

            var commonOptions = {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { labels: { color: '#171717', font: { family: 'Space Grotesk', weight: '700' } } }
                },
                scales: {
                    x: { ticks: { color: '#625d55' }, grid: { color: 'rgba(23, 23, 23, 0.08)' } },
                    y: { ticks: { color: '#625d55' }, grid: { color: 'rgba(23, 23, 23, 0.08)' }, beginAtZero: true }
                }
            };

            makeChart('ordersOverTimeChart', {
                type: 'line',
                data: {
                    labels: orders.labels,
                    datasets: [
                        {
                            label: 'Orders',
                            data: orders.counts,
                            borderColor: '#3d6fbf',
                            backgroundColor: 'rgba(61, 111, 191, 0.16)',
                            tension: 0.35,
                            fill: true
                        },
                        {
                            label: 'Revenue (RM)',
                            data: orders.revenue,
                            borderColor: '#cb421c',
                            backgroundColor: 'rgba(203, 66, 28, 0.12)',
                            tension: 0.35,
                            fill: true,
                            yAxisID: 'y'
                        }
                    ]
                },
                options: commonOptions
            });

            makeChart('topProductsChart', {
                type: 'bar',
                data: {
                    labels: topProducts.labels,
                    datasets: [{
                        label: 'Units sold',
                        data: topProducts.quantities,
                        backgroundColor: '#cb421c',
                        borderColor: '#171717',
                        borderWidth: 2
                    }]
                },
                options: Object.assign({}, commonOptions, { indexAxis: 'y' })
            });

            makeChart('inventoryHealthChart', {
                type: 'doughnut',
                data: {
                    labels: inventory.labels,
                    datasets: [{
                        data: inventory.counts,
                        backgroundColor: ['#dff1e4', '#ffe7ac', '#ffd8d1'],
                        borderColor: '#171717',
                        borderWidth: 2
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: { position: 'bottom', labels: { color: '#171717', font: { family: 'Space Grotesk', weight: '700' } } }
                    }
                }
            });
        })();
    </script>
</asp:Content>
