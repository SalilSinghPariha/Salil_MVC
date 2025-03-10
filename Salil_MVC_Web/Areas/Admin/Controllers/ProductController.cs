using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Salil_MVC.DataAccess.Repository.IRepository;
using Salil_MVC.Models;
using Salil_MVC.Models.ViewModel;

namespace Salil_MVC_Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWorks unitOfWorks,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWorks = unitOfWorks;
            _hostEnvironment = webHostEnvironment;


        }
        public IActionResult Index()
        {
            List<Product> products = (List<Product>)_unitOfWorks.ProductRepository.GetAll();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> categoryList = _unitOfWorks.CategoryRepository.GetAll().
            //   Select(u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()

            //   });
            // ViewBag.CategoryList = categoryList;

            ProductVM productVM = new ProductVM()
            {
                categoryList = _unitOfWorks.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                product = new()

            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }

            else
            {
                productVM.product = _unitOfWorks.ProductRepository.GetFirstOrDefault(c => c.Id == id);
                return View(productVM);
            }
           
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (productVM.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVM.product.ImageUrl = @"\images\products\" + fileName + extension;

                }
                if (productVM.product.Id == 0)
                {
                    _unitOfWorks.ProductRepository.Add(productVM.product);
                }

                else
                {
                    _unitOfWorks.ProductRepository.Update(productVM.product);
                }
                
                _unitOfWorks.Save();
                TempData["Success"] = "Product created succesfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error occured while creating category";
            return View(productVM);
        }

        public IActionResult Remove(int? id)
        {
            //asp-route-categoryid="@item.Id" this is from view which will pass here once we clikc edit and
            //like this we can do it directly in .net core
            if (id == null | id == 0)
            {
                return NotFound();
            }

            Product product = _unitOfWorks.ProductRepository.GetFirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Remove(Product product)
        {
            var prdFromDb = _unitOfWorks.ProductRepository.GetFirstOrDefault(c => c.Id == product.Id);
            if (prdFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWorks.ProductRepository.Remove(prdFromDb);
                _unitOfWorks.Save();
                TempData["Success"] = "Product deleted succesfully";
                return RedirectToAction("Index");

            }

        }

    }
}
