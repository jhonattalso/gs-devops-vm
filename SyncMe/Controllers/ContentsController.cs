using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SyncMe.Models;
using SyncMe.Services;
using SyncMe.ViewModels;

namespace SyncMe.Controllers {
    [Route("academy")]
    public class ContentsController : Controller {
        private readonly ContentService _service;

        public ContentsController(ContentService service) {
            _service = service;
        }

        // GET: Contents
        [Route("")]
        public async Task<IActionResult> Index(
            string searchString,
            int? categoryId,
            DifficultyLevel? difficulty,
            int? pageNumber) {

            int pageSize = 6;
            int pageIndex = pageNumber ?? 1;

            var result = await _service.GetAllAsync(searchString, categoryId, difficulty, pageIndex, pageSize);

            ViewBag.SearchString = searchString;
            ViewBag.CategoryId = categoryId;
            ViewBag.Difficulty = difficulty;

            ViewBag.CurrentPage = result.CurrentPage;
            ViewBag.TotalPages = result.TotalPages;

            ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", categoryId);

            return View(result.Items);
        }

        // GET: Contents/Details/5
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null) return NotFound();
            var content = await _service.GetByIdAsync(id.Value);
            if (content == null) return NotFound();
            return View(content);
        }

        // --- ÁREA ADMINISTRATIVA ---

        // GET: Contents/Create
        [Route("create")]
        public async Task<IActionResult> Create() {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }

            var viewModel = new ContentViewModel {
                Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name"),
                Tracks = new SelectList(await _service.GetTracksAsync(), "Id", "Title")
            };
            return View(viewModel);
        }

        // POST: Contents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(ContentViewModel viewModel) {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }

            if (ModelState.IsValid) {
                var content = new Content {
                    Title = viewModel.Title,
                    Summary = viewModel.Summary,
                    ArticleBody = viewModel.ArticleBody, // <--- NOVO
                    CoverImageUrl = viewModel.CoverImageUrl, // <--- NOVO
                    MediaUrl = viewModel.MediaUrl,
                    Difficulty = viewModel.Difficulty,
                    CategoryId = viewModel.CategoryId,
                    TrackId = viewModel.TrackId,
                    PublishDate = DateTime.Now
                };

                await _service.CreateAsync(content);
                return RedirectToAction(nameof(Index));
            }

            // Se der erro, recarrega os dropdowns
            viewModel.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", viewModel.CategoryId);
            viewModel.Tracks = new SelectList(await _service.GetTracksAsync(), "Id", "Title", viewModel.TrackId);
            return View(viewModel);
        }

        // GET: Contents/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id) {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null) return NotFound();
            var content = await _service.GetByIdAsync(id.Value);
            if (content == null) return NotFound();

            // Preenche o ViewModel com os dados do banco (incluindo os novos)
            var viewModel = new ContentViewModel {
                Id = content.Id,
                Title = content.Title,
                Summary = content.Summary,
                ArticleBody = content.ArticleBody, // <--- NOVO
                CoverImageUrl = content.CoverImageUrl, // <--- NOVO
                MediaUrl = content.MediaUrl,
                Difficulty = content.Difficulty,
                CategoryId = content.CategoryId,
                TrackId = content.TrackId,

                Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", content.CategoryId),
                Tracks = new SelectList(await _service.GetTracksAsync(), "Id", "Title", content.TrackId)
            };

            return View(viewModel);
        }

        // POST: Contents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ContentViewModel viewModel) {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }

            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid) {
                try {
                    var contentToUpdate = await _service.GetByIdAsync(id);
                    if (contentToUpdate == null) return NotFound();

                    // Atualiza os campos
                    contentToUpdate.Title = viewModel.Title;
                    contentToUpdate.Summary = viewModel.Summary;
                    contentToUpdate.ArticleBody = viewModel.ArticleBody; // <--- NOVO
                    contentToUpdate.CoverImageUrl = viewModel.CoverImageUrl; // <--- NOVO
                    contentToUpdate.MediaUrl = viewModel.MediaUrl;
                    contentToUpdate.Difficulty = viewModel.Difficulty;
                    contentToUpdate.CategoryId = viewModel.CategoryId;
                    contentToUpdate.TrackId = viewModel.TrackId;

                    await _service.UpdateAsync(contentToUpdate);
                }
                catch (DbUpdateConcurrencyException) {
                    if (await _service.GetByIdAsync(id) == null) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", viewModel.CategoryId);
            viewModel.Tracks = new SelectList(await _service.GetTracksAsync(), "Id", "Title", viewModel.TrackId);
            return View(viewModel);
        }

        // GET: Contents/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id) {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null) return NotFound();
            var content = await _service.GetByIdAsync(id.Value);
            if (content == null) return NotFound();
            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (HttpContext.Session.GetString("IsAdmin") != "true") {
                return RedirectToAction("Login", "Admin");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}