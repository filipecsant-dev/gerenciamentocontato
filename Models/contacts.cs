using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace teste.Models
{
    public class contacts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime Aniversario { get; set; }
        public ICollection<phone> Phone { get; set; }
    }
}