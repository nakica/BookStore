namespace BookStore.Web.Controllers
{
    using BookStore.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public sealed class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ViewResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.Email);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if(result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "BookStore");
                    }
                    return Redirect(loginViewModel.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Email and password doesn't match!");
                }
            }
            else
            {
                ModelState.AddModelError("", "User doesn't exists!");
            }

            return View(loginViewModel);
        }
        public ViewResult Register() => View();


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginViewModel.Email };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                AddRegistrationErrors(result);
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "BookStore");
        }


        private void AddRegistrationErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}