<%@ Page Language="C#" AutoEventWireup="true" CodeFile="groupedCountries.aspx.cs" MasterPageFile="~/template.master" Inherits="groupedCountries" %>

<asp:Content ID="Content1" ContentPlaceHolderId="headerContent" runat="server">
  <h1>Global Language use</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderId="personalContent" runat="server">
            <asp:Table runat="server" ID="Table1" CssClass="table">
                <asp:TableHeaderRow ID="TableHeaderRow1" runat="server">
                    <asp:TableHeaderCell ID="TableHeaderCell1" runat="server">Language</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell2" runat="server">Population Use</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell3" runat="server">Global Population % Use</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <div id="errorMessage" class="alert alert-danger" runat="server" style="display: none">
                <p>An error occured during connection to database.</p>
            </div>
</asp:Content>
