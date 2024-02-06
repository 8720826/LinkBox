using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LinkBox.Pages.Link
{
    public class ImportLinkDto
    {
        [Display(Name = "分类")]
        public int CategoryId { get; set; }


        [Display(Name = "书签文件")]
        [BindProperty]
        public IFormFile BookmarkFile { get; set; }


        [Display(Name = "立即更新页面")]
        public bool IsCompileImmediately { get; set; }
    }
}
