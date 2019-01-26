using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeSaude.presentation.Models;
using System;
using System.Collections.Generic;

namespace SistemaDeSaude.presentation.Controllers
{
    [Authorize]
    public class VidaController : Controller
    {
        private readonly IVidaDAO _vidaDAO;

        public VidaController(IVidaDAO vidaDAO)
        {
            _vidaDAO = vidaDAO;
        }

        // GET: Vida
        public ActionResult Index()
        {
            return View(_vidaDAO.ObterVidas());
        }

        // GET: Vida/Details/5
        public ActionResult Details(Guid id)
        {

            VidaViewModel model = _vidaDAO.ObterVidaPorId(id);

            return View(model);
        }

        // GET: Vida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VidaViewModel vidaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(vidaViewModel);

                _vidaDAO.AdicionarVida(vidaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vida/Edit/5
        public ActionResult Edit(Guid id)
        {
            VidaViewModel model = null;
            if (id != null)
            {
                model = _vidaDAO.ObterVidaPorId(id);
            }

            return View(model);
        }

        // POST: Vida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VidaViewModel vidaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(vidaViewModel);

                _vidaDAO.AtualizarVida(vidaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vida/Delete/5
        public ActionResult Delete(Guid id)
        {

            VidaViewModel model = _vidaDAO.ObterVidaPorId(id);

            return View(model);
        }

        // POST: Vida/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VidaViewModel vidaViewModel)
        {
            try
            {
                _vidaDAO.DeletarVida(vidaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PesquisarNome(VidaViewModel vidaViewModel)
        {
            var model = _vidaDAO.ConsultarPorNome(vidaViewModel.PesquisaNome);

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult CadastrarBeneficiado(Guid BeneficiarioId)
        {
            VidaViewModel model = new VidaViewModel();
            model.BeneficiarioId = BeneficiarioId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarBeneficiado(VidaViewModel vidaViewModel)
        {
            try
            {
                _vidaDAO.AdicionarBeneficiado(vidaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult VisualizarBeneficiados(Guid id)
        {
            List<VidaViewModel> model = _vidaDAO.ObterBeneficiados(id);
            return View(model);
        }
    }
}