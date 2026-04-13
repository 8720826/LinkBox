using LinkBox.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages.Category
{
    [UserAuthorize]
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet(int id)
        {
            var category = _categoryService.GetCategoryByIdAsync(id).Result;
            if (category == null)
            {
                return RedirectToPage("Index");
            }

            ViewData["CategoryName"] = category.Name;
            ViewData["CategoryId"] = id;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _categoryService.DeleteCategoryAsync(id).Wait();
            }
            catch (System.AggregateException ex) when (ex.InnerException is Common.Exceptions.ValidationException)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
                ViewData["CategoryId"] = id;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
