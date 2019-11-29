using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemplateWebApp.Data;

namespace TemplateWebApp.Models.SellingItems
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _appDbContext;

        public ItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Item> Items
        {
            get
            {
                return _appDbContext.Items.Include(c => c.Category);
            }
        }

        public IEnumerable<Item> ItemsOfTheWeek
        {
            get
            {
                return _appDbContext.Items.Include(c => c.Category).Where(p => p.IsItemOfTheWeek == true);
            }
        }
        public Item GetItemById(int itemId)
        {
            return _appDbContext.Items.FirstOrDefault(p => p.ItemId == itemId);
        }
    }
}
