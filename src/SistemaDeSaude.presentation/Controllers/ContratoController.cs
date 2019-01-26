using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeSaude.presentation.Models;
using System;

namespace SistemaDeSaude.presentation.Controllers
{
    public class ContratoController : Controller
    {

        private readonly IContratoDAO _contratoDAO;

        public ContratoController(IContratoDAO contratoDAO)
        {
            _contratoDAO = contratoDAO;
        }

        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            ContratoViewModel model = new ContratoViewModel
            {
                Vidas = _contratoDAO.ObterVidas()
            };
            model.Plano.Prestadora.Prestadoras = _contratoDAO.ObterPrestadora();

            return View(model);
        }

        // Post: Contrato/ObterNomesDoPlano
        [HttpPost]
        public ActionResult ObterNomesDoPlano(string id)
        {
            ContratoViewModel model = new ContratoViewModel();
            model.Plano.Planos = _contratoDAO.ObterNomeDoPlano(Guid.Parse(id));
            return Json(new { result = model.Plano.Planos });
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContratoViewModel contratoViewModel)
        {
            try
            {
                _contratoDAO.AdicionarContrato(contratoViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}