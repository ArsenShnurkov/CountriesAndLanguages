<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Countries.aspx.cs" Inherits="CountriesAndLanguages.Countries" %>
<asp:Content ID="Content1" ContentPlaceHolderId="headerContent" runat="server">
  <h1>Official languages of countries</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderId="personalContent" runat="server">
           <asp:Table runat="server" ID="Table1" CssClass="table">
                <asp:TableHeaderRow ID="TableHeaderRow1" runat="server">
                    <asp:TableHeaderCell ID="TableHeaderCell1" runat="server">Continent</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell2" runat="server">Country</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell3" runat="server">Language</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell4" runat="server">Population</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell5" runat="server">Official Language Use %</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell6" runat="server">Official Language Population Use</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="TableHeaderCell7" runat="server">&nbsp;</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>

            <div id="errorMessage" class="alert alert-danger" runat="server" style="display: none">
                <p>An error occured during connection to database.</p>
            </div>
</asp:Content>
