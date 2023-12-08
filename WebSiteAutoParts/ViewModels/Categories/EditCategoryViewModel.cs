using System.ComponentModel.DataAnnotations;

namespace WebSiteAutoParts.ViewModels.Categories
{
    public class EditCategoryViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите наименование категории")]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
    }
}