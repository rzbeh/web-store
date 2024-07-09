using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyEshop.Data.Repositories;
using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyEshop.Controllers
{
    
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userRepository.IsExistUserByEmail(register.Email.ToLower()))

            {

                ModelState.AddModelError("Email", "Email is not valid");

                return View();

            }
            Users user = new Users()
            {
                Email = register.Email.ToLower(),
                Password = register.Password,
                IsAdmin = false,
                RegisterDate = DateTime.Now

            };

            _userRepository.AddUser(user);
            return View("SuccessRegister", register);

        }

        #endregion

        #region Login 

        public IActionResult Login () 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userRepository.GetUsersForLogin(login.Email.ToLower() , login.Password);

            if (user == null)
            {
                ModelState.AddModelError("Email", "youre information is not valid");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.Email),
                new Claim ("IsAdmin", user.IsAdmin.ToString()),

            };

            var identity =new ClaimsIdentity(claims , CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }
        #endregion
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults .AuthenticationScheme);   
            return Redirect("/Account/Login");
        }

    }
}   
