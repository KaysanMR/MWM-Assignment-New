<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active">Manage Products</li>
            </ol>
        </nav>

        <div class="card mb-5 shadow-sm border-primary">
            <div class="card-header bg-primary text-white">Add New Keyboard</div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Product Name</label>
                        <asp:TextBox ID="txtProdName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtProdName"
                            ErrorMessage="Product name is required" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="form-label">Category</label>
                        <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCat" runat="server" ControlToValidate="ddlCategories"
                            InitialValue="0" ErrorMessage="Please select a category" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="col-md-4 mb-3">
                        <label class="form-label">Price (RM)</label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice"
                            ErrorMessage="Price required" Display="Dynamic" CssClass="text-danger small" />
                        <asp:RangeValidator ID="rvPrice" runat="server" ControlToValidate="txtPrice"
                            MinimumValue="1.00" MaximumValue="10000.00" Type="Double"
                            ErrorMessage="Price must be between 1 and 10,000" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="col-md-4 mb-3">
                        <label class="form-label">Stock Quantity</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CompareValidator ID="cvStock" runat="server" ControlToValidate="txtStock"
                            Operator="DataTypeCheck" Type="Integer"
                            ErrorMessage="Must be a whole number" Display="Dynamic" CssClass="text-danger small" />
                    </div>

                    <div class="col-md-4 mb-3">
                        <label class="form-label">Product Image</label>
                        <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="fuProductImage"
                            ErrorMessage="Please upload an image" Display="Dynamic" CssClass="text-danger small" />
                    </div>
                    <div class="col-12 mb-3">
                        <label class="form-label">Description</label>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="btnSaveProduct" runat="server" Text="Add to Inventory" CssClass="btn btn-primary px-4" OnClick="btnSaveProduct_Click" />
                <asp:Label ID="lblUploadMsg" runat="server" CssClass="ms-3"></asp:Label>
            </div>
        </div>

        <h3 class="mb-3">Current Inventory</h3>
        <div class="table-responsive">
            <asp:GridView ID="gvProducts" runat="server" CssClass="table table-hover border"
                AutoGenerateColumns="False" DataKeyNames="ProductID"
                OnRowEditing="gvProducts_RowEditing"
                OnRowDeleting="gvProducts_RowDeleting"
                OnRowUpdating="gvProducts_RowUpdating"
                OnRowCancelingEdit="gvProducts_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID" ReadOnly="True" />

                    <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate><%# Eval("ProductName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("ProductName") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate><%# Eval("CategoryName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEditCat" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
                            <asp:HiddenField ID="hfCatID" runat="server" Value='<%# Eval("CategoryID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Price (RM)">
                        <ItemTemplate><%# Eval("Price", "{0:N2}") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPrice" runat="server" Text='<%# Bind("Price") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stock">
                        <ItemTemplate><%# Eval("StockQuantity") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditStock" runat="server" Text='<%# Bind("StockQuantity") %>' CssClass="form-control form-control-sm" TextMode="Number"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                        ControlStyle-CssClass="btn btn-sm btn-outline-secondary m-1" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
