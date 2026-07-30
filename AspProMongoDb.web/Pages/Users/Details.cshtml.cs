using AspProMongoDb.web.Entities;
using AspProMongoDb.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspProMongoDb.Web.Pages.Users
{
    public class DetailsModel(IUserServices userServices) : PageModel
    {
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
    }
}
