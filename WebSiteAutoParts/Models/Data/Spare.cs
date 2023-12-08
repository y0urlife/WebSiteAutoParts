using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteAutoParts.Models.Data
{
    public class Spare
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }


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


        // навигационные свойства
        [Display(Name = "Категория")]
        [ForeignKey("IdCategory")]
        public Category Category { get; set; }
    }
}