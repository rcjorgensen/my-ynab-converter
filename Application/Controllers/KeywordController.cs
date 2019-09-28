using Application.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class KeywordController : Controller
    {
        private readonly IPayeeRepository repository;

        public KeywordController(IPayeeRepository repository)
        {
            this.repository = repository;
        }

        // GET: Keyword/Create
        public ActionResult Create(int payeeId)
        {
            var model = new KeywordViewModel
            {
                PayeeId = payeeId
            };
            return View(model);
        }

        // POST: Keyword/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KeywordViewModel model)
        {
            try
            {
                repository.AddKeyword(model.Value, model.PayeeId);
                return RedirectToAction("Details", "Payee", new { id = model.PayeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Keyword/Delete/5
        public ActionResult Delete(string keyword, int payeeId)
        {
            var model = new KeywordViewModel
            {
                Value = keyword,
                PayeeId = payeeId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(KeywordViewModel model)
        {
            try
            {
                repository.RemoveKeyword(model.Value, model.PayeeId);
                return RedirectToAction("Details", "Payee", new { id = model.PayeeId });
            }
            catch
            {
                return View();
            }
        }
    }
}