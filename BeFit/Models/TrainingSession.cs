using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Start Time", Description = "When the training session started")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time", Description = "When the training session ended")]
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
