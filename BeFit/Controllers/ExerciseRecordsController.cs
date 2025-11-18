using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTOs;

namespace BeFit.Controllers
{
    [Authorize]
    public class ExerciseRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExerciseRecordsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id ?? string.Empty;
        }

        // GET: ExerciseRecords
        public async Task<IActionResult> Index()
        {
            var userId = await GetUserId();
            var userRecords = await _context.ExerciseRecords
                .Where(er => er.UserId == userId)
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .ToListAsync();
            return View(userRecords);
        }

        // GET: ExerciseRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = await GetUserId();
            var exerciseRecord = await _context.ExerciseRecords
                .Where(er => er.Id == id && er.UserId == userId)
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync();
            if (exerciseRecord == null)
            {
                return NotFound();
            }

            return View(exerciseRecord);
        }

        // GET: ExerciseRecords/Create
        public async Task<IActionResult> Create()
        {
            var userId = await GetUserId();
            var exerciseTypes = await _context.ExerciseTypes.ToListAsync();
            var userSessions = await _context.TrainingSessions
                .Where(ts => ts.UserId == userId)
                .ToListAsync();

            ViewData["ExerciseTypeId"] = new SelectList(exerciseTypes, "Id", "Name");
            
            var sessionsList = userSessions.Select(ts => new { ts.Id, Display = ts.StartTime.ToString("yyyy-MM-dd HH:mm") });
            ViewData["TrainingSessionId"] = new SelectList(sessionsList, "Id", "Display");
            
            return View();
        }

        // POST: ExerciseRecords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExerciseRecordDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetUserId();
                var exerciseRecord = new ExerciseRecord
                {
                    ExerciseTypeId = createDTO.ExerciseTypeId,
                    TrainingSessionId = createDTO.TrainingSessionId,
                    Weight = createDTO.Weight,
                    Sets = createDTO.Sets,
                    Reps = createDTO.Reps,
                    UserId = userId
                };
                _context.Add(exerciseRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var userId2 = await GetUserId();
            var exerciseTypes = await _context.ExerciseTypes.ToListAsync();
            var userSessions = await _context.TrainingSessions
                .Where(ts => ts.UserId == userId2)
                .ToListAsync();

            ViewData["ExerciseTypeId"] = new SelectList(exerciseTypes, "Id", "Name", createDTO.ExerciseTypeId);
            
            var sessionsList = userSessions.Select(ts => new { ts.Id, Display = ts.StartTime.ToString("yyyy-MM-dd HH:mm") });
            ViewData["TrainingSessionId"] = new SelectList(sessionsList, "Id", "Display", createDTO.TrainingSessionId);
            
            return View(createDTO);
        }

        // GET: ExerciseRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = await GetUserId();
            var exerciseRecord = await _context.ExerciseRecords.FindAsync(id);
            if (exerciseRecord == null)
            {
                return NotFound();
            }

            if (exerciseRecord.UserId != userId)
            {
                return NotFound();
            }

            var exerciseTypes = await _context.ExerciseTypes.ToListAsync();
            var userSessions = await _context.TrainingSessions
                .Where(ts => ts.UserId == userId)
                .ToListAsync();

            ViewData["ExerciseTypeId"] = new SelectList(exerciseTypes, "Id", "Name", exerciseRecord.ExerciseTypeId);
            
            var sessionsList = userSessions.Select(ts => new { ts.Id, Display = ts.StartTime.ToString("yyyy-MM-dd HH:mm") });
            ViewData["TrainingSessionId"] = new SelectList(sessionsList, "Id", "Display", exerciseRecord.TrainingSessionId);
            
            return View(exerciseRecord);
        }

        // POST: ExerciseRecords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExerciseTypeId,TrainingSessionId,Weight,Sets,Reps")] ExerciseRecord exerciseRecord)
        {
            if (id != exerciseRecord.Id)
            {
                return NotFound();
            }

            var userId = await GetUserId();
            var existingRecord = await _context.ExerciseRecords.FindAsync(id);
            if (existingRecord == null || existingRecord.UserId != userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingRecord.ExerciseTypeId = exerciseRecord.ExerciseTypeId;
                    existingRecord.TrainingSessionId = exerciseRecord.TrainingSessionId;
                    existingRecord.Weight = exerciseRecord.Weight;
                    existingRecord.Sets = exerciseRecord.Sets;
                    existingRecord.Reps = exerciseRecord.Reps;
                    _context.Update(existingRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseRecordExists(exerciseRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var exerciseTypes = await _context.ExerciseTypes.ToListAsync();
            var userSessions = await _context.TrainingSessions
                .Where(ts => ts.UserId == userId)
                .ToListAsync();

            ViewData["ExerciseTypeId"] = new SelectList(exerciseTypes, "Id", "Name", exerciseRecord.ExerciseTypeId);
            
            var sessionsList = userSessions.Select(ts => new { ts.Id, Display = ts.StartTime.ToString("yyyy-MM-dd HH:mm") });
            ViewData["TrainingSessionId"] = new SelectList(sessionsList, "Id", "Display", exerciseRecord.TrainingSessionId);
            
            return View(exerciseRecord);
        }

        // GET: ExerciseRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = await GetUserId();
            var exerciseRecord = await _context.ExerciseRecords
                .Where(er => er.Id == id && er.UserId == userId)
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync();
            if (exerciseRecord == null)
            {
                return NotFound();
            }

            return View(exerciseRecord);
        }

        // POST: ExerciseRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = await GetUserId();
            var exerciseRecord = await _context.ExerciseRecords.FindAsync(id);
            if (exerciseRecord != null && exerciseRecord.UserId == userId)
            {
                _context.ExerciseRecords.Remove(exerciseRecord);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseRecordExists(int id)
        {
            return _context.ExerciseRecords.Any(e => e.Id == id);
        }
    }
}
