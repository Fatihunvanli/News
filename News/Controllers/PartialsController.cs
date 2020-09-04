using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class PartialsController : Help
    {
        // GET: Partials
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Footers()
        {
            var categoriesCount = db.Query("Select c.NAME_,Count(*) CATEGORYCOUNT from CATEGORIES c inner join NEWS n on c.ID = n.CATEGORYID group by c.NAME_");

            var popularNews = db.Query("SELECT TOP 3 n.TITLE,Convert(varchar,n.CREATEDATE,103) CREATEDATE,ni.SMALLIMAGE SMALLIMAGE " +
                "FROM NEWS n inner join NEWSIMAGES ni on ni.NEWSID = n.ID order by VIEWS_ desc");
            
            ViewBag.categoriesCount = categoriesCount;
            ViewBag.popularNews = popularNews;


            return PartialView("Footers");
        }
    }
}