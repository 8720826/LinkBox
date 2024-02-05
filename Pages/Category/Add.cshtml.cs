using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddCategoryDto Category { get; set; } = default!;

        public SelectList CategoryTypes { get; set; } = default!;



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
            CategoryTypes = new SelectList(CategoryConfig.AllTypes);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }

            var category = Category.Adapt<CategoryEntity>();



            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
