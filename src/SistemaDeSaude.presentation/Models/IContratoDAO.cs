using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Models
{
    public interface IContratoDAO
    {
        void AdicionarContrato(ContratoViewModel contrato);
        List<SelectListItem> ObterVidas();
        List<SelectListItem> ObterPrestadora();
        List<SelectListItem> ObterNomeDoPlano(Guid id_prestadora);
        List<ContratoViewModel> ObterContratos();
    }
}