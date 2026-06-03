using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Data;
using StudentPortal.web.Models;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbcontext dbcontext;

        public StudentController(ApplicationDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                IsActive = viewModel.IsActive
            };
            await dbcontext.Students.AddAsync(student);
            await dbcontext.SaveChangesAsync();

            // Use TempData to show a one-time success alert after redirect
            TempData["Success"] = "Student saved successfully in Database.";

            // Redirect to GET Add to avoid form resubmission and display the alert
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbcontext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbcontext.Students.FindAsync(id);

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var existingStudent = await dbcontext.Students.FindAsync(student.Id);
            if (existingStudent is null)
            {
                return NotFound();
            }
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.PhoneNumber = student.PhoneNumber;
            existingStudent.IsActive = student.IsActive;
            await dbcontext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbcontext.Students.FindAsync(id);
            if (student is not null)
            {
                dbcontext.Students.Remove(student);
                await dbcontext.SaveChangesAsync();
               
            }
            return RedirectToAction("List", "Student");
        }
    }
}
