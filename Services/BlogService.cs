﻿using EDP_Project.Models.Blog;
using Pract2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Services
{
    public class BlogService
    {
        private readonly UserDbContext _context;
        public BlogService(UserDbContext context)
        {
            _context = context;
        }

        public string AddBlog(Post blog)
        {

                //if (_context.Post.Any(x => x.Title == blog.Title))
                //    return $"{blog.Title} already exists.";
                _context.Post.Add(blog);
                _context.SaveChanges();
                return "True";


        }
        public Post GetPostId(int id)
        {
            return _context.Post.Where(x => x.Id == id).ToList()[0];
        }
        public List<Post> GetAllBlogPost()
        {
            return _context.Post.ToList();
        }

        public string UpdatePost(Post post)
        {
            if (_context.Post.Any(x => x.Id == post.Id))
            {
                _context.Post.Update(post);
                _context.SaveChanges();
            }
                return "True";
        }

        public void RemovePost(int postId)
        {
            Post postItem = _context.Post.Where(x => x.Id == postId).ToList()[0];
            _context.Post.Remove(postItem);
            _context.SaveChanges();
        }


        public void RemoveComment(int commentId)
        {
            Comments commentItem = _context.Comments.Where(x => x.Id == commentId).ToList()[0];
            _context.Comments.Remove(commentItem);
            _context.SaveChanges();
        }
    }
}
