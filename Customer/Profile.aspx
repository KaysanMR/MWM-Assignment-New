<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MWM_Assignment_New.Customer.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm border-0 pt-3">
                    <div class="card-body p-4">
                        <div class="text-center mb-4">
                            <div class="bg-primary bg-opacity-10 rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style="width: 80px; height: 80px;">
                                <i class="bi bi-person-badge text-primary fs-1"></i>
                            </div>
                            <h3 class="fw-bold">My Profile</h3>
                            <p class="text-muted small">Manage your account details and shipping info.</p>
                        </div>

                        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-success small mb-4">
                            <i class="bi bi-check-circle-fill me-2"></i>Profile updated successfully!
                        </asp:Panel>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Full Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3" ReadOnly="true"></asp:TextBox>
                            <div class="form-text small">Email cannot be changed for security.</div>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-muted">Phone Number</label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                        </div>

                        <div class="mb-4">
                            <label class="form-label small fw-bold text-muted">Shipping Address</label>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control border-0 bg-light py-2 px-3 rounded-3"></asp:TextBox>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" OnClick="btnUpdate_Click" 
                                CssClass="btn btn-primary btn-lg rounded-pill shadow-sm py-2 fs-6" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
