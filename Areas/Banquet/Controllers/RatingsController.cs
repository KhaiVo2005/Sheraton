using Microsoft.AspNetCore.Mvc;
using Sheraton.Data;
using Sheraton.Models;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly SheratonDbContext _context;

    public RatingsController(SheratonDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostRating([FromBody] Rating rating)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        return Ok(rating);
    }

    public async Task<IActionResult> Back()
    {
        return Redirect("/Banquet/getSuKien");
    }
}
