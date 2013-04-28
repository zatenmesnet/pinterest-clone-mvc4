using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using System.Drawing;
using System.Configuration;

namespace TT.Models
{
    public class DBModel
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Post> GetPosts()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var posts = conn.Query<Post>("Select * from posts");
                return posts;
            }
        }

        public IEnumerable<Post> GetPosts(int i)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var posts = conn.Query<Post>("Select * from posts where owner = @id", new { id = i } );
                return posts;
            }
        }

        public Post GetPost(int i, SqlConnection c = null)
        {
            //Under some circumstances we want to use an already open connection
            //and not create a new one
            string sql = @"Select * from posts where id = @i";
            Post post;
            if (c == null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    post = conn.Query<Post>(sql, new { i = i }).Single();
                }
            }
            else
            {
                post = c.Query<Post>(sql, new { i = i }).Single();
            }

            return post;
        }

        public IEnumerable<Comment> GetComments(int i, SqlConnection c = null)
        {
            string sql = @"Select * from comments where item_id = @i";
            IEnumerable<Comment> comments = null;
            if (c == null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    comments = conn.Query<Comment>(sql, new { i = i });
                }
            }
            else
            {
                comments = c.Query<Comment>(sql, new { i = i });
            }
            return comments;
        }

        public UserProfile GetProfile(int i, SqlConnection c = null)
        {
            string sql = @"Select * from UserProfile where UserId = @i";
            UserProfile profile = null;
            if (c == null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    profile = conn.Query<UserProfile>(sql, new { i = i }).Single();
                }
            }
            else
            {
                profile = c.Query<UserProfile>(sql, new { i = i }).Single();
            }
            return profile;
        }

        public PostUserCommentCombined GetPostCommentsAndUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                //Need to do testing to see if joining these 3 into one call is faster
                //than 3 separate selects.  According to articles about Pinterest, they
                //don't use joins anymore for scaling reasons. 

                //Since we're doing 3 selects in a row we should use the same connection
                //for all 3.  These 3 methods were written so they could either create their own
                //or use one passed into it.
                var post = GetPost(id, conn);
                var comments = GetComments(post.id, conn);
                var profile = GetProfile(post.owner, conn);

                Image img = Image.FromFile(@"C:\Users\bradley\Documents\Visual Studio 2010\Projects\TT\TT\" + post.filename);

                return new PostUserCommentCombined() { Post = post, Comments = comments, Profile = profile, Width = img.Width, Heigth = img.Height };
            }
        }
    }
}