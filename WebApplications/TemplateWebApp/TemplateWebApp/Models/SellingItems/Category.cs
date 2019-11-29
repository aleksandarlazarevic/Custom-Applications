using System.Collections.Generic;

namespace TemplateWebApp.Models.SellingItems
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
}