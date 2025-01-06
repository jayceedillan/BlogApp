using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;
using BlogApp.Dto.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<BlogPost>
    {
        public BlogPostDto blogPostDto { get; set; }
    }

}
