using Books.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

    
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Получение страницы для регистрации
        /// </summary>
        /// <returns>Переадресация на страницу с регистрацией</returns>
        [HttpGet("reg")]
        public IActionResult Register()
        {
            return RedirectToPage("/Account/Registration");
        }

        /// <summary>
        /// Получение введённых данных на странице регистрации и регистрация пользователя
        /// </summary>
        /// <param name="model">Модель для регистрации</param>
        /// <returns>Переадресация на страницу с книгами или на начальную страницу</returns>
        [HttpPost("reg")]
        public async Task<IActionResult> Register([FromForm] RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
              
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToPage("/BooksL");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToPage("/Index");
        }

        /// <summary>
        /// Получение страницы для входа
        /// </summary>
        /// <returns>Переадресация на страницу входа</returns>
        [HttpGet("log")]
        public IActionResult Logi()
        {
            return RedirectToPage("/Account/Login");
        }


        /// <summary>
        /// Получение введённых данных на странице входа и авторизация пользователя
        /// </summary>
        /// <param name="model">Модель для входа</param>
        /// <returns>Переадресация на страницу с книгами или переадречация на этот же метод</returns>
        [HttpPost("log")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logi([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                        return RedirectToPage("/BooksL");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return RedirectToAction("Logi", "Account");

        }

        /// <summary>
        /// Выход из учётной записи
        /// </summary>
        /// <returns>Переадресация на начальную страницу</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
