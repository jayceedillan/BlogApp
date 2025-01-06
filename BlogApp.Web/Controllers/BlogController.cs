using BlogApp.Application.Blogs.Commands.CreateBlog;
using BlogApp.Application.Blogs.Queries;
using BlogApp.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public BlogController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        // Display all blog posts for the current user
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;  // Assuming the username is used as a unique identifier
            var blogs = await _mediator.Send(new GetBlogsByUserQuery(userId));
            return View(blogs);
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
        public async Task<IActionResult> Create(CreateBlogCommand createBlogCommand)
        {
            if (ModelState.IsValid)
            {
                var newBlog = await _mediator.Send(createBlogCommand);
                return RedirectToAction(nameof(Index));  // Redirect to the index view after creating
            }
            return View(createBlogCommand);
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
