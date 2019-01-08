using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models;
using SportStoreCore1.Models.Interfaces;

namespace SportStoreCore1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index() => 
            View(repository.Products);

        public IActionResult Edit(int productId) => 
            View(repository.Products.FirstOrDefault(p => p?.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted.";
            }

            return RedirectToAction("Index");
        }

        public ViewResult Create() =>
            View("Edit", new Product());
    }

}