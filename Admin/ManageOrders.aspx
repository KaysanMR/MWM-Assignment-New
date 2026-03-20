<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active">Order Management</li>
            </ol>
        </nav>

        <h2 class="mb-4">Customer Orders</h2>

        <div class="table-responsive">
            <asp:GridView ID="gvOrders" runat="server" CssClass="table table-hover border shadow-sm"
                AutoGenerateColumns="False" DataKeyNames="OrderID"
                OnRowUpdating="gvOrders_RowUpdating" OnRowEditing="gvOrders_RowEditing"
                OnRowCancelingEdit="gvOrders_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" ReadOnly="True" />

                    <asp:BoundField DataField="FullName" HeaderText="Customer" ReadOnly="True" />

                    <asp:BoundField DataField="TotalAmount" HeaderText="Total (RM)" DataFormatString="{0:N2}" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='badge <%# GetStatusClass(Eval("Status").ToString()) %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select form-select-sm" SelectedValue='<%# Bind("Status") %>'>
                                <asp:ListItem>Pending</asp:ListItem>
                                <asp:ListItem>Processing</asp:ListItem>
                                <asp:ListItem>Shipped</asp:ListItem>
                                <asp:ListItem>Completed</asp:ListItem>
                                <asp:ListItem>Cancelled</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ButtonType="Button"
                        ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
