using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

public partial class countryCode : System.Web.UI.Page
{
    private const int NOTFOUND = 0;
    private const int ERROR = 1;
    private const int OK = 2;

    protected class CountryLanguage {
        public string label { get; set; }
        public float value { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string countryCode = Page.RouteData.Values["countryCode"] + "";
        if (countryCode.Length == 3 && countryCode.All(Char.IsLetter)) {
            int status = getDatafromPostgresql(countryCode);

            if (status == NOTFOUND) {
                dataField.Value = "";
                notfoundMessage.Style.Remove("display");
            } else if (status == ERROR) {
                dataField.Value = "";
                errorMessage.Style.Remove("display");
            }
        } else {
            dataField.Value = "";
            invalidcodeMessage.Style.Remove("display");
        }
    }

    protected int getDatafromPostgresql(string countryCode) {
        string connectString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};SSL=True", PsqlSettings.HOST, PsqlSettings.PORT,
            PsqlSettings.USERNAME, PsqlSettings.PASSWORD, PsqlSettings.DATABASE);

        string query_name = String.Format("SELECT name FROM country WHERE code = :countryCode");
        string query_languages = String.Format("SELECT language,percentage FROM countryLanguage WHERE countrycode = :countryCode");

        try {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(connectString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand(query_name, sqlConnection);
            command.Parameters.Add(new NpgsqlParameter("countryCode", NpgsqlDbType.Text));
            command.Parameters[0].Value = countryCode;

            NpgsqlDataReader datareader = command.ExecuteReader();
            System.Diagnostics.Debug.WriteLine(datareader.FieldCount);
            if (!datareader.HasRows) {
                // country not found
                sqlConnection.Close();
                return 0;
            }
            while (datareader.Read()) {
                headerTitle.InnerText = "Language use in :" + datareader[0].ToString();
            }

            command = new NpgsqlCommand(query_languages, sqlConnection);
            command.Parameters.Add(new NpgsqlParameter("countryCode", NpgsqlDbType.Text));
            command.Parameters[0].Value = countryCode;

            datareader = command.ExecuteReader();
            float full = 0f;
            List<CountryLanguage> languages = new List<CountryLanguage>();
            while (datareader.Read()) {
                string labelString = datareader[0].ToString();
                float percentage = float.Parse(datareader[1].ToString());
                full += percentage;
                CountryLanguage language = new CountryLanguage { label = labelString, value = percentage };
                languages.Add(language);
            }

            if (full < 99.9f) {
                CountryLanguage language = new CountryLanguage { label = "N/A", value = (float)Math.Round(100f - full, 2) };
                languages.Add(language);
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dataField.Value = serializer.Serialize(languages);

            sqlConnection.Close();
        } catch (Exception msg) {
            System.Diagnostics.Debug.WriteLine(msg.ToString());
            return 1;
        }

        return 2;
    }
}