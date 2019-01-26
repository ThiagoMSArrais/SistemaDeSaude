using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Models
{
    public class PlanoViewModel
    {
        public PlanoViewModel()
        {
            Prestadora = new PrestadoraViewModel();
        }

        public Guid? PlanoId { get; set; }
        public string Nome { get; set; }
        public Decimal Valor { get; set; }
        public string Acomodacao { get; set; }
        public string Abrangencia { get; set; }
        public List<SelectListItem> Planos { get; set; }

        public PrestadoraViewModel Prestadora { get; set; }
    }
}
