using System.Collections.Generic;
using teste.Models.Entities;

namespace teste.Models.Repository
{
    public interface IContactsRepository
    {
         List<Contacts> GetAll();
    }
}