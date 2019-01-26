using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeSaude.presentation.Models
{
    public class VidaViewModel
    {
        public VidaViewModel()
        {
            Endereco = new EnderecoViewModel();
            Telefone = new TelefoneViewModel();
            Beneficiarios = new List<VidaViewModel>();
        }

        public Guid? VidaId { get; set; }
        [Required(ErrorMessage = "Preencha o nome.")]
        public string Nome { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Preencha a data de nascimento.")]
        public DateTime DataDeNascimento { get; set; }
        [Required(ErrorMessage = "Preencha o sexo.")]
        public string Sexo { get; set; }
        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "Preencha o estado civil.")]
        public string EstadoCivil { get; set; }
        [Required(ErrorMessage = "Preencha o RG.")]
        public string RG { get; set; }
        [Display(Name = "CPF/CNPJ")]
        [Required(ErrorMessage = "Preencha o CPF/CNPJ.")]
        public string CPF_CNPJ { get; set; }
        public Guid? BeneficiarioId { get; set; }
        [Display(Name = "Beneficiário")]
        public bool Beneficiario { get; set; }
        List<VidaViewModel> Beneficiarios { get; set; }
        public string PesquisaNome { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public TelefoneViewModel Telefone { get; set; }
    }
}
