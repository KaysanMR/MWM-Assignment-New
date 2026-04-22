<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="MWM_Assignment_New.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <div class="row g-4">
            <div class="col-md-7">
                <div class="card p-4">
                    <h3 class="fw-bold mb-4">Shipping Information</h3>
                    
                    <div class="row g-3 mb-3">
                        <div class="col-12">
                            <label class="form-label fw-bold">Address Line 1</label>
                            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" placeholder="House/unit number and street"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddressLine1" runat="server"
                                ControlToValidate="txtAddressLine1" ErrorMessage="Please enter address line 1."
                                CssClass="text-danger small" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12">
                            <label class="form-label fw-bold">Address Line 2 <span class="text-muted fw-normal">(optional)</span></label>
                            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control" placeholder="Apartment, suite, building, landmark"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label fw-bold">City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCity" runat="server"
                                ControlToValidate="txtCity" ErrorMessage="Please enter your city."
                                CssClass="text-danger small" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label fw-bold">State</label>
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvState" runat="server"
                                ControlToValidate="txtState" ErrorMessage="Please enter your state."
                                CssClass="text-danger small" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label fw-bold">Postcode</label>
                            <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control" placeholder="Postcode"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPostcode" runat="server"
                                ControlToValidate="txtPostcode" ErrorMessage="Please enter your postcode."
                                CssClass="text-danger small" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label fw-bold">Country</label>
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" Text="Malaysia" placeholder="Country"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server"
                                ControlToValidate="txtCountry" ErrorMessage="Please enter your country."
                                CssClass="text-danger small" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </div>
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
                <div class="card bg-light p-4">
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
                        CssClass="btn btn-primary btn-lg w-100" OnClick="btnPlaceOrder_Click" />
                    
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3 d-block" />
                    
                    <p class="text-muted small mt-4 text-center">
                        <i class="bi bi-shield-lock"></i> Secure Transaction
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
