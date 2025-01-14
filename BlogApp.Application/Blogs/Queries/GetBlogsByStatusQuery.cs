﻿using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByStatusQuery : IRequest<IEnumerable<BlogPostDto>>
    {
        public BlogStatus Status { get; set; }
    }
}
