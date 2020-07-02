using events.tac.local.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class RelatedEventsController : Controller
    {
        // GET: RelatedEvents
        public ActionResult Index()
        {
            var item = RenderingContext.Current.ContextItem;
            MultilistField relatedEvents = item.Fields["RelatedEvents"];
            var events = relatedEvents.GetItems()
            .Select(i => new NavigationItem()
            {
                Title = i.DisplayName,
                URL = LinkManager.GetItemUrl(i)
            });
            return View(events);
        }
    }
}