using DataAccess;
using Domain.Payee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Application.RazorPages.Pages.Payees
{
    public class EditModel : PageModel
    {
        private readonly IPayeeRepository payeeRepository;

        [BindProperty]
        public Payee Payee { get; set; }

        [BindProperty]
        [Display(Name = "Add Keyword")]
        public string NewKeyword { get; set; }
        
        [TempData]
        public string Message { get; set; }

        public EditModel(IPayeeRepository payeeRepository)
        {
            this.payeeRepository = payeeRepository;
        }

        public IActionResult OnGet(int? payeeId)
        {
            if (payeeId.HasValue)
            {
                Payee = payeeRepository.Get(payeeId.Value);
            }
            else
            {
                Payee = new Payee();
            }
            if (Payee == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewKeyword))
            {
                Payee.Keywords.Add(new Keyword
                {
                    PayeeId = Payee.Id,
                    Value = NewKeyword,
                });
            }

            if (Payee.Id > 0)
            {
                payeeRepository.Update(Payee);
            }
            else
            {
                payeeRepository.Add(Payee);
            }
            
            return RedirectToPage("./Edit", new { payeeId = Payee.Id });
        }
    }
}