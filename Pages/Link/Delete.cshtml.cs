using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class DeleteModel : PageModel
    {
        public DeleteLinkDto Link { get; set; } = default!;


        private readonly LinkboxDbContext _db;

        public DeleteModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            var link = _db.Links.Find(id);
            if (link == null)
            {
                return RedirectToPage("Index");
            }
            Link = link.Adapt<DeleteLinkDto>();

            return Page();
        }


        public IActionResult OnPost(int id)
        {
            var link = _db.Links.Find(id);
            if (link == null)
            {
                return RedirectToPage("Index");
            }

            _db.Links.Remove(link);
            _db.SaveChanges();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
