using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseRecord
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Exercise Type")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Range(0.1, 10000)]
        [Display(Name = "Weight (kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Sets", Description = "Number of sets")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Reps", Description = "Number of repetitions per set")]
        public int Reps { get; set; }

        public ExerciseType? ExerciseType { get; set; }

        public TrainingSession? TrainingSession { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
