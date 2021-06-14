

using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShungoExpress.Web.Helper
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Models;
  using Data.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.EntityFrameworkCore;

  public class UserHelper : IUserHelper
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    public async Task<IdentityResult> AddUserAsync(User user, string password)
    {
      return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddUserAsync(User user)
    {
      return await _userManager.CreateAsync(user);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
      return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
      return await _userManager.FindByNameAsync(userName);
    }

    public async Task AddUserToRoleAsync(User user, string roleName)
    {
      await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
    {
      return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
    }

    public async Task CheckRoleAsync(string roleName)
    {
      var roleExists = await _roleManager.RoleExistsAsync(roleName);
      if (!roleExists)
      {
        await _roleManager.CreateAsync(new IdentityRole
        {
          Name = roleName
        });
      }
    }

    public async Task DeleteRoleAsync(string roleName)
    {
      var role = await _roleManager.FindByNameAsync(roleName);
      if (role != null)
      {
        await _roleManager.DeleteAsync(role);
      }
    }

    public async Task<bool> IsUserInRoleAsync(User user, string roleName)
    {
      return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<SignInResult> LoginAsync(LoginViewModel model)
    {
      return await _signInManager.PasswordSignInAsync(
          model.Username,
          model.Password,
          model.RememberMe,
          false);
    }

    public async Task LogoutAsync()
    {
      await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
      return await _userManager.UpdateAsync(user);
    }

    public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
    {
      return await _signInManager.CheckPasswordSignInAsync(
          user,
          password,
          false);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
      return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
      return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
      return await _userManager.FindByIdAsync(userId);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
      return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
    {
      return await _userManager.ResetPasswordAsync(user, token, password);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
      return await _userManager.Users
          .OrderBy(u => u.LastName)
          .ToListAsync();
    }
    
    public async Task<List<User>> GetAllClientsAsync()
    {
      return await _userManager.Users
        .OrderBy(u => u.LastName)
        .Where(u => u.Role.Equals("Client"))
        .ToListAsync();
    }

    public async Task RemoveUserFromRoleAsync(User user, string roleName)
    {
      await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task DeleteUserAsync(User user)
    {
      await _userManager.DeleteAsync(user);
    }

    public IEnumerable<SelectListItem> GetClients()
    {
      var list = _userManager.Users.Where(u=>u.Role.Equals("Client")).Select(c => new SelectListItem
      {
        Text = c.FirstName,
        Value = c.Id.ToString()
      }).OrderBy(l => l.Text).ToList();

      list.Insert(0, new SelectListItem
      {
        Text = "(Select a client...)",
        Value = "0"
      });

      return list;
    }
  }
}
