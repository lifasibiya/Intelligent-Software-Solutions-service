namespace iss.Models
{
    public interface IUserService
    {
        bool CreateUser(User user);
        User? GetUser(int id);
        User? AuthUser(string idnumber);

    }
}
