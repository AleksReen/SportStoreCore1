using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models.Interfaces;

namespace SportStoreCore1.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index() => View(repository.Products);

        public IActionResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p?.ProductID == productId));
    }
}