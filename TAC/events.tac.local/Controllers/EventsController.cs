using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;
            string message = null;
            using (var reader = new StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            Database database = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentItem = database.GetItem(parentPath);
            TemplateID templateID = new TemplateID(new ID("{3042488E-40F7-4CB5-A4A9-9913C8E72896}"));
            using (new SecurityDisabler())
            {
                foreach (var ev in events)
                {
                    string name = ItemUtil.ProposeValidItemName(ev.Name);
                    Item item = parentItem.Add(name, templateID);
                    item.Editing.BeginEdit();
                    
                    item["Name"] = ev.Name;
                    item["ContentHeading"] = ev.ContentHeading;
                    item["ContentIntro"] = ev.ContentIntro;
                    item["Highlights"] = ev.Highlights;
                    item["StartDate"] = Sitecore.DateUtil.ToIsoDate(ev.StartDate);
                    item["Duration"] = ev.Duration.ToString();
                    item["Difficulty"] = ev.Difficulty.ToString();

                    item.Editing.EndEdit();
                }                
            }

            return View("Success");
        }
    }
}