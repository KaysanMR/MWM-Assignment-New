<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MWM_Assignment_New._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <header class="py-5 bg-dark text-white text-center shadow-lg" style="background: linear-gradient(45deg, #0f0c29, #302b63, #24243e);">
        <div class="container py-5">
            <h1 class="display-4 fw-bold">Elevate Your Typing Experience</h1>
            <p class="lead mb-4">Discover premium mechanical keyboards designed with precision and art.</p>
            <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                <a href="Products.aspx" class="btn btn-primary btn-lg px-4 gap-3 shadow">Shop Now</a>
                <a href="#about" class="btn btn-outline-light btn-lg px-4">Our Story</a>
            </div>
        </div>
    </header>

    <section class="container my-5">
        <div class="text-center mb-5">
            <h2 class="fw-bold">Featured Collections</h2>
            <p class="text-muted">Handpicked favorites from our latest 3D designs.</p>
        </div>
        
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <asp:Repeater ID="rptFeatured" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 border-0 shadow-sm transition-hover">
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

    <section id="about" class="bg-light py-5">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-lg-6">
                    <h2 class="fw-bold">Why Clicky?</h2>
                    <p class="lead">Where technology meets craftsmanship.</p>
                    <p>Clicky isn't just a store; it's a project born from a passion for 3D art and mechanical precision. Every keyboard in our gallery is rendered with Non-Photorealistic Rendering (NPR) techniques to highlight the soul of the hardware.</p>
                    <ul class="list-unstyled">
                        <li><i class="bi bi-check2-circle text-primary me-2"></i> Custom-tuned switches</li>
                        <li><i class="bi bi-check2-circle text-primary me-2"></i> Artist-designed keycaps</li>
                        <li><i class="bi bi-check2-circle text-primary me-2"></i> Secure local checkout</li>
                    </ul>
                </div>
                <div class="col-lg-6 text-center">
                    <img src="Images/about-hero.png" alt="3D Keyboard Render" class="img-fluid rounded shadow">
                </div>
            </div>
        </div>
    </section>

</asp:Content>