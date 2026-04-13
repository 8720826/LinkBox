using LinkBox.Services.Interfaces;
using LinkBox.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mapster;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateCategoryRequest Category { get; set; } = default!;

        public SelectList CategoryTypes { get; set; } = default!;

        private readonly ICategoryService _categoryService;

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet(int id)
        {
            Init();
            var category = _categoryService.GetCategoryByIdAsync(id).Result;
            if (category == null)
            {
                return RedirectToPage("Index");
            }

            Category = new UpdateCategoryRequest
            {
                Id = category.Id,
                Name = category.Name,
                SortId = category.SortId,
                Type = category.Type
            };

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

            await _categoryService.UpdateCategoryAsync(Category);
            return RedirectToPage("./Index");
        }
    }
}
