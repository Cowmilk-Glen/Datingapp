using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRespository : IAuthRepository
    {
        //query data base via framework / inject data context
        private readonly DataContext _context;

        public AuthRespository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.userName == username);


            if(user == null){
                return null;
            }

            if(!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt)){
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i<computedHash.Length; i++){
                    if(computedHash[i]!=passwordHash[i]) return false;
                }

                }
                return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passHash, passSalt;
            CreatePasswordHash(password, out passHash, out passSalt);
            user.passwordHash = passHash;
            user.passwordSalt = passSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
 
        }

        private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt){
                
                using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                    passSalt = hmac.Key;
                    passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }

       

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.userName == username)) return true;

            return false;
        }
    }
}