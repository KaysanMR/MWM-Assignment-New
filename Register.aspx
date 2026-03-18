<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MWM_Assignment_New.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 mb-5">
    <div class="row justify-content-center">
        <div class="col-md-6 col-sm-12">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h2 class="text-center mb-4">Create Account</h2>
                    
                    <asp:ValidationSummary ID="vsRegister" runat="server" 
                        ValidationGroup="vgRegister" ForeColor="Red" CssClass="mb-3" />

                    <div class="mb-3">
                        <label class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                            ErrorMessage="Full Name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Choose a username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUsername"
                            ErrorMessage="Username is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="email@example.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Passwords do not match" ForeColor="Red" Display="Dynamic" ValidationGroup="vgRegister">*</asp:CompareValidator>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnRegister" runat="server" Text="Register Now" CssClass="btn btn-primary btn-lg" 
                            ValidationGroup="vgRegister" OnClick="btnRegister_Click" />
                    </div>

                    <div class="mt-3 text-center">
                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
