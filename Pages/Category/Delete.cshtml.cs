using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LinkBox.Pages.Category
{
    public class DeleteModel : PageModel
    {
      
        public CategoryEntity Category { get; set; } = default!;


        private readonly LinkboxDbContext _db;

        public DeleteModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }


        public  IActionResult OnPost(int id)
        {
            Category = _db.Categories.Find(id);
            if (Category == null)
            {
                return RedirectToPage("Index");
            }


            if (_db.Links.Any(x => x.CategoryId == id))
            {
                ModelState.AddModelError("", "分类下还有链接，无法删除！");
                return Page();
            }


            _db.Categories.Remove(Category);
            _db.SaveChanges();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
