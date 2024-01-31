using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Link
{
    public class DeleteModel : PageModel
    {
        public LinkEntity Link { get; set; } = default!;


        private readonly LinkboxDbContext _db;

        public DeleteModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            Link = _db.Links.Find(id);
            if (Link == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }


        public IActionResult OnPost(int id)
        {
            Link = _db.Links.Find(id);
            if (Link == null)
            {
                return RedirectToPage("Index");
            }


            _db.Links.Remove(Link);
            _db.SaveChanges();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
