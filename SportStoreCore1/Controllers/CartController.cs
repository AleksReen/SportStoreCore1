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

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View ( new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = repository.Products.Where(p => p.ProductID == productId).FirstOrDefault();

            if (product != null)
            {
                var cart = GetCart();
                cart.AddOrUpdateCartLine(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = repository.Products.Where(p => p.ProductID == productId).FirstOrDefault();

            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }
    }
}