<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageFeedback.aspx.cs" Inherits="MWM_Assignment_New.Admin.ManageFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-shell">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Dashboard.aspx">Admin Dashboard</a></li>
                <li class="breadcrumb-item active">Customer Feedback</li>
            </ol>
        </nav>

        <h2 class="mb-4">Inquiries & Feedback</h2>

        <div class="card mb-4 shadow-sm">
            <div class="card-header">Order Ratings & Reviews</div>
            <div class="card-body p-0 table-responsive responsive-gridview">
                <asp:GridView ID="gvFeedback" runat="server" CssClass="table table-hover border shadow-sm mb-0"
                    AutoGenerateColumns="False" DataKeyNames="FeedbackID" OnRowDeleting="gvFeedback_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="DateSubmitted" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FullName" HeaderText="Customer" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order #" />
                        <asp:TemplateField HeaderText="Rating">
                            <ItemTemplate>
                                <span class="text-warning"><%# ShowStars(Eval("Rating")) %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comment" HeaderText="Comment" />
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Remove"
                            ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="card shadow-sm">
            <div class="card-header">Visitor Contact Messages</div>
            <div class="card-body p-0 table-responsive responsive-gridview">
                <asp:GridView ID="gvContactMessages" runat="server" CssClass="table table-hover border shadow-sm mb-0"
                    AutoGenerateColumns="False" DataKeyNames="ContactMessageID" OnRowDeleting="gvContactMessages_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="DateSubmitted" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Message" HeaderText="Message" />
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Remove"
                            ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
