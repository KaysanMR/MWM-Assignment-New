<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageFeedback.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active">Customer Feedback</li>
            </ol>
        </nav>

        <h2 class="mb-4">Inquiries & Feedback</h2>

        <asp:GridView ID="gvFeedback" runat="server" CssClass="table table-hover border shadow-sm"
            AutoGenerateColumns="False" DataKeyNames="FeedbackID" OnRowDeleting="gvFeedback_RowDeleting">
            <Columns>
                <asp:BoundField DataField="DateSubmitted" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />

                <asp:BoundField DataField="FullName" HeaderText="Customer" />
                <asp:BoundField DataField="OrderID" HeaderText="Order #" />

                <asp:TemplateField HeaderText="Rating">
                    <ItemTemplate>
                        <span class="text-warning">
                            <%# ShowStars(Eval("Rating")) %>
                </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Comment" HeaderText="Comment" />

                <asp:CommandField ShowDeleteButton="True" DeleteText="Remove"
                    ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
