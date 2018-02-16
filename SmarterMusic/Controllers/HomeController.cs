using SmarterMusic.Models;
using SmarterMusic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmarterMusic.Controllers
{
    public class HomeController : Controller
    {
        private MusicContext context = new MusicContext();
        public ActionResult Index(int? category,int? name)
        {
            if (context.Music.ToList().Count() >= 1)
            {
                ViewBag.first = context.Music.First();
                var items = context.Music.OrderBy(c => c.ID).Skip(1).ToList();
                //order by categories
                if (category != null)
                {
                    items = context.Music.OrderBy(c => c.Category).ToList();
                    ViewBag.first = items.First();
                    items = context.Music.OrderBy(c => c.Category).Skip(1).ToList();

                }
                //order by song name
                if (name != null)
                {
                    items = context.Music.OrderBy(c => c.Name).ToList();
                    ViewBag.first = items.First();
                    items = context.Music.OrderBy(c => c.Name).Skip(1).ToList();

                }
                return View(items);
            }
            //if there is no music go to AddMusic
            else
            {
                return RedirectToAction("AddMusic");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddMusic()
        {
            MusicViewModel mvm = new MusicViewModel();
            return View(mvm);
        }
        [HttpPost]
        public ActionResult Create(MusicViewModel mvm, HttpPostedFileBase main)
        {
            MusicEntity entity = new MusicEntity();
            entity.Name = mvm.Name;
            entity.Category = mvm.Category;


            var fileNamee = Path.GetFileName(main.FileName);
            var name_without_extt = Path.GetFileNameWithoutExtension(main.FileName);
            var extt = Path.GetExtension(main.FileName);
            entity.MusicURL = name_without_extt + DateTime.Now.ToString("MMddyyHmmss") + extt;
            var pathh = Path.Combine(Server.MapPath("~/Songs/"), entity.MusicURL);
            main.SaveAs(pathh);

            context.Music.Add(entity);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int ID)
        {
            var item = context.Music.Find(ID);

            string fullPath = Request.MapPath("~/Songs/" + item.MusicURL);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            context.Music.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}