using Humanizer;
using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class IndexModel : PageModel
    {
        public List<ListCategoryDto> Categories { get; set; } = default!;

        private readonly LinkboxDbContext _db;

        public IndexModel(LinkboxDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Categories = _db.Categories.ProjectToType<ListCategoryDto>().ToList();
        }
    }
}
