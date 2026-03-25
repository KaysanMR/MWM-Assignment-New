<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="MWM_Assignment_New._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container vh-100 d-flex align-items-center justify-content-center">
        <div class="text-center">
            <div class="display-1 fw-bold text-primary mb-0" style="font-size: 8rem;">404</div>
            <h2 class="fw-bold text-dark mb-4">Switch Not Found</h2>
            
            <p class="text-muted mb-5 shadow-sm p-3 bg-light rounded border-start border-primary border-4">
                It looks like the page you're looking for has been de-soldered or moved to a different board. 
                Let's get you back to the home row.
            </p>

            <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                <a href='<%= ResolveUrl("~/Default.aspx") %>' class="btn btn-primary btn-lg px-4 gap-3 rounded-pill shadow-sm">
                    <i class="bi bi-house-door me-2"></i>Back to Home
                </a>
                <a href='<%= ResolveUrl("~/Products.aspx") %>' class="btn btn-outline-secondary btn-lg px-4 rounded-pill">
                    <i class="bi bi-search me-2"></i>Browse Keyboards
                </a>
            </div>
        </div>
    </div>
</asp:Content>