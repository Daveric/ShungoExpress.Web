using Microsoft.AspNetCore.Identity;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Data
{
  public class SeedDb
  {
    private readonly DataContext _context;
    private readonly IUserHelper _userHelper;

    public SeedDb(DataContext context, IUserHelper userHelper)
    {
      _context = context;
      _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
      await _context.Database.EnsureCreatedAsync();

      await CheckRolesAsync();

      await CheckUser("erickdavid17@outlook.com", "Erick", "Maldonado", "Admin");

      if (!_context.Motorizeds.Any())
      {
        AddMotorized("Motorizado 1");
        AddMotorized("Motorizado 2");
        AddMotorized("Motorizado 3");
        await _context.SaveChangesAsync();
      }

      if (!_context.Clients.Any())
      {
        AddClient("Pancho", "Villa");
        await _context.SaveChangesAsync();
      }
    }

    private void AddClient(string name, string lastName)
    {
      _context.Clients.Add(new Client()
      {
        FirstName = name,
        LastName = lastName,
        Address = "Machala"
      });
    }

    private async Task<User> CheckUser(string userName, string firstName, string lastName, string role)
    {
      // Add user
      var user = await _userHelper.GetUserByEmailAsync(userName) ?? await AddUser(userName, firstName, lastName, role);

      var isInRole = await _userHelper.IsUserInRoleAsync(user, role);
      if (!isInRole)
      {
        await _userHelper.AddUserToRoleAsync(user, role);
      }

      return user;
    }

    private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
    {
      var user = new User
      {
        FirstName = firstName,
        LastName = lastName,
        Email = userName,
        UserName = userName,
        Address = "Machala"
      };

      var result = await _userHelper.AddUserAsync(user, "Pwd1234");
      if (result != IdentityResult.Success)
      {
        throw new InvalidOperationException("Could not create the user in seeder");
      }

      await _userHelper.AddUserToRoleAsync(user, role);
      var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
      await _userHelper.ConfirmEmailAsync(user, token);
      return user;
    }

    private async Task CheckRolesAsync()
    {
      await _userHelper.CheckRoleAsync("Admin");
      await _userHelper.CheckRoleAsync("Manager");
    }

    private void AddMotorized(string name)
    {
      _context.Motorizeds.Add(new Motorized()
      {
        Name = name,
        Available = true
      });
    }
  }
}
