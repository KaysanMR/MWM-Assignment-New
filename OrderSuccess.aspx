<%@ Page Title="Order Success" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderSuccess.aspx.cs" Inherits="MWM_Assignment_New.OrderSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8 text-center">
                <div class="card shadow-sm border-0 p-5">
                    <div class="mb-4">
                        <i class="bi bi-check-circle-fill text-success" style="font-size: 5rem;"></i>
                    </div>
                    
                    <h1 class="fw-bold text-dark">Payment Received!</h1>
                    <p class="text-muted fs-5">
                        Thank you for your purchase. Your order 
                        <span class="text-primary fw-bold">#<asp:Literal ID="litOrderID" runat="server" /></span> 
                        has been placed successfully.
                    </p>
                    
                    <div class="alert alert-info d-inline-block px-4 mt-3">
                        <i class="bi bi-info-circle me-2"></i> 
                        A confirmation email has been sent to your registered address.
                    </div>

                    <hr class="my-4" />

                    <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                        <a href="Products.aspx" class="btn btn-outline-primary btn-lg px-4">Continue Shopping</a>
                        <a href="Customer/MyOrders.aspx" class="btn btn-primary btn-lg px-4">View My Orders</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>