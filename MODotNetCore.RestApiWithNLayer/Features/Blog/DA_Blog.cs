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
        public BlogResponseModel UpdateBlog(BlogModel reqModel)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == reqModel.BlogId);
            if (item is null)
            {
                model.ErrorMessage = "No data";
                goto Results;
            }
            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            _appDbContext.Blogs.Update(reqModel);
            var res = _appDbContext.SaveChanges();
            model.SuccessMessage = res > 0 ? "Update Successful" : "Update Failed";

            Results:
            return model;
        }

        public BlogResponseModel DeleteBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model.ErrorMessage = "No data";
                goto Results;
            }

            _appDbContext.Blogs.Remove(item);
            var res = _appDbContext.SaveChanges();
            model.SuccessMessage = res > 0 ? "Delete Successful" : "Delete Failed";

            Results:
            return model;
        }
    }
}
