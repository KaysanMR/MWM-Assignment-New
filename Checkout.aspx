<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="MWM_Assignment_New.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-7">
                <div class="card shadow-sm border-0 p-4">
                    <h3 class="fw-bold mb-4">Shipping Information</h3>
                    
                    <div class="mb-3">
                        <label class="form-label fw-bold">Full Delivery Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="4" 
                            CssClass="form-control" placeholder="House No, Street Name, City, Postcode, State"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
                            ControlToValidate="txtAddress" ErrorMessage="Please enter your shipping address." 
                            CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-bold">Payment Method</label>
                        <div class="form-check border rounded p-3 mb-2">
                            <input class="form-check-input ms-1" type="radio" name="pay" id="cod" checked>
                            <label class="form-check-label ms-2" for="cod">
                                Cash on Delivery (COD)
                            </label>
                        </div>
                        <div class="form-check border rounded p-3 opacity-50">
                            <input class="form-check-input ms-1" type="radio" name="pay" id="card" disabled>
                            <label class="form-check-label ms-2" for="card">
                                Credit/Debit Card (Coming Soon)
                            </label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-5">
                <div class="card shadow-sm border-0 bg-light p-4">
                    <h4 class="fw-bold mb-3">Order Summary</h4>
                    <hr />
                    
                    <div class="d-flex justify-content-between mb-2">
                        <span>Items Subtotal:</span>
                        <span class="fw-bold"><asp:Label ID="lblSubtotal" runat="server" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-2">
                        <span>Shipping:</span>
                        <span class="text-success fw-bold">FREE</span>
                    </div>
                    <hr />
                    <div class="d-flex justify-content-between mb-4">
                        <span class="h5 fw-bold">Total Amount:</span>
                        <span class="h5 fw-bold text-primary"><asp:Label ID="lblGrandTotal" runat="server" /></span>
                    </div>

                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Confirm Order" 
                        CssClass="btn btn-primary btn-lg w-100 shadow-sm" OnClick="btnPlaceOrder_Click" />
                    
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3 d-block" />
                    
                    <p class="text-muted small mt-4 text-center">
                        <i class="bi bi-shield-lock"></i> Secure Transaction
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>