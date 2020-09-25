using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public class HomeController : Help
    {
        // GET: Home 
        public ActionResult Index()
        {
            //Eğer sitede yavaşlık olursa tüm haberleri newsten çek burada linq sorgusu at.
            //var news = db.Query("select * from NEWS"); 

            var tags = db.Query("select NAME_ tagname, ID tagid from TAGS ");

            var categories = db.Query("select * from CATEGORIES");

            var google = db.Query("SELECT TOP 4 n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE, n.READTIME, n.VIEWS_, ni.SMALLIMAGE, t.NAME_, c.NAME_ CategoryName " +
                "FROM NEWS n inner join NEWSIMAGES ni on n.ID = ni.NEWSID " +
                "inner join NEWSTAGS nt on n.ID = nt.NEWSID " +
                "inner join TAGS t on nt.TAGSID = t.ID " +
                "inner join CATEGORIES c on c.ID = n.CATEGORYID " +
                "WHERE CATEGORYID = 1 ORDER BY CREATEDATE DESC").ToList();

            var microsoft = db.Query("SELECT TOP 4 n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE,n.READTIME,n.VIEWS_,ni.SMALLIMAGE,t.NAME_, c.NAME_ CategoryName " +
                "FROM NEWS n " +
                "inner join NEWSIMAGES ni on n.ID = ni.NEWSID " +
                "inner join NEWSTAGS nt on n.ID = nt.NEWSID " +
                "inner join TAGS t on nt.TAGSID = t.ID " +
                "inner join CATEGORIES c on c.ID = n.CATEGORYID " +
                "WHERE CATEGORYID = 2 ORDER BY CREATEDATE DESC").ToList();

            var apple = db.Query("SELECT TOP 4 n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE,n.READTIME,n.VIEWS_,ni.SMALLIMAGE,t.NAME_, c.NAME_ CategoryName " +
                "FROM NEWS n " +
                "inner join NEWSIMAGES ni on n.ID = ni.NEWSID " +
                "inner join NEWSTAGS nt on n.ID = nt.NEWSID " +
                "inner join TAGS t on nt.TAGSID = t.ID " +
                "inner join CATEGORIES c on c.ID = n.CATEGORYID " +
                "WHERE CATEGORYID = 3 ORDER BY CREATEDATE DESC").ToList();

            var news = db.Query("SELECT TOP 12 n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE,n.READTIME,n.VIEWS_,ni.SMALLIMAGE SMALLIMAGE,nf.NAME_ , c.NAME_ CategoryName " +
                "FROM NEWS n " +
                "inner join NEWSIMAGES ni on n.ID = ni.NEWSID " +
                "inner join NEWSTAGS nt on n.ID = nt.NEWSID " +
                "inner join TAGS nf on nt.TAGSID = nf.ID " +
                "inner join CATEGORIES c on c.ID = n.CATEGORYID " +
                "ORDER BY n.CREATEDATE DESC").ToList();



            ViewBag.popularNews = news.OrderByDescending(i => i.VIEWS_).Take(5);
            ViewBag.news = news.Take(6);
            ViewBag.tags = tags;
            ViewBag.categories = categories;
            ViewBag.google = google;
            ViewBag.microsoft = microsoft;
            ViewBag.apple = apple;
            ViewBag.sliderNews = news;


            return View();
        }


        public ActionResult NewsDetail()
        {
            //Arşiv sorgusu
            //SELECT DATEPART(YEAR,CREATEDATE) as Year_, DATEPART(MONTH,CREATEDATE) as Month_, count(*) NewsCount FROM NEWS
            //group by DATEPART(YEAR, CREATEDATE), DATEPART(MONTH, CREATEDATE)
            //order by Year_ desc

            var newsid = RouteData.Values["newsid"];

            if (newsid == string.Empty || newsid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categories = db.Query("Select c.NAME_ from CATEGORIES c inner join NEWS n on c.ID = n.CATEGORYID group by c.NAME_");
            var tags = db.Query("select NAME_ tagname, ID tagid from TAGS ");

            var news = db.Query("SELECT n.ID,n.TITLE,n.CONTENT_,c.NAME_ as CATEGORYNAME,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE,n.READTIME,n.VIEWS_,ni.SMALLIMAGE SMALLIMAGE,nf.ID id, nf.NAME_ FROM NEWS n inner join NEWSIMAGES ni on n.ID = ni.NEWSID inner join NEWSTAGS nt on n.ID = nt.NEWSID inner join TAGS nf on nt.TAGSID = nf.ID inner join CATEGORIES c on c.ID = n.CATEGORYID where n.ID = '" + newsid + "' ").FirstOrDefault();

            var popularNews = db.Query("SELECT TOP 3 n.ID,n.TITLE,Convert(varchar,n.CREATEDATE,103) CREATEDATE,ni.SMALLIMAGE SMALLIMAGE,c.NAME_ categoryname FROM NEWS n " +
                "inner join NEWSIMAGES ni on ni.NEWSID = n.ID inner join CATEGORIES c on n.CATEGORYID = c.ID order by VIEWS_ desc");


            ViewBag.popularNews = popularNews;
            ViewBag.categories = categories;
            ViewBag.news = news;
            ViewBag.tags = tags;

            return View();
        }


        public ActionResult NewsCategoryList()
        {
            var categoryname = RouteData.Values["categoryname"];

            if (categoryname == string.Empty || categoryname == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categories = db.Query("SELECT n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE, n.READTIME, n.VIEWS_, ni.SMALLIMAGE, t.NAME_, c.NAME_ categoryname " +
                "FROM NEWS n inner join NEWSIMAGES ni on n.ID = ni.NEWSID " +
                "inner join NEWSTAGS nt on n.ID = nt.NEWSID " +
                "inner join TAGS t on nt.TAGSID = t.ID " +
                "inner join CATEGORIES c on c.ID = n.CATEGORYID " +
                "WHERE c.NAME_ ='" + categoryname + "'  ORDER BY CREATEDATE DESC").ToList();

            var popularNews = db.Query("SELECT TOP 5 n.ID,n.TITLE,Convert(varchar,n.CREATEDATE,103) CREATEDATE,ni.SMALLIMAGE SMALLIMAGE,c.NAME_ categoryname FROM NEWS n " +
                "inner join NEWSIMAGES ni on ni.NEWSID = n.ID inner join CATEGORIES c on n.CATEGORYID = c.ID order by VIEWS_ desc");

            var tags = db.Query("select NAME_ tagname, ID tagid from TAGS ");

            ViewBag.categories = categories;
            ViewBag.categoryName = categoryname;
            ViewBag.popularNews = popularNews;
            ViewBag.tags = tags;

            return View();
        }

        public ActionResult NewsTagList()
        {
            var tagname = RouteData.Values["tagname"];
            var tagid = RouteData.Values["tagid"];

            if (tagname == string.Empty || tagname == null || tagid == string.Empty || tagid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tagNews = db.Query("SELECT n.ID,n.TITLE,n.AUTHOR,Convert(varchar,n.CREATEDATE,103) CREATEDATE, n.READTIME, n.VIEWS_, ni.SMALLIMAGE, t.NAME_ tagname, t.ID tagid, c.NAME_ categoryname " +
                "FROM NEWSTAGS nt " +
                "inner join NEWS n on nt.NEWSID = n.ID " +
                "inner join TAGS t on nt.TAGSID = t.ID " +
                "inner join NEWSIMAGES ni on ni.NEWSID = n.ID " +
                "inner join CATEGORIES c on n.CATEGORYID = c.ID " +
                "WHERE t.ID = '"+tagid+"'  ORDER BY CREATEDATE DESC ").ToList();

            var popularNews = db.Query("SELECT TOP 5 n.ID,n.TITLE,Convert(varchar,n.CREATEDATE,103) CREATEDATE,ni.SMALLIMAGE SMALLIMAGE,c.NAME_ categoryname FROM NEWS n " +
               "inner join NEWSIMAGES ni on ni.NEWSID = n.ID inner join CATEGORIES c on n.CATEGORYID = c.ID order by VIEWS_ desc");

            var tags = db.Query("select NAME_ tagname, ID tagid from TAGS ");

            ViewBag.tagnews = tagNews ;
            ViewBag.tagname = tagname;
            ViewBag.popularNews = popularNews;
            ViewBag.tags = tags;

            return View();
        }

    }
}