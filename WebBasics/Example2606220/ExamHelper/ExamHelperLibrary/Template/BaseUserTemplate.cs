namespace ExamHelperLibrary.Template
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants;

    public class BaseUserTemplate<Tkey> : BaseDataTemplate<Tkey>
    {
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}