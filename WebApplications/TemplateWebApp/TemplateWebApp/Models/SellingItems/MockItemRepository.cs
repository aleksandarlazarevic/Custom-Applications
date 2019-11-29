using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateWebApp.Models.SellingItems
{
    public class MockItemRepository: IItemRepository
    {

        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Item> Items
        {
            get
            {
                return new List<Item>
                {
                    new Item {ItemId = 1, Name = "Strawberry pie", Price = 15.95M, ShortDescription = "Lorem Ipsum"},
                    new Item {ItemId = 2, Name = "Cheese cake", Price = 18.95M, ShortDescription = "Lorem Ipsum"},
                    new Item {ItemId = 3, Name = "Rhubarb pie", Price = 15.95M, ShortDescription = "Lorem Ipsum"},
                    new Item {ItemId = 4, Name = "Pumpkin pie", Price = 12.95M, ShortDescription = "Lorem Ipsum"}
                };
            }
        }
        public IEnumerable<Item> ItemsOfTheWeek { get; }
        public Item GetItemById(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
