<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="MWM_Assignment_New.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6 mb-4">
                <div class="card border-0 shadow-sm p-3 text-center">
                    <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded" Style="max-height: 450px; object-fit: contain;" />
                </div>
            </div>

            <div class="col-md-6">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="Products.aspx">Shop</a></li>
                        <li class="breadcrumb-item active">
                            <asp:Literal ID="litCrumb" runat="server" /></li>
                    </ol>
                </nav>

                <h1 class="fw-bold">
                    <asp:Label ID="lblProductName" runat="server" /></h1>
                <h3 class="text-primary mb-3">RM
                    <asp:Label ID="lblPrice" runat="server" /></h3>

                <div class="mb-4">
                    <span class="badge bg-secondary">
                        <asp:Label ID="lblCategory" runat="server" /></span>
                    <span class="ms-3 text-muted">Availability: 
                   
                        <asp:Label ID="lblStock" runat="server" CssClass="fw-bold" />
                        units left
                </span>
                </div>

                <p class="text-muted mb-4" style="line-height: 1.8;">
                    <asp:Label ID="lblDescription" runat="server" />
                </p>

                <div class="card p-3 bg-light border-0">
                    <div class="row g-3 align-items-center">
                        <div class="col-auto">
                            <label class="fw-bold">Quantity:</label>
                        </div>
                        <div class="col-auto">
                            <asp:TextBox ID="txtQty" runat="server" TextMode="Number" Text="1" CssClass="form-control" Style="width: 80px;" min="1"></asp:TextBox>
                        </div>
                        <div class="col">
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary w-100" OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-3"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
