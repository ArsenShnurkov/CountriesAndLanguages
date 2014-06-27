using Npgsql;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class groupedCountries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TableHeaderRow1.TableSection = TableRowSection.TableHeader;

        bool success = getDatafromPostgresql();

        if (!success) {
            errorMessage.Style.Remove("display");
        }
    }

    protected bool getDatafromPostgresql() {
        string connectString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};SSL=True", PsqlSettings.HOST, PsqlSettings.PORT,
            PsqlSettings.USERNAME, PsqlSettings.PASSWORD, PsqlSettings.DATABASE);

        string query = @"SELECT language,FLOOR(SUM(percentage*population/100)) AS exactly, ROUND(CAST(SUM(percentage*population)/(SELECT SUM(population) FROM country) AS numeric),2) AS global
FROM countryLanguage,country WHERE countrycode=code GROUP BY language ORDER BY exactly DESC,language ASC";

        try {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(connectString);

            sqlConnection.Open();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, sqlConnection);
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable();
            dataset.Reset();
            adapter.Fill(dataset);
            datatable = dataset.Tables[0];
            foreach (DataRow datarow in datatable.Rows) {
                TableRow row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                foreach (object obj in datarow.ItemArray) {
                    TableCell cell = new TableCell();
                    cell.Text = obj.ToString();
                    row.Cells.Add(cell);
                }
                Table1.Rows.Add(row);
            }
            sqlConnection.Close();
        } catch (Exception msg) {
            System.Diagnostics.Debug.WriteLine(msg.ToString());
            return false;
        }

        return true;
    }
}