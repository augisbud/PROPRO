namespace InvoicesBackend.Exceptions
{
    public class UserAlreadyExistsException(string email, string username) : Exception($"User with Email {email} or Username {username} already exists.")
    {
    }
}