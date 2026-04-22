<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MWM_Assignment_New._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="landing-page">
        <header class="hero-panel">
            <div class="container py-5">
                <div class="row align-items-center g-4">
                    <div class="col-lg-7">
                        <span class="eyebrow">Curated pantry tins</span>
                        <div class="hero-copy">
                            <h1 class="display-4 mb-3">Reel In Your Next Pantry Favorite</h1>
                            <p class="lead mb-4">Discover premium sardines and canned fish packed with flavor, craft, and character.</p>
                            <div class="d-grid gap-2 d-sm-flex hero-actions stack-mobile">
                                <a href='<%= ResolveUrl("~/Products.aspx") %>' class="btn btn-primary btn-lg px-4">Shop Now</a>
                                <a href="#about" class="btn btn-outline-dark btn-lg px-4">Our Story</a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-5">
                        <div class="hero-image-placeholder" aria-label="Homepage hero image placeholder">
                            <div>
                                <span class="eyebrow mb-2">Image Placeholder</span>
                                <p class="mb-0">Hero product image coming soon.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <section class="page-shell section-cream">
            <div class="container">
                <div class="text-center mb-5 section-heading mx-auto">
                <span class="eyebrow">Featured Tins</span>
                <h2 class="fw-bold">Featured Collections</h2>
                <p class="text-muted">Handpicked tins packed with flavor, color, and pantry-ready charm.</p>
                </div>
        
                <div class="row row-cols-1 row-cols-md-3 g-4">
                    <asp:Repeater ID="rptFeatured" runat="server">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card h-100 product-card transition-hover">
                                    <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' class="card-img-top p-3" alt='<%# Eval("ProductName") %>' style="height: 200px; object-fit: contain;">
                                    <div class="card-body text-center">
                                        <h5 class="card-title fw-bold"><%# Eval("ProductName") %></h5>
                                        <p class="text-primary fw-bold">RM <%# Eval("Price", "{0:N2}") %></p>
                                        <a href='<%# ResolveUrl("~/ProductDetails.aspx") + "?id=" + Eval("ProductID") %>' class="btn btn-sm btn-outline-dark">View Details</a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>

        <section id="about" class="py-5 section-cream">
            <div class="container">
                <div class="row align-items-center g-4">
                    <div class="col-lg-6">
                        <span class="eyebrow">Why Golden Catch</span>
                        <h2 class="fw-bold">Canned seafood with flavor, color, and character.</h2>
                        <p>Golden Catch curates pantry-ready seafood tins that are easy to love and easy to serve. From pink salt and olive oil sardines to yuzu, shoyu, and black garlic flavor profiles, every item is selected for bold flavor, dependable quality, and a shelf presence worth showing off.</p>
                        <ul class="list-unstyled mb-0">
                            <li class="mb-2"><i class="bi bi-check2-circle text-primary me-2"></i>Carefully curated sardines and seafood tins</li>
                            <li class="mb-2"><i class="bi bi-check2-circle text-primary me-2"></i>Distinctive labels for quick, confident browsing</li>
                            <li><i class="bi bi-check2-circle text-primary me-2"></i>Secure local checkout</li>
                        </ul>
                    </div>
                    <div class="col-lg-6 text-center">
                        <div class="feature-figure">
                            <img src="Images/Product Photos/tomato and yuzu.PNG" alt="Golden Catch canned fish product render" class="img-fluid">
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="store-locator" class="py-5 section-cream store-locator-section">
            <div class="container">
                <div class="locator-panel locator-feature mb-3">
                    <div class="row g-4 align-items-center">
                        <div class="col-lg-8">
                            <div class="locator-copy">
                            <span class="eyebrow">Store Locator</span>
                            <h2 class="fw-bold mb-3">Visit the Golden Catch Pantry.</h2>
                            <p class="mb-3">Sample our signature sardine flavors, browse limited tins, and pick up ready-to-gift seafood packs from our Kuala Lumpur shelf.</p>
                            <address class="mb-0">
                                <strong>Golden Catch Tasting Room</strong><br />
                                18 Jalan Kamunting, Chow Kit,<br />
                                50300 Kuala Lumpur, Malaysia
                            </address>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <img src='<%= ResolveUrl("~/Images/Store-Layout.PNG") %>' alt="Golden Catch store layout render" class="store-layout-img" />
                        </div>
                    </div>
                </div>

                <div class="locator-map-frame">
                    <iframe
                        title="Golden Catch Kuala Lumpur store map"
                        src="https://www.openstreetmap.org/export/embed.html?bbox=101.6902%2C3.1524%2C101.7102%2C3.1724&amp;layer=mapnik&amp;marker=3.1624%2C101.7002"
                        loading="lazy"
                        referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
