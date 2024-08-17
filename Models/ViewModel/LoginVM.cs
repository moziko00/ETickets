using System.ComponentModel.DataAnnotations;

namespace ETickets.Models.ViewModel
{
    public class LoginVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; }
    }
}
