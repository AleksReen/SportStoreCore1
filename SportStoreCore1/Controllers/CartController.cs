using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Infrastructure;
using SportStoreCore1.Models;
using SportStoreCore1.Models.Interfaces;
using SportStoreCore1.Models.ViewModels;

namespace SportStoreCore1.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View ( new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.Where(p => p.ProductID == productId).FirstOrDefault();

            if (product != null)
            {          
                cart.AddOrUpdateCartLine(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.Where(p => p.ProductID == productId).FirstOrDefault();

            if (product != null)
            {
                cart.RemoveLine(product);              
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}