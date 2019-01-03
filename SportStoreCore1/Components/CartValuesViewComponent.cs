using Microsoft.AspNetCore.Mvc;
using SportStoreCore1.Models;

namespace SportStoreCore1.Components
{
    public class CartValuesViewComponent: ViewComponent
    {
        private Cart cart;

        public CartValuesViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
