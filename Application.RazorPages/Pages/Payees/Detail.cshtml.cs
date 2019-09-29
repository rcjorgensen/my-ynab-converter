using DataAccess;
using Domain.Payee;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.RazorPages.Pages.Payees
{
    public class DetailModel : PageModel
    {
        private readonly IPayeeRepository payeeRepository;

        public DetailModel(IPayeeRepository payeeRepository)
        {
            this.payeeRepository = payeeRepository;
        }

        public Payee Payee { get; set; }

        public void OnGet(int payeeId)
        {
            Payee = payeeRepository.GetAll().Find(x => x.Id == payeeId);
        }
    }
}