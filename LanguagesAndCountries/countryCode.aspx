<%@ Page Language="C#" AutoEventWireup="true" CodeFile="countryCode.aspx.cs" MasterPageFile="~/template.master" Inherits="countryCode" %>

<asp:Content ID="Content1" ContentPlaceHolderId="headerContent" runat="server">
  <h1 id="headerTitle" runat="server">Language use in :countryName</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderId="scriptsContent" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/d3.min.js") %>'></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderId="personalContent" runat="server">
    <div id="pieChart" style="padding-left: 50px;"></div>
    <form runat="server" id="dataFieldForm" clientidmode="Static">
        <input type="hidden" id="dataField" name="dataField" runat="server" clientidmode="Static" value="" />
    </form>
    <script type="text/javascript">
        function draw(data) {
            if (data === undefined) return;

            var w = 480,
               h = 480,
               r = 200,
               color = d3.scale.category20c();

            var total = d3.sum(data, function (d) {
                return d3.sum(d3.values(d));
            });

            var vis = d3.select("#pieChart")
                .append("svg:svg")
                .data([data])
                    .attr("width", w)
                    .attr("height", h)
                .append("svg:g")
                    .attr("transform", "translate(" + r * 1.1 + "," + r * 1.1 + ")")

            var arc = d3.svg.arc()
                .innerRadius(0)
                .outerRadius(r);

            var pie = d3.layout.pie()
                .value(function (d) { return d.value; });

            var arcs = vis.selectAll("g.slice")
                .data(pie)
                .enter()
                    .append("svg:g")
                        .attr("class", "slice")

            arcs.append("svg:path")
                .attr("fill", function (d, i) { return color(i); })
                .attr("d", arc);

            var legend = d3.select("#pieChart").append("svg")
              .attr("class", "legend")
              .attr("width", 1.5*r)
              .attr("height", 2*r)
              .selectAll("g")
              .data(data)
              .enter().append("g")
              .attr("transform", function (d, i) { return "translate(0," + i * 20 + ")"; });

            legend.append("rect")
              .attr("width", 18)
              .attr("height", 18)
              .style("fill", function (d, i) { return color(i); });

            legend.append("text")
              .attr("x", 24)
              .attr("y", 9)
              .attr("dy", ".35em")
              .text(function (d) { return d.label+" ("+d.value+"%)"; });
        }

        draw(<%= dataField.Value %>);
    </script>
    <div id="errorMessage" class="alert alert-danger" runat="server" style="display: none">
        <p>An error occured during connection to database.</p>
    </div>
    <div id="notfoundMessage" class="alert alert-danger" runat="server" style="display: none">
        <p>Country not found.</p>
    </div>
    <div id="invalidcodeMessage" class="alert alert-danger" runat="server" style="display: none">
        <p>Invalid country code. Must be 3 letters.</p>
    </div>
</asp:Content>

