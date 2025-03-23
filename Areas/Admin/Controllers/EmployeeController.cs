using Microsoft.AspNetCore.Mvc;
using TechLife.Data;
using TechLife.Models;
using TechLife.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using TechLife.Models.DTOs;
using Microsoft.EntityFrameworkCore;



namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var username = GenerateUsername(model.Name);
                var password = GeneratePassword();

                var employee = new IdentityUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true,
                   
                };

                var result = await _userManager.CreateAsync(employee, password);
                if (result.Succeeded)
                {
                    // Assign the role to the employee
                    if (model.Role == SD.Role_MaintEmployee)
                    {
                        await _userManager.AddToRoleAsync(employee, SD.Role_MaintEmployee);
                    }
                    else if (model.Role == SD.Role_SimEmployee)
                    {
                        await _userManager.AddToRoleAsync(employee, SD.Role_SimEmployee);
                    }

                    // Save employee details to the database
                    var newEmployee = new Employee
                    {
                        Name = model.Name,
                        Username = username,
                        Password = password,
                        Role = model.Role,
                        Email = model.Email,
                        Contact = model.Contact,
                        Location = model.Location,
                        WorkLocation = model.WorkLocation,
                        DateOfBirth = model.DateOfBirth,
                        JoiningDate = model.JoiningDate,
                        FatherName = model.FatherName,
                        Salary = model.Salary,
                        ImageProfile = model.ImageProfile,
                    };
                    _db.Employees.Add(newEmployee);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("EmployeeList");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        public IActionResult EmployeeList()
        {
            var employees = _db.Employees.ToList();
            return View(employees);
        }
        private string GenerateUsername(string name)
        {
            string randomPart = new Random().Next(100, 999).ToString();
            return name.ToLower().Replace(" ", ".") + randomPart + "@TechLife.com";
        }
        private string GeneratePassword()
        {
            // You can customize the password generation logic as needed
            return "Emp" + new Random().Next(100000, 999999) + "!";
        }
    }
}

