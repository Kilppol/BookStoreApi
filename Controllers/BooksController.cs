using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;
    private readonly ILogger<BooksController>  _logger;

    public BooksController(BooksService booksService, ILogger<BooksController> logger)
    {
        _booksService = booksService;
        _logger = logger;
    }
        

    [HttpGet]
    public async Task<List<Book>> Get()
    {
        _logger.LogInformation("Get All Books called at: "+DateTime.Now.ToString());
        return await _booksService.GetAsync();
    }
        

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            _logger.LogInformation("Book not found: "+DateTime.Now.ToString());
            return NotFound();
        }
        _logger.LogInformation($"Book{book.Id} founded at : "+DateTime.Now.ToString());
        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _booksService.CreateAsync(newBook);
        _logger.LogInformation($"Book Created at: {DateTime.Now.ToString()}");
        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
             _logger.LogInformation("Book not founded on update: "+DateTime.Now.ToString());
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);
         _logger.LogInformation($"Book Updated {book.Id} at: {DateTime.Now.ToString()}");
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
             _logger.LogInformation("Book not founded on delete: "+DateTime.Now.ToString());
            return NotFound();
        }

        await _booksService.RemoveAsync(id);
        _logger.LogInformation($"Book Deleted {book.Id} at: {DateTime.Now.ToString()}");
        return NoContent();
    }
}