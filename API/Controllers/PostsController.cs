using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostsController : ControllerBase
    {
        private readonly DataContext context;

        public PostsController(DataContext context)
        {
            this.context = context;
        }
/// <summary>
/// Get api/posts
/// </summary>
/// <returns>A List of posts</returns>
        [HttpGet]
        public ActionResult<List<Post>> GetAction()
        {
            return this.context.Posts.ToList();
        }

/// <summary>
/// Get api/post/[id]
/// </summary>
///<param name="id">Post id</param>
///<returns>A single post</returns>
[HttpGet("{id}")]
public ActionResult<Post> GetById(Guid id)
{
    return this.context.Posts.Find(id);
}

/// <summary>
/// POST api/post
/// </summary>
/// <param name="request">JSON request containing post fields</param>
/// <returns>A new post</returns>
[HttpPost]
public ActionResult<Post> Create([FromBody]Post request)
{
    var post = new Post
    {
        Id = request.Id,
        Title = request.Title,
        Body = request.Body,
        Date = request.Date
    };

    context.Posts.Add(post);
    var success = context.SaveChanges() > 0;

    if(success)
    {
        return post;
    }

    throw new Exception("Error creating post");
}

/// <summary>
/// PUT api/put
/// </summary>
/// <param name="request">JSON request containing one or more updated post fields</param>
/// <returns>An updated post</returns>
[HttpPut]
public ActionResult<Post> Update([FromBody]Post request)
{
    var post = context.Posts.Find(request.Id);

    if (post == null)
    {
        throw new Exception("Could not find post");
    }

    //Update the post properties with request values, if present.
    post.Title = request.Title != null ? request.Title : post.Title;
    post.Body = request.Body != null ? request.Body : post.Body;
    post.Date = request.Date != null ? request.Date : post.Date;

    var success = context.SaveChanges() > 0;

    if(success)
    {
        return post;
    }

    throw new Exception("Error updating post");
}
    }
}