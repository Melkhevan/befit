using Microsoft.AspNetCore.Identity;

namespace BeFit.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }
}
