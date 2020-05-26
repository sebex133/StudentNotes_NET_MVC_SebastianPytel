using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentNotes_MVC_SebastianPytel.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "StudentNotes");
                //return RedirectToPage("./StudentNotes");
            }

            return Page();
        }
    }
}
