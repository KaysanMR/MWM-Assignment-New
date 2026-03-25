<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="MWM_Assignment_New.ProductGallery" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row mb-4">
            <div class="col-md-8">
                <h2 class="fw-bold">Explore Keyboards</h2>
                <p class="text-muted">Find the perfect switch and aesthetic for your setup.</p>
            </div>
            <div class="col-md-4 text-end">
                <asp:DropDownList ID="ddlFilterCategory" runat="server" CssClass="form-select"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlFilterCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <asp:DataList ID="dlProducts" runat="server"
                RepeatDirection="Horizontal"
                RepeatLayout="Flow"
                CssClass="row w-100"
                ItemStyle-CssClass="col-12 col-sm-6 col-md-4 col-lg-3 mb-4 d-flex align-items-stretch">
                <ItemTemplate>
                    <div class="card h-100 shadow-sm border-0 product-card w-100">
                        <div class="text-center p-3">
                            <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>'
                                class="card-img-top"
                                style="height: 180px; object-fit: contain;">
                        </div>
                        <div class="card-body d-flex flex-column">
                            <h6 class="text-muted small"><%# Eval("CategoryName") %></h6>
                            <h5 class="card-title h6 fw-bold"><%# Eval("ProductName") %></h5>
                            <p class="text-primary fw-bold mt-auto mb-2">RM <%# Eval("Price", "{0:N2}") %></p>
                            <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>'
                                class="btn btn-sm btn-primary w-100">Details</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>

        <asp:Label ID="lblNoProducts" runat="server" Text="No products found in this category."
            Visible="false" CssClass="alert alert-info d-block"></asp:Label>
    </div>
</asp:Content>
