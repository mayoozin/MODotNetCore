using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MODotNetCore.RestApi.EfDbContext;
using MODotNetCore.RestApi.Model;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogController()
        {
            _db = new AppDbContext();
        }

        // GET: api/<BlogController>
        [HttpGet]
        public IActionResult Get()
        {
            List<BlogModel> blogs = _db.Blogs.AsNoTracking()
                .Select(x => new BlogModel()
                {
                    BlogId = x.BlogId,
                    BlogTitle = x.BlogTitle,
                    BlogAuthor = x.BlogAuthor,
                    BlogContent = x.BlogContent
                })
                .ToList();
            return Ok(blogs);

            //var item = _db.Blogs.AsNoTracking().ToList();
            //return Ok(item);
        }

        //[HttpGet]
        //public BlogModel? GetDataById(int blogId)
        //{
        //    var blogs = _db.Blogs.AsNoTracking()
        //        .Select(x => new BlogModel()
        //        {
        //            BlogId = x.BlogId,
        //            BlogTitle = x.BlogTitle,
        //            BlogAuthor = x.BlogAuthor,
        //            BlogContent = x.BlogContent
        //        })
        //        .Where(x => x.BlogId == blogId)
        //        .FirstOrDefault();
        //    return blogs;
        //}

        [HttpPost]
        public IActionResult Create(BlogRequestModel reqModel)
        {
            _db.Add(reqModel);
            var res = _db.SaveChanges();
            string message = res > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            string message = string.Empty;
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                message = "Data Not Found!";
                goto Results;
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var res = _db.SaveChanges();
            message = res > 0 ? "Update Successful" : "Update Failed";
            Results:
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel reqModel)
        {
            string message = string.Empty;
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                message = "Data Not Found!";
                goto Results;
            }
            reqModel.BlogId = id;
            var res = _db.SaveChanges();
            message = res > 0 ? "Update Successful" : "Update Failed";
            Results:
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string message = string.Empty;
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                message = "Data Not Found!";
                goto Results;
            }
            _db.Remove(item);
            var res = _db.SaveChanges();
            message = res > 0 ? "Delete Successful" : "Delete Failed";

            Results:
            return Ok(message);
        }
    }
}
