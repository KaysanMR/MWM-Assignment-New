<%@ Page Title="Manage Coupons" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCoupons.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageCoupons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active" aria-current="page">Manage Coupons</li>
            </ol>
        </nav>

        <h2 class="mb-4 text-center">Manage Coupons</h2>

        <div class="card coupon-form-card mb-5 shadow-sm border-primary">
            <div class="card-header bg-primary text-white">Add New Coupon</div>
            <div class="card-body">
                <div class="row g-3 align-items-end">
                    <div class="col-md-4 col-xl-2">
                        <label class="form-label fw-bold">Code</label>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" MaxLength="30" placeholder="TIN10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                            ErrorMessage="Coupon code is required." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CouponAdd" />
                    </div>
                    <div class="col-md-8 col-xl-4">
                        <label class="form-label fw-bold">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="120" placeholder="Pantry starter discount"></asp:TextBox>
                    </div>
                    <div class="col-md-4 col-xl-2">
                        <label class="form-label fw-bold">Type</label>
                        <asp:DropDownList ID="ddlDiscountType" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Percent">Percent</asp:ListItem>
                            <asp:ListItem Value="Fixed">Fixed RM</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 col-xl-2">
                        <label class="form-label fw-bold">Value</label>
                        <asp:TextBox ID="txtDiscountValue" runat="server" CssClass="form-control" placeholder="10.00"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDiscountValue" runat="server" ControlToValidate="txtDiscountValue"
                            ErrorMessage="Discount value is required." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CouponAdd" />
                        <asp:RangeValidator ID="rvDiscountValue" runat="server" ControlToValidate="txtDiscountValue"
                            MinimumValue="0.01" MaximumValue="1000.00" Type="Double"
                            ErrorMessage="Enter a value above 0." CssClass="text-danger small" Display="Dynamic" ValidationGroup="CouponAdd" />
                    </div>
                    <div class="col-md-4 col-xl-2">
                        <label class="coupon-active-toggle">
                            <input id="chkIsActive" runat="server" type="checkbox" class="coupon-active-input" checked="checked" />
                            <span class="coupon-active-box">
                                <i class="bi bi-check-lg"></i>
                            </span>
                            <span>Active</span>
                        </label>
                    </div>
                </div>
                <div class="coupon-form-actions mt-4 pt-2">
                    <asp:Button ID="btnAddCoupon" runat="server" Text="Add Coupon" CssClass="btn btn-primary px-4" OnClick="btnAddCoupon_Click" ValidationGroup="CouponAdd" />
                </div>
                <asp:Label ID="lblCouponAdminMessage" runat="server" CssClass="d-block mt-2"></asp:Label>
            </div>
        </div>

        <h3 class="mb-3">Active Coupon List</h3>
        <div class="table-responsive responsive-gridview">
            <asp:GridView ID="gvCoupons" runat="server" CssClass="table table-hover border"
                AutoGenerateColumns="False" DataKeyNames="CouponID"
                OnRowEditing="gvCoupons_RowEditing"
                OnRowUpdating="gvCoupons_RowUpdating"
                OnRowCancelingEdit="gvCoupons_RowCancelingEdit"
                OnRowDeleting="gvCoupons_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="CouponID" HeaderText="ID" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate><strong><%# Eval("Code") %></strong></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCode" runat="server" Text='<%# Bind("Code") %>' CssClass="form-control form-control-sm" MaxLength="30"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate><%# Eval("Description") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control form-control-sm" MaxLength="120"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%# Eval("DiscountType") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditDiscountType" runat="server" CssClass="form-select form-select-sm" SelectedValue='<%# Bind("DiscountType") %>'>
                                <asp:ListItem Value="Percent">Percent</asp:ListItem>
                                <asp:ListItem Value="Fixed">Fixed RM</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Value">
                        <ItemTemplate><%# Eval("DiscountValue", "{0:N2}") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditDiscountValue" runat="server" Text='<%# Bind("DiscountValue", "{0:N2}") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate><%# Convert.ToBoolean(Eval("IsActive")) ? "Yes" : "No" %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkEditIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                        ButtonType="Button"
                        CausesValidation="False"
                        ControlStyle-CssClass="btn btn-sm btn-outline-secondary m-1" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
