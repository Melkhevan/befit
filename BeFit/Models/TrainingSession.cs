using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        // Walidacja: data zakończenia nie może być wcześniejsza niż data rozpoczęcia
        public bool Validate()
        {
            return StartTime <= EndTime;
        }
    }
}
