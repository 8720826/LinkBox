using LinkBox.Authorizations;
using LinkBox.Contexts;
using LinkBox.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace LinkBox.Pages.Link
{
    [UserAuthorize]
    public class IndexModel : PageModel
    {

        public int PageIndex { get; set; }


        public int PageSize { get; set; } = 100;

 
        public PaginatedList<ListLinkDto> Links { get; set; }


        private readonly LinkboxDbContext _db;

        public IndexModel(LinkboxDbContext db)
        {
            _db = db;
        }
        public void OnGet(int pageIndex)
        {
            PageIndex = pageIndex;
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            if (PageSize < 10)
            {
                PageSize = 10;
            }

            var config = new TypeAdapterConfig();
            config.ForType<LinkEntity, ListLinkDto>()
  .Map(dest => dest.CategoryName, src => (src.Category == null ? "未分类" : src.Category.Name));


            var query = _db.Links.Include(x=>x.Category).OrderByDescending(x => x.Id).ProjectToType<ListLinkDto>(config);

          

            Links = PaginatedList<ListLinkDto>.Create(query.AsNoTracking(), PageIndex, PageSize);
        }
    }
}
