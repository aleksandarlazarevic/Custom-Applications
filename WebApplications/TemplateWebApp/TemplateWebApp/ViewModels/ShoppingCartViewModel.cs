using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateWebApp.Models.SellingItems;

namespace TemplateWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
