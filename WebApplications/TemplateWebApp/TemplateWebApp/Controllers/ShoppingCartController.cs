using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TemplateWebApp.Models.SellingItems;
using TemplateWebApp.ViewModels;

namespace TemplateWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IItemRepository itemRepository, ShoppingCart shoppingCart)
        {
            _itemRepository = _itemRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int itemId)
        {
            var selectedPie = _itemRepository.Items.FirstOrDefault(p => p.ItemId == itemId);

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _itemRepository.Items.FirstOrDefault(p => p.ItemId == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}