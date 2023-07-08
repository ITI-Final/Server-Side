namespace APIApp.Services.Authentication
{
    public interface IAuthentication<T> where T : class
    {
        public Task<T> Login(string email, string password);

        public Task<bool> IsEmailTakenAsync(string email);
    }
}
