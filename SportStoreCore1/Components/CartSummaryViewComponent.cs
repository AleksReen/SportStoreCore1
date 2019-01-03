using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models;

namespace SportStoreCore1.Components
{
    public class CartSummaryViewComponent: ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
