<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MWM_Assignment_New.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-4 col-sm-12">
                <div class="card shadow">
                    <div class="card-body">
                        <h2 class="text-center mb-4">Member Login</h2>

                        <div class="mb-3">
                            <label class="form-label">Username</label>
                            <asp:TextBox ID="txtLoginUser" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLoginUser" runat="server" ControlToValidate="txtLoginUser"
                                ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Password</label>
                            <asp:TextBox ID="txtLoginPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLoginPass" runat="server" ControlToValidate="txtLoginPass"
                                ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary"
                                ValidationGroup="vgLogin" OnClick="btnLogin_Click" />
                        </div>

                        <div class="mt-3 text-center">
                            <asp:Label ID="lblLoginMsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
