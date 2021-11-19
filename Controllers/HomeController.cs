using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using teste.Data;
using teste.Models;

namespace teste.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dc;
        public HomeController(DataContext context)
        {
            _dc = context;
        }        

        public IActionResult Index()
        {
            List<Contacts> contacts = _dc.contacts.ToList();
            return View(contacts);
        }

    }
}
