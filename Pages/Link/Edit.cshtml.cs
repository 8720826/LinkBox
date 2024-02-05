using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Extentions;
using LinkBox.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditLinkDto Link { get; set; } = default!;

        public SelectList Categories { get; set; }



        private readonly LinkboxDbContext _db;

        public EditModel(LinkboxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Init();
            var link = _db.Links.Find(id);
            if (link == null)
            {
                return RedirectToPage("Index");
            }

            Link = link.Adapt<EditLinkDto>();

            return Page();
        }

        private void Init()
        {
            var categories = _db.Categories.ToList();

            Categories = new SelectList(categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }

            var link = Link.Adapt<LinkEntity>();
            link.Icon = link.Icon.CheckIsNullOrEmpty();
            link.Description = link.Description.CheckIsNullOrEmpty();


            _db.Links.Update(link);
            await _db.SaveChangesAsync();

            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
