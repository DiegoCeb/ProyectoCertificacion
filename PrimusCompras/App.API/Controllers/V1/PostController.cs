using App.API.Contracts.V1;
using App.API.Contracts.V1.Requests;
using App.API.Contracts.V1.Responses;
using App.API.Models;
using App.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace App.API.Controllers.V1
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            var posts = from post in _postService.GetAll()
                        select new PostResponse
                        {
                            Id = post.Id,
                            Name = post.Name
                        };

            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _postService.GetById(postId);

            if (post == null) return NotFound();

            return Ok(new PostResponse
            {
                Id = post.Id,
                Name = post.Name
            });
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var newPost = new Post { Name = postRequest.Name };

            newPost.Id = Guid.NewGuid();

            bool result = _postService.Create(newPost);

            if (!result) return BadRequest();

            var response = new PostResponse
            {
                Id = newPost.Id,
                Name = newPost.Name
            };

            string urlBase = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            string locationUrl = $"{urlBase}/{ApiRoutes.Posts.Get.Replace("{postId}", newPost.Id.ToString())}";

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest postRequest)
        {
            var post = new Post
            {
                Id = postId,
                Name = postRequest.Name
            };

            var postUpdated = _postService.Update(post, postId);

            if (!postUpdated) return BadRequest();

            return Ok(post);

        }

    }
}
