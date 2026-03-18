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
                <asp:BoundField DataField="DateSubmitted" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Name" HeaderText="Customer" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" />

                <asp:TemplateField HeaderText="Message">
                    <ItemTemplate>
                        <div style="max-width: 300px; max-height: 80px; overflow-y: auto; font-size: 0.9rem;">
                            <%# Eval("Message") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowDeleteButton="True" ButtonType="Button"
                    DeleteText="Archive" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
