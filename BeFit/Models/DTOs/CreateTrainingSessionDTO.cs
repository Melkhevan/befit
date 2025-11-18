using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class CreateTrainingSessionDTO
    {
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
    }
}
