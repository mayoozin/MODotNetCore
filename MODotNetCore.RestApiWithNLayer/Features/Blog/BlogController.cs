using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MODotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_Blog;

        public BlogController()
        {
            _bl_Blog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<BlogModel> blogs = _bl_Blog.GetBlogs()
                .Select(x => new BlogModel()
                {
                    BlogId = x.BlogId,
                    BlogTitle = x.BlogTitle,
                    BlogAuthor = x.BlogAuthor,
                    BlogContent = x.BlogContent
                })
                .ToList();
            return Ok(blogs);

        }

        [HttpPost]
        public IActionResult Create(BlogModel reqModel)
        {
            var res = _bl_Blog.CreateBlog(reqModel);
            string message = res > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(BlogModel reqModel)
        {
            var res = _bl_Blog.UpdateBlog(reqModel);
            return Ok(res);
        }

        //[HttpPatch("{id}")]
        //public IActionResult Patch(int id, BlogModel reqModel)
        //{
        //    string message = string.Empty;
        //    var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        //    if (item is null)
        //    {
        //        message = "Data Not Found!";
        //        goto Results;
        //    }
        //    reqModel.BlogId = id;
        //    var res = _db.SaveChanges();
        //    message = res > 0 ? "Update Successful" : "Update Failed";
        //    Results:
        //    return Ok(message);
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _bl_Blog.DeleteBlog(id);
            return Ok(res);
        }
    }
}
