using AspProMongoDb.web.Entities;
using AspProMongoDb.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspProMongoDb.Web.Pages.Users
{
    public class CreateModel(IUserServices userService) : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            userService.Insert(User);

            return RedirectToPage("./Index");
        }
    }
}
