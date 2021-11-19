using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teste.Models
{
    public class phone
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("contacts")]
        public int PhoneId { get; set; }
        public string number { get; set; }
    }
}