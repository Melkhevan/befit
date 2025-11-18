using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseRecord
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Range(0.1, 10000)]
        [Display(Name = "Waga (kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Serie", Description = "Liczba serii")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Powtórzenia", Description = "Liczba powtórzeń w serii")]
        public int Reps { get; set; }

        public ExerciseType? ExerciseType { get; set; }

        public TrainingSession? TrainingSession { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
