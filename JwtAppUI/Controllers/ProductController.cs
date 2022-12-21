using JwtAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JwtAppUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
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

            var response = await client.GetAsync("http://localhost:5249/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<ProductListResponseModel>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                foreach (var item in list)
                {
                    var responseCat = await client.GetAsync("http://localhost:5249/api/Categories/" + item.CategoryId);
                    var categoryJsonString = await responseCat.Content.ReadAsStringAsync();
                    var cat = JsonSerializer.Deserialize<CategoryListRepsonseModel>(categoryJsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });

                    item.Category = cat;
                }

                return View(list);
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            //    return RedirectToAction("AccessDenied", "Account");
            else return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = this.CreateClient();

            await client.DeleteAsync($"http://localhost:5249/api/Products/{id}");

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Create()
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
                ViewBag.Categories = new SelectList(list, "Id","Definition");
                return View();
            }
            else return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5249/api/Products", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("http://localhost:5249/api/Products/" + id);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var productModel = JsonSerializer.Deserialize<ProductUpdateRequestModel>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                var responseCat = await client.GetAsync("http://localhost:5249/api/Categories");
                if (responseCat.IsSuccessStatusCode)
                {
                    var catJsonString = await responseCat.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<CategoryListRepsonseModel>>(catJsonString, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                    ViewBag.Categories = new SelectList(list, "Id", "Definition");
                    return View(productModel);
                }
                else return RedirectToAction("Index", "Home");
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            //    return RedirectToAction("AccessDenied", "Account");
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("http://localhost:5249/api/Products", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
