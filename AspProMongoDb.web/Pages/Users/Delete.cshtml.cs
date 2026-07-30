using AspProMongoDb.web.Entities;
using AspProMongoDb.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspProMongoDb.Web.Pages.Users
{
    public class DeleteModel(IUserServices userService) : PageModel
    {
        [BindProperty]
        public User? User { get; set; }

        public IActionResult OnGetAsync(Guid id)
        {

            User = userService.GetById(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {


            userService.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
