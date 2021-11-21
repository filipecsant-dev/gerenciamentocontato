
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using teste.Data;
using teste.Models.Entities;
using teste.Models.ViewModel;
using teste.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

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
                int num; //Destinada a verificação de busca por Telefone
                //Instanciando Lista de Contatos para armazenar todos contatos com Telefone parecido ao Search caso necessario
                List<Contacts> c = new List<Contacts>();
                if(Int32.TryParse(search, out num))//Verificar se a busca é de Nome ou Telefone
                {//Search por Telefone

                    //Criando uma lista com Telefone parecido ao do Search
                    List<Phone> phone = await _dc.phone
                                             .Where(x => x.Telefone.ToUpper().Contains(search.ToUpper()))
                                             .AsNoTracking()
                                             .ToListAsync();

                    //Entrando na lista phone para inserir na lista contatos todos contatos com numeros identicos ao do Search
                    foreach(var p in phone)
                    {
                        //Adicionando a lista de contatos
                        c.Add( _dc.contacts
                                  .Where(x => x.Id == p.ContactsId)
                                  .AsNoTracking()
                                  .FirstOrDefault());
                    }
                }
                else
                {//Search por Nome
                    c = await _dc.contacts
                                 .Where(x => x.Nome.ToUpper().Contains(search.ToUpper()))
                                 .AsNoTracking()
                                 .ToListAsync();
                }

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
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return RedirectToAction("Index");//Se não tiver o parametro Id retona para Index

            ViewEdit ve = new ViewEdit();
            ve.Contacts = _dc.contacts.Where(x => x.Id == id)
                                     .AsNoTracking()
                                     .FirstOrDefault();

            ve.Phone = await _dc.phone.Where(x => x.ContactsId == id)
                                      .AsNoTracking()
                                      .ToListAsync();

            return View(ve);
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
        public async Task<IActionResult> Cadastrar(Contacts c, string[] Telefone)
        {
            if(!ModelState.IsValid) return View("Cadastrar"); //Caso não valide retorna
            
            //Salvamento do contato
            _dc.contacts.Add(c);  
            await _dc.SaveChangesAsync();  
            
            //Capturando os telefones
            foreach(string phone in Telefone)
            {
                //Instanciando o telefone para receber dados para modificar
                var p = new Phone();
                p.ContactsId = c.Id;
                p.Telefone = phone;
                _dc.phone.Add(p);
                
            }

            //Salvando Telefones
            await _dc.SaveChangesAsync();

    
            return RedirectToAction("Index");
        }

        //Metodo Alterar
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Contacts c, int[] TelId, string[] Telefone)
        {
            if(!ModelState.IsValid) return View("Edit"); //Caso não valide retorna

            //Alteração de contato
            _dc.Entry(c).State = EntityState.Modified;
            await _dc.SaveChangesAsync();

            int i = 0;//Identificador do parametro recebido do Id do usuario

            foreach(string phone in Telefone)
            {
                //Instanciando o telefone para pegar dados e para o entity modificar
                var p = new Phone();  
                p.Id = TelId[i];//Pegando o Id do contato
                p.ContactsId = c.Id;
                p.Telefone = phone;

                _dc.Entry(p).State = EntityState.Modified;
                await _dc.SaveChangesAsync();
            
                i++;
            }

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
