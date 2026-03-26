namespace Toolkit_API.Application.Interfaces
{
    public interface IPasswordHasher
    {
        public byte[] HashPassword(string password, out byte[] salt);
        public bool VerifyPassword(string password, byte[] hash, byte[] salt);
    }
}
