using CSE_website.Common;
using CSE_website.Data;
using CSE_website.Models;
using CSE_website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSE_website.Constants;
using System.Text.RegularExpressions;
using CSE_website.Controllers;
using CSE_website.Interfaces;

namespace CSE_website.Areas.Admin.Controllers;

[Area("Admin")]
public class CheckpointController : Controller
{
    private readonly CSEWebsiteContext _dbContext;
    private readonly ISendMailService _sendMailService;

    public CheckpointController(
        CSEWebsiteContext dbContext,
        ISendMailService sendMailService
    )
    {
        _dbContext = dbContext;
        _sendMailService = sendMailService;
    }

    [FromForm][BindProperty] public Account? Account { get; set; }

    #region Action

    [HttpPost]
    public async Task<IActionResult> SendMail([FromForm] string email)
    {
        if ((email is not null && !IsEmailValid(email)) || email is null)
        {
            return BadRequest("Địa chỉ email không tồn tại!");
        }

        var user = _dbContext.Accounts?.FirstOrDefault(user => user.Email == email);
        if (user != null)
        {
            user.ResetToken = CreateResetPasswordToken(Hashing.Generate(email));
            user.ResetTokenExpiration = DateTime.UtcNow.AddMinutes(10);
            _dbContext.SaveChanges();

            SendMailReset(user.ResetToken, email);
            return Ok(SuccessMessConstants.RESET_PASS_MAIL_SUCCESS);
        }

        return BadRequest(ErrorConstants.INVALID_MAIL);
    }

    public async Task<IActionResult> ResetPassword(string? token)
    {
        bool isValidToken = await ValidateToken(token);
        if (!isValidToken)
        {
            return BadRequest(ErrorConstants.INVALID_TOKEN);
        }

        if (Request.Method == HttpMethods.Get)
        {
            return View(Account);
        }

        if (Request.Method == HttpMethods.Post && !ValidatePassword())
        {
            const string passwordError = ErrorConstants.PASSWORD;
            const string confirmPasswordError = ErrorConstants.CONFIRM_PASSWORD;
            ViewBag.PasswordError = passwordError;

            if (Account.Password != Account.ConfirmPassword)
            {
                ViewBag.ConfirmPasswordError = confirmPasswordError;
            }
            return View(Account);
        }

        var user = _dbContext.Accounts?.FirstOrDefault(acc => acc.ResetToken == token);
        if (user is not null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(Account.Password);
            _dbContext.SaveChanges();
        }

        ViewBag.SuccessUpdatePassword = SuccessMessConstants.PASSWORD_UPDATE_SUCCESS;

        return View(Account);
    }


    #endregion

    #region Non-Action
    [NonAction]
    private void SendMailReset(string token, string email)
    {
        string url = $"{Request.Scheme}://{Request.Host}/Admin/Checkpoint/ResetPassword?token={token}";
        string html = $@"<p>
                            Please click here to reset password: 
                            <a href=""{url}"">Reset Password</a>
                        </p>";
        _sendMailService.SendEmail(
            email,
            "Reset password",
            html
        );
    }

    [NonAction]
    private static string CreateResetPasswordToken(string hash)
    {
        string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return $"{timeStamp}{hash}";
    }

    [NonAction]
    private async Task<bool> ValidateToken(string? token)
    {
        if (token is not null)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.ResetToken == token && acc.ResetTokenExpiration >= DateTime.UtcNow);
            return account is not null;
        }
        return false;
    }

    [NonAction]
    private bool ValidatePassword()
    {
        string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$";
        return Regex.IsMatch(Account.Password, pattern);
    }

    [NonAction]
    private bool IsEmailValid(string email)
    {
        var account = _dbContext.Accounts?.FirstOrDefault(acc => acc.Email == email);
        return account is not null;
    }

    #endregion
}