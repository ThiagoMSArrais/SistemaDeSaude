using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Models
{
    public interface IVidaDAO
    {
        void AdicionarVida(VidaViewModel vida);
        void AdicionarBeneficiado(VidaViewModel beneficiario);
        List<VidaViewModel> ObterBeneficiados(Guid id);
        List<VidaViewModel> ObterVidas();
        void AtualizarVida(VidaViewModel vidaViewModel);
        VidaViewModel ObterVidaPorId(Guid id);
        void DeletarVida(VidaViewModel vidaViewModel);
        List<VidaViewModel> ConsultarPorNome(string nome);
    }
}