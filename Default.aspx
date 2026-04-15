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
                                <a href="Products.aspx" class="btn btn-primary btn-lg px-4">Shop Now</a>
                                <a href="#about" class="btn btn-outline-dark btn-lg px-4">Our Story</a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-5">
                        <div class="card hero-note">
                            <div class="card-body">
                                <span class="eyebrow mb-3">Shelf Notes</span>
                                <ul>
                                    <li>Rendered tins with crisp, illustrated label design.</li>
                                    <li>Curated selections instead of a noisy, endless catalog.</li>
                                    <li>Wishlist, fast checkout, and order tracking built in.</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <section class="container page-shell">
            <div class="text-center mb-5 section-heading mx-auto">
                <span class="eyebrow">Featured Tins</span>
                <h2 class="fw-bold">Featured Collections</h2>
                <p class="text-muted">Handpicked favorites from our latest 3D designs.</p>
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
                                    <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>' class="btn btn-sm btn-outline-dark">View Details</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

        <section id="about" class="py-5">
            <div class="container">
                <div class="row align-items-center g-4">
                    <div class="col-lg-6">
                        <span class="eyebrow">Why Silver Shoal</span>
                        <h2 class="fw-bold">Where pantry staples meet playful presentation.</h2>
                        <p>Silver Shoal isn't just a store; it's a project born from a love of 3D art and beautifully illustrated seafood tins. Every product in our gallery is rendered with Non-Photorealistic Rendering (NPR) techniques to highlight the charm of each can and catch.</p>
                        <ul class="list-unstyled mb-0">
                            <li class="mb-2"><i class="bi bi-check2-circle text-primary me-2"></i>Carefully curated sardine selections</li>
                            <li class="mb-2"><i class="bi bi-check2-circle text-primary me-2"></i>Artist-designed canned fish labels</li>
                            <li><i class="bi bi-check2-circle text-primary me-2"></i>Secure local checkout</li>
                        </ul>
                    </div>
                    <div class="col-lg-6 text-center">
                        <div class="feature-figure">
                            <img src="Images/about-hero.png" alt="3D canned fish render" class="img-fluid">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
