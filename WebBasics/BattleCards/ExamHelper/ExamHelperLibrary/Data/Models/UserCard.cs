namespace ExamHelperLibrary.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserCard
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Card))]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
    }
}