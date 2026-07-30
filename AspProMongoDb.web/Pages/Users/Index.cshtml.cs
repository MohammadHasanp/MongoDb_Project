using AspProMongoDb.web.Entities;
using AspProMongoDb.web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspProMongoDb.Web.Pages.Users
{
    public class IndexModel(IUserServices services) : PageModel
    {

        public List<User> Users { get; set; }

        public void OnGet()
        {
            Users = services.GetAll();
        }
    }
}
