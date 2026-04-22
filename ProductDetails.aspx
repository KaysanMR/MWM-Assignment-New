<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="MWM_Assignment_New.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <div class="row g-4">
            <div class="col-md-6">
                <div class="card p-3 text-center">
                    <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded" Style="max-height: 450px; object-fit: contain;" />
                </div>
            </div>

            <div class="col-md-6">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Products.aspx") %>'>Shop</a></li>
                        <li class="breadcrumb-item active">
                            <asp:Literal ID="litCrumb" runat="server" /></li>
                    </ol>
                </nav>

                <h1 class="fw-bold">
                    <asp:Label ID="lblProductName" runat="server" /></h1>
                <div class="product-detail-badges mb-3">
                    <asp:Literal ID="litProductBadges" runat="server" />
                </div>
                <h3 class="text-primary mb-3">RM
                    <asp:Label ID="lblPrice" runat="server" /></h3>

                <div class="mb-4 d-flex flex-wrap align-items-center gap-2">
                    <span class="badge bg-secondary">
                        <asp:Label ID="lblCategory" runat="server" /></span>
                    <span class="text-muted">Availability: 
                   
                        <asp:Label ID="lblStock" runat="server" CssClass="fw-bold" />
                        units left
                </span>
                    <asp:Label ID="lblStockBadge" runat="server" CssClass="badge"></asp:Label>
                </div>

                <p class="text-muted mb-4">
                    <asp:Label ID="lblDescription" runat="server" />
                </p>

                <div class="card p-3 bg-light">
                    <div class="row g-3 align-items-center">
                        <div class="col-sm-auto">
                            <label class="fw-bold">Quantity:</label>
                        </div>
                        <div class="col-sm-auto">
                            <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Text="1" CssClass="form-control" Style="width: 80px;" min="1"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm">
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary w-100" OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-3"></asp:Label>
            </div>
        </div>

        <asp:Panel ID="pnlRecommendations" runat="server" CssClass="recommended-products-section mt-5" Visible="false">
            <div class="d-flex justify-content-between align-items-end flex-wrap gap-3 mb-3">
                <div>
                    <h2 class="fw-bold mb-0">You may also like</h2>
                </div>
                <a href='<%= ResolveUrl("~/Products.aspx") %>' class="btn btn-outline-dark">Browse all</a>
            </div>

            <div class="row g-4">
                <asp:Repeater ID="rptRecommendations" runat="server">
                    <ItemTemplate>
                        <div class="col-12 col-sm-6 col-lg-4 d-flex">
                            <div class="card product-card recommended-product-card w-100 position-relative">
                                <div class="product-badge-stack">
                                    <asp:Literal ID="litRecommendedBadges" runat="server"
                                        Text='<%# RenderProductBadges(Eval("ProductID"), Eval("ProductName"), Eval("CategoryName"), Eval("StockQuantity"), Eval("Badges")) %>' />
                                </div>
                                <div class="text-center p-3">
                                    <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' alt="Recommended canned fish"
                                        class="card-img-top">
                                </div>
                                <div class="card-body d-flex flex-column">
                                    <h6 class="text-muted small text-uppercase fw-bold"><%# Eval("CategoryName") %></h6>
                                    <h5 class="card-title h6 fw-bold mb-3"><%# Eval("ProductName") %></h5>
                                    <div class="mt-auto d-flex justify-content-between align-items-center gap-3">
                                        <span class="text-primary fw-bold">RM <%# Eval("Price", "{0:N2}") %></span>
                                        <a href='<%# ResolveUrl("~/ProductDetails.aspx") + "?id=" + Eval("ProductID") %>' class="stretched-link text-primary small fw-bold text-decoration-none">
                                            View <i class="bi bi-arrow-right"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
