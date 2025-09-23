using Web.Sevices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.Sevices;

namespace Web.ViewModel

{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var count = _cartService.GetCartItems()?.Sum(x => x.Quantity) ?? 0;

            
            return new HtmlContentViewComponentResult(
                new Microsoft.AspNetCore.Html.HtmlString(count.ToString())
            );
        }
    }
}
