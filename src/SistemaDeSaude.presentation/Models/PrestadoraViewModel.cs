using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Models
{
    public class PrestadoraViewModel
    {
        public Guid? PrestadoraId { get; set; }
        public string Nome { get; set; }
        public List<SelectListItem> Prestadoras { get; set; }
    }
}
