using Microsoft.AspNetCore.Mvc;

namespace SyncMe.Controllers {
    public class AdminController : Controller {

        // GET: /Admin/Login
        // Apenas mostra o formulário de login
        public IActionResult Login() {
            return View();
        }

        // POST: /Admin/Login
        // Recebe os dados do formulário
        [HttpPost]
        public IActionResult Login(string username, string password) {

            // A "segurança" que você sugeriu:
            if (username == "Admin" && password == "Admin123@") {

                // Sucesso! Define a "chave" na sessão
                HttpContext.Session.SetString("IsAdmin", "true");

                // Redireciona para a lista de conteúdos (que agora terá os botões)
                return RedirectToAction("Index", "Contents");
            }

            // Falha: Mostra a view de login de novo com uma mensagem de erro
            ViewBag.Error = "Usuário ou senha inválidos.";
            return View();
        }

        // GET: /Admin/Logout
        public IActionResult Logout() {
            // Limpa a sessão
            HttpContext.Session.Clear();

            // Manda o usuário de volta para a Home
            return RedirectToAction("Index", "Home");
        }
    }
}