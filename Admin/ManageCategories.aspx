<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active" aria-current="page">Manage Users</li>
            </ol>
        </nav>

        <h2 class="mb-4 text-center">Manage Categories</h2>

        <div class="card mb-4 shadow-sm">
            <div class="card-header bg-success text-white">Add New Category</div>
            <div class="card-body">
                <div class="row g-3 align-items-center">
                    <div class="col-auto">
                        <label class="col-form-label">Category Name:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control" placeholder="e.g. Mechanical Switches"></asp:TextBox>
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" CssClass="btn btn-success" OnClick="btnAddCategory_Click" />
                    </div>
                </div>
                <asp:Label ID="lblCatMsg" runat="server" CssClass="d-block mt-2"></asp:Label>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvCategories" runat="server" CssClass="table table-hover border"
                AutoGenerateColumns="False" DataKeyNames="CategoryID"
                OnRowEditing="gvCategories_RowEditing"
                OnRowUpdating="gvCategories_RowUpdating"
                OnRowCancelingEdit="gvCategories_RowCancelingEdit"
                OnRowDeleting="gvCategories_RowDeleting">

                <Columns>
                    <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Category Name">
                        <ItemTemplate><%# Eval("CategoryName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCatName" runat="server" Text='<%# Bind("CategoryName") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                        ButtonType="Button"
                        ControlStyle-CssClass="btn btn-sm btn-outline-secondary m-1" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
