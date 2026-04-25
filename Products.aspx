<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="MWM_Assignment_New.ProductGallery" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sm1" runat="server" />

    <div class="container page-shell">
        <div class="product-filter-panel mb-5">
            <div class="product-filter-intro">
                <h2 class="fw-bold text-dark mb-2">Explore Canned Fish</h2>
                <p class="text-muted mb-0">Find the perfect tin, flavor, and finish for your pantry shelf.</p>
            </div>

            <div class="product-filter-controls">
                <div class="filter-field filter-search-field">
                    <label class="form-label small fw-bold text-muted">Search</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Sardines, spicy..." />
                </div>

                <div class="filter-field filter-category-field">
                    <label class="form-label small fw-bold text-muted">Category</label>
                    <div class="category-filter-combo">
                        <asp:DropDownList ID="ddlFilterCategory" runat="server"
                            CssClass="form-select"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddlFilterCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <button class="btn btn-outline-dark filter-toggle" type="button" data-bs-toggle="collapse" data-bs-target="#advancedProductFilters" aria-expanded="false" aria-controls="advancedProductFilters">
                            <i class="bi bi-sliders"></i>
                            Filters
                        </button>
                    </div>
                </div>

                <div class="collapse advanced-filter-collapse" id="advancedProductFilters">
                    <div class="advanced-filter-menu">
                        <div class="filter-field">
                            <label class="form-label small fw-bold text-muted">Stock</label>
                            <asp:DropDownList ID="ddlStockFilter" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="FilterControl_Changed">
                                <asp:ListItem Value="all">All stock</asp:ListItem>
                                <asp:ListItem Value="available">Available</asp:ListItem>
                                <asp:ListItem Value="low">Low stock</asp:ListItem>
                                <asp:ListItem Value="out">Out of stock</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="filter-field">
                            <label class="form-label small fw-bold text-muted">Sort</label>
                            <asp:DropDownList ID="ddlSort" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="FilterControl_Changed">
                                <asp:ListItem Value="featured">Featured</asp:ListItem>
                                <asp:ListItem Value="name-asc">Name A-Z</asp:ListItem>
                                <asp:ListItem Value="price-asc">Price low-high</asp:ListItem>
                                <asp:ListItem Value="price-desc">Price high-low</asp:ListItem>
                                <asp:ListItem Value="stock-desc">Most stock</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="filter-field">
                            <label class="form-label small fw-bold text-muted">Badge</label>
                            <asp:DropDownList ID="ddlBadgeFilter" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="FilterControl_Changed">
                                <asp:ListItem Value="">Any badge</asp:ListItem>
                                <asp:ListItem Value="Best Seller">Best Seller</asp:ListItem>
                                <asp:ListItem Value="New">New</asp:ListItem>
                                <asp:ListItem Value="Premium">Premium</asp:ListItem>
                                <asp:ListItem Value="Spicy">Spicy</asp:ListItem>
                                <asp:ListItem Value="Limited">Limited</asp:ListItem>
                                <asp:ListItem Value="Family Size">Family Size</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="filter-actions">
                            <asp:Button ID="btnApplyFilters" runat="server" Text="Apply Filters" CssClass="btn btn-primary" OnClick="btnApplyFilters_Click" />
                            <asp:Button ID="btnClearFilters" runat="server" Text="Clear" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btnClearFilters_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="upProducts" runat="server">
            <ContentTemplate>
                <div class="row">
                    <asp:Label ID="lblNoProducts" runat="server" Text="No products found in this category."
                        Visible="false" CssClass="alert alert-info d-block w-100 mb-4"></asp:Label>

                    <asp:DataList ID="dlProducts" runat="server"
                        RepeatDirection="Horizontal"
                        RepeatLayout="Flow"
                        CssClass="row w-100 mx-0 g-4"
                        OnItemCommand="dlProducts_ItemCommand"
                        ItemStyle-CssClass="col-12 col-sm-6 col-md-4 col-lg-3 mb-4 d-flex align-items-stretch">
                        <ItemTemplate>
                            <div class="card h-100 product-card w-100 position-relative">
                                <div class="product-badge-stack">
                                    <asp:Literal ID="litProductBadges" runat="server"
                                        Text='<%# RenderProductBadges(Eval("ProductID"), Eval("ProductName"), Eval("CategoryName"), Eval("StockQuantity"), Eval("Badges")) %>' />
                                </div>

                                <div class="position-absolute top-0 end-0 p-3" style="z-index: 5;">
                                    <asp:LinkButton ID="btnWishlist" runat="server"
                                        CommandName="ToggleWishlist"
                                        CommandArgument='<%# Eval("ProductID") %>'
                                        CausesValidation="false"
                                        CssClass="wishlist-btn d-flex align-items-center justify-content-center text-decoration-none"
                                        Style="width: 38px; height: 38px;">
                                        <i class='<%# IsInWishlist(Eval("ProductID")) ? "bi bi-heart-fill text-danger" : "bi bi-heart text-muted" %>'></i>
                                    </asp:LinkButton>
                                </div>

                                <div class="text-center p-3">
                                    <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>'
                                        class="card-img-top" style="height: 180px; object-fit: contain;">
                                </div>

                                <div class="card-body d-flex flex-column">
                                    <h6 class="text-muted small text-uppercase fw-bold"><%# Eval("CategoryName") %></h6>
                                    <h5 class="card-title h6 fw-bold mb-3"><%# Eval("ProductName") %></h5>
                                    <span class='<%# GetStockBadgeClass(Eval("StockQuantity")) %>'><%# GetStockBadgeText(Eval("StockQuantity")) %></span>

                                    <div class="mt-auto d-flex justify-content-between align-items-center">
                                        <span class="text-primary fw-bold">RM <%# Eval("Price", "{0:N2}") %></span>
                                        <a href='<%# ResolveUrl("~/ProductDetails.aspx") + "?id=" + Eval("ProductID") %>' class="stretched-link text-primary small fw-bold text-decoration-none">View <i class="bi bi-arrow-right"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
