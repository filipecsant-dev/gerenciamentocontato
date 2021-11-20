using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teste.Models.Entities
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        public string number { get; set; }
        [ForeignKey("contacts")]
        public int ContactsId { get; set; }
        public virtual Contacts Contacts {get; set; }
    }
}