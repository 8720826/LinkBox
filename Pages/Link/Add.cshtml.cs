using LinkBox.Contexts;
using LinkBox.Entities.Enums;
using LinkBox.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Link
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public LinkEntity Link { get; set; } = default!;

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

            _db.Links.Add(Link);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
