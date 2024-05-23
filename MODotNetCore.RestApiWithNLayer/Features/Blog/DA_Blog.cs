using MODotNetCore.RestApiWithNLayer.Db;
using System.Reflection.Metadata.Ecma335;

namespace MODotNetCore.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _appDbContext;

        public DA_Blog()
        {
            _appDbContext = new AppDbContext();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _appDbContext.Blogs.ToList();
            return lst;
        }

        public BlogModel? GetBlog(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }

        public int CreateBlog(BlogModel reqModel)
        {
            _appDbContext.Blogs.Add(reqModel);
            var res = _appDbContext.SaveChanges();
            return res;
        }
        public int UpdateBlog(BlogModel reqModel)
        {
            _appDbContext.Blogs.Add(reqModel);
            var res = _appDbContext.SaveChanges();
            return res;
        }
    }
}
