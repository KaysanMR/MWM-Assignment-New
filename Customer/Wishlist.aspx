<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="MWM_Assignment_New.Customer.Wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sm1" runat="server" />
    <div class="container mt-5">
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold">My Wishlist</h2>
                <p class="text-muted">Review and manage your saved mechanical keyboards.</p>
            </div>
        </div>

        <asp:UpdatePanel ID="upWishlist" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblEmptyWishlist" runat="server" Text="Your wishlist is empty." 
                    Visible="false" CssClass="alert alert-light border text-center py-4 d-block w-100"></asp:Label>

                <div class="card shadow-sm border-0">
                    <div class="table-responsive">
                        <asp:GridView ID="gvWishlist" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover align-middle mb-0" GridLines="None"
                            OnRowCommand="gvWishlist_RowCommand" DataKeyNames="ProductID">
                            <Columns>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center p-2">
                                            <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' 
                                                 alt="Keyboard" class="img-thumbnail me-3" style="width: 80px; height: 60px; object-fit: contain;">
                                            <div>
                                                <h6 class="mb-0 fw-bold"><%# Eval("ProductName") %></h6>
                                                <small class="text-muted"><%# Eval("CategoryName") %></small>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <span class="fw-bold text-primary">RM <%# Eval("Price", "{0:N2}") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="text-end">
                                    <ItemTemplate>
                                        <a href='../ProductDetails.aspx?id=<%# Eval("ProductID") %>' class="btn btn-sm btn-light border me-2">
                                            View Details
                                        </a>
                                        <asp:LinkButton ID="btnRemove" runat="server" 
                                            CommandName="RemoveItem" 
                                            CommandArgument='<%# Eval("ProductID") %>'
                                            CssClass="btn btn-sm btn-outline-danger">
                                            <i class="bi bi-trash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="table-light text-muted small text-uppercase" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
