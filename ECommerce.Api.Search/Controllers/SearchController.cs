using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public async Task<IActionResult> SearchAsync(SearchTermModel searchTerm)
    {
        var result = await _searchService.SearchAsync(searchTerm.CustomerId);

        if (result.isSuccess)
        {
            return Ok(result.searchResults);
        }

        return NotFound();
    }
}
