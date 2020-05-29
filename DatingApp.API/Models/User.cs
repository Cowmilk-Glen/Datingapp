namespace DatingApp.API.Models
{
    public class User
    {
        public int iD {get;set;}
        public string userName {get;set;}
        public byte[] passwordHash {get;set;}
        public byte[] passwordSalt {get;set;}

    }
}