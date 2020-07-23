using DataAccess;
using Domain.Payee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.RazorPages.Pages.Payees
{
    public class DeleteModel : PageModel
    {
        private readonly IPayeeRepository payeeRepository;

        public Payee Payee { get; set; }

        public DeleteModel(IPayeeRepository payeeRepository)
        {
            this.payeeRepository = payeeRepository;
        }

        public IActionResult OnGet(int payeeId)
        {
            Payee = payeeRepository.Get(payeeId);
            if (Payee == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int payeeId)
        {
            payeeRepository.Remove(payeeId);

            return RedirectToPage("./List");
        }
    }
}