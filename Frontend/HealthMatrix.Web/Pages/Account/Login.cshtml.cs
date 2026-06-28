using HealthMatrix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthMatrix.Web.Pages.Account
{
    public class LoginModel : PageModel


    {
        private readonly Services.AuthService _authService;
        public LoginModel(Services.AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginViewModel Login { get; set; } = new ();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result =await _authService.LoginAsync(Login);

            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }           
            switch (result.User.Role)
            {
                case "Doctor":
                    return RedirectToPage("/Doctors/Index");
                case "Admin":
                    return RedirectToPage("/Dashboard/Index");
                case "Receptionist":
                    return RedirectToPage("/Appointments/Book");
                default:
                    return RedirectToPage("/Patients/Profile");
            }
        }
    }
}