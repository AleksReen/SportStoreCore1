using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IProductRepository productRepository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            productRepository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(productRepository.Products
                .Select(prod => prod.Category)
                .Distinct()
                .OrderBy(prod => prod));
        }     
    }
}
