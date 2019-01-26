using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeSaude.presentation.Models
{
    public class TelefoneViewModel
    {
        public Guid? TelefoneId { get; set; }
        [Required(ErrorMessage = "Preencha o tipo.")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Preencha o DDD.")]
        public string DDD { get; set; }
        [Required(ErrorMessage = "Preencha o número.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public Guid Vida_Id { get; private set; }
    }
}