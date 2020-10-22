using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServices usersServices;

        public UsersController(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(loginInputModel input)
        {
            var userId = this.usersServices.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username shoul be between 5 and 20 characters.");
            }

            if (this.usersServices.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already exist.");
            }

            if (string.IsNullOrEmpty(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email");
            }

            if (!this.usersServices.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already exist.");
            }

            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password shoul be between 6 and 20 characters.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords don't match!");
            }

            this.usersServices.Create(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/login");
        }
    }
}
