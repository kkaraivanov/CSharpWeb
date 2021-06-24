namespace Exam.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(5, ErrorMessage = "The username can't by less than 5 characters.")]
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Incorrect Email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "The password can't by less than 6 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be longer than 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [MinLength(6, ErrorMessage = "The ConfirmPassword can't by less than 6 characters.")]
        [MaxLength(20, ErrorMessage = "ConfirmPassword cannot be longer than 20 characters.")]
        public string ConfirmPassword { get; set; }
    }
}