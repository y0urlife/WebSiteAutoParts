using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebSiteAutoParts.Models.Data
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название категории")]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        //Навигационные свойства
        public ICollection<Spare> Spares { get; set; }

    }
}