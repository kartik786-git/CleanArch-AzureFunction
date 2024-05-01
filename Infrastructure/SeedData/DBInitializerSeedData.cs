using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class DBInitializerSeedData
    {
        public static void InitializeDatabase(BlogDbContext blogDbContext)
        {
            if (!blogDbContext.Blogs.Any())
            {
                var blogs = new Blog[]
               {


            new Blog
            {
                Name = "Architecture",
                Description = "The Microsoft . NET architecture is the programming model for the . NET platform",

            },
             new Blog
            {
                Name = "Blazor",
                Description = "Blazor is a feature of ASP.NET for building interactive web UIs using C# instead of JavaScript",

            },
             new Blog
            {
                Name = "C#",
                Description = "C# (C-sharp) is an open-source, cross-platform, object-oriented programming language created by Microsoft for the .NET developer platform",

            }
           };
                blogDbContext.Blogs.AddRangeAsync(blogs);
                blogDbContext.SaveChanges();
            }

            if (!blogDbContext.Authors.Any())
            {
                var Authors = new Author[]
                {
                  new Author
            {
                Name ="Jhon",
                Email = "Jhon@gmail.com"
            },
                  new Author
            {
                Name ="kartik",
                Email = "Kartik@gmail.com"
            },
                   new Author
            {
                Name ="Kkp",
                Email = "Kkp@gmail.com"
            }

                };

                blogDbContext.Authors.AddRangeAsync(Authors);
                blogDbContext.SaveChanges();
            }
        }
    }
}
