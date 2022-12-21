using JwtAppUI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JwtAppUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();

            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //Artık elimdeki token bilgisini ekledim, artık isteklerimi yapabilirim

            return client;
        }
        public async Task<IActionResult> List()
        {
            var client = this.CreateClient();

            var response = await client.GetAsync("http://localhost:5249/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<CategoryListRepsonseModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                return View(list);
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            //    return RedirectToAction("AccessDenied", "Account");
            else return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = this.CreateClient();

            await client.DeleteAsync($"http://localhost:5249/api/Categories/{id}");

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View(new CategoryCreateRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5249/api/Categories", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("http://localhost:5249/api/Categories/" + id);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var catModel = JsonSerializer.Deserialize<CategoryUpdateRequestModel>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                return View(catModel);
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            //    return RedirectToAction("AccessDenied", "Account");
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("http://localhost:5249/api/Categories", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }


    }
}
