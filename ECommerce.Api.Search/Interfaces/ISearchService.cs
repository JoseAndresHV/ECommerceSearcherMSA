namespace ECommerce.Api.Search.Interfaces;

public interface ISearchService
{
    Task<(bool isSuccess, dynamic? searchResults)> SearchAsync(int customer);
}
