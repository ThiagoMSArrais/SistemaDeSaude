using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Models
{
    public class ContratoViewModel
    {
        public ContratoViewModel()
        {
            Plano = new PlanoViewModel();
        }

        public Guid? ContratoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime Validade { get; set; }
        public bool ContratoExpirado { get; set; }
        public Guid VidaId { get; set; }
        public List<SelectListItem> Vidas { get; set; }
        // Plano
        public PlanoViewModel Plano { get; set; }
    }
}
