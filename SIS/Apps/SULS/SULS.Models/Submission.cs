namespace SULS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Submission
    {
        public Submission()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(800)]
        public string Code { get; set; }
       
        [Range(0,300)]
        public int AchievedResult  { get; set; }

        public DateTime CreatedOn { get; set; }


        public string ProblemId { get; set; }
        public Problem Problem { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}