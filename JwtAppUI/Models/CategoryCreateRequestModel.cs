using System.ComponentModel.DataAnnotations;

namespace JwtAppUI.Models
{
    public class CategoryCreateRequestModel
    {
        [Required(ErrorMessage = "Kategori Adı Boş Olamaz")]
        public string Definition { get; set; }
    }
}
