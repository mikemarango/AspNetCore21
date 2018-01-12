using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class PetsClient
    {
        private readonly HttpClient _httpClient;

        public PetsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Pet>> GetPets()
        {
            var response = await _httpClient.GetAsync("api/pets");
            response.EnsureSuccessStatusCode();
            var pets = await response.Content.ReadAsAsync<List<Pet>>();
            return pets;
        }

        public async Task<Pet> GetPet(int id)
        {
            var response = await _httpClient.GetAsync($"api/pets/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var pet = await response.Content.ReadAsAsync<Pet>();
            return pet;
        }

        public async Task<Pet> AddPet(Pet pet)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pets", pet);
            response.EnsureSuccessStatusCode();
            pet = await response.Content.ReadAsAsync<Pet>();
            return pet;
        }

        public async Task UpdatePet(int id, Pet pet)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/pets/{id}", pet);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Pet> DeletePet(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/pets/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var pet = await response.Content.ReadAsAsync<Pet>();
            return pet;
        }
    }
}
