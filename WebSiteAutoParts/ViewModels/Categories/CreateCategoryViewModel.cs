using System.ComponentModel.DataAnnotations;

namespace WebSiteAutoParts.ViewModels.Categories
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Введите наименование категории")]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
    }
}