using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateWebApp.Models.SellingItems;

namespace TemplateWebApp.ViewModels
{
    public class ItemsListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public string CurrentCategory { get; set; }

    }
}
