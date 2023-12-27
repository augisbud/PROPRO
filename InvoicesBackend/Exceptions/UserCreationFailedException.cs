using Microsoft.AspNetCore.Identity;

namespace InvoicesBackend.Exceptions
{
    public class UserCreationFailedException(string username, IEnumerable<IdentityError> errors) : Exception($"Creation of user {username} failed. Errors: " + string.Join(", ", errors.Select(error => error.Description).ToArray()))
    {
    }
}