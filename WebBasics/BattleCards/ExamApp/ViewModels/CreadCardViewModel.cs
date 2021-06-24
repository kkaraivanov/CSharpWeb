namespace Exam.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreadCardViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(5, ErrorMessage = "The name can't by less than 5 characters.")]
        [MaxLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ImageUrl is required.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Keyword is required.")]
        public string Keyword { get; set; }

        [Required(ErrorMessage = "Attack is required.")]
        [Range(0, int.MaxValue)]
        public int Attack { get; set; }

        [Required(ErrorMessage = "Health is required.")]
        [Range(0, int.MaxValue)]
        public int Health { get; set; }
    }
}