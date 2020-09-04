using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class Help : Controller
    {

        public SqlConnection db { get; set; }

        public Help()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NewsDapper"].ConnectionString;

            db = new SqlConnection(connectionString);
        }

    }
}