using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comments = await _commentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToCommentDto());

        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound("Comment does not exist");
        }

        return Ok(comment);
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepository.CreateAsync(commentModel);

        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id}:int")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateComment )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = await _commentRepository.UpdateAsync(id, updateComment.ToCommentFromUpdate(id));

        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment =await _commentRepository.DeleteAsync(id);

        if (comment == null)
        {
            return NotFound("Comment not found");
        }
        return NoContent();
    }
}