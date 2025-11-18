namespace BeFit.Models
{
    public class ExerciseStats
    {
        public int ExerciseTypeId { get; set; }
        public string? ExerciseName { get; set; }
        public int TimesPerformed { get; set; }
        public int TotalReps { get; set; }
        public decimal AverageWeight { get; set; }
        public decimal MaxWeight { get; set; }
    }
}
