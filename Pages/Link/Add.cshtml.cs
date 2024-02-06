using LinkBox.Contexts;
using LinkBox.Entities.Enums;
using LinkBox.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinkBox.Models;
using LinkBox.Authorizations;
using Mapster;
using LinkBox.Extentions;
using LinkBox.Template;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddLinkDto Link { get; set; } = default!;

        public SelectList Categories { get; set; }



        private readonly LinkboxDbContext _db;

        public AddModel(LinkboxDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Init();
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

            _db.Links.Add(link);
            await _db.SaveChangesAsync();
            LinkBoxData.Refresh(true);
            if (Link.IsCompileImmediately)
            {
                TemplateProvider.NextCompileTime = DateTime.Now;
            }
            return RedirectToPage("./Index");
        }
    }
}
