using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Exercise Name", Description = "The name of the exercise type")]
        public string Name { get; set; } = string.Empty;
    }
}
