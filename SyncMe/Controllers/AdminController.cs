using Microsoft.AspNetCore.Mvc;

namespace SyncMe.Controllers {
    public class AdminController : Controller {

        // GET: /Admin/Login
        public IActionResult Login() {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string username, string password) {
            if (username == "Admin" && password == "Admin123@") {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index", "Contents");
            }

            // Falha: Mostra a view de login de novo com uma mensagem de erro
            ViewBag.Error = "Usuário ou senha inválidos.";
            return View();
        }

        // GET: /Admin/Logout
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Academy");
        }
    }
}