<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Manage Users</li>
                </ol>
            </nav>
            <h2>User Management</h2>
            <asp:Label ID="lblStatus" runat="server" CssClass="badge p-2"></asp:Label>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped table-hover border shadow-sm"
                AutoGenerateColumns="False" DataKeyNames="UserID"
                OnRowEditing="gvUsers_RowEditing"
                OnRowUpdating="gvUsers_RowUpdating"
                OnRowCancelingEdit="gvUsers_RowCancelingEdit"
                OnRowDeleting="gvUsers_RowDeleting">

                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Full Name">
                        <ItemTemplate><%# Eval("FullName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("FullName") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate><%# Eval("Email") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <span class='badge <%# Eval("Role").ToString() == "Admin" ? "bg-danger" : "bg-info" %>'>
                                <%# Eval("Role") %>
                        </span>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRole" runat="server" SelectedValue='<%# Bind("Role") %>' CssClass="form-select form-select-sm">
                                <asp:ListItem>Member</asp:ListItem>
                                <asp:ListItem>Admin</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                        ButtonType="Button"
                        ControlStyle-CssClass="btn btn-sm btn-outline-primary m-1" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
