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
    public class DeleteModel : PageModel
    {
        private readonly PetsClient _petsClient;

        public DeleteModel(PetsClient petsClient)
        {
            _petsClient = petsClient;
        }

        [BindProperty]
        public Pet Pet { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pet = await _petsClient.GetPet(id);

            if (Pet == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var pet = await _petsClient.DeletePet(id);

            if (Pet == null)
            {
                return NotFound();
            }
            return RedirectToPage("./Index");
        }
    }
}
