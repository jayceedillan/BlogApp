using BlogApp.Application.Blogs.Commands.CreateBlog;
using BlogApp.Application.Blogs.Commands.DeleteBlog;
using BlogApp.Application.Blogs.Commands.UpdateBlog;
using BlogApp.Application.Blogs.Queries;
using BlogApp.Application.Services;
using BlogApp.Dto.Blog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly string _currentUserId;
        public BlogController(IMediator mediator, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _fileService = fileService;
            _currentUserId = GetAuthenticatedUserId(httpContextAccessor) ?? string.Empty;
        }

        // Display all blog posts for the current user
        public async Task<IActionResult> Index()
        {
            var blogs = await _mediator.Send(new GetBlogsByUserQuery(_currentUserId));
            return View(blogs);
        }

        private string? GetAuthenticatedUserId(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

           
            if (user != null && user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                return userId;
            }

            return null;
        }

        // Display a specific blog post by ID for editing
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery(id));
            if (blog == null)
            {
                return NotFound();
            }
            
            return View(blog);
        }

        // Create a new blog post
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle the creation of a blog post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto model, IFormFile? bannerImage)
        {
            if (ModelState.IsValid)
            {
                
                if (bannerImage != null && bannerImage.Length > 0)
                {
                    model.ImagePath = bannerImage;
                }
               

                model.AuthorId = _currentUserId;

                var newBlog = await _mediator.Send(new CreateBlogCommand { blogPostDto = model});
                return RedirectToAction(nameof(Index)); 
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostDto model, IFormFile? bannerImage)
        {
            if (ModelState.IsValid)
            {
                if (bannerImage != null && bannerImage.Length > 0)
                {
                    model.ImagePath = bannerImage;
                }

                model.AuthorId = _currentUserId;

                var newBlog = await _mediator.Send(new UpdateBlogCommand { blogPostDto = model });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Delete a blog post by ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBlogCommand(id));
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
