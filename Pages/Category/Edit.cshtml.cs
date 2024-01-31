using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Entities.Enums;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBox.Pages.Category
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public CategoryEntity Category { get; set; }

        public SelectList CategoryTypes { get; set; }


        private readonly LinkboxDbContext _db;

        public EditModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            Init();
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        private void Init()
        {
            CategoryTypes = new SelectList(new List<string> { CategoryTypeEnum.应用.ToString(), CategoryTypeEnum.书签.ToString() });
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }

            _db.Categories.Update(Category);
            await _db.SaveChangesAsync();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
