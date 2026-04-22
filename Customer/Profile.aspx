<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MWM_Assignment_New.Customer.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <div class="row g-4 align-items-stretch justify-content-center">
            <div class="col-lg-4 col-xl-3 d-flex">
                <aside class="profile-dashboard w-100 h-100">
                    <div class="profile-dashboard-hero">
                        <h3 class="mb-1">
                            <asp:Literal ID="litDashboardName" runat="server"></asp:Literal>
                        </h3>
                        <p class="text-muted mb-0">Your pantry rewards and recent shop activity.</p>
                    </div>

                    <div class="profile-dashboard-points">
                        <span class="small text-muted">Loyalty Points</span>
                        <strong>
                            <asp:Label ID="lblLoyaltyBalance" runat="server" Text="0"></asp:Label>
                        </strong>
                        <div class="progress profile-points-progress" role="progressbar" aria-label="Reward progress">
                            <div id="pointsProgressBar" runat="server" class="progress-bar"></div>
                        </div>
                        <asp:Label ID="lblNextReward" runat="server" CssClass="small text-muted d-block mt-2"></asp:Label>
                    </div>

                    <div class="profile-dashboard-stats">
                        <div>
                            <span>Orders</span>
                            <strong><asp:Label ID="lblOrderCount" runat="server" Text="0"></asp:Label></strong>
                        </div>
                        <div>
                            <span>Wishlist</span>
                            <strong><asp:Label ID="lblWishlistCount" runat="server" Text="0"></asp:Label></strong>
                        </div>
                        <div>
                            <span>Cart</span>
                            <strong><asp:Label ID="lblCartCount" runat="server" Text="0"></asp:Label></strong>
                        </div>
                    </div>

                    <div class="profile-dashboard-section">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h5 class="mb-0">Recent Orders</h5>
                            <a href='<%= ResolveUrl("~/Customer/MyOrders.aspx") %>' class="small fw-bold">View all</a>
                        </div>
                        <asp:Repeater ID="rptRecentOrders" runat="server">
                            <ItemTemplate>
                                <a class="profile-mini-row" href='<%# ResolveUrl("~/Customer/OrderDetails.aspx?id=" + Eval("OrderID")) %>'>
                                    <span>
                                        <strong>#<%# Eval("OrderID") %></strong>
                                        <small><%# Eval("OrderDate", "{0:dd MMM yyyy}") %></small>
                                    </span>
                                    <span>
                                        <strong>RM <%# Eval("TotalAmount", "{0:N2}") %></strong>
                                        <small><%# Eval("Status") %></small>
                                    </span>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pnlNoRecentOrders" runat="server" CssClass="profile-empty-note" Visible="false">
                            No orders yet. Your first seafood haul will show up here.
                        </asp:Panel>
                    </div>

                    <div class="profile-dashboard-section">
                        <h5 class="mb-2">Points Activity</h5>
                        <asp:Repeater ID="rptPointsHistory" runat="server">
                            <ItemTemplate>
                                <div class="profile-mini-row profile-mini-row-static">
                                    <span>
                                        <strong><%# Eval("Activity") %></strong>
                                        <small><%# Eval("ActivityDate", "{0:dd MMM yyyy}") %></small>
                                    </span>
                                    <span class="profile-points-earned">+<%# Eval("Points") %></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pnlNoPointsHistory" runat="server" CssClass="profile-empty-note" Visible="false">
                            Place an order to start building your points history.
                        </asp:Panel>
                    </div>

                    <div class="d-grid gap-2 mt-3">
                        <a href='<%= ResolveUrl("~/Products.aspx") %>' class="btn btn-primary">Shop Tins</a>
                        <a href='<%= ResolveUrl("~/Customer/Wishlist.aspx") %>' class="btn btn-outline-dark">Open Wishlist</a>
                    </div>
                </aside>
            </div>

            <div class="col-lg-6 col-xl-7 d-flex">
                <div class="card shadow-sm border-0 pt-3 w-100 h-100 profile-form-card">
                    <div class="card-body p-4">
                        <div class="text-center mb-4">
                            <div class="bg-primary bg-opacity-10 rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style="width: 80px; height: 80px;">
                                <i class="bi bi-person-badge text-primary fs-1"></i>
                            </div>
                            <h3 class="fw-bold">My Profile</h3>
                            <p class="text-muted small">Manage your account details and shipping info.</p>
                        </div>

                        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-success small mb-4">
                            <i class="bi bi-check-circle-fill me-2"></i>Profile updated successfully!
                        </asp:Panel>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Full Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3" ReadOnly="true"></asp:TextBox>
                            <div class="form-text small">Email cannot be changed for security.</div>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Phone Number</label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                        </div>

                        <div class="row g-3 mb-4">
                            <div class="col-12">
                                <label class="form-label small fw-bold text-muted">Address Line 1</label>
                                <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3" placeholder="House/unit number and street"></asp:TextBox>
                            </div>
                            <div class="col-12">
                                <label class="form-label small fw-bold text-muted">Address Line 2</label>
                                <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3" placeholder="Apartment, suite, building, landmark"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">City</label>
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">State</label>
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Postcode</label>
                                <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Country</label>
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                            </div>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" OnClick="btnUpdate_Click" 
                                CssClass="btn btn-primary btn-lg rounded-pill shadow-sm py-2 fs-6" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
