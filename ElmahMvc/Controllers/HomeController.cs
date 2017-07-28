using log4net;
using Rabbit.SimpleInjectorDemo.Services;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace ElmahMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HomeController));

        public HomeController(IListingService listingService)
        {
            _listingService = listingService;
        }

        public ActionResult Index()
        {
            var randomNumber = new Random().Next(0, 2);
            if (randomNumber == 1)
            {
                throw new Exception("A sample error " + randomNumber + " generated at " + DateTime.Now);
            }

            Logger.DebugFormat("Entered at {0}", DateTime.Now);

            ViewBag.ListingCount = _listingService.Count();

            var versionInfo = FileVersionInfo.GetVersionInfo(typeof(HomeController).Assembly.Location);

            return View(versionInfo);
        }

    }
}
