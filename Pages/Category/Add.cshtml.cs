using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LinkBox.Pages.Category
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public CategoryEntity Category { get; set; } = default!;

        public SelectList CategoryTypes { get; set; }



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
            CategoryTypes = new SelectList(new List<string> { CategoryTypeEnum.App.ToString(), CategoryTypeEnum.BookMark.ToString() });
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Init();
                return Page();
            }

            _db.Categories.Add(Category);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
