using Microsoft.AspNetCore.Mvc;
using Salil_MVC.DataAccess.Data;
using Salil_MVC.DataAccess.Repository.IRepository;
using Salil_MVC.Models;

namespace Salil_MVC_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorks _unitOfWorks;
        public CategoryController(IUnitOfWorks unitOfWorks)
        {
          _unitOfWorks = unitOfWorks;
            
        }
        public IActionResult Index()
        {
            List<Category> category= (List<Category>)_unitOfWorks.CategoryRepository.GetAll();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorks.CategoryRepository.Add(category);
                _unitOfWorks.Save();
                TempData["Success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error occured while creating category";
            return View();
        }

        public IActionResult Update(int? id)
        {
            //asp-route-categoryid="@item.Id" this is from view which will pass here once we clikc edit and
            //like this we can do it directly in .net core
            if (id==null|id==0) 
            { 
                return NotFound();
            }

            Category category = _unitOfWorks.CategoryRepository.GetFirstOrDefault(c => c.Id==id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorks.CategoryRepository.Update(category);
                _unitOfWorks.Save();
                TempData["Success"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error while updating..";
            return View();
        }

        public IActionResult Remove(int? id)
        {
            //asp-route-categoryid="@item.Id" this is from view which will pass here once we clikc edit and
            //like this we can do it directly in .net core
            if (id == null | id == 0)
            {
                return NotFound();
            }

            Category category = _unitOfWorks.CategoryRepository.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Remove(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorks.CategoryRepository.Remove(category);
                _unitOfWorks.Save();
                TempData["Success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            TempData["Success"] = "Error occured while removing..";
            return View();
        }

    }
}
