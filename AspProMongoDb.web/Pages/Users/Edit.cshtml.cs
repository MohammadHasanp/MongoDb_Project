using AspProMongoDb.web.Entities;
using AspProMongoDb.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspProMongoDb.Web.Pages.Users
{
    public class EditModel (IUserServices userServices): PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(Guid id)
        {


            User = userServices.GetById(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            userServices.Update(User);

            return RedirectToPage("./Index");
        }
       
    }
}
