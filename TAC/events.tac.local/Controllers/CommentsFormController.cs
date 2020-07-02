using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class CommentsFormController : Controller
    {
        // GET: CommentsForm
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [TAC.Utils.Mvc.ValidateFormHandler]
        public ActionResult Index(string comment, string email)
        {
            return View("Confirmation");
        }
    }
}