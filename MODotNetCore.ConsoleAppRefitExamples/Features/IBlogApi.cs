using MODotNetCore.ConsoleAppRefitExamples.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleAppRefitExamples.Features;

public interface IBlogApi
{
    [Get("/api/blog")]
    Task<List<BlogModel>> GetBlogs();

    [Get("/api/blog/{id}")]
    Task<BlogModel> GetBlog(int id);

    [Post("/api/blog")]
    Task<string> CreateBlog(BlogModel blog);

    [Put("/api/blog/{id}")]
    Task<string> UpdateBlog(int id, BlogModel blog);

    [Delete("/api/blog/{id}")]
    Task<string> DeleteBlog(int id);
}
