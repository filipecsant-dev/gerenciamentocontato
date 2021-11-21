using System.Collections.Generic;
using teste.Models.Entities;

namespace teste.Models.ViewModel
{
    public class ViewEdit
    {
        public Contacts Contacts { get; set; }
        public List<Phone> Phone { get; set; }

        public List<Phone> MyPhone { get; set; }
    }
}