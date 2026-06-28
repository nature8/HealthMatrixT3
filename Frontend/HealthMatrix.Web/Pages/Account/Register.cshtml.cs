using HealthMatrix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;     
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthMatrix.Web.Services;

namespace HealthMatrix.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        

        private readonly AuthService _authService;
        public RegisterModel(Services.AuthService authService)
        {
            _authService = authService;
        }
        [BindProperty]
        public RegisterViewModel Register { get; set; } = new ();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _authService.RegisterAsync(Register);
            if (success)
            {
                // Registration successful, redirect to login page or another page
                return RedirectToPage("/Account/Login");
            }
            else
            {
                // Registration failed, display an error message
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                return Page();
            }
        }
    }
}