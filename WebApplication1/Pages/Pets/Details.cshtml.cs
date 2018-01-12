using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages.Pets
{
    public class DetailsModel : PageModel
    {
        private readonly PetsClient _petsClient;

        public DetailsModel(PetsClient petsClient)
        {
            _petsClient = petsClient;
        }

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
    }
}
