namespace ExamHelperLibrary.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}