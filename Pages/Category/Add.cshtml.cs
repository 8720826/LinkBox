using LinkBox.Services.Interfaces;
using LinkBox.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mapster;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        public CreateCategoryRequest Category { get; set; } = default!;

        public SelectList CategoryTypes { get; set; } = default!;

        private readonly ICategoryService _categoryService;

        public AddModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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

            await _categoryService.CreateCategoryAsync(Category);
            return RedirectToPage("./Index");
        }
    }
}
