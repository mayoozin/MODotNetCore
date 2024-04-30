using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.Commons.Queries
{
    internal class CommonQuery
    {
        public static string SelectQuery = "SELECT * FROM Tbl_Blog";

        public static string CreateQuery = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
                    (@BlogTitle,
                    @BlogAuthor,
                    @BlogContent)";

        public static string UpdateQuery = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";

        public static string DeleteQuery = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@BlogId";

        public static string GetDataById = @"SELECT * FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@blogId";

        public static string UpdateBatch = @"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";
    }
}
