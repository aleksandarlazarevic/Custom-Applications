using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TemplateWebApp.Models.SellingItems;
using TemplateWebApp.ViewModels;

namespace TemplateWebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemController(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            ItemsListViewModel itemsListViewModel = new ItemsListViewModel();
            itemsListViewModel.Items = _itemRepository.Items;

            itemsListViewModel.CurrentCategory = "Cheese cakes";
            return View(itemsListViewModel);
        }
    }
}
