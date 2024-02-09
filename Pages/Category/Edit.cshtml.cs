using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Entities.Enums;
using LinkBox.Models;
using LinkBox.Pages.Link;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditCategoryDto Category { get; set; } = default!;

        public SelectList CategoryTypes { get; set; } = default!;


        private readonly LinkboxDbContext _db;

        public EditModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            Init();
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToPage("Index");
            }

            Category = category.Adapt<EditCategoryDto>();

            return Page();
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

            var category = _db.Categories.Find(Category.Id);
            Category.Adapt(category);

            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
