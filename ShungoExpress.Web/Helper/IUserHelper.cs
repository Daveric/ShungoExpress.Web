using Microsoft.AspNetCore.Identity;
using ShungoExpress.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShungoExpress.Web.Models;

namespace ShungoExpress.Web.Helper
{
  public interface IUserHelper
  {
    Task<User> GetUserByEmailAsync(string email);

    Task<User> GetUserByNameAsync(string userName);

    Task<IdentityResult> AddUserAsync(User user, string password);

    Task<IdentityResult> AddUserAsync(User user);

    Task<SignInResult> LoginAsync(LoginViewModel model);

    Task LogoutAsync();

    Task<IdentityResult> UpdateUserAsync(User user);

    Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

    Task<SignInResult> ValidatePasswordAsync(User user, string password);

    Task CheckRoleAsync(string roleName);

    Task DeleteRoleAsync(string roleName);

    Task AddUserToRoleAsync(User user, string roleName);

    Task<bool> IsUserInRoleAsync(User user, string roleName);

    Task<string> GenerateEmailConfirmationTokenAsync(User user);

    Task<IdentityResult> ConfirmEmailAsync(User user, string token);

    Task<User> GetUserByIdAsync(string userId);

    Task<string> GeneratePasswordResetTokenAsync(User user);

    Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

    Task<List<User>> GetAllUsersAsync();

    Task<List<User>> GetAllClientsAsync();

    Task RemoveUserFromRoleAsync(User user, string roleName);

    Task DeleteUserAsync(User user);
  }
}
