using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using teste.Data;
using teste.Models.Entities;
using teste.Models.Repository;

namespace teste.Controllers
{
    public class HomeController : Controller, IContactsRepository
    {
        private readonly DataContext _dc;
        public HomeController(DataContext context)
        {
            _dc = context;
        }

        public List<Contacts> GetAll()
        {
            List<Contacts> contacts = _dc.contacts.ToList();
            return contacts;
        }

        public IActionResult Index()
        {
            return View(GetAll());
        }

    }
}
