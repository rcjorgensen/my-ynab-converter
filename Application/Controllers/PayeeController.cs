using Application.Models;
using DataAccess;
using Domain.Payee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Application.Controllers
{
    public class PayeeController : Controller
    {
        private readonly IPayeeRepository repository;

        public PayeeController(IPayeeRepository repository)
        {
            this.repository = repository;
        }

        // GET: Payee
        public ActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var payees = repository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                payees = payees.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            payees = payees.OrderBy(p => p.Name).ToList();

            var model = payees.Select(p => new PayeeViewModel
            {
                Id = p.Id,
                Name = p.Name
            });
            return View(model);
        }

        // GET: Payee/Details/5
        public ActionResult Details(int id)
        {
            var payee = repository.GetAll().FirstOrDefault(p => p.Id == id);
            var model = new PayeeViewModel
            {
                Id = payee.Id,
                Name = payee.Name,
                Keywords = payee.Keywords.Select(k => k.Value).ToList()
            };
            return View(model);
        }

        // GET: Payee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PayeeViewModel model)
        {
            try
            {
                repository.Add(new Payee { Name = model.Name });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}