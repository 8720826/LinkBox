using Home.Migrator;
using LinkBox.Contexts;
using LinkBox.Entities;
using LinkBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkBox.Pages
{
    public class IndexModel : BasePageModel
	{

		private readonly ILogger<IndexModel> _logger;
    
        public IndexModel(ILogger<IndexModel> logger) : base()
		{
            _logger = logger;
        }

        public void OnGet()
        {


		}
    }
}
