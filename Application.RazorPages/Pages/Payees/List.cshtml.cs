using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain.Payee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.RazorPages.Pages.Payees
{
    public class ListModel : PageModel
    {
        private readonly IPayeeRepository payeeRepository;

        public IEnumerable<Payee> Payees { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IPayeeRepository payeeRepository)
        {
            this.payeeRepository = payeeRepository;
        }

        public void OnGet()
        {
            Payees = SearchTerm != null 
                ? payeeRepository.GetAll().Where(p => p.Name.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase) || p.Keywords.Any(k => k.Value.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase))) 
                : payeeRepository.GetAll();
        }
    }
}