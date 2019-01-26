using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeSaude.presentation.Models
{
    public class EnderecoViewModel
    {
        public Guid? EnderecoId { get; set; }
        [Required(ErrorMessage = "Preencha o logradouro.")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Preencha o número.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Preencha o bairro.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Preencha o município.")]
        [Display(Name = "Município")]
        public string Municipio { get; set; }
        [Required(ErrorMessage = "Preencha o estado.")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Preencha a UF")]
        public string UF { get; set; }
        [Required(ErrorMessage = "Preencha o CEP")]
        public string CEP { get; set; }

    }
}
