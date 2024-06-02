using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IpfinalContext _context;

        public AccountController(IpfinalContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(Kullanıcılar model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _context.Kullanıcılar
        //            .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

        //        if (user != null)
        //        {
        //            HttpContext.Session.SetInt32("UserId", user.UserId);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyin.");
        //        }
        //    }

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Kullanıcılar
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış. Lütfen tekrar deneyin.");
                }
            }

            // Hatalı giriş denemesi durumunda, LoginViewModel tipinde bir model oluşturun
            var loginViewModel = new LoginViewModel
            {
                Username = model.Username,
                Password = model.Password
            };

            return View(loginViewModel);
        }


    }
}