using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTOs
{
    public class CreateTrainingSessionDTO
    {
        [Required]
        [Display(Name = "Czas startu")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Czas zakończenia")]
        public DateTime EndTime { get; set; }
    }
}
