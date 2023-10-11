using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bu Alanın Doldurulması Zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-mail giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu Alanın Doldurulması Zorunludur!")]
        public string Password { get; set; }
        public bool OturumuAcikTut { get; set; }
    }
}
