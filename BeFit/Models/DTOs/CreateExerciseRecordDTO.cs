using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class CreateExerciseRecordDTO
    {
        [Required]
        [Display(Name = "Exercise Type")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }

        [Required]
        [Range(0.1, 10000)]
        [Display(Name = "Weight (kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Sets")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Reps")]
        public int Reps { get; set; }
    }
}
