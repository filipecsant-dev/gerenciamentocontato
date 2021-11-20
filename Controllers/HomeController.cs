
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
        public async Task<IActionResult> Index(string search)
        {
            //Metodo de Buscar
            if(!string.IsNullOrEmpty(search)) //Se tiver parametro de busca
            {
                //Exibe a lista de contatos filtrados
                List<Contacts> c = await _dc.contacts
                                      .Where(x => x.Nome.ToUpper().Contains(search.ToUpper()))
                                      .AsNoTracking()
                                      .ToListAsync();
                return View(c);
            }
            else //Se não tiver parametro de busca
            {
                //Exibe a lista de contatos
                List<Contacts> c = await _dc.contacts.ToListAsync();
                return View(c);
            }
            
            
        }

        //Page Create
        public IActionResult Cadastrar()
        {
            return View();
        }

        //Page Detalhes
        public IActionResult Details(int? id)
        {
            if(id == null) return RedirectToAction("Index");//Se não tiver o parametro Id retona para Index

            Contacts c = _dc.contacts.Where(x => x.Id == id)
                                     .Include(x => x.Phone)
                                     .AsNoTracking()
                                     .FirstOrDefault();
            return View(c);
        }

        //Page Alterar
        public IActionResult Edit(int? id)
        {
            if(id == null) return RedirectToAction("Index");//Se não tiver o parametro Id retona para Index

            Contacts c = _dc.contacts.Find(id);
            return View(c);
        }

        //Page Delete
        public IActionResult Delete(int? id)        
        {
            if(id == null) return RedirectToAction("Index");//Se não tiver o parametro Id retona para Index

            return View();
        }



        //------- Metodos -----------

        //Metodo Cadastrar
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Contacts c, Phone p)
        {
            if(!ModelState.IsValid) return View("Cadastrar"); //Caso não valide retorna
            
            //Salvamento do contato
            _dc.contacts.Add(c);
            await _dc.SaveChangesAsync();

            p.ContactsId = c.Id;

            _dc.phone.Add(p);
            await _dc.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Metodo Alterar
        [HttpPost]
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
