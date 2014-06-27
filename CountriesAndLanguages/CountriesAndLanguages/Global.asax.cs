using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CountriesAndLanguages {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            RegisterRoutes(RouteTable.Routes);
        }

        void RegisterRoutes(RouteCollection routes) {
            routes.MapPageRoute(
               "Countries",
               "countries",
               "~/Countries.aspx"
            );

            routes.MapPageRoute(
               "Language by Numbers",
               "groupedCountries",
               "~/groupedCountries.aspx"
            );

            routes.MapPageRoute(
               "Country Code",
               "country/:{countryCode}",
               "~/countryCode.aspx"
            );
        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}