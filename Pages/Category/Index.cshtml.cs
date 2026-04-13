using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mapster;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class IndexModel : PageModel
    {
        public List<CategoryDto> Categories { get; set; } = default!;

        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        public async Task OnGet()
        {
            Categories = await _categoryService.GetAllCategoriesAsync();
        }
    }
}
