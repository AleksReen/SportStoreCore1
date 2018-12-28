using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models.Interfaces;

namespace SportStoreCore1.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo; 
        }

        public ViewResult List() => View(repository.Products);
        
    }
}