
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using teste.Data;
using teste.Models.Entities;
using teste.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace teste.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dc;
        public HomeController(DataContext context)
        {
            _dc = context;
        }

        //--------- Pages -------

        //Page Inicial Lista
        public async Task<IActionResult> Index()
        {
            List<Contacts> contacts = await _dc.contacts.ToListAsync();
            return View(contacts);
        }

        //Page Create
        public IActionResult Cadastrar()
        {
            return View();
        }

        //Page Detalhes
        public IActionResult Details(int? id)
        {
            if(id == null) return RedirectToAction("Index");
            Contacts contacts = _dc.contacts.Find(id);
            return View();
        }

        //Page Alterar
        public IActionResult Edit(int? id)
        {
            if(id == null) return RedirectToAction("Index");
            Contacts contacts = _dc.contacts.Find(id);
            return View(contacts);
        }

        //Page Delete
        public IActionResult Delete(int? id)        
        {
            if(id == null) return RedirectToAction("Index");
            return View();
        }

        //------- Metodos -----------

        //Metodo Cadastrar
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Contacts c)
        {
            if(!ModelState.IsValid) return View("Cadastrar"); //Caso não valide retorna
            
            _dc.contacts.Add(c);
            await _dc.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Metodo Alterar
        [HttpPut]
        public async Task<IActionResult> Edit(int id, Contacts c)
        {
            if(!ModelState.IsValid) return View("Edit"); //Caso não valide retorna
            
            _dc.Entry(c).State = EntityState.Modified;
            await _dc.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Metodo Remover
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, Contacts c)
        {
            _dc.contacts.Remove(c);
            await _dc.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
