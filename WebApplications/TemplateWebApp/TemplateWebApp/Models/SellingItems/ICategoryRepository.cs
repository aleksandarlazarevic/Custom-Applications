using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateWebApp.Models.SellingItems
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
