using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Czas startu", Description = "Kiedy rozpoczęła się sesja treningowa")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Czas zakończenia", Description = "Kiedy zakończyła się sesja treningowa")]
        public DateTime EndTime { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        public ICollection<ExerciseRecord> ExerciseRecords { get; set; } = new List<ExerciseRecord>();

        public bool IsValid()
        {
            return StartTime <= EndTime;
        }
    }
}
