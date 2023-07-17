using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //include database connection
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ACTIONS
        //Retrieve Data from categories table and store it on List
        public IActionResult Index()
        {
            List<Category> objCategory = _db.Categories.ToList();
            return View(objCategory);
        }

        //Add record|Create View
        public IActionResult Create()
        {
            return View();
        }

        //Catch Method Post from input
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Check if the
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }

            //Go to Model Category and Check for validation
            if (ModelState.IsValid)
            {
                //Insert to table
                _db.Categories.Add(obj);
                //Save to table
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Update|Edit View
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //3 Ways to Retrieve one record from database 
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Catch Method Post from input
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            //Go to Model Category and Check for validation
            if (ModelState.IsValid)
            {
                //Update to table
                _db.Categories.Update(obj);
                //Save to table
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Delete record
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Retrieve one record from database 
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Catch Method Post from input
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            //Delete using Id
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
