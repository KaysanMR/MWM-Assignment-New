<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="MWM_Assignment_New.MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="fw-bold">Purchase History</h2>
            <a href="Products.aspx" class="btn btn-outline-primary shadow-sm">
                <i class="bi bi-plus-lg"></i> New Order
            </a>
        </div>

        <div class="card shadow-sm border-0">
            <div class="card-body p-0">
                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-hover mb-0" GridLines="None" 
                    OnRowCommand="gvOrders_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="Order #" ItemStyle-CssClass="fw-bold text-primary" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total" DataFormatString="RM {0:N2}" />
                        
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='<%# GetStatusClass(Eval("Status").ToString()) %>'>
                                    <%# Eval("Status") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnViewDetails" runat="server" 
                                    CommandName="ViewDetails" 
                                    CommandArgument='<%# Eval("OrderID") %>' 
                                    CssClass="btn btn-sm btn-light border">
                                    View Details
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center py-5">
                            <i class="bi bi-bag-x text-muted" style="font-size: 3rem;"></i>
                            <p class="mt-3 text-muted">You haven't placed any orders yet.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>