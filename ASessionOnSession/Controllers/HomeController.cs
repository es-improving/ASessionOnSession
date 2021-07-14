using ASessionOnSession.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASessionOnSession.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string _SINGLE_OBJECT_KEY = "SingleObjectInSession";
        private const string _LIST_OBJECT_KEY = "ListOfObjectsInSession";
        private const string _USERNAME_KEY = "Username";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var puppyStuff = HttpContext.Session.Get<List<MyPuppyAndPopsicleDream>>(_LIST_OBJECT_KEY);

            var vm = new IndexViewModel();
            vm.ListDreamCount = puppyStuff == null ? 0 : puppyStuff.Count;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(string yourname)
        {
            HttpContext.Session.SetString(_USERNAME_KEY, yourname);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PuppiesAndPopsicles(MyPuppyAndPopsicleDream myDream)
        {
            // This action is the post for populating a list of objects in session.
            var puppyStuff = HttpContext.Session.Get<List<MyPuppyAndPopsicleDream>>(_LIST_OBJECT_KEY);
            if (puppyStuff == null)
            {
                puppyStuff = new List<MyPuppyAndPopsicleDream>();
            }
            puppyStuff.Add(myDream);

            HttpContext.Session.Set(_LIST_OBJECT_KEY, puppyStuff);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SinglePuppyAndPopsicle(MyPuppyAndPopsicleDream myDream)
        {
            // This action is the post for populating a single object in session.
            HttpContext.Session.Set(_SINGLE_OBJECT_KEY, myDream);

            return RedirectToAction("Index");
        }

        public IActionResult AllTheSessionData()
        {
            var dreamList = HttpContext.Session.Get<List<MyPuppyAndPopsicleDream>>(_LIST_OBJECT_KEY);
            var singleDream = HttpContext.Session.Get<MyPuppyAndPopsicleDream>(_SINGLE_OBJECT_KEY);
            var username = HttpContext.Session.GetString(_USERNAME_KEY);

            var vm = new AllTheSessionDataViewModel
            {
                Dreams = dreamList,
                SingleObjectInSession = singleDream,
                UsernameInSession = username
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            ViewBag.YourName = HttpContext.Session.GetString(_USERNAME_KEY);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
