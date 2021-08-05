using CrudApplication.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Models.Student> objList = _db.Student;
            return View(objList);
        }

        //Get-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Student std)
        {
            if (ModelState.IsValid)
            {
                _db.Student.Add(std);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(std);
        }

        //Get-Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            var student = _db.Student.Find(id);
            if (student == null) 
            {
                return NotFound();
            }

            return View(student);
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Student std) 
        {
            if (ModelState.IsValid)
            {
                _db.Student.Update(std);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(std);
        }

        //Get-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var student = _db.Student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var student = _db.Student.Find(id);
            if (student != null) 
            {
                _db.Student.Remove(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
            
            
        }

    }
}
