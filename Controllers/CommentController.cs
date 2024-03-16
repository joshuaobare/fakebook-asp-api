using fakebook_asp_api.Services;
using fakebook_asp_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace fakebook_asp.api.Controllers;

[ApiController]
[Route("api/[controller")]
public class CommentController: Controller {
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService) => _commentService = commentService;

    [HttpGet]
    public async Task<List<Comment>> Get() =>
        await _commentService.GetCommentsAsync();

    [HttpGet("{id:length(24)")]
    public async Task<ActionResult<Comment>> Get(string id)
    {
        var comment = await _commentService.GetCommentAsync(id);
        if (comment is null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Comment newComment)
    {
        await _commentService.CreateAsync(newComment);

        return CreatedAtAction(nameof(Get), new { id = newComment.CommentId }, newComment);
    }

    
}