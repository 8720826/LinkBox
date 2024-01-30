using LinkBox.Contexts;
using LinkBox.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Category
{
    public class IndexModel : PageModel
    {
        public List<CategoryEntity> Categories { get; set; }

        private readonly LinkboxDbContext _db;

        public IndexModel(LinkboxDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Categories = _db.Categories.OrderByDescending(x => x.Id).ToList();
        }
    }
}
