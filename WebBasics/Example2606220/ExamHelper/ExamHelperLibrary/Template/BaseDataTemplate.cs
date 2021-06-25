namespace ExamHelperLibrary.Template
{
    using static Common.DataConstants;
    public abstract class BaseDataTemplate<Tkey>
    {
        public Tkey Id { get; set; }
    }
}