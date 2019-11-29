using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.Data.OData.Atom;
using Microsoft.Extensions.DependencyInjection;
using TemplateWebApp.Data;
using TemplateWebApp.Models.SellingItems;

namespace TemplateWebApp.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories.Select(c => c.Value));
                }

              
                    context.AddRange
                    (
                        new Item { CategoryId = 1, Name = "Apple Pie", Price = 12.95M, ShortDescription = ""},
                        new Item { CategoryId = 2, Name = "Blueberry Cheese Cake", Price = 18.95M, ShortDescription = ""},
                        new Item { CategoryId = 3, Name = "Cheese Cake", Price = 18.95M, ShortDescription = ""},
                        new Item { CategoryId = 4, Name = "Cherry Pie", Price = 15.95M, ShortDescription = ""},
                        new Item { CategoryId = 5, Name = "Christmas Apple Pie", Price = 13.95M, ShortDescription = ""},
                        new Item { CategoryId = 6, Name = "Cranberry Pie", Price = 17.95M, ShortDescription = ""},
                        new Item { CategoryId = 7, Name = "Peach Pie", Price = 15.95M, ShortDescription = ""},
                        new Item { CategoryId = 8, Name = "Pumpkin Pie", Price = 12.95M, ShortDescription = ""},
                        new Item { CategoryId = 9, Name = "Rhubarb Pie", Price = 15.95M, ShortDescription = ""},
                        new Item { CategoryId = 10, Name = "Strawberry Pie", Price = 15.95M, ShortDescription = ""},
                        new Item { CategoryId = 11, Name = "Strawberry Cheese Cake", Price = 18.95M, ShortDescription = ""}

                    );
                

                context.SaveChanges();
            }
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category {CategoryName = "Fruit Pies"},
                        new Category {CategoryName = "Cheese Cakes"},
                        new Category {CategoryName = "Seasonal Pies"}
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}
