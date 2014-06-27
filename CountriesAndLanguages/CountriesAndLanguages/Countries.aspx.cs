using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CountriesAndLanguages {
    public partial class Countries : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            TableHeaderRow1.TableSection = TableRowSection.TableHeader;

            bool success = getDatafromPostgresql();

            if (!success) {
                errorMessage.Style.Remove("display");
            }
        }

        protected bool getDatafromPostgresql() {
            string connectString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};SSL=True", PsqlSettings.HOST, PsqlSettings.PORT,
                PsqlSettings.USERNAME, PsqlSettings.PASSWORD, PsqlSettings.DATABASE);

            string query = @"SELECT continent,name,language,population,percentage,FLOOR(population*percentage/100) AS exactly,countrycode
FROM country,countryLanguage WHERE code=countrycode AND isofficial='t' ORDER BY continent ASC,name ASC,percentage DESC;";

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
                    object[] datarowArray = datarow.ItemArray;
                    for (int i = 0; i < datarowArray.Length; i++) {
                        TableCell cell = new TableCell();
                        if (i == datarowArray.Length - 1) {
                            HyperLink link = new HyperLink();
                            link.Text = "Details";
                            link.NavigateUrl = "/country/:" + datarowArray[i].ToString();
                            cell.Controls.Add(link);
                        } else {
                            if (i == 4) {
                                float p = float.Parse(datarowArray[i].ToString());
                                cell.Text = p.ToString("0.00");
                            } else {
                                cell.Text = datarowArray[i].ToString();
                            }
                        }
                        row.Cells.Add(cell);
                    }
                    Table1.Rows.Add(row);
                }
                sqlConnection.Close();
            } catch (Exception msg) {
        	//errorMessage.InnerText=msg.ToString();
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                return false;
            }

            return true;
        }
    }
}