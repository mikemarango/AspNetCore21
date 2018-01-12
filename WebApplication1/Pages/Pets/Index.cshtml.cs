using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages.Pets
{
    public class IndexModel : PageModel
    {
        private readonly PetsClient _petsClient;

        public IndexModel(PetsClient petsClient)
        {
            _petsClient = petsClient;
        }

        public IList<Pet> Pets { get;set; }

        public async Task OnGetAsync()
        {
            Pets = await _petsClient.GetPets();
        }
    }
}
