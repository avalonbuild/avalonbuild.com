using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using avalonbuild.com.Models;
using avalonbuild.com.ViewModels;

namespace avalonbuild.com.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<UsersController>();
        }


        [Route("/admin/users")]
        public IActionResult Index()
        {
            var users = _userManager.Users;

            var list = new List<ApplicationUser>();

            foreach(var user in users)
            {
                list.Add(user);
            }

            return View(list);
        }

        [Route("/admin/users/add")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Route("/admin/users/add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation(3, "Created a new user with password.");

                    return RedirectToAction(nameof(UsersController.Index), "Users");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Route("/admin/users/delete/{email}")]
        [HttpGet]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
                await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(UsersController.Index), "Users");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
