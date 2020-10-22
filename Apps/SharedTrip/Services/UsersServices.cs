using Microsoft.EntityFrameworkCore.Internal;
using SharedTrip.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly ApplicationDbContext db;

        public UsersServices(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void Create(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password),
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var hashPass = ComputeHash(password);

            var userId = db.Users
                .Where(x => x.Username == username && x.Password == hashPass)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (userId == null)
            {
                return null;
            }
            return userId;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
