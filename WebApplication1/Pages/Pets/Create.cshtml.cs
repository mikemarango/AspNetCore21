using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages.Pets
{
    public class CreateModel : PageModel
    {
        private readonly PetsClient _petsClient;

        public CreateModel(PetsClient petsClient)
        {
            _petsClient = petsClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _petsClient.AddPet(Pet);

            return RedirectToPage("./Index");
        }
    }
}