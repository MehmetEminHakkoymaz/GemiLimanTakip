using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            // Basit kullanıcı doğrulama
            if (user.Username == "admin" && user.Password == "123")
            {
                // Giriş başarılı, anasayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Giriş başarısız, hata mesajı göster
            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
            return View();
        }
    }
}
