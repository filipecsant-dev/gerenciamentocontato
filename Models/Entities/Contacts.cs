using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace teste.Models.Entities
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Necessário digitar seu nome.")]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime Aniversario { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}