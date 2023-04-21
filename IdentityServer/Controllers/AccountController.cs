using Entities.Entities;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult = await _interactionService.GetLogoutContextAsync(logoutId);
                
            if (string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(logoutResult.PostLogoutRedirectUri);

        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var listGender = new List<GenderViewModel>
            {
                new GenderViewModel
                {
                    Name = "Nam",
                    ID = 1
                },
                new GenderViewModel
                {
                    Name = "Nữ",
                    ID = 2
                },
                new GenderViewModel
                {
                    Name = "Khác",
                    ID = 3
                },
            };
            ViewData["Gender"] = new SelectList(listGender.ToList(), "ID", "Name");
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var listGender = new List<GenderViewModel>
            {
                new GenderViewModel
                {
                    Name = "Nam",
                    ID = 1
                },
                new GenderViewModel
                {
                    Name = "Nữ",
                    ID = 2
                },
                new GenderViewModel
                {
                    Name = "Khác",
                    ID = 3
                },
            };
            ViewData["Gender"] = new SelectList(listGender.ToList(), "ID", "Name",vm.Gender);
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser appUser = new AppUser
            {
                UserName = vm.Username,
                Email = vm.Email,
                Gender = vm.Gender,
                Address = vm.Address,
                Name = vm.Name,
                BirthDay = vm.BirthDay,

            };
            var result = await _userManager.CreateAsync(appUser, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, false);
                return Redirect(vm.ReturnUrl);
            }

            return View(vm);
        }
    }
}
