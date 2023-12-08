using System.ComponentModel.DataAnnotations;

namespace WebSiteAutoParts.ViewModels.Spares
{
    public class CreateSpareViewModel
    {
        [Required]
        [Display(Name = "Категория")]
        public short IdCategory { get; set; }

        [Required(ErrorMessage = "Введите название запчасти")]
        [Display(Name = "Запчасть")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание запчасти")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Наличие")]
        [Range(1, 1000, ErrorMessage = "Введите наличие(кол-во) на складе")]
        public int StockAvability { get; set; }

        [Required(ErrorMessage = "Введите артикул")]
        [Display(Name = "Артикул")]
        public string VendorCode { get; set; }
    }
}