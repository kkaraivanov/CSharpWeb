namespace ExamHelperLibrary.Template
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants;

    public abstract class BaseDataTemplate<Tkey>
    {
        [Key]
        public Tkey Id { get; set; }
    }
}