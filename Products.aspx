<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="MWM_Assignment_New.ProductGallery" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sm1" runat="server" />

    <div class="container mt-5">
        <div class="row mb-4 align-items-center">
            <div class="col-md-8">
                <h2 class="fw-bold text-dark">Explore Keyboards</h2>
                <p class="text-muted">Find the perfect switch and aesthetic for your setup.</p>
            </div>
            <div class="col-md-4">
                <div class="input-group">
                    <label class="input-group-text bg-white border-end-0 text-muted">
                        <i class="bi bi-filter"></i>
                    </label>
                    <asp:DropDownList ID="ddlFilterCategory" runat="server"
                        CssClass="form-select border-start-0"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="ddlFilterCategory_SelectedIndexChanged">
                    </asp:DropDownList>
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
                        CssClass="row w-100 mx-0"
                        OnItemCommand="dlProducts_ItemCommand"
                        ItemStyle-CssClass="col-12 col-sm-6 col-md-4 col-lg-3 mb-4 d-flex align-items-stretch">
                        <ItemTemplate>
                            <div class="card h-100 shadow-sm border-0 product-card w-100 position-relative">

                                <div class="position-absolute top-0 end-0 p-3" style="z-index: 5;">
                                    <asp:LinkButton ID="btnWishlist" runat="server"
                                        CommandName="ToggleWishlist"
                                        CommandArgument='<%# Eval("ProductID") %>'
                                        CssClass="wishlist-btn shadow-sm bg-white rounded-circle d-flex align-items-center justify-content-center text-decoration-none"
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

                                    <div class="mt-auto d-flex justify-content-between align-items-center">
                                        <span class="text-primary fw-bold">RM <%# Eval("Price", "{0:N2}") %></span>
                                        <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>' class="stretched-link text-primary small fw-bold text-decoration-none">View <i class="bi bi-arrow-right"></i>
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
