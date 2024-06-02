using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_11424.Models;
using Newtonsoft.Json;
using System.Text;


namespace MVC_11424.Controllers
{
    public class SongsController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public SongsController()
        {
            _httpClient.BaseAddress = new Uri("http://ec2-16-171-5-92.eu-north-1.compute.amazonaws.com/api");
        }

        // GET: SongsController
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Song");
            if (!response.IsSuccessStatusCode) return View();
            var songs = JsonConvert.DeserializeObject<List<Song>>(response.Content.ReadAsStringAsync().Result);
            return View(songs);
        }

        // GET: SongsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/song/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var song = JsonConvert.DeserializeObject<Song>(response.Content.ReadAsStringAsync().Result);
            return View(song);
        }

        // GET: SongsController/Create
        public async Task<ActionResult> Create()
        {
            return View(new Song());
        }

        // POST: SongsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Song song)
        {
            if (ModelState.IsValid)
            {
                string songSer = JsonConvert.SerializeObject(song);
                StringContent stringContentInfo = new StringContent(songSer, Encoding.UTF8, "application/json");

                var result = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Song", stringContentInfo);

                if (!result.IsSuccessStatusCode) return View(song);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: SongsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/song/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var song = JsonConvert.DeserializeObject<Song>(response.Content.ReadAsStringAsync().Result);

            return View(song);
        }

        // POST: SongsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Song song)
        {
            if (ModelState.IsValid)
            {
                string songSer = JsonConvert.SerializeObject(song);
                StringContent stringContentInfo = new StringContent(songSer, Encoding.UTF8, "application/json");

                var result = await _httpClient.PutAsync($"{_httpClient.BaseAddress}Song/{id}", stringContentInfo);

                if (!result.IsSuccessStatusCode) return View(song);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: SongsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/song/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var song = JsonConvert.DeserializeObject<Song>(response.Content.ReadAsStringAsync().Result);
            return View(song);
        }

        // POST: SongsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Song song)
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Song/{id}");
            if (!response.IsSuccessStatusCode) return View();
            return RedirectToAction(nameof(Index));
        }
    }
}
