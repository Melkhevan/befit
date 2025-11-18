using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class CreateExerciseRecordDTO
    {
        [Required]
        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }

        [Required]
        [Range(0.1, 10000)]
        [Display(Name = "Waga (kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Serie")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Powtórzenia")]
        public int Reps { get; set; }
    }
}
