using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Ecommerce.Domain.Model.ViewModel;
using FileIO = System.IO.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string WEB_ROOT_PATH;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            WEB_ROOT_PATH = _webHostEnvironment.WebRootPath;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll("Category").ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var productView = new ProductView()
            {
                CategorySelectList = _categoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if (id is not null && id != 0)
            {
                productView.Product = _productRepository.Get(p => p.Id == id);
            }
            else
            {
                productView.Product = new Product();
            }


            return View(productView);
        }

        [HttpPost]
        public IActionResult Upsert(ProductView productView, IFormFile? file)
        {

            if (!ModelState.IsValid)
            {
                productView.CategorySelectList = _categoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                TempData["error"] = "Entry data wrong";
                return View(productView);
            }

            var fileName = "";
            try
            {
                if (file is not null && WEB_ROOT_PATH is not null)
                {
                    var productImgFolder = @"images/product";
                    var productPath = $@"{WEB_ROOT_PATH}/{productImgFolder}";

                    DeleteProductImage(productView.Product.ImageUrl);

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    using var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create);
                    file.CopyTo(fileStream);


                    productView.Product.ImageUrl = $@"{productImgFolder}/{fileName}";
                }

                if (productView.Product.Id == 0)
                {
                    _productRepository.Add(productView.Product);
                }
                else
                {
                    _productRepository.Update(productView.Product);
                }

                _productRepository.Save();

                TempData["success"] = "Product was created";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                DeleteProductImage(fileName);
                return View(productView);
            }

        }

        private void DeleteProductImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var currentImage = $@"{WEB_ROOT_PATH}/{imageUrl}";
                if (FileIO.Exists(currentImage))
                    FileIO.Delete(currentImage);
            }
        }

        [HttpGet("delete")]
        public IActionResult DeleteView(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var productToDelete = _productRepository.Get(p => p.Id == id);

            if (productToDelete is null)
                return NotFound();

            return View("Delete", productToDelete);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepository.GetAll("Category").ToList();
            return Json(new { data = products });
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var productToDelete = _productRepository.Get(p => p.Id == id);

            if (productToDelete is null)
                return Json(new { success = false, message = "Product not found" });

            DeleteProductImage(productToDelete.ImageUrl);

            _productRepository.Remove(productToDelete);
            _productRepository.Save();

            return Json(new { success = true, message = "Product deleted" });

        }
        #endregion
    }
}
