using BlogApp.Application.Blogs.Commands.CreateBlog;
using BlogApp.Application.Blogs.Commands.DeleteBlog;
using BlogApp.Application.Blogs.Commands.UpdateBlog;
using BlogApp.Application.Blogs.Queries;
using BlogApp.Application.Services;
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Blog;
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
        private readonly ICurrentUserService _currentUserService;

        public BlogController(IMediator mediator, IFileService fileService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _fileService = fileService;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _currentUserService.GetCurrentUserId() ?? string.Empty;
            var blogs = await _mediator.Send(new GetBlogsByUserQuery(userId));
            return View(blogs);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery(id));
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto model, IFormFile? bannerImage)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ProcessBannerImage(model, bannerImage);
                await _mediator.Send(new CreateBlogCommand { blogPostDto = model });

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                HandleException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View(model);
        }

        private void ProcessBannerImage(BlogPostDto model, IFormFile? bannerImage)
        {
            if (bannerImage != null && bannerImage.Length > 0)
            {
                model.ImagePath = bannerImage;
            }

            model.AuthorId = _currentUserService.GetCurrentUserId() ?? string.Empty;
        }

        private void HandleException(Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostDto model, IFormFile? bannerImage)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                ProcessBannerImage(model, bannerImage);
                await _mediator.Send(new UpdateBlogCommand { blogPostDto = model });

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                HandleException(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteBlogCommand(id));
            return result ? RedirectToAction(nameof(Index)) : NotFound() as IActionResult;
        }
    }
}
