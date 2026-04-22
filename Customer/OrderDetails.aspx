<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="MWM_Assignment_New.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="fw-bold">Order #<asp:Literal ID="litOrderID" runat="server" /></h2>
            <a href='<%= ResolveUrl("~/Customer/MyOrders.aspx") %>' class="btn btn-outline-secondary btn-sm">
                <i class="bi bi-arrow-left"></i> Back to History
            </a>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="card shadow-sm border-0 mb-4">
                    <div class="card-header bg-white fw-bold">Items Purchased</div>
                    <div class="card-body p-0">
                        <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover mb-0" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center p-2">
                                            <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' 
                                                 style="width: 50px; height: 50px; object-fit: contain;" class="me-3 border rounded" />
                                            <span class="fw-bold"><%# Eval("ProductName") %></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnitPrice" HeaderText="Price" DataFormatString="RM {0:N2}" />
                                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Total" DataFormatString="RM {0:N2}" ItemStyle-CssClass="fw-bold" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Panel ID="pnlFeedback" runat="server" CssClass="card shadow-sm border-0 mb-4">
                    <div class="card-header bg-white fw-bold">Rate This Order</div>
                    <div class="card-body">
                        <asp:Panel ID="pnlFeedbackSaved" runat="server" Visible="false" CssClass="alert alert-success">
                            Thank you. Your feedback has been recorded.
                        </asp:Panel>
                        <div class="row g-3">
                            <div class="col-lg-5">
                                <label class="form-label fw-bold">Rating</label>
                                <div class="rating-picker" data-rating-picker>
                                    <button type="button" class="rating-star" data-rating-value="1" aria-label="1 star"></button>
                                    <button type="button" class="rating-star" data-rating-value="2" aria-label="2 stars"></button>
                                    <button type="button" class="rating-star" data-rating-value="3" aria-label="3 stars"></button>
                                    <button type="button" class="rating-star" data-rating-value="4" aria-label="4 stars"></button>
                                    <button type="button" class="rating-star" data-rating-value="5" aria-label="5 stars"></button>
                                    <asp:TextBox ID="txtRating" runat="server" TextMode="Number" CssClass="form-control rating-number"
                                        Text="5" min="1" max="5" step="1" aria-label="Rating from 1 to 5"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvRating" runat="server"
                                    ControlToValidate="txtRating" ValidationGroup="vgFeedback"
                                    CssClass="text-danger small" Display="Dynamic" ErrorMessage="Please enter a rating.">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvRating" runat="server"
                                    ControlToValidate="txtRating" ValidationGroup="vgFeedback"
                                    MinimumValue="1" MaximumValue="5" Type="Integer"
                                    CssClass="text-danger small" Display="Dynamic" ErrorMessage="Rating must be between 1 and 5.">*</asp:RangeValidator>
                            </div>
                            <div class="col-lg-7">
                                <label class="form-label fw-bold">Comment</label>
                                <asp:TextBox ID="txtFeedbackComment" runat="server" TextMode="MultiLine" Rows="3"
                                    CssClass="form-control" placeholder="Tell us about the order, delivery, or product quality."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFeedbackComment" runat="server"
                                    ControlToValidate="txtFeedbackComment" ValidationGroup="vgFeedback"
                                    CssClass="text-danger small" Display="Dynamic" ErrorMessage="Please enter a short comment.">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btnSubmitFeedback" runat="server" Text="Submit Feedback"
                                    CssClass="btn btn-primary" ValidationGroup="vgFeedback" OnClick="btnSubmitFeedback_Click" />
                                <asp:Label ID="lblFeedbackError" runat="server" CssClass="text-danger ms-2"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-md-4">
                <div class="card shadow-sm border-0 bg-light p-4">
                    <h5 class="fw-bold">Order Summary</h5>
                    <hr />
                    <p class="mb-1 text-muted">Order Date:</p>
                    <p class="fw-bold"><asp:Label ID="lblOrderDate" runat="server" /></p>
                    
                    <p class="mb-1 text-muted">Status:</p>
                    <p><asp:Label ID="lblStatus" runat="server" CssClass="badge p-2" /></p>

                    <div class="order-timeline my-4">
                        <div class='timeline-step <%# GetTrackingClass("Pending") %>'>
                            <span></span>
                            <strong>Pending</strong>
                        </div>
                        <div class='timeline-step <%# GetTrackingClass("Delivery") %>'>
                            <span></span>
                            <strong>Delivery</strong>
                        </div>
                        <div class='timeline-step <%# GetTrackingClass("Delivered") %>'>
                            <span></span>
                            <strong>Delivered</strong>
                        </div>
                    </div>
                    
                    <hr />
                    <div class="d-flex justify-content-between">
                        <span class="h5 fw-bold">Grand Total:</span>
                        <span class="h5 fw-bold text-primary"><asp:Label ID="lblGrandTotal" runat="server" /></span>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        (function () {
            function wireRatingPickers() {
                var pickers = document.querySelectorAll('[data-rating-picker]');
                Array.prototype.forEach.call(pickers, function (picker) {
                    var input = picker.querySelector('input[type="number"]');
                    var stars = Array.prototype.slice.call(picker.querySelectorAll('[data-rating-value]'));

                    function setRating(value) {
                        var rating = parseInt(value, 10);
                        if (isNaN(rating)) rating = 1;
                        rating = Math.max(1, Math.min(5, rating));
                        input.value = rating;
                        stars.forEach(function (star) {
                            var starValue = parseInt(star.getAttribute('data-rating-value'), 10);
                            star.classList.toggle('is-selected', starValue <= rating);
                        });
                    }

                    stars.forEach(function (star) {
                        star.addEventListener('click', function () {
                            setRating(star.getAttribute('data-rating-value'));
                        });
                    });

                    input.addEventListener('input', function () {
                        setRating(input.value);
                    });

                    input.addEventListener('blur', function () {
                        setRating(input.value);
                    });

                    setRating(input.value || 5);
                });
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', wireRatingPickers);
            } else {
                wireRatingPickers();
            }
        })();
    </script>
</asp:Content>
