using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLoginAutenticar.Controllers
{
    public class AdministrativoController : Controller
    {
        // GET: Administrativo

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}