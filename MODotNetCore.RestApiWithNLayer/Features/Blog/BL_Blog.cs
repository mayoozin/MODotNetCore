using System.Security.Cryptography.Xml;

namespace MODotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _blog;

        public BL_Blog()
        {
            _blog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _blog.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var lst = _blog.GetBlog(id);
            return lst;
        }

        public int CreateBlog(BlogModel reqModel)
        {
            var item = _blog.CreateBlog(reqModel);
            return item;
        }

        public BlogResponseModel UpdateBlog(BlogModel reqModel)
        {
            BlogResponseModel model = _blog.UpdateBlog(reqModel);
            return model;
        }

        public BlogResponseModel DeleteBlog(int id)
        {
            BlogResponseModel model = _blog.DeleteBlog(id);
            return model;
        }
    }
}
