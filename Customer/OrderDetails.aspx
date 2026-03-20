<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="MWM_Assignment_New.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="fw-bold">Order #<asp:Literal ID="litOrderID" runat="server" /></h2>
            <a href="MyOrders.aspx" class="btn btn-outline-secondary btn-sm">
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
            </div>

            <div class="col-md-4">
                <div class="card shadow-sm border-0 bg-light p-4">
                    <h5 class="fw-bold">Order Summary</h5>
                    <hr />
                    <p class="mb-1 text-muted">Order Date:</p>
                    <p class="fw-bold"><asp:Label ID="lblOrderDate" runat="server" /></p>
                    
                    <p class="mb-1 text-muted">Status:</p>
                    <p><asp:Label ID="lblStatus" runat="server" CssClass="badge p-2" /></p>
                    
                    <hr />
                    <div class="d-flex justify-content-between">
                        <span class="h5 fw-bold">Grand Total:</span>
                        <span class="h5 fw-bold text-primary"><asp:Label ID="lblGrandTotal" runat="server" /></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>