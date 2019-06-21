using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Submissions = new HashSet<Submission>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}