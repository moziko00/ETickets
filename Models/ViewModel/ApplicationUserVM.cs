using System.ComponentModel.DataAnnotations;

namespace ETickets.Models.ViewModel
{
    public class ApplicationUserVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 20 chars")]
        public string Name {get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }


    }
}
