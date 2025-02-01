using biletcoo.Models;
using biletcoo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace biletcoo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<users> signInManager;
        private readonly UserManager<users> userManager;

        public AccountController(SignInManager<users> signInManager, UserManager<users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                users user = await userManager.FindByEmailAsync(model.email);
                var result = await signInManager.PasswordSignInAsync(model.email, model.password,false,false);
                if (result.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(user, "User"))
                    {
                        return RedirectToAction("Main", "Home");
                    }
                    else if (await userManager.IsInRoleAsync(user, "Organizer"))
                        return RedirectToAction("Index", "Organizer");
                    else
                        return RedirectToAction("Index", "Admin");


                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect");
                    return View(model);
                 }

            }
            return View(model);

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid) 
            {
                users user = new users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.email,
                    UserName = model.email,
                    password = model.password,
                    gender = model.gender,
                    date = model.date,
                    tax = model.tax,
                    roles = model.role
                };
            
                var result = await userManager.CreateAsync(user,model.password);
                var addRole = await userManager.AddToRoleAsync(user,model.role);
                if (result.Succeeded & addRole.Succeeded)
                {
                    TempData["ShowRegisterPopup"] = true;
                    TempData["PopupMessage"] = "You have successfully registered!";
                    
                }
                else { 
                    foreach (var error in result.Errors)
                    {
                        
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

            }
                return View(model);

        }


        public IActionResult forgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> forgetPassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.email);

                if (user != null)
                {
                    await userManager.RemovePasswordAsync(user);
                    var result = await userManager.AddPasswordAsync(user, model.NewPassword);

                    if (result.Succeeded)
                    {
                        user.password = model.NewPassword;
                        var a =userManager.UpdateAsync(user);
                        TempData["ShowForgetPopup"] = true;
                        TempData["PopupMessage"] = "You have successfully changed your password!";
                        
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);

                }

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
