using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id ?? string.Empty;
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var userId = await GetUserId();
            DateTime fourWeeksAgo = DateTime.Now.AddDays(-28);
            
            List<ExerciseStats> stats = new List<ExerciseStats>();

            var exerciseTypes = await _context.ExerciseTypes.ToListAsync();

            foreach (var exerciseType in exerciseTypes)
            {
                var records = await _context.ExerciseRecords
                    .Where(er => er.ExerciseTypeId == exerciseType.Id && er.UserId == userId)
                    .Include(er => er.TrainingSession)
                    .ToListAsync();

                var recentRecords = records.Where(r => r.TrainingSession!.StartTime >= fourWeeksAgo).ToList();

                if (recentRecords.Count > 0)
                {
                    int totalReps = 0;
                    decimal totalWeight = 0;
                    decimal maxWeight = 0;

                    foreach (var record in recentRecords)
                    {
                        totalReps += record.Sets * record.Reps;
                        totalWeight += record.Weight;
                        if (record.Weight > maxWeight)
                        {
                            maxWeight = record.Weight;
                        }
                    }

                    decimal averageWeight = totalWeight / recentRecords.Count;

                    ExerciseStats stat = new ExerciseStats
                    {
                        ExerciseTypeId = exerciseType.Id,
                        ExerciseName = exerciseType.Name,
                        TimesPerformed = recentRecords.Count,
                        TotalReps = totalReps,
                        AverageWeight = averageWeight,
                        MaxWeight = maxWeight
                    };

                    stats.Add(stat);
                }
            }

            return View(stats);
        }
    }
}
