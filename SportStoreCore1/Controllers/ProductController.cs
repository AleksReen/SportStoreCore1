using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models.Interfaces;
using SportStoreCore1.Models.ViewModels;

namespace SportStoreCore1.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize { get; set; } = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo; 
        }

        public ViewResult List(int page = 1) {

            var result = new ProductsListViewModel()
            {
                Products = repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };

            return View(result);
        } 
    }
}