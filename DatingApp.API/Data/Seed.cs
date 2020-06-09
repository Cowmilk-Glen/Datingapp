using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context){
            if(!context.Users.Any()){
                var userData = System.IO.File.ReadAllText("Data/USerSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach(var user in users){
                    byte[] passwordhash, passwordSalt;
                    CreatePasswordHash("password",out passwordhash, out passwordSalt);

                    user.passwordHash = passwordhash;
                    user.passwordSalt = passwordSalt;
                    user.userName = user.userName.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }
                private static void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt){
                
                using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                    passSalt = hmac.Key;
                    passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }
    }
}