using Microsoft.AspNetCore.Mvc;
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
            TempData["Success"] = "Student saved successfully.";

            // Redirect to GET Add to avoid form resubmission and display the alert
            return RedirectToAction("Add");
        }
    }
}
