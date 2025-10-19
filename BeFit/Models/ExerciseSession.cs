namespace BeFit.Models
{
    public class ExerciseSession
    {
        public int Id { get; set; }

        public int ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; }

        public int TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; }

        public double Weight { get; set; }  // Obciążenie
        public int Sets { get; set; }       // Liczba serii
        public int Repetitions { get; set; } // Liczba powtórzeń w serii
    }
}
