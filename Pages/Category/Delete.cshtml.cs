using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class DeleteModel : PageModel
    {
      
        public DeleteCategoryDto Category { get; set; } = default!;


        private readonly LinkboxDbContext _db;

        public DeleteModel(LinkboxDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToPage("Index");
            }

            Category = category.Adapt<DeleteCategoryDto>();

            return Page();
        }


        public  IActionResult OnPost(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToPage("Index");
            }


            if (_db.Links.Any(x => x.CategoryId == id))
            {
                ModelState.AddModelError("", "分类下还有链接，无法删除！");
                return Page();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            LinkBoxData.Refresh(true);

            return RedirectToPage("./Index");
        }
    }
}
