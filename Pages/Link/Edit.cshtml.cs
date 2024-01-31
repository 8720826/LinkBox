using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Link
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public LinkEntity Link { get; set; } = default!;

        public SelectList Categories { get; set; }



        private readonly LinkboxDbContext _db;

        public EditModel(LinkboxDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Init();
            Link = _db.Links.Find(id);
            if (Link == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        private void Init()
        {
            var categories = _db.Categories.ToList();

            Categories = new SelectList(categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }


            _db.Links.Update(Link);
            await _db.SaveChangesAsync();

            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
