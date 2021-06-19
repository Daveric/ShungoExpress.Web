using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShungoExpress.Web.Data.Entities;
using ShungoExpress.Web.Helper;
using ShungoExpress.Web.Models;
using ShungoExpress.Web.Properties;
using System.Linq;
using System.Threading.Tasks;

namespace ShungoExpress.Web.Controllers
{
  public class AccountController : Controller
  {
    private readonly IUserHelper _userHelper;

    public AccountController(IUserHelper userHelper)
    {
      _userHelper = userHelper;
    }
    public IActionResult Login()
    {
      var userIdentity = User.Identity;
      if (userIdentity != null && userIdentity.IsAuthenticated)
      {
        return RedirectToAction("Index", "Home");
      }

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = await _userHelper.LoginAsync(model);
        if (result.Succeeded)
        {
          if (Request.Query.Keys.Contains("ReturnUrl"))
          {
            return Redirect(Request.Query["ReturnUrl"].First());
          }

          return RedirectToAction("Index", "Home");
        }
      }

      ModelState.AddModelError(string.Empty, Resources.LoginFailed);
      return View(model);
    }

    public async Task<IActionResult> Logout()
    {
      await _userHelper.LogoutAsync();
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
      var model = new RegisterNewUserViewModel();

      return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterNewUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userHelper.GetUserByEmailAsync(model.Username);
        if (user == null)
        {
          user = new User
          {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Username,
            UserName = model.Username,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber
          };

          var result = await _userHelper.AddUserAsync(user, model.Password);
          if (result != IdentityResult.Success)
          {
            this.ModelState.AddModelError(string.Empty, Resources.UserCannotBeCreated);
            return this.View(model);
          }

          var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
          await _userHelper.ConfirmEmailAsync(user, myToken);
          return this.View(model);
        }

        ModelState.AddModelError(string.Empty, Resources.UserRegistered);
      }

      return this.View(model);
    }

    public async Task<IActionResult> ChangeUser()
    {
      var user = await _userHelper.GetUserByEmailAsync(User.Identity?.Name);
      var model = new ChangeUserViewModel();

      if (user != null)
      {
        model.FirstName = user.FirstName;
        model.LastName = user.LastName;
        model.Address = user.Address;
        model.PhoneNumber = user.PhoneNumber;
      }

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userHelper.GetUserByEmailAsync(User.Identity?.Name);
        if (user != null)
        {
          user.FirstName = model.FirstName;
          user.LastName = model.LastName;
          user.Address = model.Address;
          user.PhoneNumber = model.PhoneNumber;

          var identityResult = await _userHelper.UpdateUserAsync(user);
          //updating the user name displaying on the page
          User.Identity.AddUpdateClaim("FirstName", user.FirstName);
          await _userHelper.RefreshUser(user);
          if (identityResult.Succeeded)
          {
            ViewBag.UserMessage = Resources.UserUpdated;
          }
          else
          {
            ModelState.AddModelError(string.Empty, identityResult.Errors.FirstOrDefault()?.Description ?? string.Empty);
          }
        }
        else
        {
          ModelState.AddModelError(string.Empty, Resources.UserNotFound);
        }
      }

      return View(model);
    }

    public IActionResult ChangePassword()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userHelper.GetUserByEmailAsync(User.Identity?.Name);
        if (user != null)
        {
          var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
          if (result.Succeeded)
          {
            return RedirectToAction("ChangeUser");
          }
          else
          {
            ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
          }
        }
        else
        {
          ModelState.AddModelError(string.Empty, Resources.UserNotFound);
        }
      }

      return View(model);
    }

    public IActionResult NotAuthorized()
    {
      return View();
    }
  }
}
