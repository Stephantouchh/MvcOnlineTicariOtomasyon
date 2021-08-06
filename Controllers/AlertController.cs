using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class AlertController : Controller
    {
       
        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }
    }
}