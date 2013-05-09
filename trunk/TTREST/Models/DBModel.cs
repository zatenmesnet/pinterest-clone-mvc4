using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using System.Drawing;
using System.Configuration;

namespace TTModels
{
    public class DBModel
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Posts> GetPosts()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var posts = conn.Query<Posts>("Select * from posts");
                return posts;
            }
        }

        //starting at i, take j
        public IEnumerable<Posts> GetPosts(int i, int j)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var posts = conn.Query<Posts>("Select * from posts ORDER BY id OFFSET @i ROWS FETCH NEXT @j ROWS ONLY", new { i = i, j = j } );
                return posts;
            }
        }

        //from user
        public IEnumerable<Posts> GetPosts(int i)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var posts = conn.Query<Posts>("Select * from posts where owner = @id", new { id = i } );
                return posts;
            }
        }

        public Posts GetPost(int i, SqlConnection c = null)
        {
            //Under some circumstances we want to use an already open connection
            //and not create a new one
            string sql = @"Select * from posts where id = @i";
            Posts post;
            if (c == null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    post = conn.Query<Posts>(sql, new { i = i }).Single();
                }
            }
            else
            {
                post = c.Query<Posts>(sql, new { i = i }).Single();
            }

            return post;
        }

        public IEnumerable<Comments> GetComments(int i, SqlConnection c = null)
        {
            string sql = @"Select * from comments where item_id = @i";
            IEnumerable<Comments> comments = null;
            if (c == null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    comments = conn.Query<Comments>(sql, new { i = i });
                }
            }
            else
            {
                comments = c.Query<Comments>(sql, new { i = i });
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

        public PostUserCombined GetPostCommentsAndUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                //Need to do testing to see if joining these 2 into one call is faster
                //than 2 separate selects.  According to articles about Pinterest, they
                //don't use joins anymore for scaling reasons. 

                //Since we're doing 2 selects in a row we should use the same connection
                //for all 2.  These 2 methods were written so they could either create their own
                //or use one passed into it.
                var post = GetPost(id, conn);
                var profile = GetProfile(post.owner, conn);
                return new PostUserCombined() { Post = post, Profile = profile, Width = post.width.ToString(), Height = post.height.ToString() };
            }
        }

        public Comments PostComment(int id, string comment, string username)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                Comments c = new Comments() { dateposted = DateTime.UtcNow, item_id = id, text = comment, name = username };

                Posts p = this.GetPost(id, conn);
                p.comments_count++;

                var i = conn.Insert(c);
                conn.Update(p);
                c.id = i;
                return c;
            }
        }

        public void PostPost(string title, string filename, int owner, DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                Posts p = new Posts() { title = title, filename = filename, owner = owner, dateuploaded = date };

                var i = conn.Insert(p);
            }
        }
    }
}